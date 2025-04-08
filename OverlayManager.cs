using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using monogame;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using SharpDX.Direct3D9;


namespace monogame
{

    public class OverlayManager{

        private SpriteFont font;
        private Player player;
        private int score;
        
        private Vector2 center;


        public OverlayManager(SpriteFont font, Player player, GraphicsDevice graphicsDevice)
        {
            this.font = font;
            this.player = player;
            this.score = 0;
            Vector2 center = new Vector2(graphicsDevice.Viewport.Width/2,graphicsDevice.Viewport.Height/2);
            
        }
        public void IncreaseScore(int increase)
        {
            score += increase;
        }
        public void Update(GameTime gameTime)
        {


            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "output", center, Color.GhostWhite, 0, center, 2.0f, SpriteEffects.None, 0.5f);

        }
    }
}