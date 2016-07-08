using System;
using System.Collections.Generic;
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
    public class Rectangle : Figure
    {
        [DataMember]
        public int Height;
        [DataMember]
        public int Width;

        //public override event EventHandler<FigureEventArgs> Crossed;

        public Rectangle()
        {
        }

        public Rectangle(int xMax, int yMax)
        {
            Height = Randomizer.GetInt32(25, MaxSize);
            Width = Randomizer.GetInt32(25, MaxSize);
            Color = Randomizer.GetColorAsByteArray();

            x = Randomizer.GetInt32(xMax - MaxSize);
            y = Randomizer.GetInt32(yMax - MaxSize);
            do
            {
                dx = Randomizer.GetInt32(-4, 4);
                dy = Randomizer.GetInt32(-4, 4);
            } while (dx == 0 || dy == 0);
        }

        public override void Draw(Graphics g, Pen pen)
        {
            pen.Color = System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2], Color[3]);
            g.DrawLine(pen, x, y, x + Width, y);
            g.DrawLine(pen, x + Width, y, x + Width, y + Height);
            g.DrawLine(pen, x + Width, y + Height, x, y + Height);
            g.DrawLine(pen, x, y + Height, x, y);
        }

        public override void Move(int xMax, int yMax)
        {
            if (x < 0 || x > xMax - Width)
                dx = -dx;
            if (y < 0 || y > yMax - Height)
                dy = -dy;
            x += dx;
            y += dy;
        }

        public override void OnCrossed()
        {
            throw new NotImplementedException();
        }
    }
}
