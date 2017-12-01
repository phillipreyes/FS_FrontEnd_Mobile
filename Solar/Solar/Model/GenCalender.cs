using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Solar.Controller;

using System.Net.Http;
using System.Diagnostics;

namespace Solar.Model
{
    // Creates a generation calendar for a single day for a single plant

    // Struct representing the Irradiance and Specific Yield Percentaes for a paticular readtime
    public struct GraphPoint
    {
        public DateTime readTime { get; set; } // time, x axis, 5 minute intervals
        public double? irradiancePerc { get; set; } // power, y-axis red curve
        public double? specificYieldPerc { get; set; } // weather, y-axis green curve
    }

    public class GenCalender
    {
        public PlantInfo plant { get; set; }
        public PlotModel DataModel { get; set; }

        HttpClient client;
        TokenDTO tokenobj;

        public GenCalender (PlantInfo pi,TokenDTO td)
        {            
            plant= pi;
            // Call web services to retrieve raw Generation Calendar data
            // Currently GetPlantData always fetches data from September 1st to September 5th
            GetPlantData PlantData = new GetPlantData(td, plant.Id);            
            List<List<PlantDataViewModel>> rawData = PlantData.GetGenerationCalendarData();
            // turn data into a list of graph points            
            List<GraphPoint> points = createDayDataPoints(rawData);
            // Create the actual graph with the points generated above
            DataModel = CreatePlotModel(points);
            client = new HttpClient();
        }

        // translate data into graph points
        private List<GraphPoint> createDayDataPoints(List<List<PlantDataViewModel>> rawData)
        {
            // Currently reateDayDataPoints always creates graph from 1st day of raw data's day range
            List<GraphPoint> result = new List<GraphPoint>();            
            foreach(PlantDataViewModel pdvm in rawData.ElementAt(0))
            {
                GraphPoint gp = new GraphPoint();
                gp.irradiancePerc = pdvm.IrradiancePerc;
                gp.readTime = pdvm.ReadTime;
                gp.specificYieldPerc = pdvm.SpecificYieldPerc;
                result.Add(gp);
            }

            return result;
        }
                    
        // translate graph points into actual graph
        private PlotModel CreatePlotModel(List<GraphPoint> dataPoints)
        {
            // set graph name
            var result = new PlotModel
            {
                // Title = "OxyPlot"
                Title = plant.PlantName + " 9/1/2017"
            };

            // Define X Axis as 0-n, n being # of graphPoints
            // Define Y Axis as 0-m, m being largest irradiance/specifc yield found in the graph
            double? max = 0;
            double numPoints = 0;
            foreach(GraphPoint g in dataPoints)
            {
                double? syp = g.specificYieldPerc;
                double? irr = g.irradiancePerc;
                if(syp >= irr)
                {
                    if(syp > max)
                    {
                        max = syp;
                    }
                }
                else
                {
                    if(irr > max)
                    {
                        max = irr;
                    }
                }
                numPoints++;
            }

            // Note 0.1 padding for grah axes from largest / smallest data points
            // create axes
            double minimumXAxis = -0.1;            
            var xaxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = minimumXAxis,
                Maximum = numPoints + 0.1
            };

            var yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -0.1,
                Maximum = (double)max + -0.1
            };

            // Adds axes to graph
            result.Axes.Add(xaxis);
            result.Axes.Add(yaxis);

            // Create the 2 separate Lines
            // 1 being orange for irradiace and the other being green for specific yield
            var series1 = new LineSeries
            {
                StrokeThickness = 3,
                MarkerType = MarkerType.None,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Orange,
                MarkerStrokeThickness = 1
            };

            var series2 = new LineSeries
            {
                StrokeThickness = 3,
                MarkerType = MarkerType.None,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Green,
                MarkerStrokeThickness = 1
            };

            // Plot points onto the graph by adding to the 2 lines created above
            // auto generate graph from given GraphPoints
            int i = 0;
            foreach(GraphPoint g in dataPoints)
            {
                // xaxis value is nth graph point (not readtime)
                // yaxis is percentages for irradiance and specific yield
                series1.Points.Add(new DataPoint(i, (double)g.irradiancePerc));
                series2.Points.Add(new DataPoint(i, (double)g.specificYieldPerc));
                i++;
            }            

            // Add lines to the graph
            result.Series.Add(series1);
            result.Series.Add(series2);                        

            return result;
        }

    }
}
