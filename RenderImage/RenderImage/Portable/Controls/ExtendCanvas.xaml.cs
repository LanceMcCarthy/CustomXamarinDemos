using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RenderImage.Portable.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendCanvas : DuoPage
    {
        public ExtendCanvas()
        {
            InitializeComponent();
            searchBar.SearchButtonPressed += SearchBar_SearchButtonPressed;
            webView.Source = "file:///android_asset/googlemapsearch.html";

            StartSearch();
        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            StartSearch();
        }

        private void StartSearch()
        {
            var place = searchBar?.Text ?? string.Empty;
            webView.Source = "file:///android_asset/googlemapsearch.html?place=" + System.Web.HttpUtility.UrlEncode(place);
        }
    }
}