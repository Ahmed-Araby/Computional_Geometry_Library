using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class point_inside_concave_polygon : Algorithm
    { 
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if(polygons[0].lines.Count <3 || points.Count==0)
            { // not valid case to be solved 
                return;
            }
            // line to right of given point 
            Point a = points[0];
            Point b = new Point(a.X + 10000, a.Y);
            Line vir_line = new Line(a, b);
            List<Line> poly_segments = polygons[0].lines;

            // this may be bad practice 
            segment_segment_intersection seg_inter = new segment_segment_intersection();

            // do segment - segment intersection for vir_line with all the segments of the concave polygon 
            for(int i=0; i<poly_segments.Count; i++)
            {
                Line poly_seg = poly_segments[i];
                // check segment - segment intersection 
                List<Line> tmp_lines = new List<Line>();
                tmp_lines.Add(vir_line);
                tmp_lines.Add(poly_seg);

                /*
                 * this will add points of intersections to outpoints list 
                 * so we have to delete them later 
                 */
                seg_inter.Run(points, tmp_lines, polygons, ref outPoints, ref outLines, ref outPolygons);

                // check for colinearity (point is on a polygon segment)
                Enums.TurnType t = HelperMethods.CheckTurn(poly_seg, a);
                if (t == Enums.TurnType.Colinear && check_point_inside_segment(poly_seg, a))
                {
                    outPoints.Clear(); // clear points of intersections 
                    outPoints.Add(a); 
                    return;
                }
            }

            // intersect odd number of segments = inside the polygon 
            if (outPoints.Count % 2 != 0)
            {
                outPoints.Clear(); // clear points of intersections 
                outPoints.Add(a);
            }
            else
                outPoints.Clear();
            return;
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
            return "point inside concave polygon";
        }
    }
}
