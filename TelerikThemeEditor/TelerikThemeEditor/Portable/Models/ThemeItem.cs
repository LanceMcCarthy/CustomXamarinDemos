using System;
using System.Collections.Generic;
using System.Text;
using CommonHelpers.Common;
using Xamarin.Forms;

namespace TelerikThemeEditor.Portable.Models
{
    public class ThemeItem : BindableBase
    {
        private Color _selectedThemeColor;

        public string Title { get; set; }

        public string ControlName { get; set; }

        public string ThemeKey { get; set; }

        public Color OriginalThemeColor { get; set; }

        public Color SelectedThemeColor
        {
            get => _selectedThemeColor;
            set => SetProperty(ref _selectedThemeColor, value);
        }
    }
}
