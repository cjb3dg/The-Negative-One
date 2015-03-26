#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Tao.Sdl;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections;
#endregion

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PlatformerMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player1;
        Controls controls;
        private InversionManager inversionManager;
        private LevelManager levelManager;
        private Obstacle[] oList = new Obstacle[4];
        private static SoundEffect song;
        private static SoundEffect song_i;
        private static SoundEffectInstance backSong;
        private static SoundEffectInstance backSong_i;

        public PlatformerMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player1 = new Player(25, 400, 50, 50);
            base.Initialize();

            Joystick.Init();
            Console.WriteLine("Number of joysticks: " + Sdl.SDL_NumJoysticks());
            controls = new Controls();

            inversionManager = new InversionManager();
            levelManager = new LevelManager(inversionManager, player1);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player1.LoadContent(this.Content);
            song = Content.Load<SoundEffect>("HawkeTheme");
            song_i = Content.Load<SoundEffect>("HawkeTheme_i");
            backSong = song.CreateInstance();
            backSong_i = song_i.CreateInstance();
            backSong.IsLooped = true;
            backSong_i.IsLooped = true;
            backSong.Play();
            backSong_i.Play();
            backSong_i.Volume = 0;
            Texture2D platformGrey = Content.Load<Texture2D>("Platform_grey");
            Texture2D platformBlack = Content.Load<Texture2D>("Platform_black");
            Texture2D platformWhite = Content.Load<Texture2D>("Platform_white");
            Texture2D goal = Content.Load<Texture2D>("Goal");
            Texture2D goal_i = Content.Load<Texture2D>("Goal_i");
            oList[0] = new Obstacle(100, 400, 200, 50, platformGrey, platformGrey);
            oList[1] = new Obstacle(250, 300, 200, 50, platformWhite, platformWhite);
            oList[2] = new Obstacle(400, 200, 200, 50, platformBlack, platformBlack);
            oList[3] = new Obstacle(550, 100, 200, 50, platformGrey, platformGrey);


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //set our keyboardstate tracker update can change the gamestate on every cycle
            controls.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Up, down, left, right affect the coordinates of the sprite

            player1.Update(controls, gameTime, oList);

            if (controls.onPress(Keys.Space, Buttons.A) && inversionManager.IsWorldInverted)
            {
                backSong_i.Volume = 0;
                backSong.Volume = 1;

            }
            else if (!inversionManager.IsWorldInverted && controls.onPress(Keys.Space, Buttons.A))
            {
                backSong_i.Volume = 1;
                backSong.Volume = 0;

            }

            if (player1.victory == true)
            {
                backSong_i.Volume = 0;
                backSong.Volume = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (!inversionManager.IsWorldInverted)
            {
                GraphicsDevice.Clear(Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>("Goal"), new Rectangle(700, 50, 50, 50), Color.White);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(Content.Load<Texture2D>("Goal_i"), new Rectangle(700, 50, 50, 50), Color.White);
            }

            // TODO: Add your drawing code here
            for (int i = 0; i < 4; i++) {
                oList[i].Draw(spriteBatch, player1);
            }
            player1.Draw(spriteBatch);

            if (player1.victory == true)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Victory"), new Rectangle(50, 50, 700, 400), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}

