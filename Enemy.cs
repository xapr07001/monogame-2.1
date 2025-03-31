using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;



namespace monogame
{
    public class Enemy
    {


        private Texture2D texture;

        private Vector2 position;

        private float maxRotationSpeed = 20f;


        private float rotation;

        private Vector2 center;

        public int enemyHealth{get; private set;}
        private Texture2D projectiletexture;

        public List<Projectile> projectiles { get; private set; }
        private float maxSpeed = 5f;
        private Vector2 Velocity = Vector2.Zero;

        private float targetingupdatetimer;

        private float targetupdateinterval = 5f;
        private Random random = new Random();

        private Vector2 distance = Vector2.Zero;


        public Rectangle Hitbox{get{return new Rectangle((int)position.X-texture.Width/8,(int)position.Y-texture.Height/8,texture.Width/4,texture.Height/4);}}

        public bool enemyDead{get; private set;}



        public Enemy(Texture2D t, Texture2D ptex, int x, int y)
        {
            texture = t;
            center = new Vector2(texture.Width / 2, texture.Height / 2);
            position = new Vector2(x, y);
            projectiletexture = ptex;
            projectiles = new List<Projectile>();
            enemyHealth = 0;


        }

        public void Update(GameTime gameTime,Vector2 ppos)
        {

            KeyboardState kState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            Vector2 direction = position - ppos;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float acceleration = 50f;



            targetingupdatetimer += deltaTime;
            enemyDead = false;




            foreach(var projectile in projectiles)
            {
                if(Hitbox.Intersects(projectile.Hitbox))
                {
                    enemyHealth =- 1;  
                }
            }


            
            if (direction.Length() > 0)
            {
                direction.Normalize();
                float targetRotation = (float)Math.Atan2(direction.Y, direction.X);
                float difference = MathHelper.WrapAngle(targetRotation - rotation);
                float rotationspeed = MathHelper.Clamp(Math.Abs(difference) * 3f * deltaTime, 0f, maxRotationSpeed);
                float turnAmount = Math.Sign(difference) * rotationspeed;
                rotation += turnAmount;

            }

            if(targetingupdatetimer >= targetupdateinterval)
            {
                UpdateDistance();

            }
            Vector2 goalposition = ppos - distance;

            Vector2 inputdirection = goalposition - position;




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

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                projectiles.Add(new Projectile(projectiletexture, rotation + (float)(random.NextDouble() * 0.12 - 0.06), position));
            }


            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);
                if (!projectiles[i].InRange)
                {
                    projectiles.RemoveAt(i);
                }

            }


        }


        private void UpdateDistance()
        {
            distance = new Vector2(random.Next(-720,720),random.Next(-580,580));
            if(distance.Length() < 100)
            {
                UpdateDistance();
            }
            targetingupdatetimer = 0;
        }


        private void damage(int damage)
        {
            enemyHealth -= damage;
        }


        public void Draw(SpriteBatch spritebatch, Texture2D debugTexture)
        {
            spritebatch.Draw(texture, position, null, Color.White, rotation - (float)Math.PI / 2, center, 0.35f, SpriteEffects.None, 1f);

            spritebatch.Draw(debugTexture, Hitbox, Color.Red * 0.5f);



            foreach (var p in projectiles)
            {
                p.Draw(spritebatch,debugTexture);
            }


        }


        
    }
}


