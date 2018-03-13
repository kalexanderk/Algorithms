using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinTrees 
{
    class tNode   // A node (vertex) for a binary tree or a doubly-linked list.
    {
        private int fkey = 0;
        public int Key              // The Key to sort the tree nodes
        {
            get { return fkey; }
        }

        private tNode fleft = null;
        public tNode Left           // Left, Right -- two children nodes of the actual node.
        {
            get { return fleft; }
            set { fleft = value; }
        }
        private tNode fright = null;
        public tNode Right
        {
            get { return fright; }
            set { fright = value; }
        }

        private string fVUD = "";
        public string VeryUsefoolData       // Just the Very Usefull Data to be stored
        {                                   // in the node as a database element.
            get { return fVUD; }
            set { fVUD = value; }
        }

        private int flevel = 0;
        public int Level                    // The root node is the only node of level 0;
        {                                   // its nearest ancestors (children) are both
            get { return flevel; }          // the vertices of level 1 and so on...
        }
        public void Setlevel(int newlevel)
        {
            flevel = newlevel;
            if (Left  != null)  Left.Setlevel(newlevel + 1);
            if (Right != null) Right.Setlevel(newlevel + 1);
        }

        private int fweight;  // Count of nodes in the tree where 'this' is the root.
        private int fheight;  // Distance to the farest child.
        private int fwidth;   // 1 + distance to the most left child + distance to the most right child

        public int Tag = 0;   // is used for serice purpose.

        public int Weight     // Nodes amount in the subtree where the actual vertex (node) is a root.
        {
            get
            {
                int w = 1;
                if (Left != null) w += Left.Weight;
                if (Right != null) w += Right.Weight;
                return w;
            }
        }

        public int Height     // The distance from "this" to the farest node in the subtree
        {                     // = Max(Level) if the actual level = 0.
            get
            {
                int hl = 0, hr = 0;
                if (Left != null) 
                    hl = 1 + Left.Height;
                if (Right != null) 
                    hr = 1 + Right.Height;
                return Math.Max(hl, hr);
            }
        }

        public int Width        // == Weight :)
        {
            get
            {
                return Weight;
            }
        }


        public tNode(int newkey, string newVUD)     // Constructor, creates a node with the parameters done.
        {
            fkey = newkey;
            fVUD = newVUD;
        }

        public bool Add(int newkey, string newVUD)  // Create and add such a node, if the new key is unique.
        {
            if (newkey == Key)
            {
                VeryUsefoolData = newVUD;
                return false;
            }

            if (newkey < Key)
            {
                if (Left != null) return Left.Add(newkey, newVUD);
                // here Left==null
                Left = new tNode(newkey, newVUD);
                return true;
            }
            if (newkey > Key)
            {
                if (Right != null) return Right.Add(newkey, newVUD);
                // here Right==null
                Right = new tNode(newkey, newVUD);
                return true;
            }
            return false; // impossible!!!
        }

        ~tNode()                // Yes, yes, a destructor!
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Left  != null)  Left.Dispose();
            if (Right != null) Right.Dispose();
            GC.SuppressFinalize(this);
        }

        public string PreList(bool Detailed, bool Reverse)    // Outputs the subtree structure in the Pre-style
        {                                       // Root-Left-Right
            string s, blank = "";

            tNode first, second;
            string firsttext, secondtext;
            if (Reverse) 
            {
                first = Right; second = Left;
                firsttext = "Right"; secondtext = "Left";
            }
            else 
            {
                first = Left; second = Right;
                firsttext = "Left"; secondtext = "Right";
            }
            
            for (int i = 0; i < Level; i++) blank = blank + "    ";
            s = blank + Key.ToString() + " " + VeryUsefoolData + Environment.NewLine;
            
            if (first == null)
            {
                if (Detailed) s = s + blank + "    " + firsttext + " is empty" + Environment.NewLine;
            }
            else
                s = s + first.PreList(Detailed, Reverse);
            if (second == null)
            {
                if (Detailed) s = s + blank + "    " + secondtext + " is empty" + Environment.NewLine;
            }
            else
                s = s + second.PreList(Detailed, Reverse);
            return s;
        }

        public string InList(bool Detailed, bool Reverse)     // Outputs the subtree structure in the In-style
        {                                       // Left-Root-Right
            string s = "", blank = "";

            tNode first, second;
            string firsttext, secondtext;
            if (Reverse)
            {
                first = Right; second = Left;
                firsttext = "Right"; secondtext = "Left";
            }
            else
            {
                first = Left; second = Right;
                firsttext = "Left"; secondtext = "Right";
            }

            for (int i = 0; i < Level; i++) blank = blank + "    ";
            if (first == null)
            {
                if (Detailed) s = s + blank + "    " + firsttext + " is empty" + Environment.NewLine;
            }
            else
                s = first.InList(Detailed, Reverse);
            s = s + blank + Key.ToString() + ". " + VeryUsefoolData +  Environment.NewLine;
            if (second == null)
            {
                if (Detailed) s = s + blank + "    " + secondtext + " is empty" + Environment.NewLine;
            }
            else
                s = s + second.InList(Detailed, Reverse);
            return s;
        }

        public string PostList(bool Detailed, bool Reverse)   // Outputs the subtree structure in the Post-style
        {                                       // Left-Right-Root
            string s = "", blank = "";

            tNode first, second;
            string firsttext, secondtext;
            if (Reverse)
            {
                first = Right; second = Left;
                firsttext = "Right"; secondtext = "Left";
            }
            else
            {
                first = Left; second = Right;
                firsttext = "Left"; secondtext = "Right";
            }

            for (int i = 0; i < Level; i++) blank = blank + "    ";
            if (first == null)
            {
                if (Detailed) s = s + blank + "    " + firsttext + " is empty" + Environment.NewLine;
            }
            else
                s = first.PostList(Detailed, Reverse);
            if (second == null)
            {
                if (Detailed) s = s + blank + "    " + secondtext + " is empty" + Environment.NewLine;
            }
            else
                s = s + second.PostList(Detailed, Reverse);
            s = s + blank + Key.ToString() + ". " + VeryUsefoolData + Environment.NewLine;
            return s;
        }

    }

    class tBintree   // A binary tree
    {
        private string fName = "noname";
        public string Name                  // M. b. "Pine", "Birch", "Oak" and so on.
        {
            get { return fName; }
            set { fName = value; }
        }

        // private tNode fRoot = null;
        public tNode fRoot = null;          // The root node.

        public tBintree(string newname)     // A constructor, creates an empty, but named tree.
        {
            fName = newname;
        }

        ~tBintree()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (fRoot != null) fRoot.Dispose();
            GC.SuppressFinalize(this);
        }

        public tNode Node(int aKey)         // Return a reference to the node with the given Key
        {
            tNode node = fRoot;
            while ((node != null) && (node.Key != aKey))
            {
                if (aKey < node.Key)
                    node = node.Left;
                else
                    node = node.Right;
            }
            return node;
        }

        public bool Add(int newkey, string newVUD)  // Creates and adds new node to the tree.
        {
            bool b;
            if (fRoot == null)
            {
                fRoot = new tNode(newkey, newVUD);
                Setlevels();
                return true;
            }
            if (newkey < fRoot.Key) {
                if (fRoot.Left == null)
                {
                    fRoot.Left = new tNode(newkey, newVUD);
                    Setlevels();
                    return true;
                }               
                if (b = fRoot.Left.Add(newkey, newVUD))
                {
                    Setlevels();
                }
                return b; 
            }
            if (newkey > fRoot.Key)
            {
                if (fRoot.Right == null)
                {
                    fRoot.Right = new tNode(newkey, newVUD);
                    Setlevels();
                    return true;
                }
                if (b = fRoot.Right.Add(newkey, newVUD))
                {
                    Setlevels();
                }
                return b;
            }
            return false;
        }

        private void Setlevels()    // Refresh levels after possible structure changings
        {
            if (fRoot != null)
                fRoot.Setlevel(0);
        }

        public string PreList(bool Detailed, bool Reverse)    // The same as in tNode
        {
            if (fRoot == null)
                return "No tree exists.";
            else
                return Name + Environment.NewLine + fRoot.PreList(Detailed, Reverse);
        }
        public string InList(bool Detailed, bool Reverse)     // The same as in tNode
        {
            if (fRoot == null)
                return "No tree exists.";
            else
                return Name + Environment.NewLine + fRoot.InList(Detailed, Reverse);
        }
        public string PostList(bool Detailed, bool Reverse)   // The same as in tNode
        {
            if (fRoot == null)
                return "No tree exists.";
            else
                return Name + Environment.NewLine + fRoot.PostList(Detailed, Reverse);
        }

        public int Weight                       // The same as in tNode
        {
            get
            {
                if (fRoot == null) return 0;
                else return fRoot.Weight;
            }
        }
        public int Height                       // The same as in tNode
        {
            get
            {
                if (fRoot == null) return -1;
                else return fRoot.Height;
            }
        }
        public int Width                        // The same as in tNode
        {
            get
            {
                if (fRoot == null) return 0;
                else return fRoot.Width;
            }
        }

        public bool SimpleToLeft(int aKey)      // Simple turn of a node(aKey) to the left.
        {
            tNode node, temp, parent;
            if ((fRoot == null))
                return false;
            if (fRoot.Key == aKey) 
            {
                node = fRoot;
                parent = null;
            }
            else
            {
                // Here fRoot.Key <> aKey
                parent = fRoot;
                if (aKey < fRoot.Key) 
                    node = fRoot.Left;
                else 
                    node = fRoot.Right;

                while ((node != null) && (node.Key != aKey))
                {
                    parent = node;
                    if (aKey < parent.Key)
                        node = parent.Left;
                    else
                        node = parent.Right;
                }                
            }

            if ((node != null) && (node.Key == aKey) && (node.Right != null))
            {
                // Turn!!!
                temp = node;
                node = temp.Right;
                temp.Right = node.Left;
                node.Left = temp;
                if (parent != null)
                    if (temp == parent.Left)
                        parent.Left = node;
                    else
                        parent.Right = node;
                else
                    fRoot = node;
                Setlevels();
                return true;                
            }
            
            return false;
        }

        public bool SimpleToRight(int aKey)     // Simple turn of a node(aKey) to the right.
        {
            tNode node, temp, parent;
            if ((fRoot == null))
                return false;
            if (fRoot.Key == aKey)
            {
                node = fRoot;
                parent = null;
            }
            else
            {
                // Here fRoot.Key <> aKey
                parent = fRoot;
                if (aKey > fRoot.Key)
                    node = fRoot.Right;
                else
                    node = fRoot.Left;

                while ((node != null) && (node.Key != aKey))
                {
                    parent = node;
                    if (aKey > parent.Key)
                        node = parent.Right;
                    else
                        node = parent.Left;
                }
            }
            if ((node != null) && (node.Key == aKey) && (node.Left != null))
            {
                // Turn!!!
                temp = node;
                node = temp.Left;
                temp.Left = node.Right;
                node.Right = temp;
                if (parent != null)
                    if (temp == parent.Right)
                        parent.Right = node;
                    else
                        parent.Left = node;
                else
                    fRoot = node;
                Setlevels();
                return true;
            }

            return false;
        }

        public bool DoubleToLeft(int aKey)      // Double turn of a node(aKey) to the left.
        {
            tNode node = Node(aKey), temp = node.Right;
            if ((node != null) && (temp != null) && (temp.Left != null))
                return SimpleToRight(temp.Key) && SimpleToLeft(node.Key);
            else return false;
        }

        public bool DoubleToRight(int aKey)      // Double turn of a node(aKey) to the right.
        {
            tNode node = Node(aKey), temp = node.Left;
            if ((node != null) && (temp != null) && (temp.Right != null))
                return SimpleToLeft(temp.Key) && SimpleToRight(node.Key);
            else return false;
        }

        public bool DeleteNode(int aKey)        // Yes, delete the node with this key
        {
            tNode node, temp, parent;
            if ((fRoot == null))
                return false;
            if (fRoot.Key == aKey)
            {
                parent = null;
                node = fRoot;
            }
            else
            {

                // Here fRoot.Key <> aKey
                parent = fRoot;
                if (aKey > fRoot.Key)
                    node = fRoot.Right;
                else
                    node = fRoot.Left;

                while ((node != null) && (node.Key != aKey))
                {
                    parent = node;
                    if (aKey > parent.Key)
                        node = parent.Right;
                    else
                        node = parent.Left;
                }
            }

            if ((node != null) && (node.Key == aKey))
            {
                temp = node;

                if ((node.Left == null) && (node.Right == null))
                {
                    node = null;
                } else
                if ((node.Left == null) && (node.Right != null))
                {
                    node = node.Right;
                } else
                    if ((node.Left != null) && (node.Right == null))
                    {
                        node = node.Left;
                    }
                    else
                    {
                        // Here node to be deleted has 2 children
                        // Either the most left node from the right subtree
                        // or the most right node from the left subtree
                        // has to substitute the node been deleted.
                        tNode parent2, temp2;
                        parent2 = node;
                        if (node.Left.Weight > node.Right.Weight)
                        {
                            // replace from the Left
                            temp2 = node.Left;
                            while (temp2.Right != null)
                            {
                                parent2 = temp2;
                                temp2 = temp2.Right;
                            }
                            if (parent2 == node)
                                parent2.Left = temp2.Left;
                            else
                                parent2.Right = temp2.Left;    // errror here!
                        }
                        else
                        {
                            // replace from the Right
                            temp2 = node.Right;
                            while (temp2.Left != null)
                            {
                                parent2 = temp2;
                                temp2 = temp2.Left;
                            }
                            if (parent2 == node)
                                parent2.Right = temp2.Right;
                            else
                                parent2.Left = temp2.Right;    // errror here!
                        }
                        temp2.Left = node.Left;
                        temp2.Right = node.Right;
                        node = temp2;
                    }

                if (parent != null)
                    if (temp == parent.Right)
                        parent.Right = node;
                    else
                        parent.Left = node;
                else
                    fRoot = node;

                Setlevels();
                return true;

            }

            return false;
        }


    }
}
