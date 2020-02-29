using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class Point_in_convex_polygon : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             *  this function require lines to have start and end poitns 
             *  in a s pecific order CCW or CW 
             *  line order it self don't mater 
             */

            // as point inside concave polygon is a general algo for 
            // any kind of polygon we can just call the other function here 

            List<Line> l = polygons[0].lines;
            Point p = points[0];

            int left = 0, right = 0;
            for(int i=0; i<l.Count; i++)
            {
                Line tmp_l = l[i];
                // check turn 
                Enums.TurnType t = HelperMethods.CheckTurn(tmp_l, p);
                if (t == Enums.TurnType.Left)
                    left += 1;
                else if (t == Enums.TurnType.Right)
                    right += 1;
                // else colinear , no problem 
            }
            if (left == 0 || right == 0)
                outPoints.Add(p);
            return;
        }

        public override string ToString()
        {
            return "point inside convex polygon";
        }
    }
}
