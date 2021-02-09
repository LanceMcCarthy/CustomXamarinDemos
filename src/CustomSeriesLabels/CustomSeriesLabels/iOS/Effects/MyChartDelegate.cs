using System;
using TelerikUI;
using UIKit;

namespace CustomSeriesLabels.iOS.Effects
{
    public class MyChartDelegate : TKChartDelegate
    {
        private readonly bool rotateLabels;

        public MyChartDelegate(bool rotateLabels)
        {
            this.rotateLabels = rotateLabels;
        }

        public override TKChartPointLabel LabelForDataPoint(TKChart chart, TKChartData dataPoint, string propertyName, TKChartSeries series, nuint dataIndex)
        {
            // If it's a BarSeries use vertical label
            if (series is TKChartColumnSeries)
            {
                if (this.rotateLabels)
                {
                    return CreateVerticalLabel(series, (TKChartDataPoint)dataPoint);
                }
                else
                {
                    return CreateHorizontalLabel(series, (TKChartDataPoint)dataPoint);
                }
            }

            if (series is TKChartDonutSeries)
            {
                return CreateDonutLabel(series, (TKChartDataPoint)dataPoint);
            }

            if (series is TKChartPieSeries)
            {
                return CreatePieLabel(series, (TKChartDataPoint)dataPoint);
            }

            // use horizontal label for everything else
            return CreateHorizontalLabel(series, (TKChartDataPoint)dataPoint);
        }

        private TKChartPointLabel CreateDonutLabel(TKChartSeries series, TKChartDataPoint dataPoint)
        {
            series.Style.PointShape = new TKPredefinedShape(TKShapeType.Circle, new System.Drawing.SizeF(8, 8));
            series.Style.PointLabelStyle.TextHidden = false;
            series.Style.PointLabelStyle.LabelOffset = new UIOffset(0, -24);
            series.Style.PointLabelStyle.Insets = new UIEdgeInsets(-1, -5, -1, -5);
            series.Style.PointLabelStyle.LayoutMode = TKChartPointLabelLayoutMode.Manual;
            series.Style.PointLabelStyle.Font = UIFont.SystemFontOfSize(10);
            series.Style.PointLabelStyle.TextAlignment = UITextAlignment.Center;
            series.Style.PointLabelStyle.Fill = new TKSolidFill(new UIColor((float)(102 / 255.0), (float)(255 / 255.0), (float)(178 / 255.0), (float)1.0));
            series.Style.PointLabelStyle.ClipMode = TKChartPointLabelClipMode.Hidden;
            series.Style.PointLabelStyle.TextOrientation = TKChartPointLabelOrientation.Horizontal;
            series.Style.PointLabelStyle.TextColor = UIColor.Red;
            series.Style.ShapeMode = TKChartSeriesStyleShapeMode.AlwaysShow;

            var text = $"Category: {dataPoint.DataName}\r\nValue: {dataPoint.DataXValue}";

            return new TKChartPointLabel(dataPoint, series, text);
        }

        private TKChartPointLabel CreatePieLabel(TKChartSeries series, TKChartDataPoint dataPoint)
        {
            series.Style.PointShape = new TKPredefinedShape(TKShapeType.Circle, new System.Drawing.SizeF(8, 8));
            series.Style.PointLabelStyle.TextHidden = false;
            series.Style.PointLabelStyle.LabelOffset = new UIOffset(0, -24);
            series.Style.PointLabelStyle.Insets = new UIEdgeInsets(-1, -5, -1, -5);
            series.Style.PointLabelStyle.LayoutMode = TKChartPointLabelLayoutMode.Manual;
            series.Style.PointLabelStyle.Font = UIFont.SystemFontOfSize(10);
            series.Style.PointLabelStyle.TextAlignment = UITextAlignment.Center;
            series.Style.PointLabelStyle.Fill = new TKSolidFill(new UIColor((float)(102 / 255.0), (float)(255 / 255.0), (float)(178 / 255.0), (float)1.0));
            series.Style.PointLabelStyle.ClipMode = TKChartPointLabelClipMode.Hidden;
            series.Style.PointLabelStyle.TextOrientation = TKChartPointLabelOrientation.Horizontal;
            series.Style.PointLabelStyle.TextColor = UIColor.Red;
            series.Style.ShapeMode = TKChartSeriesStyleShapeMode.AlwaysShow;

            var text = $"{dataPoint.DataName}: {dataPoint.DataXValue}";

            return new TKChartPointLabel(dataPoint, series, text);
        }

        private TKChartPointLabel CreateVerticalLabel(TKChartSeries series, TKChartDataPoint dataPoint)
        {
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

            return new TKChartPointLabel(dataPoint, series, dataPoint.DataYValue.ToString());
        }

        private TKChartPointLabel CreateHorizontalLabel(TKChartSeries series, TKChartDataPoint dataPoint)
        {
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

            return new TKChartPointLabel(dataPoint, series, $"{dataPoint.DataName}, {dataPoint.DataYValue}");
        }
    }
}