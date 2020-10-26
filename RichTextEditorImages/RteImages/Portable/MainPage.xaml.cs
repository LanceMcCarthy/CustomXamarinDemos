using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.XamarinForms.RichTextEditor;
using Xamarin.Forms;

namespace RteImages.Portable
{
    public partial class MainPage : ContentPage
    {
        private RichTextHtmlStringSource htmlSource;

        public MainPage()
        {
            InitializeComponent();

            htmlSource = new RichTextHtmlStringSource
            {
                Html = @"<h4>One of the Most Beautiful Islands on Earth - Tenerife</h4>
            <p><strong>Tenerife</strong> is the largest and most populated island of the eight <a href='https://en.wikipedia.org/wiki/Canary_Islands' target='_blank'>Canary Islands</a>.</p>
            <p style='color:#808080'>It is also the most populated island of <strong>Spain</strong>, with a land area of <i>2,034.38 square kilometers</i> and <i>904,713</i> inhabitants, 43% of the total population of the <strong>Canary Islands</strong>.</p>"
            };

            this.richTextEditor.Source = htmlSource;
        }

        private async void ToolbarItem_OnTapped(object sender, EventArgs e)
        {
            //var imgSrcData = GetTestImage();
            //var imgSrcData = ConvertFilePathToBase64("");
            var imgSrcData = ConvertUrlImageToBase64("https://cdn0.iconfinder.com/data/icons/customicondesignoffice5/256/examples.png");


            var imgHtml = $"<img src='{imgSrcData}'></img>";


            //RichTextSelection currentSelection = await richTextEditor.GetSelectionAsync();
            //var startPosition = currentSelection.Start;

            var startPosition = richTextEditor.SelectionRange.Start;

            

            var newHtml = htmlSource.Html.Insert(startPosition, imgHtml);

            htmlSource = new RichTextHtmlStringSource {Html = newHtml};

            richTextEditor.Source = htmlSource;

        }

