using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommonHelpers.Common;
using Telerik.XamarinForms.Common;
using TelerikThemeEditor.Portable.Common;
using TelerikThemeEditor.Portable.Models;
using Xamarin.Forms;

namespace TelerikThemeEditor.Portable.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ThemeItem> ThemeColors { get; set; } = new ObservableCollection<ThemeItem>();

        public void LoadThemeResources()
        {
            IsBusy = true;

            var blueResources = new BlueResources();
            
            foreach (var key in blueResources.Keys)
            {
                IsBusyMessage = $"reading {key}...";

                try
                {
                    var item = new ThemeItem
                    {
                        ThemeKey = key,
                        Title = ParsingHelpers.BreakCaseNamedWord(key),
                        ControlName = ParsingHelpers.GetControlName(key),
                        OriginalThemeColor = (Color) blueResources[key],
                        SelectedThemeColor = (Color) blueResources[key]
                    };
                    
                    ThemeColors.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception reading value for: {key}. \n{ex}");
                }
            }

            IsBusyMessage = "";
            IsBusy = false;
        }
    }
}
