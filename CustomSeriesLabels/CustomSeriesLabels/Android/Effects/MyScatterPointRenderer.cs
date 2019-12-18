using System;
using Android.Graphics;
using Android.Runtime;
using Com.Telerik.Widget.Chart.Engine.DataPoints;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Pointrenderers;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Scatter;

namespace CustomSeriesLabels.Android.Effects
{
    public class MyScatterPointRenderer : ScatterPointRenderer
    {
        protected MyScatterPointRenderer(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MyScatterPointRenderer(ScatterPointSeries p0) : base(p0)
        {
        }

        public override void RenderPoint(Canvas canvas, DataPoint dataPoint)
        {
            // Determine how big you want the data point's box to be
            double boxWidth = 20;
            double boxHeight = 20;

            // Determine where the point is going to be located on the chart's canvas
            float rectLeft = (float)(dataPoint.CenterX - (boxWidth / 2)); // notice we're using the dataPoint's X center
            float rectTop = (float)(dataPoint.CenterY - (boxHeight / 2)); // notice we're using the dataPoint's Y center
            float rectRight = (float) (rectLeft + boxWidth);
            float rectBottom = (float)(rectTop + boxHeight);

            // Draw the data point
            canvas.DrawRect(
                rectLeft, 
                rectTop, 
                rectRight, 
                rectBottom, 
                new Paint { Color = Color.ParseColor("#F5413F") });
        }
    }
}