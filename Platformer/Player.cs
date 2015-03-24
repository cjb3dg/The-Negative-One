using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Platformer
{
	class Player : Invertible
    {
		private bool moving;
		private bool grounded;
		private int speed;
		private int x_accel;
		private double friction;
		public double x_vel;
		public double y_vel;
		public int movedX;
		private bool pushing;
		public double gravity = .5;
		public int maxFallSpeed = 10;
		private int jumpPoint = 0;
        public bool victory = false;
        public int maxHP;
        public int curHP;
        public Player(int x, int y, int width, int height)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
			grounded = false;
			moving = false;
			pushing = false;

			// Movement
			speed = 5;
			friction = .15;
			x_accel = 0;
			x_vel = 0;
			y_vel = 0;
			movedX = 0;
        }

        public int getX(){
            return spriteX;
        }
        public int getY()
        {
            return spriteY;
        }
        public void setX(int x)
        {
            spriteX = x;
        }
        public void setY(int y)
        {
            spriteY = y;
        }

        public void LoadContent(ContentManager content)
        {
            {
                image = content.Load<Texture2D>("Zero.png");
                image_i = content.Load<Texture2D>("Zero_i.png");
            }
     
        }

		public void Update(Controls controls, GameTime gameTime, Obstacle[] oList)
		{
			Move (controls, oList);
            Invert(controls);
			Jump (controls, gameTime);
		}

        public void Invert(Controls controls)
        {

            if (controls.onPress(Keys.Space, Buttons.A))
                inverted = !inverted;

        }
		public void Move(Controls controls, Obstacle[] oList)
		{
            if (victory == true)
            {
                return;
            }

			// Sideways Acceleration
			if (controls.onPress(Keys.Right, Buttons.DPadRight))
				x_accel += speed;
			else if (controls.onRelease(Keys.Right, Buttons.DPadRight))
				x_accel -= speed;
			if (controls.onPress(Keys.Left, Buttons.DPadLeft))
				x_accel -= speed;
			else if (controls.onRelease(Keys.Left, Buttons.DPadLeft))
				x_accel += speed;

			double playerFriction = pushing ? (friction * 3) : friction;
			x_vel = x_vel * (1 - playerFriction) + x_accel * .10;
			movedX = Convert.ToInt32(x_vel);
			spriteX += movedX;

			// Gravity
			if (!grounded)
			{
				y_vel += gravity;
				if (y_vel > maxFallSpeed)
					y_vel = maxFallSpeed;
				spriteY += Convert.ToInt32(y_vel);
			}
			else
			{
				y_vel = 1;
			}

			grounded = false;

			// Check up/down collisions, then left/right
			checkYCollisions(oList);

		}

		private void checkYCollisions(Obstacle[] oList)
		{
            if (spriteY < 60 && spriteX > 685 && spriteX < 750)
            {
                victory = true;
            }
            if (spriteY >= 400)
            {
                grounded = true;
                return;
            }
            else if (spriteY >= 347 && spriteY <= 353 && spriteX > 50 && spriteX < 300 && y_vel >= 0)
            {
                grounded = true;
                return;
            }
            else if (spriteY >= 247 && spriteY <= 253 && spriteX > 200 && spriteX < 450 && y_vel >= 0 && inverted == true)
            {
                grounded = true;
                return;
            }
            else if (spriteY >= 147 && spriteY <= 153 && spriteX > 350 && spriteX < 600 && y_vel >= 0 && inverted == false)
            {
                grounded = true;
                return;
            }
            else if (spriteY >= 47 && spriteY <= 53 && spriteX > 500 && spriteX < 750 && y_vel >= 0)
            {
                grounded = true;
                return;
            }
                grounded = false;
		}

		private void Jump(Controls controls, GameTime gameTime)
		{
			// Jump on button press
			if (controls.onPress(Keys.Up, Buttons.DPadUp) && grounded)
			{
				y_vel = -11;
				jumpPoint = (int)(gameTime.TotalGameTime.TotalMilliseconds);
				grounded = false;
			}

			// Cut jump short on button release
			else if (controls.onRelease(Keys.Space, Buttons.A) && y_vel < 0)
			{
				y_vel /= 2;
			}
		}
    }
}
