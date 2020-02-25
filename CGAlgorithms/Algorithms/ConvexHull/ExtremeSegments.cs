using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             * this function will take set of points
             * assume all points are distinct *********
             * it don't deal with colinear points 
             * return segment lines that bound the convex HULL.
             */

            // build segments 
            for(int i=0; i<points.Count; i++)
            {
                for(int j=i+1; j<points.Count; j++)
                {
                    Line seg = new Line(points[i], points[j]);

                    // check if all other points are in the same side of the segment or colinear with the segment 
                    int right = 0, left = 0, co = 0;
                    for (int k=0; k<points.Count; k++)
                    {
                        if (k == i || k == j) // they will be colinear !
                            continue;
                        Point p = points[k];
                        Enums.TurnType t = HelperMethods.CheckTurn(seg, p);
                        if (t == Enums.TurnType.Right)
                            right += 1;
                        else if (t == Enums.TurnType.Left)
                            left += 1;
                        else if (t == Enums.TurnType.Colinear)
                            co += 1;
                    }
                    if (left == 0 || right == 0)
                        outLines.Add(seg);
                }
            }
            return;
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}
