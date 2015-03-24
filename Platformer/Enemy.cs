using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class Enemy : Invertible
    {
        public int curHP;
        public MovementPattern mPattern;

        public Enemy(int x, int y, int width, int height, Texture2D normal, Texture2D inverted, int curHP, MovementPattern mPattern) //Moving Constructor
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
            this.image = normal;
            this.image_i = inverted;
            this.curHP = curHP;
            this.mPattern = mPattern;
        }

        public Enemy(Enemy e) //Copy Constructor
        {
            this.spriteX = e.spriteX;
            this.spriteY = e.spriteY;
            this.spriteWidth = e.spriteWidth;
            this.spriteHeight = e.spriteHeight;
            this.image = e.image;
            this.image_i = e.image_i;
            this.curHP = e.curHP;
            this.mPattern = e.mPattern;
        }
    }
}
