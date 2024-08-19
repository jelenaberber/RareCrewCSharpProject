using OxyPlot;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using RareCrewC_Project.Models;

namespace RareCrewC_Project.Services
{
    public class PieChartService
    {
        public void GeneratePieChart(List<EmployeeWorkData> data, string filePath)
        {
            var plotModel = new PlotModel { Title = "Employee work hours for month" };

            var pieSeries = new PieSeries
            {
                StrokeThickness = 1,
                Diameter = 0.5,
                InsideLabelPosition = 0.7,
                AngleSpan = 360,
                StartAngle = 0
            };

            foreach (var item in data)
            {
                pieSeries.Slices.Add(new PieSlice(item.Name, item.TotalTimeWorked));
            }

            plotModel.Series.Add(pieSeries);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var exporter = new PngExporter { Width = 1000, Height = 800 };
                exporter.Export(plotModel, stream);
            }
        }
    }
}
