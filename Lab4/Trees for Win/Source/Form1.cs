using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BinTrees
{

    public partial class Form1 : Form
    {
        private string endl = Environment.NewLine;
        private tBintree btree = null;
        private string Filename = "";

        // Graphic settings
        private Color Bgcolor;
        private Color Nodecolor;
        private Color Textcolor;
        private int Linewidth = 3;
        private int Nodelinewidth = 2;
        private int Noderadius = 10;
        private string Treefontname = "";
        private float Treefontsize = 12;

        public Form1()
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 1;
            ofDlg.InitialDirectory = Directory.GetCurrentDirectory();
            Bgcolor = new Color();
            Bgcolor = Color.Green;
            Nodecolor = new Color();
            Nodecolor = Color.Aquamarine;
            Textcolor = new Color();
            Textcolor = Color.Pink;

            ReadIniFile();
        }

        private void ReadIniFile()
        {
            string sbuff = "";
            string FN = "Courier New";
            float FS = 10;

            System.IO.StreamReader sr = new System.IO.StreamReader("BinTree.ini");
            if (!sr.EndOfStream)
            {
                sbuff = sr.ReadLine();
                sbuff = sbuff.Trim();
                Memo.AppendText(sbuff + Environment.NewLine);
            }
            while (!sr.EndOfStream)
            {

                if (sbuff.ToLower().Trim() == "[initial tree]")
                {
                    bool EndOfGroup = false;
                    while (!sr.EndOfStream && !EndOfGroup)
                    {
                        sbuff = sr.ReadLine();
                        sbuff = sbuff.Trim();
                        Memo.AppendText(sbuff + Environment.NewLine);
                        if ((sbuff.Length>0) && (sbuff[0] == '['))
                            EndOfGroup = true;
                        else
                        {
                            string[] s = sbuff.Split(new char[] { '=' });  // splits s to the substrings
                            if (s.Length == 2)
                            {
                                if (s[0].ToLower().Trim() == "filename")
                                {
                                    Filename = s[1].Trim();
                                }

                            }
                        }
                    }
                } // [Initial Tree]
                else
                if (sbuff.ToLower().Trim() == "[memo]")
                {
                    
                        bool EndOfGroup = false;
                        while (!sr.EndOfStream && !EndOfGroup)
                        {
                            sbuff = sr.ReadLine();
                            sbuff = sbuff.Trim();
                            Memo.AppendText(sbuff + Environment.NewLine);
                            if ((sbuff.Length > 0) && (sbuff[0] == '['))
                                EndOfGroup = true;
                            else
                            {
                                string[] s = sbuff.Split(new char[] { '=' });  // splits s to the substrings
                                if (s.Length == 2)
                                {
                                        if (s[0].ToLower().Trim() == "fontname")
                                            FN = s[1].Trim();
                                        if (s[0].ToLower().Trim() == "fontsize")
                                            FS = float.Parse(s[1]);
                                }
                                lblMemoFont.Text = FN + ", " + FS + " pt.";
                                Memo.Font = new Font(FN, FS);
                            }
                        }
                    
                } // [Memo]
                else
                if (sbuff.ToLower().Trim() == "[picture]")
                {
                    bool EndOfGroup = false;
                    while (!sr.EndOfStream && !EndOfGroup)
                    {
                        sbuff = sr.ReadLine();
                        sbuff = sbuff.Trim();
                        Memo.AppendText(sbuff + Environment.NewLine);
                        if ((sbuff.Length > 0) && (sbuff[0] == '['))
                            EndOfGroup = true;
                        else
                        {
                            string[] s = sbuff.Split(new char[] { '=' });  // splits s to the substrings
                            if (s.Length == 2)
                            {
                                if (s[0].ToLower().Trim() == "bgcolor")
                                {
                                    // Bla-bla-bla set Bgcolor.Name = s[1];
                                    Bgcolor = Color.FromName(s[1].Trim());
                                    lblBgClr.Text = "BgColor" + Environment.NewLine + s[1].Trim();
                                }
                                if (s[0].ToLower().Trim() == "nodecolor")
                                {
                                    // Bla-bla-bla set Nodecolor.Name = s[1];
                                    Nodecolor = Color.FromName(s[1].Trim());
                                    lblLnClr.Text = "Lines Color" + Environment.NewLine + s[1].Trim();
                                }
                                if (s[0].ToLower().Trim() == "textcolor")
                                {
                                    // Bla-bla-bla set Textcolor.Name = s[1];
                                    Textcolor = Color.FromName(s[1].Trim());
                                    lblTxtClr.Text = "Text Color" + Environment.NewLine + s[1].Trim();
                                }
                                if (s[0].ToLower().Trim() == "linewidth")
                                {
                                    Linewidth = int.Parse(s[1]);
                                    nudELW.Value = Linewidth;
                                }
                                if (s[0].ToLower().Trim() == "nodelinewidth")
                                {
                                    Nodelinewidth = int.Parse(s[1]);
                                    nudNLW.Value = Nodelinewidth;
                                }
                                if (s[0].ToLower().Trim() == "noderadius")
                                {
                                    Noderadius = int.Parse(s[1]);
                                    nudNRad.Value = Noderadius;
                                }
                                if (s[0].ToLower().Trim() == "treefontname")
                                {
                                    Treefontname = s[1].Trim();
                                }
                                if (s[0].ToLower().Trim() == "treefontsize")
                                {
                                    Treefontsize = float.Parse(s[1]);
                                }
                                lblTextFont.Text = Treefontname + ", " + Treefontsize + " pt.";
                            }
                        }
                    }
                } // [Picture]
            } 
            sr.Close();
            if (Filename != "")
                OpenTree();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Open the tree text file.

        private void OpenTree()
        {
            saveTreeToolStripMenuItem.Enabled = true;
            sfDlg.FileName = ofDlg.FileName;

            // Reading the file
            // First line -- a tree name.
            // Each next line contains <int node key> <string VeryUsefullData>
            // that is an int number and some string WITHOUT delimiters (blanks and/or tabs).
            int newkey;
            string newVUD, sbuff;

            System.IO.StreamReader sr = new System.IO.StreamReader(ofDlg.FileName);

            Memo.AppendText("-----" + endl);

            sbuff = sr.ReadLine();  // First line -- a tree name.
            btree = new tBintree(sbuff);
            tbName.Text = sbuff;
            Memo.AppendText("Tree " + sbuff + " created." + endl);
            cbNodes.Sorted = true;
            cbNodes.Items.Clear();

            do      // Each next line contains <int node key> <string VeryUsefullData>
            {       // that is an int number and some string data.
                do
                {
                    sbuff = sr.ReadLine();
                } while (!sr.EndOfStream && sbuff.Length == 0);

                if (sbuff.Length != 0)
                {
                    Memo.AppendText("Read: \"" + sbuff + "\": ");
                    sbuff = sbuff.Replace('\t', ' ');
                    Memo.AppendText("Replaced: \"" + sbuff + "\": ");
                    string[] s = sbuff.Split(new char[] { ' ' });  // splits s to the substrings
                    int i = 0, i1;                                 // delimited by ' '.
                    while ((i < s.Length) && (s[i].Length == 0)) i++;
                    if (i < s.Length)
                    {
                        Memo.AppendText("Key: " + s[i] + ", ");     // First nonzero string is a key
                        try
                        {
                            newkey = int.Parse(s[i]);
                        }
                        catch (FormatException)
                        {
                            //MessageBox.Show("\"" + s[i] + "\" isn't an integer value");
                            Memo.AppendText("\"" + s[i] + "\" isn't an integer value." + Environment.NewLine);
                            Memo.AppendText("Input file format is incorrect." + Environment.NewLine);
                            btree.Dispose();
                            btree = null;
                            cbNodes.Items.Clear();
                            return;
                        }

                        if (newkey < 0)
                        {
                            newkey = -newkey;
                            Memo.AppendText("Negative numbers are forbidden. " + newkey + " was get." + Environment.NewLine);
                        }
                        if (newkey > 999)
                        {
                            newkey = 999;
                            Memo.AppendText("Numbers are to be less then 1000. " + newkey + " was get." + Environment.NewLine);
                        }

                        i1 = i;
                        i++;
                        while ((i < s.Length) && (s[i].Length == 0)) i++;
                        if (i == s.Length)
                        {
                            Memo.AppendText("Warninig: the node " + newkey + " has no Very Usefull Data." + Environment.NewLine);
                            newVUD = "";
                        }
                        else
                        {
                            sbuff = sbuff.Trim().Substring(s[i1].Length);
                            Memo.AppendText("VUD: " + sbuff + Environment.NewLine);
                            newVUD = sbuff;                         // Second one is the VUD
                        }
                        if (btree.Add(newkey, newVUD))              // Create the new node
                        {
                            if ((0 <= newkey) && (newkey <= 9))
                                cbNodes.Items.Add("  " + newkey);
                            if ((10 <= newkey) && (newkey <= 99))
                                cbNodes.Items.Add(" " + newkey);
                            if ((100 <= newkey) && (newkey <= 999))
                                cbNodes.Items.Add("" + newkey);
                        }
                    }

                }
            } while (!sr.EndOfStream);
            sr.Close();

            Memo.AppendText("Weight: " + btree.Weight +
                            "; Height: " + btree.Height +
                            "; Width:  " + btree.Width + Environment.NewLine);

            picBox.Visible = true;
            panel1.Visible = true;
            label3.Visible = false;

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                Filename = ofDlg.FileName;
                OpenTree();
            }

        }

        // Save the tree structure to a text file.
        // File structure is in the matter of fact the PreList tree description
        private void saveTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfDlg.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfDlg.FileName);
                sw.Write(btree.PreList(false, false));
                sw.Close();
            }

        }

        // Repaint the tree in the picBox
        private void picBox_Paint(object sender, PaintEventArgs e)
        {

            Linewidth = (int)nudELW.Value;
            Nodelinewidth = (int)nudNLW.Value;
            Noderadius = (int)nudNRad.Value;

            Pen nodepen = new Pen(Nodecolor, Nodelinewidth);
            Pen linepen = new Pen(Nodecolor, Linewidth);
            SolidBrush brush = new SolidBrush(Bgcolor);
            SolidBrush textbrush = new SolidBrush(Textcolor);
            Font font = new Font(Treefontname, Treefontsize);
            //  "Courier New" "Bookman Old Style";

            // Creating a picture
            int picWidth = picBox.ClientSize.Width;
            int picHeight = picBox.ClientSize.Height;
            int Margine = 20;

            Bitmap bmp;
            Graphics g;
            if (picBox.Image == null)
            {
                bmp = new Bitmap(picWidth, picHeight);
                picBox.Image = bmp;
            }
            else
            {
                bmp = (Bitmap)(picBox.Image);
            }
            g = Graphics.FromImage(bmp);

            picBox.Image = bmp;

            // Painting the tree

            // Background
            g.FillRectangle(brush, 0, 0, picWidth, picHeight);

            if ((btree == null) || (btree.fRoot == null)) return;

            // The Title
            g.DrawString(btree.Name, font, textbrush, Margine / 2, (Margine + font.Height) / 2);

            
            // horisontal and vertical distance between the nodes at the picture.
            int xstep = (picWidth - 2 * Margine) / btree.Width,
                ystep = (picHeight - 2 * Margine) / (btree.Height + 1);

            int N = btree.Width;
            int M = btree.Height + 1;

            // Non-recursive mass operation - drawing tree nodes
            nodes = new tNode[btree.Weight];
            // nodes[0] = btree.fRoot;
            int X, Y, Xto, Yto;
            
            nCount = 0;
            CollectNodes(btree.fRoot);      // Creates and fills nodes array sorted by the Key
            string s = nCount + " nodes: ";
            for (int i = 0; i < nCount; i++)
            {
                s = s + nodes[i].Key + "(" + i + ", " + nodes[i].Level + "; ";
                if (nodes[i].Left != null) s = s + "L:" + nodes[i].Left.Key + ", ";
                if (nodes[i].Right != null) s = s + "R:" + nodes[i].Right.Key;
                s = s + "); ";
                // Nodes coords in pixels
                X = Margine + xstep / 2 + i * xstep;
                Y = Margine + ystep / 2 + nodes[i].Level * ystep;
                // Draw the node circle
                g.DrawEllipse(nodepen, X - Noderadius, Y - Noderadius, 2 * Noderadius, 2 * Noderadius);
                // Output the node info
                string Info = "";
                float Xt = 0, Yt = 0;
                if (rbNum.Checked)
                {
                    Info = nodes[i].Key + "";
                    Xt = X - font.Height * 2 / 3;
                    Yt = Y - font.Height / 2;
                }
                else
                {
                    Info = nodes[i].Key + " " + nodes[i].VeryUsefoolData;
                    Xt = X - Noderadius;
                    Yt = Y - Noderadius - Treefontsize * 2;
                }
                g.DrawString(Info, font, textbrush, Xt, Yt);
                
                // Draw the links to node's children
                if (nodes[i].Left != null)
                {
                    int ito = nodes[i].Left.Tag;
                    Xto = Margine + xstep / 2 + ito * xstep;
                    Yto = Margine + ystep / 2 + nodes[ito].Level * ystep;
                    double ll = Math.Sqrt((X - Xto) * (X - Xto) + (Y - Yto) * (Y - Yto)),
                           dx = Math.Abs(X - Xto) / ll * Noderadius,
                           dy = Math.Abs(Y - Yto) / ll * Noderadius;
                    g.DrawLine(linepen, X - (int)dx, Y + (int)dy, Xto + (int)dx, Yto - (int)dy);
                }

                if (nodes[i].Right != null)
                {
                    int ito = nodes[i].Right.Tag;
                    Xto = Margine + xstep / 2 + ito * xstep;
                    Yto = Margine + ystep / 2 + nodes[ito].Level * ystep;
                    double ll = Math.Sqrt((X - Xto) * (X - Xto) + (Y - Yto) * (Y - Yto)),
                           dx = Math.Abs(X - Xto) / ll * Noderadius,
                           dy = Math.Abs(Y - Yto) / ll * Noderadius;
                    g.DrawLine(linepen, X + (int)dx, Y + (int)dy, Xto - (int)dx, Yto - (int)dy);
                }

            }
            // Memo.AppendText(s + Environment.NewLine);  // Debug control
            
        }

        private tNode[] nodes = null;
        int nCount = 0;

        // Creates and fills nodes array sorted by the Key
        private void CollectNodes(tNode node)
        {
            if (node == null) return;
            if (node.Left != null) CollectNodes(node.Left);
            nodes[nCount] = node;
            nodes[nCount].Tag = nCount;
            nCount++;
            if (node.Right != null) CollectNodes(node.Right);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            picBox.Image = null;
        }

        // Add a new node to the tree.
        private void btnInsert_Click(object sender, EventArgs e)
        {
            int newkey;
            if ((btree == null) && (tbName.Text == ""))
            {
                // MessageBox.Show("No binary tree exists. Enter the tree name to create the tree.");
                Memo.AppendText("No binary tree exists. Enter the tree name to create the tree." + Environment.NewLine);
                return;
            }
            if (btree == null)
                btree = new tBintree(tbName.Text);

            newkey = (int)nudNo.Value;
            if (!btree.Add(newkey, tbVUD.Text))
            {
                // MessageBox.Show("Such a node key (" + int.Parse(tbNo.Text) + ") already exists!");
                Memo.AppendText("Such a node key (" + newkey + ") already exists! Very usefull data was changed." + Environment.NewLine);
            }
            else
            {
                Memo.AppendText("The node No " + newkey + " was added successfully." + Environment.NewLine);
                //cbNodes.Items.Add(nudNo.Value + "");
                if ((0 <= newkey) && (newkey <= 9))
                    cbNodes.Items.Add("  " + newkey);
                if ((10 <= newkey) && (newkey <= 99))
                    cbNodes.Items.Add(" " + newkey);
                if ((100 <= newkey) && (newkey <= 999))
                    cbNodes.Items.Add("" + newkey);

                if (tbVUD.Text == "")
                    Memo.AppendText("Warning: the node has no very usefull data." + Environment.NewLine);
            }
        }

        // Print the tree structure to the Memo
        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool format = cbFormat.Checked;
            bool reverse = cbReverse.Checked;
            if (btree == null)
            {
                //MessageBox.Show("No binary tree exists.");
                Memo.AppendText("No binary tree exists." + Environment.NewLine);
                return;
            }
            if (rbPre.Checked)
                Memo.AppendText(Environment.NewLine + btree.PreList(format, reverse) + Environment.NewLine);
            if (rbIn.Checked)
                Memo.AppendText(Environment.NewLine + btree.InList(format, reverse) + Environment.NewLine);
            if (rbPost.Checked)
                Memo.AppendText(Environment.NewLine + btree.PostList(format, reverse) + Environment.NewLine);
        }

        // Change the tree structure -- turn some node to the left or to the right.
        private void btnTurn_Click(object sender, EventArgs e)
        {
            
            if (btree == null)
            {
                //MessageBox.Show("No binary tree exists.");
                Memo.AppendText("No binary tree exists." + Environment.NewLine);
                return;
            }
            Memo.AppendText("Attempt for \"" + cbNodes.Text + "\" turning ");
            int TurnedNode;
            try
            {
                TurnedNode = int.Parse(cbNodes.Text);
            }
            catch (FormatException)
            {
                // MessageBox.Show("\"" + cbNodes.Text + "\" isn't an integer value");
                Memo.AppendText("\"" + cbNodes.Text + "\" isn't an integer value" + Environment.NewLine);
                tNode node = btree.fRoot;
                while (node.Left != null) node = node.Left;
                TurnedNode = node.Key - 1;
            }

            if (rbSL.Checked)
            {
                Memo.AppendText("(simple) to left ");
                if (btree.SimpleToLeft(TurnedNode))
                    Memo.AppendText(" is successfull." + Environment.NewLine);
                else
                    Memo.AppendText(" isn't successfull." + Environment.NewLine);
            }
            if (rbSR.Checked)
            {
                Memo.AppendText("(simple) to right");
                if (btree.SimpleToRight(TurnedNode))
                    Memo.AppendText(" is successfull." + Environment.NewLine);
                else
                    Memo.AppendText(" isn't successfull." + Environment.NewLine);
            }

            if (rbDL.Checked)
            {
                Memo.AppendText("(double) to left ");
                if (btree.DoubleToLeft(TurnedNode))
                    Memo.AppendText(" is successfull." + Environment.NewLine);
                else
                    Memo.AppendText(" isn't successfull." + Environment.NewLine);
            }
            if (rbDR.Checked)
            {
                Memo.AppendText("(double) to right");
                if (btree.DoubleToRight(TurnedNode))
                    Memo.AppendText(" is successfull." + Environment.NewLine);
                else
                    Memo.AppendText(" isn't successfull." + Environment.NewLine);
            }

        }

        // Attempt to deleting some node
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btree == null)
            {
                //MessageBox.Show("No binary tree exists.");
                Memo.AppendText("No binary tree exists." + Environment.NewLine);
                return;
            }
            int TurnedNode;
            try
            {
                TurnedNode = int.Parse(cbNodes.Text);
            }
            catch (FormatException)
            {
                // MessageBox.Show("\"" + cbNodes.Text + "\" isn't an integer value");
                Memo.AppendText("\"" + cbNodes.Text + "\" isn't an integer value" + Environment.NewLine);
                tNode node = btree.fRoot;
                while (node.Left != null) node = node.Left;
                TurnedNode = node.Key - 1;
            }
            Memo.AppendText("Attempt to delete the node No \"" + cbNodes.Text + "\" is ");
            if (btree.DeleteNode(TurnedNode))
            {
                Memo.AppendText(" successfull." + Environment.NewLine);
                cbNodes.Items.Remove(cbNodes.Text);
            }
            else
                Memo.AppendText("n't successfull." + Environment.NewLine);
        }

        // Create a new tree
        private void newTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btree != null)
            {
                if (btree.fRoot != null)
                    btree.fRoot.Dispose();
                btree.Dispose();
            }

            btree = new tBintree(tbName.Text);
        }

        // Change the tree name
        private void btnChName_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
                tbName.Text = "noname";
            btree.Name = tbName.Text;
            Memo.AppendText("Tree name was changed to " + btree.Name + Environment.NewLine);
        }

        // Save the log from the Memo to a text file.
        private void saveMemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldFileName = sfDlg.FileName;
            sfDlg.FileName = "Memo.txt";
            if (sfDlg.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(sfDlg.FileName);
                sw.Write(Memo.Text);
                sw.Close();
            }
            sfDlg.FileName = oldFileName;
        }

        // About me and my terrrific and magnific program.
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinTree.AboutBTree ab = new BinTree.AboutBTree();
            ab.ShowDialog();
        }

        private void helpBlinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, "index.htm");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Reading the .ini file

        }

        private void btnMemoFont_Click(object sender, EventArgs e)
        {
            fntDlg.Font = new Font(Memo.Font.Name, Memo.Font.Size, Memo.Font.Style, Memo.Font.Unit);
            if (fntDlg.ShowDialog() == DialogResult.OK)
            {
                Memo.Font = new Font(fntDlg.Font.Name, fntDlg.Font.Size, fntDlg.Font.Style, fntDlg.Font.Unit);
                lblMemoFont.Text = fntDlg.Font.Name + ", " + fntDlg.Font.Size + " pt.";
            }
        }

        private void btnTextFont_Click(object sender, EventArgs e)
        {
            fntDlg.Font = new Font(Treefontname, Treefontsize);
            if (fntDlg.ShowDialog() == DialogResult.OK)
            {
                Treefontname = fntDlg.Font.Name;
                Treefontsize = fntDlg.Font.Size;
                lblTextFont.Text = Treefontname + ", " + Treefontsize + " pt.";
            }
        }

        private void btnBgClr_Click(object sender, EventArgs e)
        {
            clrDlg.Color = Bgcolor;
            if (clrDlg.ShowDialog() == DialogResult.OK)
            {
                Bgcolor = clrDlg.Color;
                lblBgClr.Text = "BgColor" + Environment.NewLine + Bgcolor.Name;
            }
        }

        private void btnLnClr_Click(object sender, EventArgs e)
        {
            clrDlg.Color = Nodecolor;
            if (clrDlg.ShowDialog() == DialogResult.OK)
            {
                Nodecolor = clrDlg.Color;
                lblLnClr.Text = "Line Color" + Environment.NewLine + Nodecolor.Name;
            }
        }

        private void btnTxtClr_Click(object sender, EventArgs e)
        {
            clrDlg.Color = Textcolor;
            if (clrDlg.ShowDialog() == DialogResult.OK)
            {
                Textcolor = clrDlg.Color;
                lblTxtClr.Text = "Text Color" + Environment.NewLine + Textcolor.Name;
            }
        }

        private void btnBgClr_MouseHover(object sender, EventArgs e)
        {
            btnBgClr.BackColor = Bgcolor;
        }

        private void btnBgClr_MouseLeave(object sender, EventArgs e)
        {
            btnBgClr.BackColor = Color.Transparent;
        }

        private void btnLnClr_MouseHover(object sender, EventArgs e)
        {
            btnLnClr.BackColor = Nodecolor;
        }

        private void btnLnClr_MouseLeave(object sender, EventArgs e)
        {
            btnLnClr.BackColor = Color.Transparent;
        }

        private void btnTxtClr_MouseHover(object sender, EventArgs e)
        {
            btnTxtClr.BackColor = Textcolor;
        }

        private void btnTxtClr_MouseLeave(object sender, EventArgs e)
        {
            btnTxtClr.BackColor = Color.Transparent;
        }

    }

    // Not in use yet.
    struct tNodeInfo
    {
        int X, Y;
        tNode node;
    }

}
