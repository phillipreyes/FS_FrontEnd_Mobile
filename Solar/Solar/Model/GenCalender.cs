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
    /*
var client2 = new HttpClient();
            var url = "http://fsdevweb.azurewebsites.net/api/plantapi/plants/data?ID=4141&StartDate=9/1/2017&EndDate=9/5/2017";
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.access_token);
            var response2 = client2.GetAsync(url).Result;

            var ListOfDays = JsonConvert.DeserializeObject<List<List<PlantDataViewModel>>>(response2.Content.ReadAsStringAsync().Result);
            foreach (List<PlantDataViewModel> DataObjectList in ListOfDays)
            {
                foreach (PlantDataViewModel DataObject in DataObjectList)
                {
                    Console.WriteLine("\nSiteAssetID: " + DataObject.SiteAssetID + "\nReadTime: " + DataObject.ReadTime + "\nPower_KW: " + DataObject.Power_kW + "\nSpecificDataObjectieldPerc: " + DataObject.SpecificYieldPerc
                        + "\nIrradiancePOAWm2: " + DataObject.IrradiancePOAWm2 + "\nIrradiancePerc: " + DataObject.IrradiancePerc + "\nTemperatureC: " + DataObject.TemperatureC + "\nETLInsertLogID: "
                        + DataObject.ETLInsertLogID + "\nETLInsertTimestamp: " + DataObject.ETLInsertTimestamp);
                }

            }     
    */

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
        public double? irradiancePerc { get; set; } // power, y-axis red curve
        public double? specificYieldPerc { get; set; } // weather, y-axis green curve
    }

    public class GenCalender
    {
        public PlantInfo plant { get; set; }
        public PlotModel DataModel { get; set; }

        HttpClient client;
        TokenDTO tokenobj;

        // public GenCalender(/*end point data*/)
        // {
        // List<GraphPoint> dataPoints = createDataPoints(/*endpointData*/);
        // DataModel = CreatePlotModel(dataPoints);
        // }
        public GenCalender (PlantInfo pi,TokenDTO td)
        {
            // plantID = pid;
            plant= pi;
            // just temporary for testing, use TryGenCalenderFetch which will call GenerateDataModel
            GetPlantData PlantData = new GetPlantData(td, plant.Id);
            List<List<PlantDataViewModel>> rawData = PlantData.GetGenerationCalendarData();
            List<GraphPoint> points = createDayDataPoints(rawData);
            DataModel = CreatePlotModel(points);
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
        private List<GraphPoint> createDayDataPoints(List<List<PlantDataViewModel>> rawData)
        {
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
                    
        // use list created in createDataPoints
        private PlotModel CreatePlotModel(List<GraphPoint> dataPoints)
        {
            var result = new PlotModel
            {
                // Title = "OxyPlot"
                Title = plant.PlantName + " 9/1/2017"
            };

            // redefine axes later
            // static or depend upon data?

            // Define X Axis as 0-n, n being # of graphPoints
            // Define Y Axis as 0-m, m being largest y value found
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

            double minimumXAxis = -0.1;
            // double minimumXAxis = find
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

            result.Axes.Add(xaxis);
            result.Axes.Add(yaxis);

            var series1 = new LineSeries
            {
                StrokeThickness = 3,
                // MarkerType = MarkerType.Circle,
                // MarkerSize = 4,
                MarkerType = MarkerType.None,
                MarkerSize = 4,

                MarkerStroke = OxyColors.Orange,
                // MarkerStroke = OxyColors.Green,
                MarkerStrokeThickness = 1
            };

            var series2 = new LineSeries
            {
                StrokeThickness = 3,
                MarkerType = MarkerType.None,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Green,
                // MarkerStroke = OxyColors.Orange,
                MarkerStrokeThickness = 1
            };

            // Plot points onto the graph
            // auto generate graph from given GraphPoints
            int i = 0;
            foreach(GraphPoint g in dataPoints)
            {
                series1.Points.Add(new DataPoint(i, (double)g.irradiancePerc));
                series2.Points.Add(new DataPoint(i, (double)g.specificYieldPerc));
                i++;
            }            

            // just dummy data to test that it shows up
            /*
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
            */

            result.Series.Add(series1);
            result.Series.Add(series2);                        

            return result;
        }

    }
}
