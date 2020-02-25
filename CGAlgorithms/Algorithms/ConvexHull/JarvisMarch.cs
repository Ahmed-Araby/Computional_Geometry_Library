using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            /*
             * this function will take set of points 
             * return ordered list of points on the convex hull in CCW (as we want)
             * 
             * if we want CW we can reverse after the algo 
             * or get the most left turn which mean 
             * angularly left most point respect to the current point on the hull we have 
             */

            // extreme have to be on the polygon , it have <=180 with the axis passing straight throw it respect to all points.
            int minp_index = get_min_point_index(points); 
            Point current_p = points[minp_index];
            int currentp_index = minp_index;

            List<Point> hull = new List<Point>();
            hull.Add(current_p);
            while (true)
            {
                // get initial next point 
                int nextp_index = (currentp_index + 1) % points.Count;
                Point next_p = points[nextp_index];

                // get initial extreme segment
                Line extreme_seg = new Line(current_p, next_p);

                for(int i=0; i<points.Count; i++)
                {
                    Point tmp_p = points[i];
                    // check turn 
                    Enums.TurnType t = HelperMethods.CheckTurn(extreme_seg, tmp_p);
                    if (t == Enums.TurnType.Right)
                    {
                        extreme_seg.End = tmp_p;
                        nextp_index = i;
                        next_p = tmp_p;
                    }
                    else if(t==Enums.TurnType.Colinear)
                    {
                        // get the furthest one in CCW direction 
                        double current_dis = get_dis2(current_p, next_p);
                        double tmp_dis = get_dis2(current_p, tmp_p);
                        if(tmp_dis > current_dis)
                        {
                            extreme_seg.End = tmp_p;
                            nextp_index = i;
                            next_p = tmp_p;
                        }
                    }
                }

                // stop condition , return back to start point 
                if (nextp_index == minp_index)
                    break;

                // advance current state  of the algo 
                hull.Add(next_p);
                current_p = next_p;
                currentp_index = nextp_index;                
            }
            outPoints = hull;
            return;
        }
        public double get_dis2(Point a , Point b)
        { // distance is squared to avoid precision errors 
            double dis = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
            return dis;
        }
        public int get_min_point_index(List<Point> points)
        {
            Point tmp_p = points[0];
            int index = 0;
            for(int i=0; i<points.Count; i++)
            {
                if (points[i].Y == tmp_p.Y)
                {
                    if (points[i].X < tmp_p.X)
                    {
                        tmp_p = points[i];
                        index = i;
                    }
                }
                else if (points[i].Y < tmp_p.Y)
                {
                    tmp_p = points[i];
                    index = i;
                }

            }
            return index;
        }

        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}
