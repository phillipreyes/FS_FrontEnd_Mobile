using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

using System.Net.Http;
using System.Diagnostics;

namespace Solar.Model
{
    // assume just 1 day graph on screen

    // Struct Point for x val and Y val on graph
    // Idea being define start and endtime, fill w/ data have and empty w/ data don't have

    // First Test is just creating a graph

    // https://blog.xamarin.com/visualize-your-data-with-charts-graphs-and-xamarin-forms/
    public struct GraphPoint
    {
        // keep as datetime or just convert to some #?
        // public float readTime { get; set; }
        public DateTime readTime { get; set; } // time, x axis, 5 minute intervals
        public float irradiancePerc { get; set; } // power, y-axis red curve
        public float specificYieldPerc { get; set; } // weather, y-axis green curve        
    }

    public class GenCalender
    {
        public int plantID { get; set; }
        public PlotModel DataModel { get; set; }

        HttpClient client;
        TokenDTO tokenobj;

        // public GenCalender(/*end point data*/)
        // {
        // List<GraphPoint> dataPoints = createDataPoints(/*endpointData*/);
        // DataModel = CreatePlotModel(dataPoints);
        // }
        public GenCalender (int pid/*, TokenDTO td*/)
        {
            plantID = pid;
            // just temporary for testing, use TryGenCalenderFetch which will call GenerateDataModel
            DataModel = CreatePlotModel();
            client = new HttpClient();
            // tokenobj = td;
        }

        /*
        public async Task<>TryGenCalenderFetch()
        {                                    
            var uri = "https://fsdevweb.azurewebsites.net/Help/Api/GET-api-plantapi-plants-data"
        }
        */

        /*
        private void GenerateDataModel(DateTime start, DateTime end)
        {                           
            if(start.Date == end.Date)
            {
                createDayDataPoints();
            }
            else
            {
                // TODO
            }
        }
        */

        // depend upon data recieved from endpoint        
        private List<GraphPoint> createDayDataPoints(/*object endpoint returns*/)
        {
            return null;
        }
                    
        // use list created in createDataPoints
        private PlotModel CreatePlotModel(/*List<GraphPoint> dataPoints*/)
        {
            var result = new PlotModel
            {
                Title = "OxyPlot"
            };

            // redefine axes later
            // static or depend upon data?

            double minimumXAxis = -1.0;
            // double minimumXAxis = find
            var xaxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = minimumXAxis,
                Maximum = 6
            };

            var yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -1,
                Maximum = 11
            };

            result.Axes.Add(xaxis);
            result.Axes.Add(yaxis);

            var series1 = new LineSeries
            {
                StrokeThickness = 3,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Red,
                MarkerStrokeThickness = 1
            };

            var series2 = new LineSeries
            {
                StrokeThickness = 3,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Green,
                MarkerStrokeThickness = 1
            };

            // Plot points onto the graph
            // auto generate graph from given GraphPoints
            /*
            foreach(GraphPoint g in dataPoints)
            {
                series1.Points.Add(new DataPoint(g.readTime, g.irradiancePerc));
                series2.Points.Add(new DataPoint(g.readTime, g.specificYieldPerc));
            }
            */

            // just dummy data to test that it shows up
            series1.Points.Add(new DataPoint(0.0, 1.0));
            series1.Points.Add(new DataPoint(1.0, 2.0));
            series1.Points.Add(new DataPoint(2.0, 3.0));
            series1.Points.Add(new DataPoint(3.0, 4.0));
            series1.Points.Add(new DataPoint(4.0, 5.0));
            series1.Points.Add(new DataPoint(5.0, 6.0));

            series2.Points.Add(new DataPoint(0.0, 10.0));
            series2.Points.Add(new DataPoint(1.0, 9.0));
            series2.Points.Add(new DataPoint(2.0, 8.0));
            series2.Points.Add(new DataPoint(3.0, 7.0));
            series2.Points.Add(new DataPoint(4.0, 6.0));
            series2.Points.Add(new DataPoint(5.0, 5.0));

            result.Series.Add(series1);
            result.Series.Add(series2);

            return result;
        }

    }
}
