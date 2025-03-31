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

        private Vector2 direction,position;

        private float rotation, speed = -2000f;

        public bool InRange = true;

        public Rectangle Hitbox{get{return new Rectangle((int)position.X-texture.Width/8,(int)position.Y-texture.Height/8,texture.Width,texture.Height);}}

        public object owner{get; private set;}

        public Projectile(Texture2D texture, float rotation,Vector2 position, object owner)
        {
            this.texture = texture;
            direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            this.position = position;

            this.rotation = rotation;
            this.owner = owner;
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

        public void Draw(SpriteBatch spriteBatch,Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, Hitbox, Color.Red * 0.5f);

            spriteBatch.Draw(texture, position, null, Color.White, rotation - (float)Math.PI/2, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0.99f);
        }

    }
}


