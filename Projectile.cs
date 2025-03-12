using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class Projectile
    {
        private Texture2D texture;
        private Vector2 position;

        private Vector2 direction;

        private float rotation;

        public bool InRange = true;




        private float speed = -3000f;


        public Projectile(Texture2D t, float rotation,Vector2 p)
        {
            texture = t;
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            position = p;

            this.rotation = rotation;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            position += direction * speed * deltaTime;

            if(position.X < 0 || position.X > 1920 || position.Y < 0 || position.Y > 1080)
            {
                InRange = false;
            }

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, null, Color.White, rotation - (float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0.99f);
        }

    }
}


