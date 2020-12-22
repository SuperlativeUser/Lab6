using System;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace L6
{
    abstract class Sprite 
    {
        protected ImageBrush sprite = new ImageBrush();
        protected Sprite(string titlePNG)
        {
            string uri = "pack://application:,,,/images/" + titlePNG;
            sprite.ImageSource = new BitmapImage(new Uri(uri));
        }
        public ImageBrush getImageBrush()
        {
            return sprite;
        }

        public abstract Rectangle getRectangle();
    }
}
