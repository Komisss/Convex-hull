using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convex_hull
{
    class Point: IComparer<Point>
    {
        public double x;
        public double y;

        public Point()
        {
            this.x = 0;
            this.y = 0;
        }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }


        public int Compare(Point a, Point b)
        {
            if (a.x < b.x || a.x == b.x && a.y < b.y)
            {
                return -1;
            }
            else if(a.x == b.x && a.y == b.y)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
