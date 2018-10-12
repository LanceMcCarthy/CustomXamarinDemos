using System.Text;

namespace TelerikThemeEditor.Portable.Common
{
    public static class ParsingHelpers
    {
        public static string BreakCaseNamedWord(string input)
        {
            var stringBuilder = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsUpper(c))
                {
                    stringBuilder.Append(" ");
                }

                stringBuilder.Append(c);
            }

            if (input.Length > 0 && char.IsUpper(input[0]))
            {
                stringBuilder.Remove(0, 1);
            }

            // removes "Telerik " from start of the name
            stringBuilder.Remove(0, 8);

            return stringBuilder.ToString();
        }

        public static string GetControlName(string key)
        {
            if (key.Contains("TelerikAutoComplete"))
            {
                return "RadAutoComplete";
            }
            else if (key.Contains("TelerikBusyIndicator"))
            {
                return "RadBusyIndicator";
            }
            else if (key.Contains("TelerikButton"))
            {
                return "RadButton";
            }
            else if (key.Contains("TelerikCalendar"))
            {
                return "RadCalendar";
            }
            else if (key.Contains("TelerikChart"))
            {
                return "RadChart";
            }
            else if (key.Contains("TelerikChat"))
            {
                return "Conversational UI (RadChat)";
            }
            else if (key.Contains("TelerikCheckBox"))
            {
                return "RadCheckBox";
            }
            else if (key.Contains("TelerikDataForm"))
            {
                return "RadDataForm";
            }
            else if (key.Contains("TelerikDataGrid"))
            {
                return "RadDataGrid";
            }
            else if (key.Contains("TelerikEntry"))
            {
                return "RadEntry";
            }
            else if (key.Contains("TelerikListView"))
            {
                return "RadListView";
            }
            else if (key.Contains("TelerikMaskedInput"))
            {
                return "RadMaskedInput";
            }
            else if (key.Contains("TelerikNonVirtualizedItemsControl"))
            {
                return "NonVirtualizedItemsControl";
            }
            else if (key.Contains("TelerikNumericInput"))
            {
                return "RadNumericInput";
            }
            else if (key.Contains("TelerikRating"))
            {
                return "RadRating";
            }
            else if (key.Contains("TelerikSegmentControl"))
            {
                return "RadSegmentedControl";
            }
            else if (key.Contains("TelerikSlideView"))
            {
                return "RadSlideView";
            }
            else if (key.Contains("TelerikTabView"))
            {
                return "RadTabView";
            }
            else if (key.Contains("TelerikTimePicker"))
            {
                return "RadTimePicker";
            }
            else if (key.Contains("TelerikTreeView"))
            {
                return "RadTreeView";
            }

            return "General";
        }
    }
}
