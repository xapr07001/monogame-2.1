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

        public Vector2 playerposition;


        private float maxRotationSpeed = 100f;


        private float rotation;

        private Vector2 center;


        private Texture2D projectiletexture;

        private List<Projectile> projectiles;


        private float maxSpeed = 15f;
        private Vector2 Velocity = Vector2.Zero;

        private Random random = new Random();

        

        public Player(Texture2D t,Texture2D ptex, int x, int y)
        {
            texture = t;
            center = new Vector2(texture.Width / 2, texture.Height / 2);
            position = new Vector2(x, y);
            projectiletexture = ptex;
            projectiles = new List<Projectile>(); 

        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            Vector2 mouseposition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 direction = position - mouseposition;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float acceleration = 50f;


            playerposition = position;



            if (direction.Length() > 0)
            {
                direction.Normalize();
                float targetRotation = (float)Math.Atan2(direction.Y, direction.X);
                float difference = MathHelper.WrapAngle(targetRotation - rotation);
                float rotationspeed = MathHelper.Clamp(Math.Abs(difference) * 3f * deltaTime, 0f, maxRotationSpeed);
                float turnAmount = Math.Sign(difference) * rotationspeed;
                rotation += turnAmount;

            }



            Vector2 inputdirection = Vector2.Zero;


            if (kState.IsKeyDown(Keys.A))
            {
                inputdirection.X -= 1;
            }
            if (kState.IsKeyDown(Keys.D))
            {
                inputdirection.X += 1;
            }
            if(kState.IsKeyDown(Keys.W))
            {
                inputdirection.Y -= 1;
            }
            if(kState.IsKeyDown(Keys.S))
            {
                inputdirection.Y += 1;
            }


            if (inputdirection.LengthSquared() > 0)
            {
                inputdirection.Normalize();
                Velocity += inputdirection * acceleration * deltaTime;
            }


            if(inputdirection == Vector2.Zero)
            {
                if(Velocity.Length() > 0)
                {
                    Vector2 frictionforce = Vector2.Normalize(Velocity)* acceleration * deltaTime;
                    Velocity -= frictionforce;
                }

            }


            
            if (Velocity.Length() > maxSpeed)
            {
                Velocity = Vector2.Normalize(Velocity) * maxSpeed;

            }
            






            position += Velocity;



            position.X = MathHelper.Clamp(position.X, 0, 1920);
            position.Y = MathHelper.Clamp(position.Y, 0, 1080);



            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                projectiles.Add(new Projectile(projectiletexture,rotation + (float)(random.NextDouble() * 0.12 - 0.06),position));
            }


            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);
                if(!projectiles[i].InRange)
                {
                    projectiles.RemoveAt(i);
                }

            }


        }





        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, null, Color.White, rotation - (float)Math.PI / 2, center, 0.35f, SpriteEffects.None, 1f);

            foreach(var p in projectiles)
            {             
                p.Draw(spritebatch);
            }
        }
    }
}
