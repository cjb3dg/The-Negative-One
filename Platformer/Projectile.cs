using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace Platformer
{
    class Projectile : Invertible
    {
        private double x_vel;
        private double y_vel;

        public Projectile(int x, int y, int width, int height, Texture2D normal, Texture2D inverted, double x_vel, double y_vel)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
            this.image = normal;
            this.image_i = inverted;
            this.x_vel = x_vel;
            this.y_vel = y_vel;
        }


    }
}
