using System.Reflection;
using System.Windows.Media.Imaging;

namespace plugins_browser_back_revit
{
    internal class ResourceImage
    {
        public static BitmapImage GetImage(string folder, string name)
        {
            string path = typeof(ResourceImage).Namespace + $".Resources.{folder}." + name;
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            var image = new BitmapImage();

            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();

            return image;
        }
    }
}
