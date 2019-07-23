using System;
using TelerikUI;
using UIKit;

namespace CustomSeriesLabels.iOS.Effects
{
    public class MyChartDelegate : TKChartDelegate
    {
        public override TKChartPointLabel LabelForDataPoint(TKChart chart, TKChartData dataPoint, string propertyName, TKChartSeries series, nuint dataIndex)
        {
            // On iOS, a vertical bar chart is a "Column" series
            if (series is TKChartColumnSeries)
            {
                // If it's a BarSeries use vertical label
                TKChartDataPoint point = (TKChartDataPoint)dataPoint;
                series.Style.ShapeMode = TKChartSeriesStyleShapeMode.AlwaysShow;

                series.Style.PointLabelStyle.LayoutMode = TKChartPointLabelLayoutMode.Manual;
                series.Style.PointLabelStyle.Fill = new TKSolidFill(UIColor.White);
                series.Style.PointLabelStyle.Insets = new UIEdgeInsets(-1, -3, -1, -3);

                // Position the label at your preferred position
                series.Style.PointLabelStyle.LabelOffset = new UIOffset(0, 200);

                // Set the text properties
                series.Style.PointLabelStyle.TextOrientation = TKChartPointLabelOrientation.Vertical;
                series.Style.PointLabelStyle.TextAlignment = UITextAlignment.Center;
                series.Style.PointLabelStyle.TextColor = UIColor.Blue;
                series.Style.PointLabelStyle.TextHidden = false;

                return new TKChartPointLabel(point, series, point.DataYValue.ToString());
            }
            else
            {
                // use horizontal label for everything else
                TKChartDataPoint point = (TKChartDataPoint)dataPoint;

                series.Style.PointShape = new TKPredefinedShape(TKShapeType.Circle, new System.Drawing.SizeF(8, 8));
                series.Style.PointLabelStyle.TextHidden = false;
                series.Style.PointLabelStyle.LabelOffset = new UIOffset(0, -24);
                series.Style.PointLabelStyle.Insets = new UIEdgeInsets(-1, -5, -1, -5);
                series.Style.PointLabelStyle.LayoutMode = TKChartPointLabelLayoutMode.Manual;
                series.Style.PointLabelStyle.Font = UIFont.SystemFontOfSize(10);
                series.Style.PointLabelStyle.TextAlignment = UITextAlignment.Center;
                series.Style.PointLabelStyle.Fill = new TKSolidFill(new UIColor((float)(108 / 255.0), (float)(181 / 255.0), (float)(250 / 255.0), (float)1.0));
                series.Style.PointLabelStyle.ClipMode = TKChartPointLabelClipMode.Hidden;
                series.Style.PointLabelStyle.TextOrientation = TKChartPointLabelOrientation.Horizontal;
                series.Style.PointLabelStyle.TextColor = UIColor.Red;
                series.Style.ShapeMode = TKChartSeriesStyleShapeMode.AlwaysShow;


                return new TKChartPointLabel(point, series, $"{point.DataName}, {point.DataYValue}");
            }
        }
    }
}