using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class Line_Line_intersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            /*
             * two line don't intersect if they are parallel  
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
            double x = (y - b1) / s1;

            outPoints.Add(new Point(x, y));
        }
        public bool parallel_line(Line l1 , Line l2)
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
            if(l1_type=="HV" && l2_type=="HV")
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
            if (l1_type == l2_type && l1_type!="HV")
                return true;
            return false;
        }

        public override string ToString()
        {
            return "Line Line intersection";
        }
    }
}
