using AVLTree_Balance;
using System;
using System.Collections.Generic;
using System.Text;

namespace AVL
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        public AVLTreeNode<T> Head
        {
            get;
            internal set;
        }

        public int Count
        {
            get;
            private set;
        }

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }
            else
            {
                AddTo(Head, value);
            }
            Count++;
        }

        private void AddTo(AVLTreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }

                else
                {
                    AddTo(node.Left, value);
                }
            }

            else
            {
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    AddTo(node.Right, value);
                }
            }
        }


        public bool Contains(T value)
        {
            return Find(value) != null;
        }


        public bool Remove(T value)
        {
            AVLTreeNode<T> current;
            current = Find(value); // поиск удаляемого значения

            if (current == null)
            {
                return false;
            }

            if (current.Right == null) // если нет правого потомка
            {
                if (current.Parent == null) // удаляемый узел является корнем
                {
                    Head = current.Left; //на место корня перемещаем левый потомок
                    if (Head != null)
                    {
                        Head.Parent = null; // для данного корня удаляем ссылку на родителя
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        current.Parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = current.Left;
                    }
                }

            }

            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (current.Parent == null)
                {
                    Head = current.Right;

                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                    else
                    {
                        int result = current.Parent.CompareTo(current.Value);
                        if (result > 0)
                        {
                            current.Parent.Left = current.Right;
                        }
                        else if (result < 0)
                        {
                            current.Parent.Right = current.Right;
                        }
                    }
                }

            }

            else
            {
                AVLTreeNode<T> leftmost = current.Right.Left;

                while (leftmost.Left != null)
                {
                    leftmost = leftmost.Left;
                }
                leftmost.Parent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (current.Parent == null)
                {
                    Head = leftmost;
                    if (Head != null)
                    {
                        Head.Parent = null;
                    }
                }
                else
                {
                    int result = current.Parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        current.Parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        current.Parent.Right = leftmost;
                    }
                }
            }
            return true;
        }

        private AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> current = Head; // помещаем текущий элемент в корень дерева

            while (current != null)
            {
                int result = current.CompareTo(value);

                if (result > 0)
                {
                    current = current.Left;
                }
                else if (result < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }


        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)//существует ли корень дерева
            {
                Stack<AVLTreeNode<T>> stac = new Stack<AVLTreeNode<T>>();
                AVLTreeNode<T> current = Head;

                bool goLeftNext = true;

                stac.Push(current);

                while (stac.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stac.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stac.Pop();
                        goLeftNext = false;
                    }

                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
