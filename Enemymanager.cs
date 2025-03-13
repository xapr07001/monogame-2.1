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
        private List<Enemy> enemies;
        private Texture2D enemytexture;
        private Texture2D projectiletexture;

        public void Update(GameTime gameTime,Vector2 playerposition)
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


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var Enemy in enemies)
            {
            Enemy.Draw(spriteBatch);
            }
        }

        private void SpawnEnemy()
        {  
            enemies.Add(new Enemy(enemytexture,projectiletexture,0,0));
        }

        public Enemymanager(Texture2D texture, Texture2D ptex)
        {
            this.enemies = new List<Enemy>();
            this.enemytexture = texture;
            this.projectiletexture = ptex;
            for (int i = 0; i < 5; i++)
            {
                SpawnEnemy();
            }
        }

    }
}
