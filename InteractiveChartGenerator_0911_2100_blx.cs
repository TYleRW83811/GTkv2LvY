// 代码生成时间: 2025-09-11 21:00:40
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Maps;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace ChartGeneratorApp
{
    public class InteractiveChartGenerator : ContentPage
    {
        private SKCanvasView canvasView;
        private SKChart chart;
        private List<float> dataPoints;

        public InteractiveChartGenerator()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Initialize the chart canvas
            canvasView = new SKCanvasView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            // Subscribe to the paint surface event
            canvasView.PaintSurface += OnCanvasViewPaintSurface;

            // Initialize the chart
            chart = new SKChart
            {
                BackgroundColor = SKColors.Transparent,
                BorderStroke = SKColors.Black
            };

            dataPoints = new List<float>();

            // Layout configuration
            Content = new StackLayout
            {
                Children =
                {
                    canvasView
                }
            };

            // Add button to add data points to the chart
            Button addButton = new Button
            {
                Text = "Add Data Point",
                HorizontalOptions = LayoutOptions.Center
            };

            addButton.Clicked += (sender, e) => AddDataPoint();
            Content.Children.Add(addButton);
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            // Clear the canvas
            canvas.Clear(SKColors.Transparent);
            // Draw the chart
            chart.Draw(canvas, args.Info.Rect);
        }

        private void AddDataPoint()
        {
            try
            {
                // Randomly generate a data point
                float randomValue = (float)(new Random().NextDouble() * 100);
                // Add the data point
                dataPoints.Add(randomValue);
                // Update the chart
                chart.Series.Clear();
                chart.Series.Add(new SKPointSeries
                {
                    Points = dataPoints.ToArray()
                });
            }
            catch (Exception ex)
            {
                // Error handling
                DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
