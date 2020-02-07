using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Telerik.XamarinForms.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SegmentedCustomControl.Portable.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonSegments : ContentView
    {
        public ButtonSegments()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty =
                BindableProperty.Create("ItemsSource", typeof(IList<string>), typeof(ButtonSegments), null, propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create("SelectedIndex", typeof(int), typeof(ButtonSegments), null, propertyChanged: OnSelectedIndexChanged);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof(int), typeof(ButtonSegments), null, propertyChanged: OnCornerRadiusChanged);

        public static readonly BindableProperty SelectedSegmentBackgroundColorProperty =
            BindableProperty.Create("SelectedSegmentBackgroundColor", typeof(Color), typeof(ButtonSegments));

        public static readonly BindableProperty SelectedSegmentTextColorProperty =
            BindableProperty.Create("SelectedSegmentTextColor", typeof(Color), typeof(ButtonSegments));

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create("BorderColor", typeof(Color), typeof(ButtonSegments), null, propertyChanged: OnBorderColorChanged);

        public IList<string> ItemsSource
        {
            get => (IList<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Color SelectedSegmentBackgroundColor
        {
            get => (Color)GetValue(SelectedSegmentBackgroundColorProperty);
            set => SetValue(SelectedSegmentBackgroundColorProperty, value);
        }

        public Color SelectedSegmentTextColor
        {
            get => (Color)GetValue(SelectedSegmentTextColorProperty);
            set => SetValue(SelectedSegmentTextColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        // Bindable PropertyChanged Handlers

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSegments self && newValue is List<string> items)
            {
                self.CreateButtons();
            }
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSegments self)
            {
                if ((int)newValue > self.ItemsSource.Count - 1)
                {
                    throw new ArgumentOutOfRangeException($"The SelectedIndex {(int)newValue} is out of range");
                }

                self.SetSelectedButtonColor();
            }
        }

        private static void OnCornerRadiusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSegments self)
            {
                self.RootBorder.CornerRadius = (int)newValue;
            }
        }

        private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ButtonSegments self)
            {
                self.RootBorder.BorderColor = (Color)newValue;
            }
        }

        // internal methods

        private void CreateButtons()
        {
            // ensuring no abandoned event handlers
            if (ButtonsGrid.Children.Any())
            {
                foreach (var child in ButtonsGrid.Children)
                {
                    if (child is RadButton childButton)
                    {
                        childButton.Clicked -= Button_Clicked;
                    }
                }
            }

            // remove previous buttons
            ButtonsGrid.Children.Clear();

            if (ItemsSource == null)
                return;

            // Create new buttons
            foreach (var item in ItemsSource)
            {
                ButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition());

                var button = new RadButton();
                button.Text = item;

                var index = ItemsSource.IndexOf(item);

                Grid.SetColumn(button, index);

                button.BorderThickness = 0;
                button.BorderColor = Color.Transparent;

                // this adds the "walls" between segments
                if (index > 0)
                {
                    button.BorderThickness = new Thickness(2, 0, 0, 0);
                    button.BorderColor = BorderColor;
                }

                button.Clicked += Button_Clicked;

                ButtonsGrid.Children.Add(button);
            }

            SetSelectedButtonColor();
        }

        // Changes the SelectedIndex
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (sender is RadButton clickedButton)
            {
                try
                {
                    this.SelectedIndex = ButtonsGrid.Children.IndexOf(clickedButton);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ButtonSegments Exception: {ex.Message}");
                }
            }
        }

        // This will change the segment color according to the selected index.
        private void SetSelectedButtonColor()
        {
            foreach (var child in ButtonsGrid.Children)
            {
                if (child is RadButton childButton)
                {
                    if (childButton.Text == ItemsSource[SelectedIndex])
                    {
                        childButton.BackgroundColor = SelectedSegmentBackgroundColor;
                        childButton.TextColor = SelectedSegmentTextColor;
                    }
                    else
                    {
                        childButton.BackgroundColor = Color.White;
                        childButton.TextColor = Color.Black;
                    }
                }
            }
        }
    }
}