using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuadTree
{
    public class QuadTree
    {
        public Graphics Graphics { get; set; } //Reference to program drawing area
        public List<Element> ElementsMatrix { get; set; } //List for created triangle elements

        private QuadTreeService _quadTreeService { get; set; } //Contains methods for QuadTree

        public QuadTree(Graphics graphics)
        {
            Graphics = graphics;
            _quadTreeService = new QuadTreeService(Graphics);
            
        }

        public void Recurence(List<Node> Parents)
        {
            ElementsMatrix = new List<Element>();

            //Draw lines of all parent nodes passed to method 
            foreach (Node node in Parents)
            {
                _quadTreeService.DrawLines(node.getData());               
            }

            List<Node> Childs = new List<Node>(); //List for childs of parent nodes
            List<Node> ChildsToDo = new List<Node>(); //List for childs that are intended for further divide
            Node[] ChildsArray = new Node[4]; //Array with childs of currently considered node

            //Get childs of nodes passed to the method
            foreach (Node node in Parents)
            {
                ChildsArray = _quadTreeService.DivideTree(node.getData()); //Divide currenty considered node for 4 childrens

                //Add childs of currently considered node to list of childs of all parent nodes passed to method
                foreach (Node child in ChildsArray)
                {

                    Childs.Add(child);
                }
            }

            //Check which childs of parent nodes passed to the method are intended for further division 
            foreach (Node node in Childs)
            {
                if (_quadTreeService.CheckChild(node.getData()) && node.getData().Count > 1) //if node is intended for division, add it to list
                {
                    ChildsToDo.Add(node);
                }
            }

            // Use method recursively for new parents to divide
            if (ChildsToDo.Count != 0)
            {
                Recurence(ChildsToDo);
            }
        }

        public List<Element> getElementsMatrix()
        {
            if (ElementsMatrix == null)
                throw new ArgumentNullException("Grid not created, ElementsMatrix is null");

            return ElementsMatrix;
        }
        


    }
}
