using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndivTask1
{
    public partial class Form1 : Form
    {
        List<Point> points = new List<Point>();

        Point[] convexHull = new Point[0];
       
        public Form1()
        {
            InitializeComponent();
           
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            points.Clear();
            convexHull=new Point[0];
            Refresh();
        }

        private void convexHullButton_Click(object sender, EventArgs e)
        {
            if (points.Count < 3) return;
            convexHull = Graham.GetConvexHull(points.ToArray());
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g =e.Graphics;
            g.Clear(Color.White);
            foreach (var p in points)
            {
                Brush brush = new SolidBrush(Color.Red);
                g.FillEllipse(brush, p.X - 2, p.Y - 2, 4, 4);
            }
            Pen pen = new Pen(new SolidBrush(Color.Black));
            if (convexHull.Length == 0)
                return;
            for (int i = 0; i < convexHull.Length-1; i++)
            {               
                g.DrawLine(pen, convexHull[i], convexHull[i + 1]);
            }
            g.DrawLine(pen, convexHull.Last(), convexHull.First());
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(new Point(e.X, e.Y));
            Refresh();
        }
    }
}
