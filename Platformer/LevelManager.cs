using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer
{
    class LevelManager
    {
        private InversionManager inversionManager; /* handles invertibles */

        public List<Sprite> objects { get; private set; } /* all objects in level */

        public List<Enemy> enemies { get; set; }
        public List<Obstacle> obstacles { get; set; }
        public List<Projectile> projectiles { get; set; }
        public List<Boss> bosses { get; set; }
        public Player player { get; set; }

        public LevelManager(InversionManager inversionManager, Player player)
        {
            this.inversionManager = inversionManager;
            this.player = player;
            enemies = new List<Enemy>();
            obstacles = new List<Obstacle>();
            projectiles = new List<Projectile>();
            bosses = new List<Boss>();

            objects = new List<Sprite>();
        }

        /*
         * Registers all objects with LevelManager
         * Registers all Invertibles with InversionManager
         */
        public void setObjects(List<Sprite> objects) {
            this.objects = objects;

            for (int i = 0; i < objects.Count; i++)
            {
                addObject(objects[i]);
            }
        }

        /*
         * Adds a new object to LevelManager
         * If it is Invertible, it is also registered with InversionManager
         */
        public void addObject(Sprite obj) {
            objects.Add(obj);

            /* list from top of potential inheritance tree to bottom */
            if (obj is Enemy)
            {
                enemies.Add((Enemy)obj);
            }
            else if (obj is Projectile)
            {
                projectiles.Add((Projectile)obj);
            }
            else if (obj is Boss)
            {
                bosses.Add((Boss)obj);
            }
            else if (obj is Projectile)
            {
                projectiles.Add((Projectile)obj);
            }
            else if (obj is Obstacle)
            {
                obstacles.Add((Obstacle)obj);

            }
            else if (obj is Invertible)
            {
                inversionManager.registerInvertible((Invertible)obj);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Sprite obj = objects[i];
                if (obj is Invertible)
                {
                    if (((Invertible)obj).IsInverted == inversionManager.IsWorldInverted)
                    {
                        obj.Draw(sb);
                    }
                }
                else
                {
                    objects[i].Draw(sb);
                }
            }
        }
    }
}
