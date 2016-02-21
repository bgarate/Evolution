using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Evolution.Samples.TravelingSalesmanProblem
{
    public partial class FrmTsp : Form
    {
        private const int CITY_RECTANGLE_SIZE = 3;
        private Graphics graphics;

        public FrmTsp()
        {
            InitializeComponent();
            graphics = pctMap.CreateGraphics();
        }

        private Tsp Tsp { get; set; }

        private void btnGenerateMap_Click(object sender, EventArgs e)
        {
            GenerateEngine();
        }

        private void GenerateEngine()
        {
            Tsp = new Tsp((int) nbrNumberOfCities.Value,
                chkCircular.Checked ? Tsp.PositionSelection.Circular : Tsp.PositionSelection.Random);

            DrawMap();
        }

        private void DrawCities()
        {
            Size size = pctMap.Size;
            foreach (City city in Tsp.Cities)
            {
                Point position = new Point((int) (city.Position.X*size.Width - CITY_RECTANGLE_SIZE/2),
                    (int) (city.Position.Y*size.Height - CITY_RECTANGLE_SIZE/2));
                graphics.DrawRectangle(Pens.Red, position.X, position.Y, CITY_RECTANGLE_SIZE, CITY_RECTANGLE_SIZE);
            }
        }

        private void DrawPath()
        {
            List<City> cities = Tsp.GetPath();
            for (int i = 1; i < cities.Count; i++)
            {
                City city = cities[i];
                City previousCity = cities[i - 1];

                Size size = pctMap.Size;

                Point cityPos = new Point((int) (city.Position.X*size.Width), (int) (city.Position.Y*size.Height));
                Point previousCityPos = new Point((int) (previousCity.Position.X*size.Width),
                    (int) (previousCity.Position.Y*size.Height));

                graphics.DrawLine(Pens.GreenYellow, cityPos, previousCityPos);
            }
        }


        private void FrmTsp_Resize(object sender, EventArgs e)
        {
            graphics = pctMap.CreateGraphics();
            DrawMap();
        }

        private void FrmTsp_Load(object sender, EventArgs e)
        {
        }

        private void btnStepForward_Click(object sender, EventArgs e)
        {
            if (Tsp == null)
                GenerateEngine();
            Tsp.Evolve();
            DrawMap();
        }

        private void DrawMap()
        {
            graphics.Clear(Color.White);
            DrawPath();
            DrawCities();
            lblGeneration.Text = "Generation: " + (Tsp?.Generation ?? 0);
            lblBestGeneration.Text = "Best Generation: " + (Tsp?.BestFitnessGeneration ?? 0);
            lblBestFitness.Text = "Best Fitness: " + (Tsp?.BestFitness ?? 0).ToString("00.00");
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (Tsp == null)
                GenerateEngine();
            int x = 0;
            while (Tsp.Evolve())
            {
                x++;
                if (x%100 == 0)
                {
                    DrawMap();
                    Application.DoEvents();
                }
            }
            DrawMap();
        }

        private void lblGeneration_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}