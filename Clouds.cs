using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace monogame
{
    public class Asteroid
    {
        private Texture2D texture;
        private Vector2 position;

        public bool InRange = true;

        private Random random = new Random();

        private float speed, rotation, opacity;



        public Asteroid(Texture2D t,int x, int y, int speed, float rotation)
        {
            texture = t;
            this.position.X = x;
            this.position.Y = y;
            this.speed = speed;
            this.rotation = rotation;
            opacity = (float)random.NextDouble();


        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            position.X +=  speed  * deltaTime;

            if (position.X > 2920)
            {
                InRange = false;
            }



        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, null, Color.White*opacity, rotation, new Vector2(texture.Width / 2, texture.Height / 2), opacity*5, SpriteEffects.None, opacity);

        }

    }
}
