using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RandomizingLibrary;
using TestTask.Models;

namespace TestTask.Models
{
    [Serializable]
    [DataContract]
    public class Circle : Figure
    {
        [DataMember]
        public int Radius { get; set; }

        //public sealed override event EventHandler<FigureEventArgs> Crossed;

        public Circle()
        {
        }

        public Circle(int xMax, int yMax)
        {
            Radius = Randomizer.GetInt32(15, MaxSize / 2);
            Color = Randomizer.GetColorAsByteArray();

            x = Randomizer.GetInt32(xMax - MaxSize);
            y = Randomizer.GetInt32(yMax - MaxSize);
            do
            {
                dx = Randomizer.GetInt32(-4, 4);
                dy = Randomizer.GetInt32(-4, 4);
            } while (dx == 0 || dy == 0);

            Crossed += BeepWhenCrossed;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            pen.Color = System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2], Color[3]);
            g.DrawEllipse(pen, new System.Drawing.Rectangle(x, y, 2 * Radius, 2 * Radius));
        }

        public override void Move(int xMax, int yMax)
        {
            if (x < 0 || x > xMax - Radius*2)
            {
                dx = -dx;
                OnCrossed();
            }
            if (y < 0 || y > yMax - Radius*2)
            {
                dy = -dy;
                OnCrossed();
            }
            x += dx;
            y += dy;
        }

        public override void OnCrossed()
        {
            FigureEventArgs e = new FigureEventArgs();
            if (Crossed != null) Crossed.Invoke(this, e);
            else AddBeep();
        }
        
    }
}
