using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadTree
{
    class SurfaceGenerator
    {

        public bool NormalMode { get; set; }
        public bool RandomMode { get; set; }

        private Graphics _graphics { get; set; }
        private PictureBox _pictureBox { get; set; }
        private int _areaUnitSize { get; set; }
        private Random _rand { get; set;}

        public SurfaceGenerator(PictureBox pictureBox, Graphics graphics, int areaUnitSize, bool normalMode, bool randomMode)
        {
            _graphics = graphics;
            _pictureBox = pictureBox;
            _areaUnitSize = areaUnitSize;
            NormalMode = normalMode;
            RandomMode = randomMode;
            _rand = new Random();
        }


        //Generate work area and red figures to localize
        public List<Element> GenerateFigures()
        {
            var AreaPoints = new List<Point>(); //List of points that work area consist of
            int size = _pictureBox.Width - 1;
            //int AreaUnitSize = areaUnitSizeTrackBar.Value;



            //Create area consisting of points distanted by min. unit size
            for (int i = 0; i < size; i = i + _areaUnitSize)// create row
            {
                Point px = new Point(i, 0); //first in column
                AreaPoints.Add(px);
                for (int j = _areaUnitSize; j < size; j = j + _areaUnitSize) //create column
                {
                    Point py = new Point(i, j);
                    AreaPoints.Add(py);
                }
            }

            //Group points in area to squares with size of unit
            var AreaUnits = new List<Element>();
            foreach (Point p in AreaPoints)
            {
                Point a = new Point(p.X, p.Y);
                Point b = new Point(p.X + _areaUnitSize, p.Y);
                Point c = new Point(p.X + _areaUnitSize, p.Y + _areaUnitSize);
                Point d = new Point(p.X, p.Y + _areaUnitSize);
                int index = 0;
                Element element = new Element(a, b, c, d, index);
                AreaUnits.Add(element);
            }



            //Create squares
            foreach (Element e in AreaUnits)
            {
              
                //Normal
                if (NormalMode)
                {
                    //Draw squares in scepyfied coordinates
                    if (e.A.X >= 10 && e.B.X <= 60 && e.A.Y >= 10 && e.D.Y <= 60)
                    {
                        e.Index = 1;
                        ColorElement(e, new SolidBrush(Color.Red));
                    }

                    if (e.A.X > 350 && e.B.X < 400 && e.A.Y > 350 && e.D.Y < 400)
                    {
                        e.Index = 1;
                        ColorElement(e, new SolidBrush(Color.Red));
                    }
                }


                //Draw random figures in random places
                if (RandomMode)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (e.A.X > _rand.Next(0, _pictureBox.Width - 1) && e.B.X < _rand.Next(0, _pictureBox.Width - 1) && e.A.Y > _rand.Next(0, _pictureBox.Height - 1) && e.D.Y < _rand.Next(0, _pictureBox.Height - 1))
                        {
                            e.Index = 1;
                            ColorElement(e, new SolidBrush(Color.Red));
                        }
                    }
                }

               


            }
            return AreaUnits;
        }

        //Color unit
        public void ColorElement(Element element, Brush brush)
        {
            //Get all pixels that belong to unit
            var Points = new List<Point>();
            for (int i = 0; i < _pictureBox.Width - 1; i++)
            {
                Point px = new Point(i, 0);
                Points.Add(px);
                for (int j = 1; j < _pictureBox.Height - 1; j++)
                {
                    Point py = new Point(i, j);
                    Points.Add(py);
                }
            }

            //Color all pixels that belong to unit
            foreach (Point P in Points)
            {
                if (P.X >= element.A.X && P.X <= element.B.X && P.Y >= element.A.Y && P.Y <= element.D.Y)
                {
                    _graphics.FillRectangle(brush, P.X, P.Y, 1, 1);
                }
            }
        }

    }
}
