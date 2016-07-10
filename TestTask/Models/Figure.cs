using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TestTask.Interfaces;
using TestTask.Models;
using TestTask.Models.EventArgs;

namespace TestTask.Models
{
    [XmlInclude(typeof(Triangle))]
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    [DataContract]
    [Serializable]
    public class Figure : IMovable, IDrawable  
    {
        public const int MaxSize = 200;

        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
        [DataMember]
        public int Dx { get; set; }
        [DataMember]
        public int Dy { get; set; }
        [DataMember]
        public byte[] Color { get; set; }

        public virtual int RightBorder { get; set; }
        public virtual int BottomBorder { get; set; }

        protected bool IsDxReversed;
        protected bool IsDyReversed;

        public event EventHandler<FigureEventArgs> Crossed;

        public Figure()
        {
        }

        public virtual void Move(int xMax, int yMaxm)
        {
        }

        public virtual void Draw(Graphics g, Pen pen)
        {
        }

        void IMovable.Foo()
        {
            MessageBox.Show("Hello from IMovable");
        }

        void IDrawable.Foo()
        {
            MessageBox.Show("Hello from IDrawable");
        }

        public void OnCross(object sender, FigureEventArgs e)
        {
            Crossed?.Invoke(sender, e);
            Debug.WriteLine("X:{0}, Y:{1}, Type:{2}, DateTime:{3}", e.X, e.Y, e.Type, e.DateTime);
        }
        public void Beep(object sender, FigureEventArgs e)
        {
            new Thread(Console.Beep).Start();
            //Console.Beep();
        }

        public void AddBeep()
        {
            Crossed += Beep;
        }
        public void RemoveBeep()
        {
            if (Crossed != null)
            {
                Crossed -= Beep;
            }
        }

        public void ReverseDx(bool isRun)
        {
            if (isRun)
            {
                IsDxReversed = true;
            }
        }

        public void ReverseDy(bool isRun)
        {
            if (isRun)
                IsDyReversed = true;
        }

        public bool IsCoordinatesCorrect(List<Figure> figures)
        {
            foreach (var figure in figures)
            {
                if ((X >= figure.X && X <= figure.RightBorder) 
                    || (RightBorder >= figure.X && RightBorder <= figure.RightBorder)
                    || (X <= figure.X && RightBorder >= figure.RightBorder))
                {
                    if ((BottomBorder >= figure.Y && BottomBorder <= figure.BottomBorder)
                        || (Y >= figure.Y && Y <= figure.BottomBorder)
                        || (Y <= figure.Y && BottomBorder >= figure.BottomBorder))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
