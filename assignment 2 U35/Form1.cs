﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace assignment_2_U35
{
    public partial class Form1 : Form
    {

        class row
        {
            public double time;
            public double Velocity;
            public double acceleration;
            public double VelocityDerivative;
            public double AltitudeDerivative;
            public double Altitude;
        }

        List<row> table = new List<row>();
        private string imageFileName;

        void tableSort()
        {
            table = table.OrderBy(x => x.time).ToList();
        }

        void derivative()
        {
            for (int i = 1; i < table.Count; i++)

            {
                double dA = table[i].Altitude - table[i - 1].Altitude;
                double dt = table[i].time - table[i - 1].time;
                table[i].AltitudeDerivative = dA / dt;
                table[i].Velocity = table[i].AltitudeDerivative;
            }
        }

        void secondderivative()
        {

            for (int i = 1; i < table.Count; i++)
            {
                double dA = table[i].Velocity - table[i - 1].Velocity;
                double dt = table[i].time - table[i - 1].time;
                table[i].VelocityDerivative = dA / dt;
            }



        }



        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "CSV Files|*.csv";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {

                        string line = sr.ReadLine();
                        while (!sr.EndOfStream)
                        {
                            table.Add(new row());
                            string[] l = sr.ReadLine().Split(',');
                            table.Last().time = double.Parse(l[0]);
                            table.Last().Altitude = double.Parse(l[1]);

                        }

                    }

                }
                catch
                {

                }
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void saveGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.SaveImage(imageFileName, ChartImageFormat.Png);
        }

        private void altitudeToolStripMenuItem_Click(object sender, EventArgs e)
        {


            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series1 = new Series
            {
                Name = "Points",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2

            };

            chart1.Series.Add(series1);
            for (int i = 0; i < table.Count; i++)
            {
                series1.Points.AddXY(table[i].time, table[i].AltitudeDerivative);
            }

            chart1.ChartAreas[0].AxisX.Title = "time / s";
            chart1.ChartAreas[0].AxisY.Title = "Altitude / V";
            chart1.ChartAreas[0].RecalculateAxesScale();
        }



        private void accelerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            secondderivative();

            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Series series1 = new Series
            {
                Name = "Points",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Spline,
                BorderWidth = 2

            };

            chart1.Series.Add(series1);
            for (int i = 0; i < table.Count; i++)
            {
                series1.Points.AddXY(table[i].time, table[i].VelocityDerivative);
            }

            chart1.ChartAreas[0].AxisX.Title = "time / s";
            chart1.ChartAreas[0].AxisY.Title = "Velocity / V";
            chart1.ChartAreas[0].RecalculateAxesScale();


        }
        private void velocityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int i = 1; i < table.Count; i++)

            {
                double dA = table[i].Altitude - table[i - 1].Altitude;
                double dt = table[i].time - table[i - 1].time;
                table[i].AltitudeDerivative = dA / dt;
                table[i].Velocity = table[i].AltitudeDerivative;
            }


        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
    
}
