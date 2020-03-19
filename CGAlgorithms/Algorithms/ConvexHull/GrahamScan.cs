using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class my_sort_algo: IComparer<Point>
    {
        /*
         * sort points respect to point a in angular manner  
         * in ascending order 
         */

        public Point a; // min point in the set of given points
        public my_sort_algo(Point p)
        {
            a = p;
        }
        public int Compare(Point b , Point c)
        {
            /*
             *  function return :
             *  0 if b == c
             *  1 if b > c
             *  -1 if  b < c
             *  in angular manner 
             *  
             *  logic :
             * check turn 
             * if from a to b to c is turn right so c have less angler value than b return c less then b
             * else if right left turn si b<c 
             * else if colinear check for furthest one , return any threr will be no problem 
            */

            Line seg = new Line(a, b);
            Enums.TurnType t = HelperMethods.CheckTurn(seg, c);
            if (t == Enums.TurnType.Right)
                return 1;
            else if(t==Enums.TurnType.Left)
                return -1;

            else
            {// colinear 
                double bdis = get_dis2(a, b);
                double cdis = get_dis2(a, c);
                if (bdis >= cdis)
                    return 1;
                else
                    return -1;
            }
        }
        public double get_dis2(Point a, Point b)
        { // distance is squared to avoid precision errors 
            double dis = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
            return dis;
        }
    }

    public class GrahamScan : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            // handle non logical cases  
            if (points.Count == 0)
                return;
            /*
             * input : set of points 
             * return : segments , points in CCW manner 
             * check how the algo will behave for many colinear points  !!!!!!
             */

            int minp_index = get_min_point_index(points);
            Point minp = points[minp_index];
            my_sort_algo anguler_sort = new my_sort_algo(minp);

            /*
             * sort the points in angular manner respect to min point 
             * sort using turn left and turn right 
            */

            List<Point> sorted_points = points;
            sorted_points.Sort(anguler_sort);

            // apply graham scan algo 
            List<Point> hull = new List<Point>();
            // ***********************
            /*
             * this introduce a bug , why !???
            if(minp!=sorted_points[0])
            {
                return;
            }*/
            // ***********************

            hull.Add(minp);
            if(points.Count==1)
            {
                outPoints.Add(minp);
                return;
            }

            hull.Add(sorted_points[1]);
            for(int i=2; i<sorted_points.Count; i++)
            {
                int index = hull.Count - 1;
                Point a = hull[index - 1];
                Point b = hull[index];
                Point c = sorted_points[i];

                // check turn 
                Line seg = new Line(a, b);
                Enums.TurnType t = HelperMethods.CheckTurn(seg, c);
                if (t == Enums.TurnType.Left)
                {
                    hull.Add(c);
                }
                else if(t==Enums.TurnType.Right)
                {
                    // c enclose some points that are on the boundary of the hull 

                    hull.RemoveAt(hull.Count - 1);  // O(1)
                    i -= 1;

                    // don't go further untill we are making left turn 
                    // as hull could have more points that are enclosed by c
                }
                else // colinear , pick farthest 
                { 
                    // only 2 points could be colinear with c
                    // I don't think that we need to get the dis 
                    // c is farther than b 

                    double bdis = get_dis2(a, b);
                    double cdis = get_dis2(a, c);
                    if(cdis > bdis)
                    {
                        hull.RemoveAt(hull.Count - 1); // O(1) no further elements to shift left 
                        hull.Add(c);
                    }
                }
            }

            
            /*
             * handle similar points  
             */
            if(hull.Count==2)
            {
                if (Math.Abs(hull[0].X - hull[1].X) <= Constants.Epsilon && Math.Abs(hull[0].Y - hull[1].Y) <= Constants.Epsilon)
                    hull.RemoveAt(hull.Count - 1);
            }

            // return 
            for (int i=0; i<hull.Count; i++)
            {
                if (i>0)
                    outLines.Add(new Line(hull[i - 1], hull[i]));
                outPoints.Add(hull[i]);
            }
            if(hull.Count>2) // closing segment 
                outLines.Add(new Line(hull[hull.Count - 1], hull[0]));
            return;
        }

        public double get_dis2(Point a, Point b)
        { // distance is squared to avoid precision errors 
            double dis = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
            return dis;
        }
        public int get_min_point_index(List<Point> points)
        {
            Point tmp_p = points[0];
            int index = 0;
            /*
             *  I did have a bug in the way that I pick min point on 
             *  nope I guess any extreme vertex will do 
             */
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X == tmp_p.X)
                {
                    if (points[i].Y < tmp_p.Y)
                    {
                        tmp_p = points[i];
                        index = i;
                    }
                }
                else if (points[i].X < tmp_p.X)
                {
                    tmp_p = points[i];
                    index = i;
                }

            }
            return index;
        }

        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}
