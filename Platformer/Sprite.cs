using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
namespace Platformer
{
	abstract class Sprite
	{
		protected int spriteX, spriteY;
		protected int spriteWidth, spriteHeight;
		protected Texture2D image;
        protected bool ofInverseWorld;

		public Sprite(Texture2D image, int spriteWidth, int spriteHeight, int spriteX, int spriteY, bool ofInverseWorld)
		{
            this.image = image;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            this.spriteX = spriteX;
            this.spriteY = spriteY;
            this.ofInverseWorld = ofInverseWorld;
		}

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }
	}
}

