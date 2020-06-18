using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree_Balance
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

        #region метод Balance

        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Right.BalanceFactor < 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }

        }

        private int MaxChildHeight(AVLTreeNode<TNode> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }
            return 0;
        }

        private int LeftHeight
        {
            get
            {
                return MaxChildHeight(Left);
            }
        }

        private int RightHeight
        {
            get
            {
                return MaxChildHeight(Right);
            }
        }

        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }
                return TreeState.Balanced;
            }
        }

        private int BalanceFactor
        {
            get
            {
                return RightHeight - LeftHeight;
            }
        }

        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy,
        }

        #endregion

        #region метод LeftRotation
        private void LeftRotation()
        {
            // сделать правого потомка новым корнем дерева
            AVLTreeNode<TNode> newRoot = Right;
            ReplaseRoot(newRoot);

            Right = newRoot.Left;
            newRoot.Left = this;
        }
        #endregion

        #region метод RightRotation
        private void RightRotation()
        {
            // сделать левого потомка новым корнем дерева
            AVLTreeNode<TNode> newRoot = Left;
            ReplaseRoot(newRoot);

            Left = newRoot.Right;
            newRoot.Right = this;
        }
        #endregion

        #region метод LeftRightRotation
        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }
        #endregion

        #region метод RightLeftRotation
        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }
        #endregion

        #region Перемещение корня

        private void ReplaseRoot(AVLTreeNode<TNode> newRoot)
        {
            if (this.Parent != null)
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else 
            {
                _tree.Head = newRoot;
            }

            newRoot.Parent = this.Parent;
            this.Parent = newRoot;
        }

        #endregion
    }
}
