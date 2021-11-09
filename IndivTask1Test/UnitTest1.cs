using IndivTask1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace IndivTask1Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPointComparer()
        {
            var p1 = new Point(1, 4);
            var p2 = new Point(0, 4);
            PointsComparer comparer = new PointsComparer(p1);
            var cmpRes = comparer.Compare(p1, p2);
            Assert.IsTrue(cmpRes < 0);


            p1 = new Point(0, 4);
            p2 = new Point(1, 4);
            cmpRes = comparer.Compare(p1, p2);
            Assert.IsTrue(cmpRes > 0);

            p1 = new Point(1, 4);
            p2 = new Point(1, -4);
            cmpRes = comparer.Compare(p1, p2);
            Assert.IsTrue(cmpRes < 0);

            p1 = new Point(1, 4);
            p2 = new Point(-1, 4);
            cmpRes = comparer.Compare(p1, p2);
            Assert.IsTrue(cmpRes < 0);

            p1 = new Point(1, 4);
            p2 = new Point(2, 2);
            cmpRes = comparer.Compare(p1, p2);
            Assert.IsTrue(cmpRes > 0);

        }

        [TestMethod]
        public void TestSortWithPointComparer()
        {
            var p1 = new Point(1, 4);
            var p2 = new Point(2, 2);
            var p3 = new Point(3,0);
            var p4 = new Point(1, -2);
            var p5 = new Point(-2, 0);
            var p6 = new Point(-5, 3);
            var p7 = new Point(-2, 5);
            var p8 = new Point(0, 1);
            var p9 = new Point(0, -1);
            var p10 = new Point(0, 0);
            var points = new Point[] {
                p1,p2,p3,p4,p5,p6,p7,p8,p9,p10
            };
            Array.Sort(points, new PointsComparer(new Point(0,0)));
            var ptsExpected = new Point[] {
               p10, p3, p2, p1, p8, p7, p6, p5, p9, p4, 
            };

            CollectionAssert.AreEqual(ptsExpected, points);
        }

        [TestMethod]
        public void TestBackTransform()
        {
            var p0 = new Point(4, 3);
            var points = new Point[]{
                new Point(1,1) ,
                new Point(-1,1),
                new Point(-1,-1),
                new Point(1,-1)};

            Graham.TransformPoints(points, p0);
            Graham.BackTransformPoints(points, p0);
            var ptsExpected = new Point[]
            {
                new Point(1,1) ,
                new Point(-1,1),
                new Point(-1,-1),
                new Point(1,-1)};

            CollectionAssert.AreEqual(points, ptsExpected);
        }
        
      
        [TestMethod]
        public void TestConvexHull()
        {
            var p1 = new Point(5, 0);
            var p2 = new Point(1, -1);
            var p3 = new Point(0, -3);
            var p4 = new Point(-4, -2);
            var p5 = new Point(-2, -1);
            var p6 = new Point(-3, 2);
            var p7 = new Point(-6, 4);
            var p8 = new Point(-1, 4);
            var p9 = new Point(1, 2);
            var p10 = new Point(3, 6);
            var points = new Point[] {
                p1,p2,p3,p4,p5,p6,p7,p8,p9,p10
            };
            var convexHull = Graham.GetConvexHull(points);
            var expected = new Point[] { p3, p1, p10, p7, p4 };
            CollectionAssert.AreEqual(convexHull, expected);
            
        }
    }
}
