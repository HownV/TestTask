using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RandomizingLibrary;
using TestTask.Models.Exceptions;


namespace TestTask.Models
{
    [Serializable]
    [DataContract]
    public class Triangle : Figure
    {
        [DataMember(Name = "a")]
        public Point A;
        [DataMember(Name = "b")]
        public Point B;
        [DataMember(Name = "c")]
        public Point C;

        public override int RightBorder => X + RightOffset;
        public override int BottomBorder => Y + BottomOffset;

        [DataMember]
        public readonly int RightOffset;
        [DataMember]
        public readonly int BottomOffset;
        
        public Triangle()
        {
        }

        public Triangle(int xMax, int yMax)
        {
            do
            {
                A = Randomizer.GetPoint(MaxSize);
                B = Randomizer.GetPoint(MaxSize);
                C = Randomizer.GetPoint(MaxSize);
            } while (A == B && B == C && C == A && 
                    ((A.X != 0 && A.Y != 0) && (B.X != 0 && B.Y != 0) && (C.X != 0 && C.Y != 0)));

            Color = Randomizer.GetColorAsByteArray();

            X = Randomizer.GetInt32(xMax - MaxSize);
            Y = Randomizer.GetInt32(yMax - MaxSize);
            do
            {
                Dx = Randomizer.GetInt32(-4, 4);
                Dy = Randomizer.GetInt32(-4, 4);
            } while (Dx == 0 && Dy == 0);

            IsRun = true;
            
            int leftOffset = Math.Min(Math.Min(A.X, B.X), Math.Min(B.X, C.X));
            A.X -= leftOffset;
            B.X -= leftOffset;
            C.X -= leftOffset;

            int topOffset = Math.Min(Math.Min(A.Y, B.Y), Math.Min(B.Y, C.Y));
            A.Y -= topOffset;
            B.Y -= topOffset;
            C.Y -= topOffset;

            RightOffset = Math.Max(Math.Max(A.X, B.X), Math.Max(B.X, C.X));
            BottomOffset = Math.Max(Math.Max(A.Y, B.Y), Math.Max(B.Y, C.Y));

            Crossed += Beep;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            pen.Color = System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2], Color[3]);
            g.DrawLine(pen, A.X + X, A.Y + Y, B.X + X, B.Y + Y);
            g.DrawLine(pen, B.X + X, B.Y + Y, C.X + X, C.Y + Y);
            g.DrawLine(pen, C.X + X, C.Y + Y, A.X + X, A.Y + Y);
        }

        public override void Move(int xMax, int yMax)
        {
            Validate(xMax, yMax);

            if (IsDxReversed || X < 0 || X > xMax - RightOffset)
            {
                Dx = -Dx;
                IsDxReversed = false;
            }

            if (IsDyReversed || Y < 0 || Y > yMax - BottomOffset)
            {
                Dy = -Dy;
                IsDyReversed = false;
            }
            X += Dx;
            Y += Dy;
        }

        public override bool Validate(int xMax, int yMax)
        {
            if (X > xMax - RightOffset + 4 || Y > yMax - BottomOffset + 4)
            {
                throw new FigureOutOfRangeException();
            }
            return true;
        }
    }
}
