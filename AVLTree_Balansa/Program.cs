using System;

namespace AVLTree_Balance
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> Oak = new AVLTree<int>();


            Oak.Add(10);
            Oak.Add(3);
            Oak.Add(2);
            Oak.Add(4);
            Oak.Add(12);
            Oak.Add(15);
            Oak.Add(11);
            Oak.Add(25);

            Oak.Remove(11);

            foreach (var item in Oak)
            {
                Console.WriteLine(item);
            }
        }
    }
}
