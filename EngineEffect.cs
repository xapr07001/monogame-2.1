using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogame
{
    public class EngineEffect
    {
        Texture2D texture;
        private Vector2 position;

        private int frame;
        private float timer;
        private float frameSpeed = 0.05f;
        private int totalFrames;
        private int frameWidth;
        private int frameHeight;
        private int currentFrame;
        private int frameCount;
        private float rotation;
        private Vector2 offset;
        private Rectangle sourceRect;

    

        public EngineEffect(Texture2D explosionTexture, Vector2 position, int totalFrames, float rotation)
        {
            this.texture = explosionTexture;
            this.position = position;
            this.totalFrames = totalFrames;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
            frame = 0;
            this.rotation = rotation;
        }

        
        public void Update(GameTime gameTime)
            {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= frameSpeed)
            {
                timer = 0;
                currentFrame = (currentFrame + 1) % frameCount; // Loopar animationen
            }

            int frameWidth = texture.Width / frameCount;
            int frameHeight = texture.Height;
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            Vector2 drawPosition = position + offset; // Placera flamman bakom skeppet
            spriteBatch.Draw(texture, drawPosition, sourceRect, Color.White, rotation, new Vector2(sourceRect.Width / 2, sourceRect.Height / 2), 1f, SpriteEffects.None, 0.8f);

        }
    }        
}
