using System.Drawing;

namespace TestTask.Interfaces
{
    public interface IDrawable
    {
        void Draw(Graphics g, Pen pen);

        void Foo();
    }
}
