using ReverseLoadOnDemand.Portable.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReverseLoadOnDemand.Portable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : ContentPage
    {
        public RootPage()
        {
            InitializeComponent();
        }

        private void DataGridViewButton_OnClicked(object sender, EventArgs e)
        {
            if(drawer.MainContent.GetType() != typeof(DataGridView))
            {
                drawer.MainContent = new DataGridView();
                Button_OnClicked(null, null);
            }
        }

        private void ChatViewButton_OnClicked(object sender, EventArgs e)
        {
            if (drawer.MainContent.GetType() != typeof(ChatView))
            {
                drawer.MainContent = new ChatView();
                Button_OnClicked(null, null);
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            drawer.IsOpen = !drawer.IsOpen;
        }
    }
}