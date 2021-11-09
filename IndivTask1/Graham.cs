using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace IndivTask1
{
    public class PointsComparer : IComparer<Point>
    {
        Point p0;
        public PointsComparer(Point p0)
        {
            this.p0 = p0;
        }

        public double GetAngleWithQuarter(Point p)
        {
            if (p.Y == 0)
            {
                if (p.X == 0)
                {
                    return -1;
                }
                return p.X > 0 ? 0 : Math.PI;
            }
            if (p.X == 0)
            {
                return p.Y > 0 ? Math.PI / 2 : Math.PI / 2 * 3;
            }
            //var angle = Math.Atan(p.Y * 1.0 / p.X);
            //if((p.X<0&&p.Y>0)||(p.X<0&&p.Y<0))
            //{
            //    angle += Math.PI;
            //}
            //if (p.X > 0 && p.Y < 0)
            //{
            //    angle += Math.PI * 2;
            //}
            //return angle;
            var angle = Math.Atan2(p.Y, p.X);
            if (angle < 0)
            {
                angle += Math.PI * 2;
            }
            return angle;
        }

        double Dist(Point p1)
        {
            return Math.Sqrt(Math.Pow(p1.X - p0.X, 2) + Math.Pow(p1.Y - p0.Y, 2));
        }

        public int Compare(Point p1, Point p2)
        {
            var angle1 = GetAngleWithQuarter(p1);
            var angle2 = GetAngleWithQuarter(p2);
            var cmpRes = angle1.CompareTo(angle2);
            return cmpRes == 0 ? Dist(p1).CompareTo(Dist(p2)): cmpRes;
        }
    }

    public class Graham
    {
        static Point FindP0(Point[] points)
        {
            var p = points[0];
            var ind = 0;
            for(var i = 1; i < points.Length; i++)
            {
                if (points[i].Y < p.Y)
                {
                    p = points[i];
                    ind = i;
                } else if(points[i].Y==p.Y && points[i].X < p.X)
                {
                    p = points[i];
                    ind = i;
                }
            }
            var p0 = points[0];
            points[0] = p;
            points[ind] = p0;
            return p;
        }

        public static void TransformPoints(Point[] points, Point p0)
        {
            Matrix matrix = new Matrix();
            matrix.Translate(-p0.X,-p0.Y);
            matrix.TransformPoints(points);    
        }

        public static void BackTransformPoints(Point[] points, Point p0)
        {
            Matrix matrix = new Matrix();
            matrix.Translate(p0.X, p0.Y);
            matrix.TransformPoints(points);
        }


        public static bool IsLeftTurn(Point a, Point b, Point c)
        {
            var u = new Point(b.X - a.X, b.Y - a.Y);
            var v = new Point(c.X - a.X, c.Y - a.Y);
            var res = u.X * v.Y - u.Y * v.X;
            return res>= 0;
        }

        public static Point[] GetConvexHull(Point[] points)
        {
            var p0 = FindP0(points);
            var s = new Stack<Point>();
            TransformPoints(points, p0);
            Array.Sort(points, new PointsComparer(points[0]));
            s.Push(points[0]);
            s.Push(points[1]);
            for(int i =2; i<points.Length; i++)
            {
                var top = s.Peek();
                s.Pop();
                var nextToTop =s.Peek();
                s.Push(top);
                var cond = !IsLeftTurn(nextToTop, top, points[i]);
                while (cond)
                {
                    s.Pop();
                    top = s.Peek();
                    s.Pop();
                    nextToTop = s.Peek();
                    s.Push(top);
                    cond = !IsLeftTurn(nextToTop, top, points[i]);
                }
                s.Push(points[i]);
            }
            var res = s.ToArray();
            BackTransformPoints(res, p0);
           // Array.Sort(res, new PointsComparer( new Point(0,0)));
            return res;
            
        }
    }
}
