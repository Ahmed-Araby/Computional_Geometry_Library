using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;

namespace CGAlgorithms.Algorithms
{
    class line_point_turn : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            Enums.TurnType turn = HelperMethods.CheckTurn(lines[0], points[0]);
            if (turn == Enums.TurnType.Left)
                outPoints.Add(points[0]);
            else if(turn == Enums.TurnType.Colinear)
            {
                outPoints.Add(points[0]);
                outLines.Add(lines[0]);
            }
            else
                outLines.Add(lines[0]); ;
            return;
        }

        public override string ToString()
        {
            return "line point turn";
        }
    }
}
