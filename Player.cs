using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame
{
    public class Player
    {

        private Texture2D texture;
        
        private Vector2 position;
        
        private float speed = 10f;

        private Keys up;
        private Keys down;

        private float rotation;

        private Vector2 center;






        public Player(Texture2D t, int x, int y, Keys u,Keys d){
            texture = t;
            up = u;
            down = d;
            center = new Vector2(texture.Width / 2, texture.Height / 2);

            
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            Vector2 mouseposition = new Vector2(mouseState.X,mouseState.Y);
            Vector2 direction = mouseposition - position;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            direction.Normalize();
            position += direction * speed * deltaTime;

            rotation = (float)Math.Atan2(direction.Y, direction.X);


        }

        public void  Draw(SpriteBatch spritebatch){
            spritebatch.Draw(texture,position,Color.White);
        }
    }
}
