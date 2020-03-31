using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
using CGUtilities.DataStructures;
using System.Collections.Generic;

namespace CGAlgorithms.Algorithms.SegmentIntersection
{
    class SweepLine:Algorithm
    {
        public int sweep_line_compare(Event event1, Event event2)
        {
            Line L1 = new Line(event1.start, event1.end);
            Line L2 = new Line(event2.start, event2.end);

            double sweep_line_x = Math.Max(L1.Start.X, L2.Start.X);
            double slope1 = (L1.End.Y - L1.Start.Y) / (L1.End.X - L1.Start.X);
            double slope2 = (L2.End.Y - L2.Start.Y) / (L2.End.X - L2.Start.X);
            for (int i = 0; i < 2; i++) // start points at i=0 , end points at i = 1
            { 
                double Y_inter1 = slope1 * (sweep_line_x - L1.Start.X) + L1.Start.Y;
                double Y_inter2 = slope2 * (sweep_line_x - L2.Start.X) + L2.Start.Y;
                if (Math.Abs(Y_inter1 - Y_inter2) <= Constants.Epsilon)
                {
                    // work on end points 
                    sweep_line_x = Math.Max(L1.End.X, L2.End.X);
                    continue;
                }
                if (Y_inter1 < Y_inter2)
                    return -1;
                else if (Y_inter1 > Y_inter2)
                    return 1;
            }
            return 0;
        }
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            OrderedSet<Event> sweep_line = new OrderedSet<Event>(new Comparison<Event>(sweep_line_compare));
            OrderedSet<Event> events = new OrderedSet<Event>();

            // get the events 
            for(int i=0; i<lines.Count; i++)
            {
                Point start = lines[i].Start;
                Point end = lines[i].End;
                if (start.CompareTo(end) == 1)
                {
                    // swap ends of the line 
                    lines[i].Start = end;
                    lines[i].End = start;

                }
                // what if we have a degenerate line 
                Event beginp = new Event(lines[i].Start,lines[i].End, 0, i, -1); // begin 
                Event endp = new Event(lines[i].End, lines[i].Start, 1, i, -1); //  end 
                events.Add(beginp);
                events.Add(endp);
            }

            // sweep left to right 
            OrderedSet<Point> inter_points = new OrderedSet<Point>();
            while(events.Count!=0)
            {
                Event tevent = events.GetFirst();
                events.RemoveFirst();

                int line_index1 = tevent.line_index1;
                int line_index2 = tevent.line_index2;
                double current_x = lines[line_index1].Start.X;
                if (tevent.event_type==0) // start
                {
                    sweep_line.Add(tevent);
                    KeyValuePair<Event, Event> UL = sweep_line.DirectUpperAndLower(tevent);
                    // intersection between the smaller , bigger
                    if (UL.Key != null)
                        get_inter(tevent, UL.Key, ref inter_points,ref events, current_x);
                    if(UL.Value!=null)
                        get_inter(UL.Value, tevent, ref inter_points,ref events, current_x);
                }
                else if(tevent.event_type==1) // end 
                {
                    // edit  the event so we can match 
                    // potional bug 
                    Point tpoint = tevent.start;
                    tevent.start = tevent.end;
                    tevent.end = tpoint;
                    tevent.event_type = 0;

                    KeyValuePair<Event, Event> UL = sweep_line.DirectUpperAndLower(tevent);
                    sweep_line.Remove(tevent); // potional bug 
                    // smaller - bigger 
                    if (UL.Key != null && UL.Value != null)
                        get_inter(UL.Value, UL.Key, ref inter_points,ref events, current_x);
                }
                else // intersection 
                {
                    // don't change the original line 
                    // smaller, bigger 
                    Line line1 = lines[line_index1].Clone() as Line;
                    Line line2 = lines[line_index2].Clone() as Line;
                    // get events 
                    Event event1 = new Event(line1.Start, line1.End, 0, line_index1, -1);
                    Event event2 = new Event(line2.Start, line2.End, 0, line_index2, -1);
                    // remove them from the sweep line tree 
                    sweep_line.Remove(event1);
                    sweep_line.Remove(event2);
                    // put the intersecction point as the start point = swap them in order 
                    event1.start = tevent.start;
                    event2.start = tevent.start;
                    // insert them back again 
                    sweep_line.Add(event1);
                    sweep_line.Add(event2);

                    // ****** potional bug  ****** 
                    // get the upper and lower for line 1 , line 2 respectivally 
                    KeyValuePair<Event, Event> ULE1 = sweep_line.DirectUpperAndLower(event1);
                    KeyValuePair<Event, Event> ULE2 = sweep_line.DirectUpperAndLower(event2);
                    // check event 1 with it's upper 
                    if (ULE1.Key != null)
                    {
                        get_inter(event1, ULE1.Key, ref inter_points, ref events, current_x);
                        //get_inter(event2, ULE1.Key, ref inter_points, ref events, current_x);
                    }
                    // check event2 with it's lower 
                    if (ULE2.Value != null)
                    {
                        get_inter(event2, ULE2.Value, ref inter_points, ref events, current_x);
                        //get_inter(ULE2.Value, event1,ref inter_points, ref events, current_x);
                    }

                }
            }
            // get points 
            while (inter_points.Count != 0)
            {
                outPoints.Add(inter_points.GetFirst());
                inter_points.RemoveFirst();
            }
        }

        public void get_inter(Event event1 , Event event2, ref OrderedSet<Point> inter_points ,ref OrderedSet<Event> events, double current_x)
        {
            // upper , lower 
            // get information 
            Line l1 = new Line(event1.start, event1.end);
            Line l2 = new Line(event2.start, event2.end);
            int line_index1 = event1.line_index1;
            int line_index2 = event2.line_index1;

            // get the intresection between two segments 
            List<Line> dummy_L = new List<Line>();
            List<Polygon> dummy_poly = new List<Polygon>();
            List<Point> tmp_pl = new List<Point>();

            List<Line> tmp_ll = new List<Line>();
            tmp_ll.Add(l1);
            tmp_ll.Add(l2);
            segment_segment_intersection seg_seg_inter = new segment_segment_intersection();
            seg_seg_inter.Run(new List<Point>(), tmp_ll, new List<Polygon>(), ref tmp_pl, ref dummy_L, ref dummy_poly);
            if(tmp_pl.Count!=0)
            {
                //without this we go into inf loop , why !????
                if (tmp_pl[0].X < current_x || inter_points.Contains(tmp_pl[0]))
                    return;
                // add to intersection points 
                inter_points.Add(tmp_pl[0]);
                // we need to add the intersection point event 
                Event tevent = new Event(tmp_pl[0],tmp_pl[0], 2, line_index1, line_index2);
                events.Add(tevent);
            }
            return;
        }
        public override string ToString()
        {
            return "Sweep Line";
        }
    }
}