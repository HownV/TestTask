using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TestTask.Interfaces;
using TestTask.Models;

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

        public virtual event EventHandler<FigureEventArgs> Crossed;

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
        
        public void BeepWhenCrossed(object sender, FigureEventArgs e)
        {
            Console.Beep();
        }

        public void AddBeep()
        {
            Crossed += BeepWhenCrossed;
        }

        public void ReverseDx(bool isRun)
        {
            if(isRun)
                IsDxReversed = true;
        }

        public void ReverseDy(bool isRun)
        {
            if (isRun)
                IsDyReversed = true;
        }
    }
}
