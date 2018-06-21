using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuadTree
{
    public partial class Form1 : Form
    {
        Graphics Graphics;
               
        //List for created triangle elements
        public List<Element> ElementsMatrix;

        //Prepares picturebox to work 
        private SurfaceGenerator _surfaceGenerator;

        //Creates QuadtreeGrid, returns list of obtained grid elements
        private QuadTree _quadTree; 


        public Form1()
        {
            InitializeComponent();
            Graphics = pictureBox.CreateGraphics();
            areaUnitSizeTrackBar.Value = areaUnitSizeTrackBar.Maximum -((areaUnitSizeTrackBar.Maximum - areaUnitSizeTrackBar.Minimum)/2); //Set trackbar pointer at the middle
            areaUnitSizeLabel.Text = "Element size: " + areaUnitSizeTrackBar.Value; // Show trackbar value
        }
       
        //Generate Button
        private void button1_Click(object sender, EventArgs e)
        {
            Graphics.Clear(Color.White); 

            _surfaceGenerator = new SurfaceGenerator(pictureBox,Graphics, areaUnitSizeTrackBar.Value, normalRadioButton.Checked, randomRadioButton.Checked);
            _quadTree = new QuadTree(Graphics);

            List<Element> AreaUnits = _surfaceGenerator.GenerateFigures(); //Surface prepared to work in form of list of elements

            //First node that contains whole area
            List<Node> Mother = new List<Node>();
            Mother.Add(new Node(AreaUnits));

            _quadTree.Recurence(Mother); //Create Quad Tree grid form Node that contains whole area

            ElementsMatrix = _quadTree.getElementsMatrix(); //Get list of created grid elements
        }

        //Show chosen element size
        private void elementSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            areaUnitSizeLabel.Text = "Element size: " + areaUnitSizeTrackBar.Value;
        }

        // File/Save results button
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ElementsMatrix != null)
            {
                Save(ElementsMatrix);
            }
            else
            {
                MessageBox.Show("Nothing to save!", "Error");
                return;
            }
        }
       
        //Save file with list of grid nodes
        private void Save(List<Element> Elementslist)
        {
            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Save file";
            saveFileDialog.Filter = "Txt file|*.txt";
            saveFileDialog.FileName = "QuadTreeElements";
                       
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var file = new StreamWriter(saveFileDialog.FileName))
                {
                    int number = 0;
                    foreach (Element E in Elementslist)
                    {
                        file.WriteLine("Element {0}: a={1} b={2} c={3}\n", number, E.A, E.B, E.C);
                        number++;
                    }
                }
            }
            
        }
        
    }
}
