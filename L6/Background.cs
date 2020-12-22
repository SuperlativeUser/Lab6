using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace L6
{
    class Background : Sprite
    {
        public Background (string imagetitle) : base (imagetitle) 
        {
            setBrush();
        }

        private  ImageBrush setBrush()
        {
            sprite.TileMode = TileMode.Tile;
            sprite.Viewport = new Rect(0, 0, 0.15, 0.15);
            sprite.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            return sprite;
        }
        
        public override Rectangle getRectangle()
        {
            return null;
        }
    }
}
