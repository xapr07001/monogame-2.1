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




        public Vector2 playerposition;



        private Vector2 center, position;


        private Texture2D projectiletexture, texture;

        public List<Projectile> projectiles { get; private set; }

        public bool IsAlive = true;

        public int playerHealth = 10;
        private Vector2 Velocity = Vector2.Zero;

        private Random random = new Random();

        private float bulletCooldown = 0.2f, maxSpeed = 15f, bulletTimer, maxRotationSpeed = 100f, rotation;



        public Rectangle Hitbox{get{return new Rectangle((int)position.X-texture.Width,(int)position.Y-texture.Height,texture.Width*2,texture.Height*2);}}

            

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

            IsAlive = true;
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

            bulletTimer += deltaTime;

            if(bulletTimer > bulletCooldown && mouseState.LeftButton == ButtonState.Pressed)
            {
                bulletTimer = 0;
                projectiles.Add(new Projectile(projectiletexture,rotation + (float)(random.NextDouble() * 0.12 - 0.06),position,this));

            }
            


            for (int j = projectiles.Count - 1; j >= 0; j--)
            {
                if(Hitbox.Intersects(projectiles[j].Hitbox) && projectiles[j].owner is Enemy)
                {

                    playerHealth -= 1;
                        
                    projectiles.RemoveAt(j);
                    break;
                        
                }
            }

            if(playerHealth <= 0)
            {
                IsAlive = false;
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





        public void Draw(SpriteBatch spritebatch, Texture2D debugTexture)
        {
            spritebatch.Draw(texture, position, null, Color.White, rotation - (float)Math.PI / 2, center, 3f, SpriteEffects.None, 1f);

            spritebatch.Draw(debugTexture, Hitbox, Color.Red * 0.5f);


            foreach(var p in projectiles)
            {             
                p.Draw(spritebatch,debugTexture);
            }
        }
    }
}
