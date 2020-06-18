using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree_Clear
{
    public class AVLTreeNode<TNode> : IComparable<TNode> where TNode : IComparable
    {
        AVLTree<TNode> _tree; // дерево 

        AVLTreeNode<TNode> _left;  //левый потомок
        AVLTreeNode<TNode> _right; //правый потомок

        public AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            Value = value;  //значение в узле
            Parent = parent;//указатель на родительский элемент
            _tree = tree;   //указатель на дерево
        }

        public AVLTreeNode<TNode> Left
        {
            get
            {
                return _left;
            }

            internal set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this; //установка указателя на родительский элемент 
                }
            }
        }


        public AVLTreeNode<TNode> Right
        {
            get
            {
                return _right;
            }

            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this; //установка указателя на родительский элемент 
                }
            }
        }

        //указатель на родительский узел

        public AVLTreeNode<TNode> Parent
        {
            get;
            internal set;
        }

        //знчение текущего узла

        public TNode Value
        {
            get;
            private set;
        }

        //возвращает 1, если значение экземпляра больше переданного значения
        //возвращает -1, если значение экземпляра меньше переданного значения, 0 - когда они равны;

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}
