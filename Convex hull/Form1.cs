using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Convex_hull
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            draw();
        }

        //Здесь будут храниться все введенные точки.
        List<Point> points = new List<Point> { };

        List<Point> up = new List<Point> { };
        List<Point> down = new List<Point> { };


        public void draw()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            myPane.Title.Text = "Алгоритм Эндрю выпуклой оболочки";

            //Делаю движение по графику при помощи ЛКМ
            zedGraphControl1.PanButtons = MouseButtons.Left;
            zedGraphControl1.PanModifierKeys = Keys.None;

            //Очищаю массивы с точками для многоугольника, чтобы они не перечерчивались многократно.
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            //Выводятся введенные точки
            PointPairList pointsList = new PointPairList();
            for(int i = 0; i < points.Count; i++)
            {
                pointsList.Add(points[i].x, points[i].y);
            }
            LineItem myCurve = myPane.AddCurve("Scatter", pointsList, Color.Blue, SymbolType.Circle);
            //Линии между точками не видны
            myCurve.Line.IsVisible = false;
            // Цвет заполнения отметок (кружков) - голубой
            myCurve.Symbol.Fill.Color = Color.Blue;
            // Тип заполнения - сплошная заливка
            myCurve.Symbol.Fill.Type = FillType.Solid;

            //Вывожу оболочку, которая расползяется вверх.
            if (up.Count >= 2) 
            {
                //Выводится оболочка
                for (int i = 0; i < up.Count - 1; i++)
                {
                    LineObj line = new LineObj(Color.Green, up[i].x, up[i].y, up[i + 1].x, up[i + 1].y);
                    myPane.GraphObjList.Add(line);
                }
            }

            //Вывожу оболочку, которая расползается вниз.
            if (down.Count >= 2)
            {
                //Выводится оболочка
                for (int i = 0; i < down.Count - 1; i++)
                {
                    LineObj line = new LineObj(Color.Green, down[i].x, down[i].y, down[i + 1].x, down[i + 1].y);
                    myPane.GraphObjList.Add(line);
                }
            }

            // Добавьте линии сетки на график и сделайте их серыми
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            // Ось X будет пересекаться с осью Y на уровне Y = 0
            myPane.XAxis.Cross = 0.0;

            // Ось Y будет пересекаться с осью X на уровне X = 0
            myPane.YAxis.Cross = 0.0;

            // Отключим отображение первых и последних меток по осям
            myPane.XAxis.Scale.IsSkipFirstLabel = true;
            myPane.XAxis.Scale.IsSkipLastLabel = true;

            // Отключим отображение меток в точке пересечения с другой осью
            myPane.XAxis.Scale.IsSkipCrossLabel = true;

            // Спрячем заголовки осей
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;

            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        //Высчитываю, по часовой стрелке идут точки, или нет (алгоритм знаковой площади).
        bool isClockWise(Point a, Point b, Point c)
        {
            return a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y) < 0;
        }

        //Высчитываю, против часовой стрелки идут точки, или по часовой (алгоритм знаковой площади).
        bool isCounterClockWise (Point a, Point b, Point c)
        {
            return a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y) > 0;
        }

        private void zedGraphControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Сюда будут записаны координаты в системе координат графика.
            double x, y;

            // Пересчитываем пиксели в координаты на графике.
            // У ZedGraph есть несколько перегруженных методов ReverseTransform.
            zedGraphControl1.GraphPane.ReverseTransform(e.Location, out x, out y);

            //Если человек ошибся, кликнув не на ту точку, на которую хотел, можно отменить, т.к. вылазиет
            //окно с выбором.
            DialogResult result = MessageBox.Show($"x = {x}, y = {y}", "Продолжить?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                points.Add(new Point(x, y));
                up.Clear();
                down.Clear();
                draw();
            }
        }


        /*Алгоритм. Найдём самую левую и самую правую точки A и B 
         * (если таких точек несколько, то возьмём самую нижнюю среди левых, и самую верхнюю среди правых).
         * Понятно, что и A, и B обязательно попадут в выпуклую оболочку. Далее, проведём через них прямую AB,
         * разделив множество всех точек на верхнее и нижнее подмножества S1 и S2 (точки, лежащие на прямой, 
         * можно отнести к любому множеству - они всё равно не войдут в оболочку). Точки A и B отнесём к обоим множествам.
         * Теперь построим для S1 верхнюю оболочку, а для S2 - нижнюю оболочку, и объединим их, получив ответ. 
         * Чтобы получить, скажем, верхнюю оболочку, нужно отсортировать все точки по абсциссе, затем пройтись по всем точкам, 
         * рассматривая на каждом шаге кроме самой точки две предыдущие точки, вошедшие в оболочку. Если текущая тройка
         * точек образует не правый поворот (что легко проверить с помощью знаковой площади), то ближайшего соседа
         * нужно удалить из оболочки. В конце концов, останутся только точки, входящие в выпуклую оболочку.
         * 
         * Итак, алгоритм заключается в сортировке всех точек по абсциссе и двух (в худшем случае) обходах всех точек, т.е. требуемая асимптотика O (N log N) достигнута.
        */
        private void AndrewAlgo_Click(object sender, EventArgs e)
        {
            //Если хочу повторить алгоритм на тех же точках, очищаю отрезки оболочки.
            up.Clear();
            down.Clear();
            draw();


            if(points.Count == 1)
            {
                MessageBox.Show("В массиве должно быть больше одной точки!");
                return; 
            }

            //Сортирую точки от левой нижней до правой верхней.
            points.Sort(new Point().Compare);

            //Самая левая нижняя точка
            Point p1 = points[0];

            //Самая правая верхняя точка
            Point p2 = points[points.Count - 1];

            up.Add(p1);
            down.Add(p1);


            bool drawBySteps = false;

            //Спрашиваю, отрисовывать по шагам оболочку или нет.
            DialogResult drawByStepsLines = MessageBox.Show("Отрисовывать оболочку по шагам?", "", MessageBoxButtons.YesNo);
            if (drawByStepsLines == DialogResult.Yes)
            {
                drawBySteps = true;
            }

            for (int i = 1; i < points.Count; i++)
            {
                if(i == points.Count - 1 || isClockWise(p1, points[i], p2))
                {
                    while (up.Count >= 2 &&  !isClockWise(up[up.Count - 2], up[up.Count - 1], points[i]))
                    {
                        up.RemoveAt(up.Count - 1);
                    }
                    up.Add(points[i]);
                    if (drawBySteps)
                    {
                        draw();
                        MessageBox.Show($"Шаг {i}");
                    }
                }
                if(i == points.Count - 1 || isCounterClockWise(p1, points[i], p2))
                {
                    while (down.Count >= 2 && !isCounterClockWise(down[down.Count - 2], down[down.Count - 1], points[i]))
                    {
                        down.RemoveAt(down.Count - 1);
                    }
                    down.Add(points[i]);
                    if (drawBySteps)
                    {
                        draw();
                        MessageBox.Show($"Шаг {i}");
                    }
                }
            }

            //Отрисовываю выпуклую оболочку.
            draw();
        }

        private void ClearPlane_Click(object sender, EventArgs e)
        {
            points.Clear();
            up.Clear();
            down.Clear();
            draw();
        }
    }
}
