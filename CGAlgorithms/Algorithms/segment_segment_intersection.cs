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
            /*
             *   how to handle buggy cases with H and V lines !??
             */
            Line l1 = lines[0];
            Line l2 = lines[1];

            if (parallel_line(l1, l2))
                return;

            double s1 = (l1.Start.Y - l1.End.Y) / (l1.Start.X - l1.End.X);
            double s2 = (l2.Start.Y - l2.End.Y) / (l2.Start.X - l2.End.X);

            double b1 = l1.Start.Y - s1 * l1.Start.X;
            double b2 = l2.Start.Y - s2 * l2.Start.X;

            // point of intersection (treating segments as lines)
            double y = (s2 * b1 - s1 * b2) / (s2 - s1);

            double x = 0;
            if (s1 != 0)
                x = (y - b1) / s1;  //  BUG it divides by zero and the test cases have Horizontal line and this give x =nan as inter point which
            // can't be drawn on the canves 
            else
                x = (y - b2) / s2;

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

        public bool parallel_line(Line l1, Line l2)
        {

            string l1_type = "HV", l2_type = "HV";
            if (l1.Start.X == l1.End.X)
                l1_type = "V";
            else if (l1.Start.Y == l1.End.Y)
                l1_type = "H";

            if (l2.Start.X == l2.End.X)
                l2_type = "V";
            else if (l2.Start.Y == l2.End.Y)
                l2_type = "H";
            if (l1_type == "HV" && l2_type == "HV")
            {
                double slope1 = (l1.Start.Y - l1.End.Y) - (l1.Start.X - l1.End.X);
                double slope2 = (l2.Start.Y - l2.End.Y) - (l2.Start.X - l2.End.X);
                /*
                 *  we should do comparison using eps as we could have floating point 
                 *  numbers for slopes 
                 */
                if (Math.Abs(slope1 - slope2) <=Constants.Epsilon) // edited swap line 
                    return true;
            }
            if (l1_type == l2_type && l1_type != "HV")
                return true;
            return false;
        }
        public override string ToString()
        {
            return "segment_segment_intersection";
        }
    }
}
