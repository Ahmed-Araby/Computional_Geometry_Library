using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {
        public int comp(Point a, Point b)
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
             * take points 
             * return points for test cases 
             * [ I want to return segments also ] !!!!
             */

            // handle special cases 
            if (points.Count == 0)
                return;
            if(points.Count==1)
            {
                outPoints.Add(points[0]);
                return;
            }
            points.Sort(comp);

            // representation of the hull 
            int [] next = new int[points.Count];
            int [] prv  = new int[points.Count];
         
            int index = 1;
            while (index < points.Count && compare_points(points[0], points[index]))
                index += 1;
            if(index==points.Count)
            {
                outPoints.Add(points[0]);
                return;
            }
            else
            {
                next[0] = index;
                prv[0] = index;
                next[index] = 0;
                prv[index] = 0;

            }

            // build the rest of the hull incremental 
            int mrp_index = index;
            for(index = index+1; index<points.Count; index++)
            {
                Point new_point = points[index];
                // ignore equal point
                // index-1 is the most right point 
                // index-1 cuz bug in case of repeated points as index-1 prv and next have wrong information 
                // so the hull get seprated , and we loss the next part of it after the repation 
                if (compare_points(new_point, points[mrp_index]))
                    continue;

                /*
                 as it's most right we know it's out of the polygon 
                 plug your self immediatly ,  this step helps me avoiding special cases 
                 ******************* we need to make sure that we add it in CCW **********************
                 the if statements make sure that Iam keeping the HULL CCW 

                 we can handle colinear poitns in the step of getting supporting lines 
                */
                if(new_point.Y >= points[mrp_index].Y)
                {
                    next[index] = next[mrp_index];
                    //next[walker] = index;
                    //prv[next[index]] = index;
                    prv[index] = mrp_index;
                }
                else
                {
                    next[index] = mrp_index;
                    //next[prv[walker]] = index;
                    prv[index] = prv[mrp_index];
                    //prv[walker] = index;
                }
                // very cool one 
                next[prv[index]] = index;
                prv[next[index]] = index;

                // get support lines 
                // get the upper line 
                while(true)
                {
                    Line seg = new Line(new_point, points[next[index]]);
                    Point next_point = points[next[next[index]]]; // points[(walker + 1) % points.Count];
                    Enums.TurnType turn = HelperMethods.CheckTurn(seg, next_point);

                    // colinear case will go into inf loop , with only 3 points 
                    if (turn != Enums.TurnType.Left)
                    {
                        // redirect the edges  , delete enclosed vertices 
                        next[index] = next[next[index]];
                        prv[next[index]] = index;
                        // ************ check for colinear case with 3 points  **********
                        if (turn == Enums.TurnType.Colinear)
                            break;
                    }
                    else
                        break;
                }

                /*
                 * could I use my next always as the starting point to look for the supporting line  
                 * I guess this will prodce a bug 
                 */
                // get the lower line 
                // colinear case will go into inf loop , with only 3 points 
                while (true)
                {
                    Line seg = new Line(new_point, points[prv[index]]);
                    Point next_point = points[prv[prv[index]]];
                    Enums.TurnType turn = HelperMethods.CheckTurn(seg, next_point);
                    if (turn != Enums.TurnType.Right)
                    {
                        // redirect the edges 
                        prv[index] = prv[prv[index]];
                        next[prv[index]] = index;
                        // walker = (walker + 1) % points.Count , this was my bug as I WAS MOVING forward 
                        // and I did have to move backward 
                        // *************** check for colinear case with 3 points ***************
                        if (turn == Enums.TurnType.Colinear)
                            break;
                    }
                    else break;
                }
                 
                // update the most right point index 
                mrp_index = index;
            }

            // build the hull CCW , we know point at index 0 is extreme
            index = 0;
            while(true)
            {
                outPoints.Add(points[index]);
                index = next[index];
                if (index == 0)
                    break;
            }
            return;
        }
        public bool compare_points(Point a, Point b)
        {
            if (Math.Abs(a.X - b.X) <= Constants.Epsilon && Math.Abs(a.Y - b.Y) <= Constants.Epsilon)
                return true;
            return false;
        }

        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
    }
}
