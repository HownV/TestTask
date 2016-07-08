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
        public int x;
        [DataMember]
        public int y;
        [DataMember]
        public int dx;
        [DataMember]
        public int dy;
        [DataMember]
        public byte[] Color;

        public event EventHandler<FigureEventArgs> Crossed;

        public Figure()
        {
        }

        public virtual void Move(int xMax, int yMax)
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

        public virtual void OnCrossed()
        {
        }

        public void BeepWhenCrossed(object sender, FigureEventArgs e)
        {
            Console.Beep();
        }

        public virtual void AddBeep()
        {
            Crossed += BeepWhenCrossed;
        }
    }
}
