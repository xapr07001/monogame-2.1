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

namespace monogame_2._1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D playertexture, bullettexture, explosiontexture, backgroundtexture, debugTexture;


    private SoundEffect airplanesound;
    private List<Texture2D> asteroidTextures;

    private SoundEffectInstance airplaneSoundInstance;

    private asteroidManager asteroidManager;

    private Enemymanager enemymanager;

    private Random random = new Random();
    Player player;





    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        // TODO: Add your initialization logic here

        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        playertexture = Content.Load<Texture2D>("Kla'ed - Fighter - Base");
        bullettexture = Content.Load<Texture2D>("tile_0012");
        explosiontexture = Content.Load<Texture2D>("Kla'ed - Fighter - Destruction");
        backgroundtexture = Content.Load<Texture2D>("background");


        debugTexture = new Texture2D(GraphicsDevice, 1, 1);
        debugTexture.SetData(new[] { Color.Red });

        asteroidTextures = new List<Texture2D>
        {
            Content.Load<Texture2D>("Asteroids#01"),
            Content.Load<Texture2D>("Asteroids#02"),
        };

        airplanesound = Content.Load<SoundEffect>("jet-engine");
        airplaneSoundInstance = airplanesound.CreateInstance();
        airplaneSoundInstance.IsLooped = true;  
        airplaneSoundInstance.Volume = 0.05f;
        airplaneSoundInstance.Play();


        enemymanager = new Enemymanager(playertexture, bullettexture,explosiontexture);
        asteroidManager = new asteroidManager(asteroidTextures);
        player = new Player(playertexture,bullettexture,500,500);

        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        player.Update(gameTime);
        asteroidManager.Update(gameTime);
        enemymanager.Update(gameTime, player.playerposition, player.projectiles);



        if (!player.IsAlive)
        {
            Exit();
        }
  
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(SpriteSortMode.FrontToBack,null,Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp);
        
        _spriteBatch.Draw(backgroundtexture, Vector2.Zero,null, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0.01f);

        player.Draw(_spriteBatch, debugTexture);
        enemymanager.Draw(_spriteBatch, debugTexture);
        asteroidManager.Draw(_spriteBatch); 
        

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
