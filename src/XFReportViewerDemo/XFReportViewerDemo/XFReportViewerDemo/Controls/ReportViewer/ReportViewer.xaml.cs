using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFReportViewerDemo.Controls.ReportViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportViewer : ContentView
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        public void LoadReport()
        {
            ViewRoot.Source = CreateWebViewSource();
        }

        public static BindableProperty ReportNameProperty = BindableProperty.Create(
            "ReportName",
            typeof(string),
            typeof(ReportViewer),
            string.Empty,
            propertyChanged: OnReportNameChanged);

        private static void OnReportNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ReportViewer self)
            {
                self.LoadReport();
            }
        }

        public string ReportName
        {
            get => (string) GetValue(ReportNameProperty);
            set => SetValue(ReportNameProperty, value);
        }

        public static BindableProperty BaseUrlProperty = BindableProperty.Create(
            "BaseUrl",
            typeof(string),
            typeof(ReportViewer),
            string.Empty);

        public string BaseUrl
        {
            get => (string)GetValue(BaseUrlProperty);
            set => SetValue(BaseUrlProperty, value);
        }

        public static BindableProperty ServiceUrlProperty = BindableProperty.Create(
            "ServiceUrl",
            typeof(string),
            typeof(ReportViewer),
            "/api/reports");

        public string ServiceUrl
        {
            get => (string)GetValue(ServiceUrlProperty);
            set => SetValue(ServiceUrlProperty, value);
        }

        public static BindableProperty ViewerResourcesUrlProperty = BindableProperty.Create(
            "ViewerResourcesUrl",
            typeof(string),
            typeof(ReportViewer),
            "/ReportViewer/js");

        public string ViewerResourcesUrl
        {
            get => (string)GetValue(ViewerResourcesUrlProperty);
            set => SetValue(ViewerResourcesUrlProperty, value);
        }

        public static BindableProperty KendoVersionProperty = BindableProperty.Create(
            "KendoVersion",
            typeof(string),
            typeof(ReportViewer),
            "2019.1.115");

        /// <summary>
        /// Override the default version of the Kendo CSS
        /// Example replacement - "2019.1.115"
        /// </summary>
        public string KendoVersion
        {
            get => (string)GetValue(KendoVersionProperty);
            set => SetValue(KendoVersionProperty, value);
        }

        private HtmlWebViewSource CreateWebViewSource()
        {
            var source = new HtmlWebViewSource();

            // By default, the ReportViewer will use the locally stored javascript and css.
            // TODO Expose more dependency properties to override the CSS and ReportViewer JavaScript location
            //source.BaseUrl = BaseUrl;

            // TODO Build a serializer for the HTMLReportViewer's properties instead of string interpolation
            source.Html = $@"<!DOCTYPE html>
                            <html xmlns=""http://www.w3.org/1999/xhtml"">
                                <head>
                                    <title>Telerik MVC HTML5 Report Viewer</title>
                                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1, maximum-scale=1"" />

                                    <link href=""https://kendo.cdn.telerik.com/{KendoVersion}/styles/kendo.common.min.css"" rel=""stylesheet"" />
                                    <link href=""https://kendo.cdn.telerik.com/{KendoVersion}/styles/kendo.blueopal.min.css"" rel=""stylesheet"" />

                                    <script src=""https://code.jquery.com/jquery-1.9.1.min.js""></script>
                                    <script src=""{BaseUrl}{ViewerResourcesUrl}/telerikReportViewer.kendo-13.0.19.222.min.js""></script>
                                    <script src=""{BaseUrl}{ViewerResourcesUrl}/telerikReportViewer-13.0.19.222.min.js""></script>

                                    <style>
                                        #reportViewer1 {{
                                            position: absolute;
                                            left: 5px;
                                            right: 5px;
                                            top: 5px;
                                            bottom: 5px;
                                            font-family: 'segoe ui', 'ms sans serif';
                                            overflow: hidden;
                                        }}
                                    </style>
                                </head>

                                <body>
                                    <div id=""reportViewer1"">
                                    </div>

                                    <script>
                                        $(""#reportViewer1"").telerik_ReportViewer({{
                                            serviceUrl: ""{BaseUrl}{ServiceUrl}"",
                                            reportSource: {{ 
                                                report: ""{ReportName}"",
                                                parameters: {{
                                                   CultureID: ""en""
                                                }}
                                            }}
                                        }});
                                    </script>
                                </body>
                            </html>";

            return source;
        }
    }
}