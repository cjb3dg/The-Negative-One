using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Tao.Sdl;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Platformer
{
    class CharacterManager
    {
        ContentManager content;
        List<Enemy> enemyList;
        List<Projectile> projectileList;
        Player player;
        LevelManager lvlManager;
        InversionManager invManager;

        public CharacterManager(LevelManager lvl, InversionManager inv, ContentManager cont)
        {
            player = new Player(25, 400, 50, 50);
            enemyList = new List<Enemy>();
            projectileList = new List<Projectile>();
            lvlManager = lvl;
            invManager = inv;
            content = cont;
            inv.registerInvertible(player);
        }

        public void Load()
        {
            player.LoadContent(this.content);
            //shouldn't be in the final version, but here to provide some textures
            Texture2D platformGrey = content.Load<Texture2D>("Platform_grey");
            Texture2D platformBlack = content.Load<Texture2D>("Platform_black");
            Texture2D platformWhite = content.Load<Texture2D>("Platform_white");
            enemyList.Add(new Enemy(550, 80, 20, 20, platformBlack, platformWhite, 20, null));
        }

        public void Update(Controls controls, Microsoft.Xna.Framework.GameTime gametime, List<Obstacle> oList)
        {
            player.Update(controls, gametime, oList, enemyList);
            foreach (Enemy e in enemyList)
            {
                e.Update(gametime, oList);
            }
            foreach (Projectile p in projectileList)
            {
                p.Update(oList, ref enemyList);
            }
            projectileList.RemoveAll(p => !p.isAlive());
            enemyList.RemoveAll(e => !e.isAlive());

            if (player.Shoot(controls))
            {
                double projectileXVel = 3;
                int projectileX = player.getWidth() - 2;
                int projectileY = 18;
                if (!player.facingRight())
                {
                    projectileXVel = -3;
                    projectileX = 2;
                }
                projectileList.Add(new Projectile(player.getX() + projectileX, player.getY() + projectileY, 20, 10, content.Load<Texture2D>("Platform_grey"), content.Load<Texture2D>("Platform_grey"), projectileXVel, 0));
            }

            //update levelmanager and inversion manager
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile p in projectileList)
            {
                p.Draw(spriteBatch);
            }
            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
            }
            if (player.isAlive())
            {
                player.Draw(spriteBatch);
            }
            if (player.victory == true)
            {
                spriteBatch.Draw(content.Load<Texture2D>("Victory"), new Rectangle(50, 50, 700, 400), Color.White);
            }
        }

    }
}