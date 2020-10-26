using Telerik.XamarinForms.RichTextEditor;

namespace RteImages.Portable
{
    public class InsertImageToolbarItem : RichTextEditorToolbarItem
    {
        public InsertImageToolbarItem()
        {
            this.Text = "Insert Image";
            this.Description = @"Select an image from your device to be inserted into the document. The image will be converted to base64 and inserted as an <img> element.";
        }
    }

}
