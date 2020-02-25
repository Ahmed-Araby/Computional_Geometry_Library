using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class segment_segment_intersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Line l1 = lines[0];
            Line l2 = lines[1];

            double s1 = (l1.Start.Y - l1.End.Y) / (l1.Start.X - l1.End.X);
            double s2 = (l2.Start.Y - l2.End.Y) / (l2.Start.X - l2.End.X);

            double b1 = l1.Start.Y - s1 * l1.Start.X;
            double b2 = l2.Start.Y - s2 * l2.Start.X;

            // point of intersection (treating segments as lines)
            double y = (s2 * b1 - s1 * b2) / (s2 - s1);
            double x = (y - b1) / s1;

            // check if the point is inside th segments 
            /*
            // naive logic 
            if(x >= Math.Min(l1.Start.X , l1.End.X) && x <= Math.Max(l1.Start.X, l1.End.X))
                if (y >= Math.Min(l1.Start.Y, l1.End.Y) && y <= Math.Max(l1.Start.Y, l1.End.Y))
                    if (x >= Math.Min(l2.Start.X, l2.End.X) && x <= Math.Max(l2.Start.X, l2.End.X))
                        if (y >= Math.Min(l2.Start.Y, l2.End.Y) && y <= Math.Max(l2.Start.Y, l2.End.Y))
                            outPoints.Add(new Point(x, y));
            */

            // using orientation test 
            Enums.TurnType t1 = HelperMethods.CheckTurn(l1, l2.Start);
            Enums.TurnType t2 = HelperMethods.CheckTurn(l1, l2.End);
            Enums.TurnType t3 = HelperMethods.CheckTurn(l2, l1.Start);
            Enums.TurnType t4 = HelperMethods.CheckTurn(l2, l1.End);
            if (t1 != t2 && t3 != t4)
                outPoints.Add(new Point(x, y));
        }

        public override string ToString()
        {
            return "segment_segment_intersection";
        }
    }
}
