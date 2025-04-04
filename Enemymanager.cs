using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class Enemymanager
    {
        private float cloudinterval, cloudtimer, deltaTime;

        private Random random = new Random();
        public List<Enemy> enemies { get; private set; }
        private Texture2D enemytexture, projectiletexture, explosionTexture;

        public List<Explosion> explosions { get; private set; }



        public void Update(GameTime gameTime,Vector2 playerposition,List<Projectile> projectiles)
        {   
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            cloudtimer += deltaTime;
            cloudinterval = 3f; 

            


            if(cloudtimer > cloudinterval)
            {
                cloudtimer = 0;
                cloudinterval = (float)random.NextDouble()*2;
                SpawnEnemy();
            }


            for (int i = enemies.Count-1; i >= 0; i--)
            {
                enemies[i].Update(gameTime, playerposition);

                for (int j = projectiles.Count - 1; j >= 0; j--)
                {
                    if(enemies[i].Hitbox.Intersects(projectiles[j].Hitbox) && projectiles[j].owner is Player)
                    {

                        enemies[i].damage(1);
                        
                        projectiles.RemoveAt(j);
                        


                    }

                    

                }
            }

            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].IsFinished)
                {
                    explosions.RemoveAt(i);
                }
            }




        }

        public void Draw(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            foreach(var Enemy in enemies)
            {
                Enemy.Draw(spriteBatch, debugTexture);

            }

            foreach (var explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }

        }

        private void SpawnEnemy()
        {  
            if(enemies.Count < 2)
            {
                enemies.Add(new Enemy(enemytexture,projectiletexture,0,0,HandleEnemyDeath));
            }   
        }

        private void HandleEnemyDeath(Enemy enemy)
        {
            explosions.Add(new Explosion(explosionTexture, enemy.Position, 9, enemy.Rotation));
            enemies.Remove(enemy);
        }

        public Enemymanager(Texture2D texture, Texture2D ptex, Texture2D explosiontexture)
        {
            this.enemies = new List<Enemy>();
            this.enemytexture = texture;
            this.projectiletexture = ptex;
            this.explosionTexture = explosiontexture;
            this.explosions = new List<Explosion>();
            SpawnEnemy();


        }

    }
}
