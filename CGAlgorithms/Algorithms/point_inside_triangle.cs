using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class point_inside_triangle : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             *  [on purpose]
             *  don't handle repeated points 
             *  neither colinear points 
             */

            if(polygons[0].lines.Count!=3)
            {// not a triangle 
                return;
            }
            Line l1 = polygons[0].lines[0];
            Line l2 = polygons[0].lines[1];
            Line l3 = polygons[0].lines[2];
            Point p = points[0];

            Enums.TurnType t1 = HelperMethods.CheckTurn(l1, p);
            Enums.TurnType t2 = HelperMethods.CheckTurn(l2, p);
            Enums.TurnType t3 = HelperMethods.CheckTurn(l3, p);
            // have to be inside the triangle not colinear with one of the edges 
            // not repeated points 
            if (t1 == t2 && t2 == t3)
                outPoints.Add(p);
            return;
        }

        public override string ToString()
        {
            return "point inside triangle";
        }
    }
}
