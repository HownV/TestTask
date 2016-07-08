using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask.Models.MyList
{
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }

        public Node()
        {
        }
        public Node(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();

            if(capacity > 0)
                Next = new Node<T>(capacity - 1);
        }
        public Node(T item)
        {
            Item = item;
        }
    }
}
