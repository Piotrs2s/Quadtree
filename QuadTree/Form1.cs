using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadTree
{
    public partial class Form1 : Form
    {
        Graphics Graphics;
        

        private List<Element> Elements;

        public List<Element> Matrix = new List<Element>();

        private List<Point> Vertices;
        private List<Point> Points;
        Pen pen = new Pen(Color.Black);

        Random rand = new Random();


        public int ElementSize = 5;

        public Form1()
        {
            InitializeComponent();
            Graphics = pictureBox1.CreateGraphics();

        }
        //Generate Button
        private void button1_Click(object sender, EventArgs e)
        {
            
            ElementSize = Int32.Parse(textBox1.Text);
            Graphics.Clear(Color.White);
            GeneratePoints();


            Node Father = new Node();
            Father.setData(Elements);
            List<Node> Mother = new List<Node>();
            Mother.Add(Father);
            Najlepsza(Mother);

        }

        // Save button
        private void button2_Click(object sender, EventArgs e)
        {
            Save(Matrix);
        }

        // Laplace/test button
        private void button3_Click(object sender, EventArgs e)
        {
            TestDraw();
            //Laplace();
        }


        public void GeneratePoints()
        {
            Vertices = new List<Point>();
            int size = pictureBox1.Width-1;

            for (int i = 0; i < size; i = i + ElementSize)
            {
                Point px = new Point(i, 0);
                Vertices.Add(px);
                for (int j = ElementSize; j < size; j = j + ElementSize)
                {
                    Point py = new Point(i, j);
                    Vertices.Add(py);
                }
            }
            Elements = new List<Element>();
            foreach (Point p in Vertices)
            {
                Point a = new Point(p.X, p.Y);
                Point b = new Point(p.X + ElementSize, p.Y);
                Point c = new Point(p.X + ElementSize, p.Y + ElementSize);
                Point d = new Point(p.X, p.Y + ElementSize);
                int index = 0;
                Element element = new Element(a, b, c, d, index);
                Elements.Add(element);
            }

           

            //Create squares
            foreach (Element e in Elements)
            {
                //Ordinar

                //if (e.a.X >= 200 && e.b.X <= 300 && e.a.Y >= 200 && e.d.Y <= 300)
                //{
                //    e.index = 1;
                //    ColorElement(e, new SolidBrush(Color.Red));
                //}

                //Normal
                if (checkBox1.Checked)
                {
                    if (e.a.X >= 10 && e.b.X <= 60 && e.a.Y >= 10 && e.d.Y <= 60)
                    {
                        e.index = 1;
                        ColorElement(e, new SolidBrush(Color.Red));
                    }

                    if (e.a.X > 350 && e.b.X < 400 && e.a.Y > 350 && e.d.Y < 400)
                    {
                        e.index = 1;
                        ColorElement(e, new SolidBrush(Color.Red));
                    }
                }
                

                //Random
                if (checkBox2.Checked)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (e.a.X > rand.Next(0, pictureBox1.Width - 1) && e.b.X < rand.Next(0, pictureBox1.Width - 1) && e.a.Y > rand.Next(0, pictureBox1.Height - 1) && e.d.Y < rand.Next(0, pictureBox1.Height - 1))
                        {
                            e.index = 1;
                            ColorElement(e, new SolidBrush(Color.Red));
                        }
                    }
                }
               



            }
        }

        public void ColorElement(Element element, Brush brush)
        {
            Points = new List<Point>();
            for (int i = 0; i < pictureBox1.Width-1; i++)
            {
                Point px = new Point(i, 0);
                Points.Add(px);
                for (int j = 1; j < pictureBox1.Height-1; j++)
                {
                    Point py = new Point(i, j);
                    Points.Add(py);
                }
            }

            foreach (Point P in Points)
            {
                if (P.X >= element.a.X && P.X <= element.b.X && P.Y >= element.a.Y && P.Y <= element.d.Y)
                {
                    Graphics.FillRectangle(brush, P.X, P.Y, 1, 1);
                }
            }
        }

        public void Najlepsza(List<Node> Parents)
        {
            foreach (Node node in Parents)
            {
                DrawLines(node.getData());
            }

            List<Node> Childs = new List<Node>();
            List<Node> ChildsToDo = new List<Node>();
            Node[] ChildsArray = new Node[4];

            foreach (Node node in Parents)
            {
                ChildsArray = DivideTree(node.getData());
                foreach (Node child in ChildsArray)
                {
                    
                    Childs.Add(child);
                }
            }

            foreach (Node node in Childs)
            {
                if (CheckChild(node.getData()) == 1 && node.getData().Count > 1)
                {
                    ChildsToDo.Add(node);
                }
            }

            if (ChildsToDo.Count != 0)
            {
                Najlepsza(ChildsToDo);
            }
            

        }

        public void DrawLines(List<Element> list)
        {
            int maxX = 0, maxY = 0, minX = 500, minY = 500;

            foreach (Element e in list)
            {
                if (e.c.X > maxX)
                {
                    maxX = e.c.X;
                }

                if (e.c.Y > maxY)
                {
                    maxY = e.c.Y;
                }

                if (e.a.X < minX)
                {
                    minX = e.a.X;
                }

                if (e.a.Y < minY)
                {
                    minY = e.a.Y;
                }
            }



            int halfX = (maxX - minX) / 2;
            int halfY = (maxX - minX) / 2;


            #region try
            //int difX = 0;
            //int difY = 0;
            //foreach (var e in list)
            //{
            //    if (halfX > e.a.X && halfX <= e.b.X)
            //    {
            //        halfX = e.b.X;
            //        difX = e.b.X - halfX;                    
            //    }

            //    if (halfY > e.b.Y && halfY <= e.c.Y)
            //    {
            //        halfY = e.c.Y;
            //        difY = halfY - e.c.Y;                    
            //    }
            //}

            ////maxX = maxX - difX;
            ////maxY = maxY + difY;

            ////minX = minX + difX;
            ////minY = minY - difY;
            #endregion

            Graphics.DrawLine(pen, minX + halfX, minY, minX+halfX, maxY);
            Graphics.DrawLine(pen, minX, minY + halfY, maxX, minY+halfY);

            //triangles
            Graphics.DrawLine(pen, minX, minY, minX + halfX, minY + halfY);
            Graphics.DrawLine(pen, minX + halfX, minY, maxX, minY + halfY);
            Graphics.DrawLine(pen, minX, minY + halfY, minX + halfX, maxY);
            Graphics.DrawLine(pen, minX + halfX, minY + halfY, maxX, maxY);

            SaveElements(maxX, maxY, minX, minY, halfX, halfY);
        }

        public void SaveElements(int maxX,int maxY,int minX,int minY,int halfX,int halfY )
        {
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

            Matrix.Add(element1);
            Matrix.Add(element2);
            Matrix.Add(element3);
            Matrix.Add(element4);
            Matrix.Add(element5);
            Matrix.Add(element6);
            Matrix.Add(element7);
            Matrix.Add(element8);

        }

        public Node[] DivideTree(List<Element> list)
        {
            Node[] ChildsList = new Node[4];
            //DrawLines(list);
            int maxX = 0, maxY = 0, minX = 500, minY = 500;

            foreach (Element e in list)
            {

                if (e.c.X >= maxX)
                {
                    maxX = e.c.X;
                }

                if (e.c.Y >= maxY)
                {
                    maxY = e.c.Y;
                }

                if (e.a.X <= minX)
                {
                    minX = e.a.X;
                }

                if (e.a.Y <= minY)
                {
                    minY = e.a.Y;
                }
            }

            int lenX = (maxX - minX) / 2;
            int lenY = (maxX - minX) / 2;

            List<Element> ch1 = new List<Element>();
            List<Element> ch2 = new List<Element>();
            List<Element> ch3 = new List<Element>();
            List<Element> ch4 = new List<Element>();

            Node child1 = new Node();
            Node child2 = new Node();
            Node child3 = new Node();
            Node child4 = new Node();

            if (list.Count == 4)
            {
                foreach (Element e in list)
                {
                    if (e.a.X < minX + lenX && e.a.Y < minY + lenY)
                    {
                        ch1.Add(e);
                    }
                
                    if (e.b.X > minX + lenX && e.b.Y < minY + lenY)
                    {
                        ch2.Add(e);
                    }
                
                    if (e.d.X < minX + lenX && e.d.Y > minY + lenY)
                    {
                        ch3.Add(e);
                    }
                
                    if (e.c.X > minX + lenX && e.c.Y > minY + lenY)
                    {
                        ch4.Add(e);
                    }
                }
            }

            else
            {
                foreach (Element e in list)
                {
                    if (e.a.X <= minX + lenX && e.a.Y <= minY + lenY)
                    {
                        ch1.Add(e);
                    }


                    if (e.b.X >= minX + lenX && e.b.Y <= minY + lenY)
                    {
                        ch2.Add(e);
                    }


                    if (e.d.X <= minX + lenX && e.d.Y >= minY + lenY)
                    {
                        ch3.Add(e);
                    }


                    if (e.c.X >= minX + lenX && e.c.Y >= minY + lenY)
                    {
                        ch4.Add(e);
                    }
                }

            }

            child1.setData(ch1);
            child2.setData(ch2);
            child3.setData(ch3);
            child4.setData(ch4);

            child1.setIndex(1);
            child1.setIndex(2);
            child1.setIndex(3);
            child1.setIndex(4);

            ChildsList[0] = child1;
            ChildsList[1] = child2;
            ChildsList[2] = child3;
            ChildsList[3] = child4;


            return ChildsList;
        }

        public int CheckChild(List<Element> child)
        {
            int state = 0;

            int count = 0;
            foreach (Element e in child)
            {
                if (e.index == 1)
                {
                    count++;
                }
            }

            if (count == 0 || count == child.Count)
            {
                state = 0; //empty or full child
            }
            else
            {
                state = 1;
            }


            return state;
        }


        public void Laplace()
        {
            List<Element> elementsOfPoint = new List<Element>();

            foreach (var point in Vertices)
            {
                foreach (var element in Matrix)
                {
                    if (element.a == point || element.b == point || element.c == point  )
                    {
                        elementsOfPoint.Add(element);
                    }
                }

                int sumX = 0;
                int sumY = 0;
                int countX = 0;
                int countY = 0;
                Point newPoint = new Point();

                foreach (var el in elementsOfPoint)
                {   //X
                    if (el.a.X != point.X)
                    {
                        sumX = sumX + el.a.X;
                        countX++;
                    }

                    if (el.b.X != point.X)
                    {
                        sumX = sumX + el.b.X;
                        countX++;
                    }

                    if (el.c.X != point.X)
                    {
                        sumX = sumX + el.c.X;
                        countX++;
                    }

                    //Y
                    if (el.a.Y != point.Y)
                    {
                        sumY = sumY + el.a.Y;
                        countY++;
                    }

                    if (el.b.Y != point.Y)
                    {
                        sumY = sumY + el.b.Y;
                        countY++;
                    }

                    if (el.c.Y != point.Y)
                    {
                        sumY = sumY + el.c.Y;
                        countY++;
                    }

                    newPoint.X = (sumX / countX);
                    newPoint.Y = (sumY / countY);

                    if (el.a == point)
                    {
                        el.a = newPoint;
                    }

                    if (el.b == point)
                    {
                        el.b = newPoint;
                    }

                    if (el.c == point)
                    {
                        el.c = newPoint;
                    }

                }              
            }

            Graphics.Clear(Color.White);
            foreach (var e in Matrix)
            {
                Graphics.DrawLine(pen, e.a, e.b);
                Graphics.DrawLine(pen, e.b, e.c);
                Graphics.DrawLine(pen, e.c, e.a);
            }
        }

        private void Save(List<Element> Elementslist)
        {

            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter("C:\\Users\\Piotrek\\Desktop\\Results\\QuadTreeElements.txt"))
            {
                int o = 0;
                foreach (Element E in Elementslist)
                {
                    file.WriteLine("Element {0}: a={1} b={2} c={3}\n", o, E.a, E.b, E.c);
                    o++;
                }
            }
        }

        private void TestDraw()
        {

            Graphics.Clear(Color.White);
            foreach (var e in Matrix)
            {
                Graphics.DrawLine(pen, e.a, e.b);
                Graphics.DrawLine(pen, e.b, e.c);
                Graphics.DrawLine(pen, e.c, e.a);
            }
        }



        #region nomatter
        #region ExpandTree
        //versja 3
        //public void ExpandTree(List<Element> Elemente)
        //{
        //    List<Element>[] childs = new List<Element>[4];
        //    Node mother = new Node(null, Elemente);

        //    while (Elemente.Count>1)
        //    {
        //        childs = DivideTree(Elemente);

        //        Node n1 = mother.addChild(childs[0]);
        //        Node n2 = mother.addChild(childs[1]);
        //        Node n3 = mother.addChild(childs[2]);
        //        Node n4 = mother.addChild(childs[3]);



        //        //List<Node> ChildsToDo = new List<Node>();

        //        //if (CheckChild(childs[0]) == 1)
        //        //{
        //        //    ChildsToDo.Add(n1);
        //        //    DrawLines(childs[0]);
        //        //}
        //        //if (CheckChild(childs[0]) == 1)
        //        //{
        //        //    ChildsToDo.Add(n2);
        //        //    DrawLines(childs[1]);
        //        //}
        //        //if (CheckChild(childs[0]) == 1)
        //        //{
        //        //    ChildsToDo.Add(n3);
        //        //    DrawLines(childs[2]);
        //        //}
        //        //if (CheckChild(childs[0]) == 1)
        //        //{
        //        //    ChildsToDo.Add(n4);
        //        //    DrawLines(childs[3]);
        //        //}



        //        if (CheckChild(childs[0]) == 1)
        //        {
        //            ExpandTree(childs[0]);
        //        }
        //        if (CheckChild(childs[0]) == 1)
        //        {
        //            ExpandTree(childs[1]);
        //        }
        //        if (CheckChild(childs[0]) == 1)
        //        {
        //            ExpandTree(childs[2]);
        //        }
        //        if (CheckChild(childs[0]) == 1)
        //        {
        //            ExpandTree(childs[3]);
        //        }


        //    }

        //    //k++;

        //}
        #endregion
        #region mothernode

        //public void GenerateNode(Node node )
        //{
        //    while (node.List.Count > 1)
        //    {
        //        node.key = key;
        //        int maxX = 0, maxY = 0, minX = pictureBox1.Size.Width, minY = pictureBox1.Size.Height;
        //        foreach (Element e in node.List)
        //        {
        //            if (e.b.X > maxX)
        //            {
        //                maxX = e.b.X;
        //            }

        //            if (e.d.Y > maxY)
        //            {
        //                maxY = e.d.Y;
        //            }

        //            if (e.a.X < minX)
        //            {
        //                maxX = e.a.X;
        //            }

        //            if (e.a.Y > minY)
        //            {
        //                maxY = e.a.Y;
        //            }
        //        }


        //        node.Child1 = new Node();
        //        node.Child2 = new Node();
        //        node.Child3 = new Node();
        //        node.Child4 = new Node();


        //        node.Child1.List = new List<Element>();
        //        node.Child2.List = new List<Element>();
        //        node.Child3.List = new List<Element>();
        //        node.Child4.List = new List<Element>();

        //        foreach (Element e in Elements)
        //        {
        //            if (e.b.X < maxX / 2 && e.d.Y < maxY / 2)
        //            {
        //                node.Child1.List.Add(e);
        //            }
        //        }

        //        foreach (Element e in Elements)
        //        {
        //            if (e.b.X > maxX / 2 && e.d.Y < maxY / 2)
        //            {
        //                node.Child2.List.Add(e);
        //            }
        //        }


        //        foreach (Element e in Elements)
        //        {
        //            if (e.b.X < maxX / 2 && e.d.Y > maxY / 2)
        //            {
        //                node.Child3.List.Add(e);
        //            }
        //        }


        //        foreach (Element e in Elements)
        //        {
        //            if (e.b.X > maxX / 2 && e.d.Y > maxY / 2)
        //            {
        //                node.Child4.List.Add(e);
        //            }
        //        }




        //        if (CheckChild(node.Child1) == 1)
        //        {
        //            GenerateNode(node.Child1);
        //            Graphics.DrawLine(pen, maxX / 2, maxY, maxX / 2, minY);
        //            Graphics.DrawLine(pen, minX, maxY / 2, maxX, minY / 2);
        //            key++;
        //        }

        //        if (CheckChild(node.Child2) == 1)
        //        {
        //            GenerateNode(node.Child2);
        //            key++;
        //        }

        //        if (CheckChild(node.Child3) == 1)
        //        {
        //            GenerateNode(node.Child3);
        //            key++;
        //        }

        //        if (CheckChild(node.Child4) == 1)
        //        {
        //            GenerateNode(node.Child4);
        //            key++;
        //        }

        //    }
        #endregion
        #region Treetry
        //Tree tree = new Tree();
        ////tree.AddParent(Elements);

        //Tree.Child1.List = new List<Element>();
        //foreach (Element e in Elements)
        //{
        //    if (e.b.X < maxX/2 && e.d.Y < maxY/2)
        //    {
        //        Tree.Child1.list.Add(e);
        //    }
        //}
        ////tree.AddChild(Lc1);

        //tree.Child2 = new List<Element>();
        //foreach (Element e in Elements)
        //{
        //    if (e.b.X > maxX / 2 && e.d.Y < maxY / 2)
        //    {
        //        tree.Child2.Add(e);
        //    }
        //}
        ////tree.AddChild(Lc2);

        //tree.Child3 = new List<Element>();
        //foreach (Element e in Elements)
        //{
        //    if (e.b.X < maxX / 2 && e.d.Y > maxY / 2)
        //    {
        //        tree.Child3.Add(e);
        //    }
        //}
        ////tree.AddChild(Lc3);

        //tree.Child4 = new List<Element>();
        //foreach (Element e in Elements)
        //{
        //    if (e.b.X > maxX / 2 && e.d.Y > maxY / 2)
        //    {
        //        tree.Child4.Add(e);
        //    }
        //}
        ////tree.AddChild(Lc4);

        //while (Elements.Count > 1)
        //{
        //    if (CheckChild(tree.Child1) == 1)
        //    {
        //        GenerateTree(tree.Child1);
        //    }

        //    if (CheckChild(tree.Child2) == 1)
        //    {
        //        GenerateTree(tree.Child2);
        //    }

        //    if (CheckChild(tree.Child3) == 1)
        //    {
        //        GenerateTree(tree.Child3);
        //    }

        //    if (CheckChild(tree.Child4) == 1)
        //    {
        //        GenerateTree(tree.Child4);
        //    }
        //}

        //}
        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        #endregion
      
    }
}
