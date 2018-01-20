using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace QuadTree
{
    public class Element
    {
        public Point a { get; set; }
        public Point b { get; set; }
        public Point c { get; set; }
        public Point d { get; set; }
        public int index { get; set; }
        

        public Element(Point A, Point B, Point C, Point D, int Index)
        {
            a = A;
            b = B;
            c = C;
            d = D;
            index = Index;            
        }
        public Element(Point A, Point B, Point C)
        {
            a = A;
            b = B;
            c = C;
        }
        public Element()
        {
           
        }
    }

  
    public class Node
    {
        private List<Element> data;
        private List<Node> children;
        private int Index;
        //private Node parent;


        public Node()
        {
            //parent = null;
            children = new List<Node>();
        }

        public List<Element> getData()
        {
            return data;
        }

        public void setData(List<Element> data)
        {
            this.data = data;
        }

        public void setIndex(int index)
        {
            this.Index = index;
        }







        #region
        //public Node(Node parent)
        //{

        //    children = new List<Node>();
        //    this.parent = parent;
        //}

        //public Node(Node parent, List<Element> data)
        //{
        //    children = new List<Node>();
        //    this.parent = parent;

        //    this.data = data;
        //}

        //public Node getParent()
        //{
        //    return parent;
        //}

        //public void setParent(Node parent)
        //{
        //    this.parent = parent;
        //}


        //public bool isLeaf()
        //{
        //    if (children.Count==0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public Node addChild(List<Element> data)
        //{
        //    Node child = new Node(this, data);
        //    children.Add(child);
        //    return child;
        //}
        #endregion
    }
}
