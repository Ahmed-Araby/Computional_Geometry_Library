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
        public int comp(Point a , Point b)
        {
            if (a.X == b.X)
            {
                if (a.Y < b.Y)
                    return -1;
                return 1;
            }
            else if (a.X < b.X)
                return -1;
            return 1;
        }
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             * this function will take set of points
             * assume all points are distinct *********
             * it don't deal with colinear points 
             * return segment lines that bound the convex HULL , and boundary points for the test cases 
             * in the package 
             */
            // [ preprocessing ]
            List<Point> tmp = new List<Point>();
            points.Sort(comp); 

            bool[] vis = new bool[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                vis[i] = false;
            }
            
            // build segments 
            for (int i=0; i<points.Count; i++)
            {
                if (i > 0 && compare_points(points[i], points[i - 1]))
                    continue;

                for(int j=i+1; j<points.Count; j++)
                {
                    if (compare_points(points[i], points[j]))
                        continue;
                    if (j>0 && compare_points(points[j], points[j-1])) // j will always be > 0
                        continue;

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
                        {
                            co += 1;
                            if(!check_point_inside_segment(seg , p))
                            { // bug 
                                // so we will not take this segment 
                                left += 1;
                                right += 1;
                                break;
                            }
                        }
                    }
                    if (left == 0 || right == 0)
                    {
                        /*
                         * don't same point more than once 
                         * as same point will appear in 2 segments 
                        */

                        if (!vis[i])
                        {
                            vis[i] = true;
                            outPoints.Add(points[i]);
                        }
                        if(!vis[j])
                        {
                            vis[j] = true;
                            outPoints.Add(points[j]);
                        }
                        // no need for it 
                        outLines.Add(seg);
                    }
                }
            }

            // case of all the points are the same 
            if (points.Count>0 && outPoints.Count==0)
            {
                outPoints.Add(points[0]);
            }
            return;
        }

        public bool compare_points(Point a , Point b)
        {
            if (Math.Abs(a.X - b.X) <= Constants.Epsilon && Math.Abs(a.Y - b.Y) <= Constants.Epsilon)
                return true;
            return false;
        }
        public bool check_point_inside_segment(Line l, Point p)
        {
            // naive logic 
            double x = p.X, y = p.Y;
            if (x >= Math.Min(l.Start.X, l.End.X) && x <= Math.Max(l.Start.X, l.End.X))
                if (y >= Math.Min(l.Start.Y, l.End.Y) && y <= Math.Max(l.Start.Y, l.End.Y))
                    return true;
            return false;
        }
        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}
