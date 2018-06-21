using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree
{
    public class QuadTreeService
    {
        public Graphics Graphics { get; set; }
        public List<Element> ElementsMatrix { get; set; }

        public QuadTreeService(Graphics graphics)
        {
            this.Graphics = graphics;
            this.ElementsMatrix = new List<Element>();
        }

        public void DrawLines(List<Element> list)
        {
            //Grid pen
            Pen pen = new Pen(Color.Black);
            int maxX = 0, maxY = 0, minX = 500, minY = 500;

            foreach (Element e in list)
            {
                //Find farthest to the right point of node
                if (e.C.X > maxX)
                {
                    maxX = e.C.X;
                }

                //Find lowest point of node
                if (e.C.Y > maxY)
                {
                    maxY = e.C.Y;
                }

                //Find farthest to the left point of node
                if (e.A.X < minX)
                {
                    minX = e.A.X;
                }

                //Find highest point of node
                if (e.A.Y < minY)
                {
                    minY = e.A.Y;
                }
            }


            //half of X and Y line of node (center line)
            int halfX = (maxX - minX) / 2;
            int halfY = (maxX - minX) / 2;

            
            //Diagonal lines
            Graphics.DrawLine(pen, minX + halfX, minY, minX + halfX, maxY);
            Graphics.DrawLine(pen, minX, minY + halfY, maxX, minY + halfY);

            //Circuit lines
            Graphics.DrawLine(pen, minX, minY, minX + halfX, minY + halfY);
            Graphics.DrawLine(pen, minX + halfX, minY, maxX, minY + halfY);
            Graphics.DrawLine(pen, minX, minY + halfY, minX + halfX, maxY);
            Graphics.DrawLine(pen, minX + halfX, minY + halfY, maxX, maxY);

            //Save created grid elements to elements matrix
            SaveElements(maxX, maxY, minX, minY, halfX, halfY);
        }

        public Node[] DivideTree(List<Element> list)
        {
            Node[] ChildsList = new Node[4];
            //DrawLines(list);
            int maxX = 0, maxY = 0, minX = 500, minY = 500;

            foreach (Element element in list)
            {
                //Find farthest to the right point of node
                if (element.C.X >= maxX)
                {
                    maxX = element.C.X;
                }

                //Find lowest point of node
                if (element.C.Y >= maxY)
                {
                    maxY = element.C.Y;
                }

                //Find farthest to the left point of node
                if (element.A.X <= minX)
                {
                    minX = element.A.X;
                }

                //Find highest point of node
                if (element.A.Y <= minY)
                {
                    minY = element.A.Y;
                }
            }

            //half of X and Y line of node (center line)
            int lenX = (maxX - minX) / 2;
            int lenY = (maxX - minX) / 2;

            //Lists with new childs data
            List<Element> ch1 = new List<Element>();
            List<Element> ch2 = new List<Element>();
            List<Element> ch3 = new List<Element>();
            List<Element> ch4 = new List<Element>();

            //New childs
            Node child1 = new Node();
            Node child2 = new Node();
            Node child3 = new Node();
            Node child4 = new Node();

            //if/else statement to prevent childs borders from overlapping
            if (list.Count == 4)
            {
                //Divide content of node to his children
                foreach (Element element in list)
                {
                    if (element.A.X < minX + lenX && element.A.Y < minY + lenY)
                    {
                        ch1.Add(element);
                    }

                    if (element.B.X > minX + lenX && element.B.Y < minY + lenY)
                    {
                        ch2.Add(element);
                    }

                    if (element.D.X < minX + lenX && element.D.Y > minY + lenY)
                    {
                        ch3.Add(element);
                    }

                    if (element.C.X > minX + lenX && element.C.Y > minY + lenY)
                    {
                        ch4.Add(element);
                    }
                }
            }

            else
            {
                foreach (Element element in list)
                {
                    if (element.A.X <= minX + lenX && element.A.Y <= minY + lenY)
                    {
                        ch1.Add(element);
                    }


                    if (element.B.X >= minX + lenX && element.B.Y <= minY + lenY)
                    {
                        ch2.Add(element);
                    }


                    if (element.D.X <= minX + lenX && element.D.Y >= minY + lenY)
                    {
                        ch3.Add(element);
                    }


                    if (element.C.X >= minX + lenX && element.C.Y >= minY + lenY)
                    {
                        ch4.Add(element);
                    }
                }

            }

            //Create new child nodes with data
            child1.setData(ch1);
            child2.setData(ch2);
            child3.setData(ch3);
            child4.setData(ch4);
            
            ChildsList[0] = child1;
            ChildsList[1] = child2;
            ChildsList[2] = child3;
            ChildsList[3] = child4;


            return ChildsList;
        }

        //Check if child is for division 
        public bool CheckChild(List<Element> child)
        {
            int count = 0;
            foreach (Element e in child)
            {
                //Count elements of figure in child
                if (e.Index == 1)
                {
                    count++;
                }
            }

            //if child is completely empty or filled with firuge elements, its not for further division
            if (count == 0 || count == child.Count)
            {
                return false;  //empty or full child
            }
            else
            {
                return true;
            }
        }

        //Save created grid elements to elements matrix
        public void SaveElements(int maxX, int maxY, int minX, int minY, int halfX, int halfY)
        {            
            //Create elements from node characteristic points
            Point a = new Point(minX, minY);
            Point b = new Point(minX + halfX, minY + halfY);
            Point c = new Point(minX, minY + halfY);
            Element element1 = new Element(a, b, c);

            a = new Point(minX, minY);
            b = new Point(minX + halfX, minY);
            c = new Point(minX + halfX, minY + halfY);
            Element element2 = new Element(a, b, c);

            a = new Point(minX + halfX, minY);
            b = new Point(maxX, minY + halfY);
            c = new Point(minX + halfX, minY + halfY);
            Element element3 = new Element(a, b, c);

            a = new Point(minX + halfX, minY);
            b = new Point(maxX, minY);
            c = new Point(maxX, minY + halfY);
            Element element4 = new Element(a, b, c);

            a = new Point(minX, minY + halfY);
            b = new Point(minX + halfX, maxY);
            c = new Point(minX, maxY);
            Element element5 = new Element(a, b, c);

            a = new Point(minX, minY + halfY);
            b = new Point(minX + halfX, minY + halfY);
            c = new Point(minX + halfX, maxY);
            Element element6 = new Element(a, b, c);

            a = new Point(minX + halfX, minY + halfY);
            b = new Point(maxX, maxY);
            c = new Point(minX + halfX, maxY);
            Element element7 = new Element(a, b, c);

            a = new Point(minX + halfX, minY + halfY);
            b = new Point(maxX, minY + halfY);
            c = new Point(maxX, maxY);
            Element element8 = new Element(a, b, c);

            ElementsMatrix.Add(element1);
            ElementsMatrix.Add(element2);
            ElementsMatrix.Add(element3);
            ElementsMatrix.Add(element4);
            ElementsMatrix.Add(element5);
            ElementsMatrix.Add(element6);
            ElementsMatrix.Add(element7);
            ElementsMatrix.Add(element8);

        }
    }
}
