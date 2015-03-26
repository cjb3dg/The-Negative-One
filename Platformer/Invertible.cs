using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Platformer
{
    /* All non-neutral objects */
    abstract class Invertible : Sprite
    {
        public bool IsInverted { get; set; } /* inverted = of inverse world */

        protected bool allowInversion = true; /* should be false for objects without dual-states */

        protected Texture2D image_i;

        public override void Draw(SpriteBatch sb)
        {
            if (!IsInverted)
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
            if (allowInversion)
            {
                IsInverted = !IsInverted;
            }
        }

        /* prevents inversion */
        public void lockInversion()
        {
            allowInversion = false;
        }

        public void unlockInversion()
        {
            allowInversion = true;
        }
    }
}