        private string GetTestImage()
        {
            return @"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHIAAAByCAYAAACP3YV9AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxEAAAsRAX9kX5EAAA89SURBVHhe7V0LkBxVFb3hI2oARUCqUEBECMhPUFC+KaFA/ECIZJPdmenZZEMCEQJKgkJJahUQUCghllqgVKgIQlUqglABSksS1IoKIR8SDGExbGamp2f2E7Kbf0Ky6zlv32xlN707v+7Z7uadqls7MzvT79173r3vvk+/liiiYZ0cFXPkxnhW1lh52agErxOOzJjQIkfrrxkEFU0dcljMlu9YjiwGab2NnZAOLXjNz/C/JfxOvEUO1z8zCBIStlwIz5uHv9sbN4KwHIjLDhR+xv/xOyD0iVhWLtI/NxhpNGblVMuWH8Pb0lM2g6z8/gQOFn5HfdeRDEi9C39P05czqDW+1yaHJh25AaS8PnlTX/h0I2044W/420ROlqEx3MjQrC9vUAsgkfkmPOklkPjB5C5FhCtRJQn6TV4DYXcPr9mQlWt1MQZ+IW7LGBj+d8lOydH4yXYXYioUXktds0Pake3OY8jWxRp4BXjJUSCwOZ6RlDL4++5keCFMhpJteG1LCsnQT1m2roZBpeAQAV44Ef3XGoa+obJRr6U/u3Vkrxp/2lLf2Cqf1NUyKBVjl8hBHBqgD1wAI+7hGLCUbNRrYZk6ierB60Ug9WLWTVfTYDgkMnK21S73wXjdKgkZAQIHC+vAuqDv3IzQfj/rqKtrMBiTbDkOfeD3kYG+07QFxmM/5WLUkRTWSdUtJy2sK+usq2/Q3CsHsR9Ef7QY2Whf3+RixCCJSog45ZeXxfDOZF2vfESr8+GElZaxcUeeghduUjMtNUhkvBLWlXVG3behET5tOTJWq/XhQTIlJ8EL58IIKQ4l1IS2i7HCIBh39s0OOZKOZeRXibR8QasZXYxDCg/ybgGJazlWawSJaMmuBgqTUIfC+JO6gdQZ10R5uq8hJTOQ/XUwYQhDX1iuqOEKogsy250g8x+Ts3IFGu5orX50MLNFDoFi9UjjlyID3MWQFKZ+sVRR0QYhF3ruYA4Qs+VLSOoO0GaIDjhLghB0O8MQx2dR9E54pBqu6P6/PWFLc2T7z/q8nGVl5ddQ2mna1hea9jNI2AWEqvlhRp92eQ2Nt3HcyohO98VTciVa79NUXGeA7kYJuUzp1l2JI8/G0zJeqx8t1L0lh7K1wiv/wyEJW3HkCLX7ok7TVrzOyUb0n/PiG+RcbYJoIZaSz0PpOyGOmixHPxNFD6Vuk+Gh0K0FDXiOZcuR2gTRAvfRIAz9Fkpv56BbrRG6GCTUggaq+s682p2wXC+XfVSbIDqgUvUZuQwt9mWEop3MbqOYEBUmR9CdbEXy9zK88wJ0NdGbv02sktForTeB0DfhqT1RTYiYF7CxYvzZjmj0IKOSNkG0YK2X46HcfVDy3bDP0Q4paKDMC6gfdF2XzMrNkV0uS+bkfHjnfCjcHbZVk5IFhKrcAF6K7PaVRFrqZ/bIIdoE0UH/OmZOXlLeCXE1SNgFjZRdCcjkdN+8hnREl8um5uUY9CkzofCKKQHdWeCF0DMZffC3FQ34l0iQTtImiBag3BgodzdI7VTLY8xuI5gQ0Tuv3w1CHblBqx491C2QA5NpOR8p/EIkCnu4AhE5D7Wld9oH6u9srXbtMT0rH4/n5NvwlGZ4zk8Q829jiu31FsNr3pbDUMZ1UHwZPHO38tAIJUTX78TfjPxAq1s7TH9DDgZx54K0Z2DYXahMD1pUD/q1vXidx//uiWfks/rrnoHLZShzFsJuKzPAqCyXjQiRDa1yKrzjZ/C+Tu48Y6ijdyhBP6YGwRwPOrIinpYG3m2sf+oZUPYXQeZjybw4XIFgmW4GCovUlMj69XIMZ2NA3Bp20GqudKjkA5/TW/gdkLswkZIrcIlRfVfyDg0Z+RYI/TPK2612wIU0GaoJkXW9ciAKuQ4JxyKSo6bTXCrjJgiDfUs8WenigrPVKufoy3qGCWn5GK4/Fd75KvvOMI4/fScytkEuQot/HF7V1b+I6lKRYQVZGUOf2tHtyBp89qN6jBV1EZ5hUqt8jgkX+un1av0zRNmtb0QiG+Vtb/db7bKeXujJHCi8s3DvBwy+FA2kkd6ui/QM9fB6y5ZH0Wi2qvXPEGS3nhPJabIGW2bA6G/AEGpVguHRrfBKhckRCUXW2Q1CXwCxF+jiPQO8cjRC+TdQ3ouQvcxw2YD2rUeQxDMir+K2xoxcBgL/AoW3N5JAnxWntyhPtyUPeSSxQU7U1fEMariSlWmxrKxDA+oJqod6QmQiLWegBT+KC24uZJqDC/JL6O2F3QIItWvx2S1TkR3rqnmGSe/KcSjr54gAdhCXy6oisjGnkoNbYUB1v8awwwmfhV7ChEgNVxx5JZmV8U1ve79FH/qeCw9dgGjzvlouC0i4rYhIGgihJgbjLVUEcjA9QgQOFhLKqAAyd3GJByH3Ql1tT8GJCpS3mA2H2fjgetRayiYyllHHoDzNlqjGWwEhcLCwfsrAebHx/u74ezJGq+AZZnTJEWgws1DWSg6NRnJ2qGQiJ62XU0Dgw+jw29V4LsAZ3L6iJiD6osbrPFTQjx1psbScibJ+gUbdWfFYuUopSuSUFjkarW4OCFxNg1DcLhRoQdTQ02878f4ldAvjtHqeAn3nxcm8PIMyeFCEe118kqJEIhs9A4Pj5VO3o1WHZHDsKnp2SHlMXt1A8yQaqOc70mIpOQIJ0YMoczdIda+LD1KUSN72pnZ6O3IXlHdUqg8Jat9YVFDvwvgT2bbqP61ub3d0w+OnwVY7azkMKz3Z6ZVRTL05bwrZNIUzKyPYuVcrKrsloR3Sg+z2jaQjExo3eXNHFOw0E2UElMh9kEzLeJD5T4TdHWHfesi+jA1S9Wk5eRYkXMJZKq1qRQgNkQTnIllh9JvL2bLLWZoKohTGn3i9GeH34YlV3BEVKiILaMjIyfDOB5DVblCJRA0r74f0D/Bz8g70ug3R5zNa1ZIRSiILwFjq0hgyQbTmvco7w5oMaVFjT4ZbHtqUk4lM+rSqRRFqIgmeNpzMyASEqb+pyXP2PSEmlP0mDQRiVjWUcVZr6IksoCklxyL9noEC1E41pVBICdUTIOvQIL+i1SuKyBBZQCyv7jR+CH3NFm4MDuNwRRP5FsgpOfmJHJEENxrzJhMkDy8i5G7p3z3nUpkgiiFyEHp7ZRTCaxJKLkMSsScsCZEhcghMcORoZLd8rkYLyQzaSvtgMUQWQTwjX7Oy8hi8srN/pd0eWLkgiCGyBPAsNovPrcrJQlQmkOcAGCLLwNS0fAqZbRO8cmVh/BkUQg2RFYAH7MFD54DEjWq4QkOMMKGGyArBcEujIRn6fSIvO/onFFwqXwsxRFYJteG5Ta5CArTEaped7D9VQuSihJ9iiPQIPBgJ4fY2GOcdemetEyJDpMfgdpO4I3Phla08qUNtN3FRyGsxRPoELpeh4k+C1K21mLs1RPoIJkSo9PRETnJ+95uGSJ8R75TDkQgt99srDZE+gyd+wGBvGiL7xBBZRAyRPsMQOVAMkUXEEFkBpvfKwcxI9dthYYgcKIEg0lorR2J8ONvKyrhST+kwRA6UESfSsqXOcuR5HooEA9yjPy4KQ+RAGTEiUejZ8MAnMKjvIolql3pG7tD/LgpD5ECpOZFcRI5l5F4rJ+v0WTmqImoyPCN36q8VhSFyoNSMSPV4QUeSKHSFld9/W4chsjrxnUg+dATh86sIo4tg9G08QMJNQUNkdeIrkUhiToPXPYyCunjb+nBGN0RWJ74QqY08E/KeOvOGChVZDDZEVieeElnXJofGbJmEROavJE8ZucTVfENkdeIZkbFW+bq6PzInO9RRnGXejm6IrE6qJjLmyAkJWx5BpVNqOFGhYQ2R1UnFRKrjo22ZjUqv6N8U5VJAqWKIrE7KJlJts8jJ1XFbXsXfXf1htMr7NgyR1UnJRDY3ywEgbwwqOR/Dim4q6KURDZHVybTdcChbZukq7I+6XjWgPxED+jtQwXz/eNDjvaWGyMqFG9Bgvz28f0ZXYX+AxDNQsdV0XTUv6tPmYENkZcJujecEgsS/D3sUODzxePR/zfjBaj6RWx8q5LkYIssXctG0WS3/vRlPyZW6+OGhziTlAfA5yZFQr/eTGiJLF9peH+qUgyfOjeXlTF106ajfoJ44vhAhdq/yTo9CrSGyBIGttc33goM/8unvusjKgOzoyKQjCVzwX+w3aUxksu6FlygBJ/K/MUe+rIsuCq+JZD9IPXWO8m8rI1bdevmELq56qEcoIOVFYRvYUpRRKyQ0sESyXo6srM/J6broovCMSNiS+qmzZR1JwVlm+fe0814ZlUzJ6Yjbj6PwbkWoXvUvR4JKpM4In+NMli66KLwgUhHIMJqVjegLf8MchbbWRfiH5r4njl+OVrQYpG5RMz5lJESBIxLeoBKKrGxBWQldbEmohkjaTNmuXbagDksa83K510+3LQk8xT+WlalQZpWqFAlyqfBgCRKRPL9AHWafkxTKuFUXWTIqJZI24ImUCKOr4mm5vrHX+ycilI36DjkWCt1jtcn6Up6JEQQimVToM3+2Irr8KZmWS3VxZaFcIqmDegQHbIX39yKMl31GrO/AQPUSKDYfrXubbuWuyow0kWovETwRCcUrMWSF7Cp0UWWjZCILDceR7Wg4f6Ct9CWCCe4eR2W/i5b+AsOW2/hzpIhk+Oet7PDGt3HNW3ldXUTFKEokdKcNmI3iOy/QNmOraDg1h3rGclpuRktcSyX23ctTayL7+29btsGYDyUzcpa+dNUYkkjoSp1VQ4YN8NlU2kT/LHyYnJGTodT9UGRTYUKBmVotiCSBjAogsAdGfd7KysXcsqkv6wn2IxIE8rUaltnSBQ98AOPvU/TXww3eeYXM7Dwo+Sz7Tz1em6P/XRTlEslERp1EmZMevF+NsibyIZ/6cp5iXyJZP1Uu+0FHnoOc53XDCQQ4XEG4vRaKr8Ww5YelDnpLJrLQH9EbHAwnbHmAR43qy/gCTeQHTGRQv114/xoa7Xg/HrAWOMTekxN4Ft0CD2+r4//UhrAOaQOBTyXLmGarBjFbZiN8sg7/Q7m3j/PJ8yOBYYlEGNV7iXbj/SIY8xr9s5qAYRt94FyrVc7RHxkMhaGIZH+ks8JlVhohrkdG65/UDIGYkQkLBhOpxoN6Wg3v74Y3eP5IQQMfUCCy3wOzsgdkzm+w5QL9FYMwgOtyIHKt6gsdWYrx2dWeLrYa1AZ8dD6GK88n2uQmhNNP648N+iHyf7q38nAv3US5AAAAAElFTkSuQmCC";
        }

        private Task<string> ConvertFilePathToBase64(string filePath)
        {
            var ex = Path.GetExtension(filePath);

            var imageFormat = "png";

            if (!string.IsNullOrEmpty(ex))
            {
                imageFormat = ex.TrimStart('.');
            }

            byte[] imageArray = File.ReadAllBytes(filePath);
            string base64EncodedString = Convert.ToBase64String(imageArray);

            return Task.FromResult($"data:image/{imageFormat};base64,{base64EncodedString}");
        }

        private async Task<string> ConvertUrlImageToBase64(string url)
        {
            var ex = Path.GetExtension(url);

            var imageFormat = "png";

            if (!string.IsNullOrEmpty(ex))
            {
                imageFormat = ex.TrimStart('.');
            }

            byte[] imgBytes;

            using (var client = new HttpClient())
            {
                imgBytes = await client.GetByteArrayAsync(url);
            }

            string base64EncodedString = Convert.ToBase64String(imgBytes);

            return $"data:image/{imageFormat};base64,{base64EncodedString}";
        }
    }
}
