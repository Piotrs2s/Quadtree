using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace QuadTree
{


    public class Element
    {
        //Vertices of element
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }  
        public Point D { get; set; }

        // State of element (colored or not)
        public int Index { get; set; }
        

        public Element(Point a, Point b, Point c, Point d, int index)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
            this.Index = index;            
        }
        public Element(Point a, Point b, Point c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }
        public Element()
        {
           
        }
    }
}
