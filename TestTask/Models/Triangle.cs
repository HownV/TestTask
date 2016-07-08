using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RandomizingLibrary;


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

        [DataMember]
        public int RightBorder;
        [DataMember]
        public int BottomBorder;

        //public event EventHandler<FigureEventArgs> Crossed;

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
            } while (A == B && B == C && C == A);

            Color = Randomizer.GetColorAsByteArray();

            x = Randomizer.GetInt32(xMax - MaxSize);
            y = Randomizer.GetInt32(yMax - MaxSize);
            do
            {
                dx = Randomizer.GetInt32(-4, 4);
                dy = Randomizer.GetInt32(-4, 4);
            } while (dx == 0 || dy == 0);
            
            int leftBorder = Math.Min(Math.Min(A.X, B.X), Math.Min(B.X, C.X));
            A.X -= leftBorder;
            B.X -= leftBorder;
            C.X -= leftBorder;

            int heightBorder = Math.Min(Math.Min(A.Y, B.Y), Math.Min(B.Y, C.Y));
            A.Y -= heightBorder;
            B.Y -= heightBorder;
            C.Y -= heightBorder;

            RightBorder = Math.Max(Math.Max(A.X, B.X), Math.Max(B.X, C.X));
            BottomBorder = Math.Max(Math.Max(A.Y, B.Y), Math.Max(B.Y, C.Y));
        }

        public override void Draw(Graphics g, Pen pen)
        {
            pen.Color = System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2], Color[3]);
            g.DrawLine(pen, A.X + x, A.Y + y, B.X + x, B.Y + y);
            g.DrawLine(pen, B.X + x, B.Y + y, C.X + x, C.Y + y);
            g.DrawLine(pen, C.X + x, C.Y + y, A.X + x, A.Y + y);
        }

        public override void Move(int xMax, int yMax)
        {
            if (x < 0 || x > xMax - RightBorder)
                dx = -dx;
            if (y < 0 || y > yMax - BottomBorder)
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
