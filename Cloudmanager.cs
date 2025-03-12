using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class Cloudmanager
    {


        private float cloudinterval;
        private float cloudtimer;

        private float deltaTime;

        private Random random = new Random();
        private List<Clouds> clouds;
        private List<Texture2D> cloudtextures;
       
        private float depth;
        private float size;
        public void Update(GameTime gameTime)
        {   
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            cloudtimer += deltaTime;
            cloudinterval = (float)random.NextDouble() * 2; 

            foreach(var cloud in clouds)
            {
                cloud.Update(gameTime);
            }

            if(cloudtimer > cloudinterval)
            {
                cloudtimer = 0;
                cloudinterval = (float)random.NextDouble()*2;
                Spawncloud();
            }

            for (int i = clouds.Count - 1; i >= 0; i--)
            {
                clouds[i].Update(gameTime);
                if (!clouds[i].InRangeClouds)
                {
                    clouds.RemoveAt(i);
                }

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var cloud in clouds)
            {
            cloud.Draw(spriteBatch);
            }
        }

        private void Spawncloud()
        {  
            Texture2D texture = cloudtextures[random.Next(cloudtextures.Count)];
            float depth = (float)random.NextDouble();
            float size = (float)random.NextDouble()*2+0.5f; 
            clouds.Add(new Clouds(texture,random.Next(0,1080),random.Next(50,75),depth,size));
        }

        public Cloudmanager(List<Texture2D> texture)
        {
            this.clouds = new List<Clouds>();
            this.cloudtextures = texture;
            for (int i = 0; i < 5; i++)
            {
                Spawncloud();
            }
        }

    }
}