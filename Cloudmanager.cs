using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class asteroidManager
    {



        private float asteroidTimer, deltaTime, asteroidInterval;



        private Random random = new Random();
        private List<Asteroid> asteroids;
        private List<Texture2D> asteroidTextures;
       

        public void Update(GameTime gameTime)
        {   
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            asteroidTimer += deltaTime;
            asteroidInterval = (float)random.NextDouble() * 2; 

            foreach(var asteroid in asteroids)
            {
                asteroid.Update(gameTime);
            }

            if(asteroidTimer > asteroidInterval)
            {
                asteroidTimer = 0;
                asteroidInterval = (float)random.NextDouble()*2;
                SpawnAsteroid();
            }

            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                asteroids[i].Update(gameTime);
                if (!asteroids[i].InRange)
                {
                    asteroids.RemoveAt(i);
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var asteroid in asteroids)
            {
                asteroid.Draw(spriteBatch);
            }
        }

        private void SpawnAsteroid()
        {  
            Texture2D texture = asteroidTextures[random.Next(asteroidTextures.Count)];
            float depth = (float)random.NextDouble();
            float size = (float)random.NextDouble()*2+0.5f; 
            asteroids.Add(new Asteroid(texture,random.Next(0,1080),random.Next(50,75),depth,size));
        }

        public asteroidManager(List<Texture2D> texture)
        {
            this.asteroids = new List<Asteroid>();
            this.asteroidTextures = texture;
            for (int i = 0; i < 5; i++)
            {
                SpawnAsteroid();
            }
        }

    }
}