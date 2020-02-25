using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             * this function will take set of points
             * return only the points on the boundary of the polygon 
             * it don't deal with colinear points 
             * we could make the algo faster by excluding the non extreme points from building the triangle also 
             * proof that this will not affect the non extreme detection !!! ******
             */

            /*
             * this will affect outpoints and I will deal with it to make things alright  
             */
            point_inside_triangle check_p_t = new point_inside_triangle();

            // assume all points are extreme at the begining 
            bool[] extreme_point = new bool[points.Count];
            for (int i = 0; i < points.Count; i++)
                extreme_point[i] = true;

            // build the triangle 
            for(int i=0; i<points.Count; i++)
            {
                for(int j=0; j<points.Count; j++)
                {
                    if (i == j)
                        continue;
                    for(int k=0; k<points.Count; k++)
                    {
                        if (k == i || k == j)
                            continue;
                        Point a = points[i], b = points[j], c = points[k];
                        // loop throw candidate points 
                        for(int index = 0; index<points.Count; index++)
                        {
                            if (index == i || index == j || index == k || extreme_point[index]==false)
                                continue;
                            Point p = points[index];

                            // check if point is inside the triangle 
                            // point 
                            List<Point> tmp_pl = new List<Point>();
                            tmp_pl.Add(p);
                            // triangle  (polygon)
                            // segments are clock wise 
                            List<Line> tmp_tl = new List<Line>();
                            Polygon tmp_poly = new Polygon();
                            
                            tmp_tl.Add(new Line(a, b));
                            tmp_tl.Add(new Line(b, c));
                            tmp_tl.Add(new Line(c, a));

                            tmp_poly.lines = tmp_tl;
                            polygons.Add(tmp_poly);

                            // check 
                            check_p_t.Run(tmp_pl, lines, polygons, ref outPoints, ref outLines, ref outPolygons);
                            if(outPoints.Count!=0)
                            {
                                // non extreme point 
                                extreme_point[index] = false;
                                outPoints.Clear();
                            }

                            polygons.Clear();
                        }
                    }
                }
            }

            // add extreme points to outpoints 
            for (int i = 0; i < points.Count; i++)
                if (extreme_point[i] == true)
                    outPoints.Add(points[i]);
            return;
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}
