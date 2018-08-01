using System;
using Telerik.XamarinForms.ChartRenderer.iOS;
using TelerikUI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MyCompany")]
[assembly: ExportEffect(typeof(CustomSeriesLabels.iOS.Effects.CustomLabelEffect), "CustomLabelEffect")]
namespace CustomSeriesLabels.iOS.Effects
{
    public class CustomLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            TKExtendedChart nativeChart = (TKExtendedChart)this.Control;

            nativeChart.Delegate = new MyChartDelegate();
        }

        protected override void OnDetached()
        {

        }
    }

    public class MyChartDelegate : TKChartDelegate
    {
        public override TKChartPointLabel LabelForDataPoint(TKChart chart, TKChartData dataPoint, string propertyName, TKChartSeries series, nuint dataIndex)
        {
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

            return new TKChartPointLabel(point, series, String.Format("{0}", point.DataYValue));
        }
    }
}