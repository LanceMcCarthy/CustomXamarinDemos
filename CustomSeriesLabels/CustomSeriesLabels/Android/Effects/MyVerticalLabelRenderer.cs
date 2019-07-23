using System;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Com.Telerik.Android.Common.Math;
using Com.Telerik.Widget.Chart.Engine.DataPoints;
using Com.Telerik.Widget.Chart.Engine.ElementTree;
using Com.Telerik.Widget.Chart.Visualization.CartesianChart.Series.Categorical;
using Com.Telerik.Widget.Chart.Visualization.Common;
using Telerik.XamarinForms.Common.Android;
using Color = Android.Graphics.Color; // Disambiguate .NET or Xamarin.Forms Color

namespace CustomSeriesLabels.Android.Effects
{
    public class MyVerticalLabelRenderer : CategoricalSeriesLabelRenderer
    {
        private readonly TextPaint paint = new TextPaint();
        private readonly Paint strokePaint = new Paint();
        private readonly Paint fillPaint = new Paint();
        private string labelFormat = "{0}";
        private float labelMargin = 10.0f;
        private float labelPadding = 20.0f;

        public MyVerticalLabelRenderer(ChartSeries p0) : base(p0)
        {
            strokePaint.SetStyle(Paint.Style.Stroke);
            strokePaint.Color = Color.White;
            strokePaint.StrokeWidth = 2;

            fillPaint.Color = Color.ParseColor("#F5413F");
            paint.TextSize = 35.0f;
            paint.Color = Color.White;
        }

        protected override string GetLabelText(DataPoint p0)
        {
            var convertibleObject = (ConvertibleObject<object>)p0.DataItem;
            var categoricalData = (CustomSeriesLabels.Portable.Models.CategoricalData)convertibleObject.Instance;
            return categoricalData.Category.ToString();
        }

        public override void RenderLabel(Canvas canvas, ChartNode relatedLabelNode)
        {
            var dataPoint = relatedLabelNode.JavaCast<CategoricalDataPoint>();

            RadRect dataPointSlot = dataPoint.LayoutSlot;
            double val = dataPoint.Value;
            string labelText = string.Format(labelFormat, (int)val);

            StaticLayout textInfo = CreateTextInfo(labelText, dataPoint);

            RenderLabel(canvas, dataPointSlot, labelText, textInfo);
        }

        private StaticLayout CreateTextInfo(string labelText, CategoricalDataPoint dataPoint)
        {
            return new StaticLayout(labelText,
                0,
                labelText.Length,
                paint,
                (int)Math.Round((float)dataPoint.LayoutSlot.Width),
                Layout.Alignment.AlignCenter,
                1.0f,
                1.0f,
                false);
        }

        private void RenderLabel(Canvas canvas, RadRect dataPointSlot, string labelText, StaticLayout textBounds)
        {
            // TODO - Rotate the canvas on the center before drawing label
            //canvas.Rotate(90, (float)canvas.Width / 2, (float)canvas.Height / 2);

            RectF labelBounds = new RectF();
            float height = textBounds.Height + labelPadding * 2;

            // Calculate the middle of the bar
            var barY = (float)dataPointSlot.GetY();
            var barHeight = canvas.Height - barY;
            var middlePoint = barHeight / 2;
            var labelY = canvas.Height - middlePoint;

            // Calculate left and right padding around the label
            var barX = (float)dataPointSlot.GetX();
            var barRight = (float)dataPointSlot.Right;
            var barWidth = barRight - barX;
            var padding = barWidth / 10;

            // Calculate the Rect bounds
            var rectLeft = barX + padding;
            var rectTop = labelY;
            var rectRight = barRight - padding;
            var rectBottom = rectTop + height;

            // Set's the label's position on the Chart's canvas
            labelBounds.Set(rectLeft, rectTop, rectRight, rectBottom);

            // Draws Rect's fill and stroke color
            canvas.DrawRect(labelBounds.Left, labelBounds.Top, labelBounds.Right, labelBounds.Bottom, fillPaint);
            canvas.DrawRect(labelBounds.Left, labelBounds.Top, labelBounds.Right, labelBounds.Bottom, strokePaint);

            // Draws the Text on the canvas
            canvas.DrawText(
                labelText,
                (float)dataPointSlot.GetX() + (float)(dataPointSlot.Width / 2.0) - textBounds.GetLineWidth(0) / 2.0f,
                labelBounds.CenterY() + textBounds.GetLineBottom(0) - textBounds.GetLineBaseline(0),
                paint);

            // TODO Undo rotation after drawing label
            //canvas.Rotate(-90, (float)canvas.Width / 2, (float)canvas.Height / 2);
        }
    }
}