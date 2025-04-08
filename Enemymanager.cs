using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;

namespace monogame
{
    public class Enemymanager
    {
        private float spawnInterval, spawnTimer, deltaTime;

        private Random random = new Random();
        public List<Enemy> enemies { get; private set; }
        private Texture2D enemytexture, projectiletexture, explosionTexture;

        public List<Explosion> explosions { get; private set; }

        private bool playerExplosion = true;


        private SoundEffect shootSound, explosionsound, hitsound;
        public event Action<int> onKill;

        public void Update(GameTime gameTime,Vector2 playerposition,List<Projectile> projectiles, Player player, float playerRotation)
        {   
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            spawnTimer += deltaTime;
            spawnInterval = 1f; 

            if(!player.IsAlive && playerExplosion)
            {
                explosions.Add(new Explosion(explosionTexture, playerposition, 9, playerRotation));
                playerExplosion = false;
            }


            if(spawnTimer > spawnInterval)
            {
                spawnTimer = 0;
                spawnInterval = (float)random.NextDouble()*2;
                SpawnEnemy();
            }


            for (int i = enemies.Count-1; i >= 0; i--)
            {
                enemies[i].Update(gameTime, playerposition, player);

                for (int j = projectiles.Count - 1; j >= 0; j--)
                {
                    if(enemies[i].Hitbox.Intersects(projectiles[j].Hitbox) && projectiles[j].owner is Player)
                    {

                        enemies[i].damage(1);
                        
                        projectiles.RemoveAt(j);
                        break;
                        


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
            if(enemies.Count < 3)
            {
                enemies.Add(new Enemy(enemytexture,projectiletexture,random.Next(0,1920),0,HandleEnemyDeath, shootSound, explosionsound, hitsound));
            }   
        }

        private void HandleEnemyDeath(Enemy enemy)
        {
            explosions.Add(new Explosion(explosionTexture, enemy.Position, 9, enemy.Rotation));
            enemies.Remove(enemy);
            onKill?.Invoke(100);
        }

        public Enemymanager(Texture2D texture, Texture2D ptex, Texture2D explosiontexture, SoundEffect shootSound, SoundEffect explosionsound, SoundEffect hitsound)
        {
            this.enemies = new List<Enemy>();
            this.enemytexture = texture;
            this.projectiletexture = ptex;
            this.explosionTexture = explosiontexture;
            this.explosions = new List<Explosion>();
            this.shootSound = shootSound;
            this.explosionsound = explosionsound;
            this.hitsound = hitsound;
            SpawnEnemy();


        }

    }
}
