using System;

namespace TestTask.Models.EventArgs
{
    public class FigureEventArgs : System.EventArgs
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public Type Type { get; set; }
        public DateTime DateTime { get; set; }

        public FigureEventArgs()
        {
            DateTime = DateTime.Now;
        }
    }
}