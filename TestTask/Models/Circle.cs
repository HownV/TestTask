﻿using System;
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

        public override int RightBorder => X + Radius*2;
        public override int BottomBorder => Y + Radius*2;

        public sealed override event EventHandler<FigureEventArgs> Crossed;

        public Circle()
        {
        }

        public Circle(int xMax, int yMax)
        {
            Radius = Randomizer.GetInt32(15, MaxSize / 2);
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
            g.DrawEllipse(pen, new System.Drawing.Rectangle(X, Y, 2 * Radius, 2 * Radius));
        }

        public override void Move(int xMax, int yMax)
        {
            //if ()
            //    Dx = -Dx;
            //if ()
            //    Dy = -Dy;

            if (IsDxReversed || X < 0 || X > xMax - Radius * 2)
            {
                Dx = -Dx;
            }
            if (IsDyReversed || Y < 0 || Y > yMax - Radius * 2)
            {
                Dy = -Dy;
            }
            X += Dx;
            Y += Dy;
        }
    }
}
