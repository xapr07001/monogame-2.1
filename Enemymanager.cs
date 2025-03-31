using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class Enemymanager
    {
        private float cloudinterval;
        private float cloudtimer;

        private float deltaTime;

        private Random random = new Random();
        public List<Enemy> enemies { get; private set; }
        private Texture2D enemytexture;
        private Texture2D projectiletexture;

        public void Update(GameTime gameTime,Vector2 playerposition,List<Projectile> projectiles)
        {   
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            cloudtimer += deltaTime;
            cloudinterval = 3f; 

            foreach(var Enemy in enemies)
            {
                Enemy.Update(gameTime, playerposition);
            }

            if(cloudtimer > cloudinterval)
            {
                cloudtimer = 0;
                cloudinterval = (float)random.NextDouble()*2;
                SpawnEnemy();
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime, playerposition);

                for (int j = projectiles.Count - 1; j >= 0; j--)
                {
                    if(enemies[i].Hitbox.Intersects(projectiles[j].Hitbox))
                    {
                        enemies.RemoveAt(i);
                        projectiles.RemoveAt(j);
                        break;
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            foreach(var Enemy in enemies)
            {
                Enemy.Draw(spriteBatch, debugTexture);
            }
        }

        private void SpawnEnemy()
        {  
            if(enemies.Count < 2)
            {
                enemies.Add(new Enemy(enemytexture,projectiletexture,0,0));
            }   
        }

        public Enemymanager(Texture2D texture, Texture2D ptex)
        {
            this.enemies = new List<Enemy>();
            this.enemytexture = texture;
            this.projectiletexture = ptex;


            SpawnEnemy();


        }

    }
}
