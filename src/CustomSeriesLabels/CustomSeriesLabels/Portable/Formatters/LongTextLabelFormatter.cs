using System;
using System.Text;
using Telerik.XamarinForms.Chart;

namespace CustomSeriesLabels.Portable.Formatters
{
public class LongTextLabelFormatter : LabelFormatterBase<string>
{
    public int MaxLineLength { get; set; } = 12;

    public int MaxLength { get; set; } = 100;

    public override string FormatTypedValue(string text)
    {
        // Condition 1
        // If the label text is less than the line length, just return it.
        if (text.Length <= MaxLineLength)
            return text;

        // Condition 2
        // If the text is longer than the desired line length, we need to split it into separate lines.

        // Clean up double spaces between words.
        text = text.Replace("  ", " ");

        // Get a list of the words.
        string[] words = text.Split(' ');

        // This holds the final output.
        var sb1 = new StringBuilder();

        // This holds the current working value.
        var sb2 = new StringBuilder();

        // Create the new lines
        foreach (var word in words)
        {
            if (sb2.Length + word.Length + 1 < MaxLineLength)
            {
                sb1.AppendFormat(" {0}", word);
                sb2.AppendFormat(" {0}", word);
            }
            else
            {
                sb2.Clear();
                sb1.AppendFormat("{0}{1}", Environment.NewLine, word);
                sb2.AppendFormat(" {0}", word);
            }
        }

        // Trim the end depending on the maximum desired length of the label.
        if (sb1.Length > MaxLength)
        {
            return sb1.ToString().Substring(0, MaxLength) + " ...";
        }

        // Trim the result and return it.
        return sb1.ToString().TrimStart().TrimEnd();
    }
}
}
