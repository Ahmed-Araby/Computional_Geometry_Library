using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms
{
    class polygon_polygon_intersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            //List<Line> p1_l = polygons[0].lines;
            List<Line> p2_l = polygons[1].lines;
            Polygon poly2 = polygons[1];
            polygons.RemoveAt(1);
            /*
             * this will affect the outpoints 
             * and this function is okay with this 
             */
            segment_polygon_intersection seg_poly_inter = new segment_polygon_intersection();
            for(int i=0; i<p2_l.Count; i++)
            {
                // segment polygon intersection
                lines.Add(p2_l[i]);
                seg_poly_inter.Run(points, lines, polygons, ref outPoints, ref outLines, ref outPolygons);
                lines.RemoveAt(0); 
            }
            polygons.Add(poly2);
            return;
        }

        public override string ToString()
        {
            return "polygon polygon intersection";
        }
    }
}
