using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class line_segment_intersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             * this function assume that the line is at index 0 
             * and the segment is at index 1 
             */
            Line l = lines[0];
            Line seg = lines[1];

            if (parallel_line(l, seg))
                return;

            // get point of intersection as if they are lines 
            double s1 = (l.Start.Y - l.End.Y) / (l.Start.X - l.End.X);
            double s2 = (seg.Start.Y - seg.End.Y) / (seg.Start.X - seg.End.X);

            double b1 = l.Start.Y - s1 * l.Start.X;
            double b2 = seg.Start.Y - s2 * seg.Start.X;

            // point of intersection (treating segments as lines)
            double y = (s2 * b1 - s1 * b2) / (s2 - s1);
            double x = (y - b1) / s1;

            // check the intersection 
            Enums.TurnType t1 = HelperMethods.CheckTurn(l, seg.Start);
            Enums.TurnType t2 = HelperMethods.CheckTurn(l, seg.End);
            if(t1!=t2)
            {
                Enums.TurnType t3 = HelperMethods.CheckTurn(seg, l.Start);
                Enums.TurnType t4 = HelperMethods.CheckTurn(seg, l.End);
                if (t1 == Enums.TurnType.Left && t3 == Enums.TurnType.Right)
                    outPoints.Add(new Point(x , y));
                else if (t1 == Enums.TurnType.Right && t3 == Enums.TurnType.Left)
                    outPoints.Add(new Point(x , y));
                // coolinear end points case 
                else if (t3 == Enums.TurnType.Colinear || t4==Enums.TurnType.Colinear)
                    outPoints.Add(new Point(x , y));
            }
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
                if (slope1 == slope2)
                    return true;
            }
            if (l1_type == l2_type && l1_type != "HV")
                return true;
            return false;
        }

        public override string ToString()
        {
            return "line - segment intersection ";
        }

    }
}
