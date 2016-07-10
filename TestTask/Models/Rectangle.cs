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

        public override int RightBorder => X + Width;
        public override int BottomBorder => Y + Height;

        //public override event EventHandler<FigureEventArgs> Crossed;

        public Rectangle()
        {
        }

        public Rectangle(int xMax, int yMax)
        {
            Height = Randomizer.GetInt32(25, MaxSize);
            Width = Randomizer.GetInt32(25, MaxSize);
            Color = Randomizer.GetColorAsByteArray();

            X = Randomizer.GetInt32(xMax - MaxSize);
            Y = Randomizer.GetInt32(yMax - MaxSize);
            do
            {
                Dx = Randomizer.GetInt32(-4, 4);
                Dy = Randomizer.GetInt32(-4, 4);
            } while (Dx == 0 && Dy == 0);

            Crossed += BeepWhenCrossed;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            pen.Color = System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2], Color[3]);
            g.DrawLine(pen, X, Y, X + Width, Y);
            g.DrawLine(pen, X + Width, Y, X + Width, Y + Height);
            g.DrawLine(pen, X + Width, Y + Height, X, Y + Height);
            g.DrawLine(pen, X, Y + Height, X, Y);
        }

        public override void Move(int xMax, int yMax)
        {
            //if ()
            //    Dx = -Dx;
            //if ()
            //    Dy = -Dy;

            if (IsDxReversed || X < 0 || X > xMax - Width)
            {
                Dx = -Dx;
                IsDxReversed = false;
            }
            if (IsDyReversed || Y < 0 || Y > yMax - Height)
            {
                Dy = -Dy;
                IsDyReversed = false;
            }

            X += Dx;
            Y += Dy;
        }
    }
}
