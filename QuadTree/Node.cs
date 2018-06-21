using System.Collections.Generic;

namespace QuadTree
{
    public class Node
    {

        private List<Element> _data;
        private List<Node> _children;
        private int _index;
        //private Node parent;


        public Node()
        {
            //parent = null;
            _children = new List<Node>();
        }

        public Node(List<Element> data)
        {
            this._data = data;
        }

        public List<Element> getData()
        {
            return _data;
        }

        public void setData(List<Element> data)
        {
            this._data = data;
        }

        public void setIndex(int index)
        {
            this._index = index;
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
