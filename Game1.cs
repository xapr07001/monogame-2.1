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

    private Texture2D playertexture;
    private Texture2D bullettexture;

    private SoundEffect airplanesound;
    private List<Texture2D> cloudtextures;

    private SoundEffectInstance airplaneSoundInstance;

    private Cloudmanager cloudmanager;

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
        playertexture = Content.Load<Texture2D>("a10");
        bullettexture = Content.Load<Texture2D>("tile_0012");

        cloudtextures = new List<Texture2D>
        {
            Content.Load<Texture2D>("cloud_shape3_1"),
            Content.Load<Texture2D>("cloud_shape3_2"),
            Content.Load<Texture2D>("cloud_shape3_3"),
            Content.Load<Texture2D>("cloud_shape3_4"),
            Content.Load<Texture2D>("cloud_shape3_5"),
        };

        airplanesound = Content.Load<SoundEffect>("jet-engine");
        airplaneSoundInstance = airplanesound.CreateInstance();
        airplaneSoundInstance.IsLooped = true;  
        airplaneSoundInstance.Volume = 0.05f;
        airplaneSoundInstance.Play();


        enemymanager = new Enemymanager(playertexture, bullettexture);
        cloudmanager = new Cloudmanager(cloudtextures);
        player = new Player(playertexture,bullettexture,500,500);

        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        player.Update(gameTime);
        cloudmanager.Update(gameTime);
        enemymanager.Update(gameTime, player.playerposition);
  
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SkyBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(SpriteSortMode.FrontToBack);

        player.Draw(_spriteBatch);
        enemymanager.Draw(_spriteBatch);
        cloudmanager.Draw(_spriteBatch); 
        


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
