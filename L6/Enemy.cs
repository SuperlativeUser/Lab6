
using System;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace L6
{
    class Enemy : Sprite
    {
        public Enemy(string uri) : base(uri) { }

        public override Rectangle getRectangle()
        {
            Rectangle body = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = sprite
            };

            Canvas.SetTop(body, -100);
            Canvas.SetLeft(body, new Random().Next(10, 300));
            return body;
        }
    }
}
