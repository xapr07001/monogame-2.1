using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class Explosion
    {
        Texture2D texture;
        private Vector2 position;

        private float timer;
        private float updateSpeed = 0.05f;

        private int frameWidth, frameHeight, totalFrames, frame;

        private float rotation;
        public bool IsFinished { get; private set; }
    

        public Explosion(Texture2D explosionTexture, Vector2 position, int totalFrames, float rotation)
        {
            this.texture = explosionTexture;
            this.position = position;
            this.totalFrames = totalFrames;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
            frame = 0;
            IsFinished = false;
            this.rotation = rotation;
        }

        
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= updateSpeed)
            {
                timer = 0;
                frame++;
                if (frame >= totalFrames)
                {
                    IsFinished = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsFinished)
            {
                Rectangle sourceRect = new Rectangle(frame * frameWidth, 0, frameWidth, frameHeight);
                spriteBatch.Draw(texture, position, sourceRect, Color.White, rotation - (float)Math.PI/2, new Vector2(frameWidth / 2, frameHeight / 2), 3f, SpriteEffects.None, 1f);
            }
        }
    }
}