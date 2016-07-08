using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models.MyList;

namespace TestTask.Models
{
    public class MyList<T> : IList<T> //IEnumerable, IList, ICollection
    {
        private Node<T> _list;

        public MyList()
        {
            _list = null;
            Count = 0;
        }

        //+
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException();

                Node<T> tempList = _list;
                for (int i = 0; i < index + 1; i++)
                {
                    if (i != index)
                        tempList = tempList.Next;
                    else if (i == index)
                        return tempList.Item;
                }

                throw new Exception("Internal error in the indexator");
            }
            set
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException();

                Node<T> tempList = _list;
                for (int i = 0; i < index + 1; i++)
                {
                    if (i != index)
                        tempList = tempList.Next;
                    else
                        tempList.Item = value;
                }
            }
        }

        // ICollection<T>
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        //+
        public void Add(T item)
        {
            if (item == null)
                throw new NullReferenceException();

            // Adding item as the first
            if (Count == 0)
            {
                _list = new Node<T>(item);
                Count++;
                return;
            }

            Node<T> tempList = _list;
            while (tempList.Next != null)
            {
                tempList = tempList.Next;
            }
            tempList.Next = new Node<T>(item);
            Count++;
        }

        //+
        public void Clear()
        {
            _list = null;
            Count = 0;
        }

        //+
        public bool Contains(T item)
        {
            if (item == null)
                throw new NullReferenceException();

            Node<T> tempList = _list;
            do
            {
                if (tempList.Item.Equals(item))
                    return true;
                tempList = tempList.Next;
            } while (tempList.Next != null);

            return false;
        }

        // copy each item from whole MyList<T> into finite T[] starting with the specified index 
        //+
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new NullReferenceException();
            if (array.Length - arrayIndex < Count)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            var tempList = _list;
            for(int i = arrayIndex; i < Count + arrayIndex; i++)
            { 
                array[i] = tempList.Item;
                tempList = tempList.Next;
            }
        }

        //+
        public bool Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            // removing first item
            if (_list.Item.Equals(item))
            {
                _list = _list.Next;
                return true;
            }

            // removing internal item
            var tempList = _list.Next;
            do
            {
                if (tempList.Next.Item.Equals(item))
                {
                    tempList.Next = tempList.Next.Next;
                    return true;
                }
                tempList = tempList.Next;
            } while (tempList.Next.Next != null);
            
            // checking if the sought-for item is the last
            if (tempList.Next.Item.Equals(item))
            {
                tempList.Next = null;
                return true;
            }

            return false;
        }

        // IList<T>
        //+
        public int IndexOf(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            int index = 0;
            var tempList = _list;
            do
            {
                if (tempList.Item.Equals(item))
                    return index;
                
                tempList = tempList.Next;
                index++;
            } while (tempList.Next != null);

            // check last item
            if (tempList.Item.Equals(item))
                return index;

            return -1;
        }

        //+
        public void Insert(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException();
            if(index < 0 || index > Count)
                throw  new ArgumentOutOfRangeException(nameof(index));

            // inserting first item
            if (index == 0)
            {
                Node<T> firstNode = new Node<T>(item) { Next = _list };
                _list = firstNode;
                return;
            }

            // inserting into internal part of _list
            int tempIndex = 0;
            var tempList = _list;
            do
            {
                tempIndex++;
                if (tempIndex == index)
                {
                    Node<T> newNode = new Node<T>(item) { Next = tempList.Next };
                    tempList.Next = newNode;
                    return;
                }
                tempList = tempList.Next;
            } while (tempList.Next != null);

            // checking if the sought-for item is the last
            tempIndex++;
            if (index == tempIndex)
            {
                Node<T> newNode = new Node<T>(item);
                tempList.Next = newNode;
                return;
            }

            throw new Exception("Internal error in void Insert(T item) method"); // to delete
        }
        
        //+
        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(nameof(index), 
                    @"Parameter 'index' must be non-negative and less then size of collection");

            // removing first item
            if (index == 0)
            {
                _list = _list.Next;
                return;
            }

            // removing internal item
            int tempIndex = 0;
            var tempList = _list;
            do
            {
                tempIndex++;
                if (tempIndex == index)
                {
                    tempList.Next = tempList.Next.Next;
                    return;
                }
                tempList = tempList.Next;
            } while (tempList.Next.Next != null);

            // checking if the sought-for item is the last
            tempIndex++;
            if (tempIndex == index)
            {
                tempList.Next = null;
                return;
            }
            throw new Exception("Internal error in void RemoveAt(int index) method");
        }

        // IEnumerable<T>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new MyEnumerator(_list);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnumerator(_list);
        }
        
        private struct MyEnumerator : IEnumerator<T>
        {
            private Node<T> _list;
            private int _position;
            private Node<T> _currentNode;

            public MyEnumerator(Node<T> list)
            {
                _list = list;
                _position = -1;
                _currentNode = null;
            }

            public object Current
            {
                get
                {
                    return _currentNode.Item;
                }
            }
            T IEnumerator<T>.Current
            {
                get
                {
                    return _currentNode.Item;
                }
            }

            public bool MoveNext()
            {
                if (_list == null)
                    return false;

                _position++;
                if (_position > 0 && _currentNode.Next != null)
                {
                    _currentNode = _currentNode.Next;
                    return true;
                }
                else if (_position == 0)
                {
                    _currentNode = _list;
                    return true;
                }

                //Reset();
                return false;
            }

            public void Reset()
            {
                _position = -1;
                _currentNode = null;
            }

            public void Dispose()
            {
                _list = null;
                _currentNode = null;
            }
        }
    }
}
