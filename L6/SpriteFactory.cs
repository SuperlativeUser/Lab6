using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L6
{
    class SpriteFactory
    {
        public static Sprite getSprite(string sprite)
        {
            string uri;
            switch (sprite)
            {
                case "enemy":
                    uri = "enemy" + new Random().Next(1, 3) + ".png";
                    return new Enemy(uri);
                case "player":
                    uri = "player.png";
                    return new Player(uri);
                case "background":
                    uri = "background.png";
                    return new Background(uri);
                default:
                    return null;
            }
            
        }
    }
}
