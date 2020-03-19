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

            // preprocessing 
            List<Point> tmp = new List<Point>();
            for(int i=0; i<points.Count; i++)
            {
                bool dup = false;
                for(int j=0; j<i; j++)
                {
                    if(Math.Abs(points[i].X-points[j].X) <= Constants.Epsilon && Math.Abs(points[i].Y-points[j].Y) <=Constants.Epsilon)
                    {
                        dup = true;
                        break;
                    }
                }
                if (dup)
                    continue;
                tmp.Add(points[i]);
            }
            points.Clear();
            points = tmp;

            Point_in_convex_polygon check_p_in_t = new Point_in_convex_polygon();
            bool[] is_extreme = new bool[points.Count];
            for (int i = 0; i < points.Count; i++)
                is_extreme[i] = true;

            for(int i=0; i<points.Count; i++)
            {
                // don't use non extreme points in checking 
                // as if we have the same point more than 1 time they will exclude each other 
                if (is_extreme[i] == false)
                    continue;

                for(int j=0; j<points.Count; j++)
                {
                    if (is_extreme[j] == false)
                        continue;
                    if (i == j)
                        continue;

                    for (int k=0; k<points.Count; k++)
                    {
                        if (is_extreme[k] == false)
                            continue;
                        if (k == i || k == j)
                            continue;
                        Point a = points[i], b = points[j], c = points[k];

                        // check if points are colinear 
                        Enums.TurnType t = HelperMethods.CheckTurn(new Line(a, b), c);
                        if (t == Enums.TurnType.Colinear)
                            continue;

                        // loop throw the points 
                        for(int index=0; index<points.Count; index++)
                        {
                            if (is_extreme[index] == false || index==i || index == j || index==k)
                                continue;
                            // check if the point is inside the polygon triangle 
                            List<Line> tmp_ll = new List<Line>();
                            List<Polygon> tmp_pl = new List<Polygon>();
                            Polygon tmp_poly = new Polygon();
                            Point p = points[index];

                            // does the order matters !???
                            tmp_ll.Add(new Line(a, b));
                            tmp_ll.Add(new Line(b, c));
                            tmp_ll.Add(new Line(c, a));

                            tmp_poly.lines = tmp_ll;
                            tmp_pl.Add(tmp_poly);

                            // tmp out lists 
                            // does C# have garbage collector !??
                            List<Point> tmp_outpoints = new List<Point>();
                            List<Line> tmp_outlines = new List<Line>();
                            List<Polygon> tmp_outpolygons = new List<Polygon>();
                            List<Point> in_points = new List<Point>();
                            in_points.Add(p);
                            check_p_in_t.Run(in_points, new List<Line>(), tmp_pl, ref tmp_outpoints, ref tmp_outlines, ref tmp_outpolygons);
                            if(tmp_outpoints.Count!=0)
                            {
                                is_extreme[index] = false;   
                            }
                        }
                    }
                }
            }

            // return extreme points 
            for (int i = 0; i < points.Count; i++)
            {
                if (is_extreme[i] == true)
                {
                    outPoints.Add(points[i]);
                }
            }
            return;
        }

        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}
