using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGUtilities
{
    public class Event :ICloneable,IComparable
    {
        public Point start { get; set; }
        public Point end { get; set; }
        public int event_type { get; set; }  // 0 start , 1 end , 2 intersection 
        public int line_index1 { get; set; }
        public int line_index2 { get; set; }
        public Event(Point start, Point end, int event_type, int line_index1 , int line_index2)
        {
            this.start= start;
            this.end= end;
            this.event_type = event_type;
            this.line_index1 = line_index1;
            this.line_index2 = line_index2;
            return;
        }

        public int CompareTo(Object obj)
        {
            Event tmp_Event = obj as Event;
            return this.start.CompareTo(tmp_Event.start);
        }
        public object Clone()
        {
            return new Event(start,end, event_type, line_index1 , line_index2);
        }
    }
}
