using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace AVL_TREE
{
    public class AVLTree<T> : IEnumerable<T> where T : IComparable
    {
        //cвойство для корня дерева

        public AVLTreeNode<T> Head
        {
            get;
            internal set;
        }

        #region Количество узлов дерева
        public int Count 
        {
            get;
            private set;
        }
        #endregion

        #region метод Add
        // метод добавляет новый узел

        public void Add(T value)
        {
            // вариант 1: дерево пустое - создание корня дерева
            if (Head == null)
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }

            // вариант 2: дерево не пустое - найти место для добавления нового узла
            else 
            {
                AddTo(Head, value);
            }
            Count++;
        }

        // алгоритм рекурсивного добаления нового узла в дерево

        private void AddTo(AVLTreeNode<T> node, T value)
        {
            // вариант 1: добавление нового значения в дерево. Значение добавляемого узла меньше чем текущего узла

            if (value.CompareTo(node.Value) < 0)
            {
                // создание нового левого узла,  если его нет
                if (node.Left == null)
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }

                else
                {
                    // переходим к следующему левому узлу
                    AddTo(node.Left, value);
                }
            }
            // вариант 2: добаление значения больше и равно текущему значению

            else 
            {
                // создание правого узла, если его нет
                if (node.Right == null)
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else 
                {
                    // переход к следующему правому узлу
                    AddTo(node.Right, value);
                }
            }
        }

        //node.Balace()
        #endregion

        #region Итератор

        public IEnumerator<T> InOrderTraversal()
        {
            // рекурсивное перемещение по дереву

            if (Head != null)//существует ли корень дерева
            {
                Stack<AVLTreeNode<T>> stac = new Stack<AVLTreeNode<T>>();
                AVLTreeNode<T> current = Head;

                // при рекурсивном перемещении по дереву нужно укзывать какой потомок будет следующим (правый или левый)

                bool goLeftNext = true;

                // начинаем с помещения корня в стек
                stac.Push(current);

                while (stac.Count > 0) 
                {
                    // если перемещаемся влево
                    if (goLeftNext)
                    {
                        // перемещение всех левых потомков в стек
                        while (current.Left != null)
                        {
                            stac.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    // если перемещение вправо

                    if (current.Right != null)
                    {
                        current = current.Right;
                        // идинажды перемещвемся вправо, после чего опять идем влево

                        goLeftNext = true;
                    }
                    else 
                    {
                        // если перейти вправо нельзя - извлекаем родительский узел

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
        #endregion

    }
}
