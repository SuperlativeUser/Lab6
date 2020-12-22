using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Media;
using System.Windows.Shapes;

namespace L6
{
    class Player : Sprite
    {
        public Player(string uri) : base(uri)
        {}
       
        public override Rectangle getRectangle()
        {
            Rectangle body = new Rectangle
            {
                Height = 50,
                Width = 70,
                Fill = sprite
            };

            return body;
        }

    }
}
