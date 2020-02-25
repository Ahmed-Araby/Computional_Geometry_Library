using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class segment_polygon_intersection : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Line l = lines[0];
            List<Line> polygon_lines = polygons[0].lines;
            /*
             * this will affect the outpoints list 
             * and this function need this , and every other function that call it need this too 
             */
            segment_segment_intersection seg_seg_inter = new segment_segment_intersection();
            for(int i=0; i<polygon_lines.Count; i++)
            {
                Line pline = polygon_lines[i];
                // segment segment intersection
                List<Line> tmp_lines = new List<Line>();
                tmp_lines.Add(l);
                tmp_lines.Add(pline);
                seg_seg_inter.Run(points, tmp_lines, polygons, ref outPoints, ref outLines, ref outPolygons);
            }
        }

        public override string ToString()
        {
            return "segment polygon intersection ";
        }
    }
}
