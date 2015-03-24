using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Platformer
{
    class Invertible : Sprite
    {
        public bool inverted = false;
        public bool invertible = true;

        public Texture2D image_i;
        public MovementPattern movement_i;

        public void Draw(SpriteBatch sb)
        {
            if (inverted == false)
            {
                sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
            }
            else
            {
                sb.Draw(image_i, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
            }
        }

        public void invert()
        {
            if (invertible)
            {
                inverted = !inverted;
            }
        }

        public void lockInversion()
        {
            invertible = false;
        }

        public void unlockInversion()
        {
            invertible = true;
        }
    }
}
