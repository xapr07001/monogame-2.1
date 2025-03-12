using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace monogame
{
    public class Clouds
    {
        private Texture2D texture;
        private Vector2 position;

        public bool InRangeClouds = true;

        private Random random = new Random();

        private float depth;
        private float size;

        private float speed = 100f;


        public Clouds(Texture2D t, int y, int speed,float depth, float size)
        {
            texture = t;
            position.X = -300;
            position.Y = y;
            this.speed = speed;
            this.depth = depth;
            this.size = size;

        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            position.X +=  speed  * deltaTime;

            if (position.X > 2920)
            {
                InRangeClouds = false;
            }



        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), size, SpriteEffects.None, depth);

        }

    }
}
