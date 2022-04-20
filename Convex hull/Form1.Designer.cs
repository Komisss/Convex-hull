
namespace Convex_hull
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.AndrewAlgo = new System.Windows.Forms.Button();
            this.ClearPlane = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(531, 435);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl1_MouseDoubleClick);
            // 
            // AndrewAlgo
            // 
            this.AndrewAlgo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AndrewAlgo.Location = new System.Drawing.Point(12, 453);
            this.AndrewAlgo.Name = "AndrewAlgo";
            this.AndrewAlgo.Size = new System.Drawing.Size(108, 32);
            this.AndrewAlgo.TabIndex = 1;
            this.AndrewAlgo.Text = "Алгоритм Эндрю";
            this.AndrewAlgo.UseVisualStyleBackColor = true;
            this.AndrewAlgo.Click += new System.EventHandler(this.AndrewAlgo_Click);
            // 
            // ClearPlane
            // 
            this.ClearPlane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearPlane.Location = new System.Drawing.Point(126, 453);
            this.ClearPlane.Name = "ClearPlane";
            this.ClearPlane.Size = new System.Drawing.Size(121, 32);
            this.ClearPlane.TabIndex = 2;
            this.ClearPlane.Text = "Очистить плоскость";
            this.ClearPlane.UseVisualStyleBackColor = true;
            this.ClearPlane.Click += new System.EventHandler(this.ClearPlane_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 487);
            this.Controls.Add(this.ClearPlane);
            this.Controls.Add(this.AndrewAlgo);
            this.Controls.Add(this.zedGraphControl1);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button AndrewAlgo;
        private System.Windows.Forms.Button ClearPlane;
    }
}

