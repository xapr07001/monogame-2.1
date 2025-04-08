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
using SharpDX.DXGI;


namespace monogame
{

    public class OverlayManager{

        private SpriteFont font;
        private Player player;
        public int score = 0;
        private string health;
        private string scoreOverlay;

        public Action<int> OnKill;
        

        private Vector2 center;


        public OverlayManager(SpriteFont font, Player player, GraphicsDevice graphicsDevice)
        {
            this.font = font;
            this.player = player;
            this.score = 0;
            center = new Vector2(graphicsDevice.Viewport.Width/2,graphicsDevice.Viewport.Height/2);

        }
        public void IncreaseScore(int increase)
        {
            score += increase;
        }
        public void Update(GameTime gameTime, Player player)
        {

            health = $"Health: {player.playerHealth}";
            scoreOverlay = $"Score: {score}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(font, "gameover", center, Color.GhostWhite, 0, font.MeasureString(health), 2.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font,health, center, Color.GhostWhite, 0, font.MeasureString(health) / 2, 2.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(font,scoreOverlay, center - new Vector2(0,300), Color.GhostWhite, 0, font.MeasureString(scoreOverlay) / 2, 1.0f, SpriteEffects.None, 1.0f);

        }
    }
}