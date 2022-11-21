using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://simpledevcode.wordpress.com/2014/09/16/avl-tree-in-c/

namespace Library_Kiosk
{
    /// <summary>
    /// Class implementation from https://simpledevcode.wordpress.com/2014/09/16/avl-tree-in-c/
    /// This is the class that creates the AVL Tree and handels all of its functions
    /// </summary>
    class AVL
    {
        /// <summary>
        /// Node class that creates the Nodes that are stored in the Tree
        /// </summary>
        class Node
        {
            public Book book;
            public Node left;
            public Node right;
            public Node(Book data)
            {
                this.book = data;
            }
        }
        Node root;
        public AVL()
        {
        }
        /// <summary>
        /// Method that adds data to the tree 
        /// if the root is null the data will be set to the root value
        /// is the root is not null the RecursiveInsert() method is used to insert the data
        /// </summary>
        /// <param name="data">what is getting added to the tree</param>
        public void Add(Book data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        /// <summary>
        /// Checks where in the tree (left or right) the data belongs based of alphabetical order
        /// then checks if the tree needs to be rebalanced
        /// </summary>
        /// <param name="current"> the current node that it is checking</param>
        /// <param name="n">the node that the current node is being compared to</param>
        /// <returns></returns>
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (string.Compare(n.book.getTitle(), current.book.getTitle()) == 1) // n.book < current.book
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (string.Compare(n.book.getTitle(), current.book.getTitle()) == -1) // n.book > current.book
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        /// <summary>
        /// Checks the node that is passed into it to deside what type of rotation needs to be performed
        /// </summary>
        /// <param name="current">The node that is being checked</param>
        /// <returns></returns>
        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        /// <summary>
        /// Calls the Delete method on the node that is passed in 
        /// </summary>
        /// <param name="target">The node that will be deleted from the tree</param>
        public void Delete(Book target)
        {//and here
            root = Delete(root, target);
        }
        /// <summary>
        /// Similar to the insert method this method determines what side the node is on and deletes it 
        /// it then checks if the tre needs to be rebalanced
        /// </summary>
        /// <param name="current">Current place in the tree, the root is usually passed in</param>
        /// <param name="target">The node that will be deleted from the tree</param>
        /// <returns></returns>
        private Node Delete(Node current, Book target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                //left subtree
                if (string.Compare(target.getTitle(), current.book.getTitle()) == 1) //target < current.book
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)//here
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //right subtree
                else if (string.Compare(target.getTitle(), current.book.getTitle()) == -1) //target > current.book
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //if target is found
                else
                {
                    if (current.right != null)
                    {
                        //delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.book = parent.book;
                        current.right = Delete(current.right, parent.book);
                        if (balance_factor(current) == 2)//rebalancing
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }
        /// <summary>
        /// Allows you to search for a value in the tree 
        /// </summary>
        /// <param name="key">the data you want to search for</param>
        public void Find(Book key)
        {
            if (Find(key, root).book == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        /// <summary>
        /// The method that does the work for the find method
        /// Finds what side of the tree the data is on and returns the data
        /// </summary>
        /// <param name="target">The data you are trying to find</param>
        /// <param name="current">The current node the program is looking at</param>
        /// <returns></returns>
        private Node Find(Book target, Node current)
        {

            if (string.Compare(target.getTitle(), current.book.getTitle()) == 1)
            {
                if (target == current.book)
                {
                    return current;
                }
                else
                    return Find(target, current.left);
            }
            else
            {
                if (target == current.book)
                {
                    return current;
                }
                else
                    return Find(target, current.right);
            }

        }
        /// <summary>
        /// Checks if the tree is empty
        /// if not the InOrderDisplayTree method is called
        /// </summary>
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }
        /// <summary>
        /// Displays the tree in order
        /// </summary>
        /// <param name="current">The node that the tree will start the display at, usually the root</param>
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.book);
                InOrderDisplayTree(current.right);
            }
        }
        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        /// <summary>
        /// returns the height of the tree
        /// </summary>
        /// <param name="current">the node that the height is counted from</param>
        /// <returns></returns>
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
