using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Xml.Serialization;
using System.IO;

namespace Lecture10Examples
{
    public struct GameConfig
    {
        public float PlayerSpeed;
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Vector2 _playerPos;
        private Texture2D _playerArt;
        private Texture2D _grassArt;

        private KeyboardState _prevState;
        private KeyboardState _curState;

        private GameConfig _config;
        private XmlSerializer _configSerializer = new XmlSerializer(typeof(GameConfig));

        public Game1()
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
            _curState = Keyboard.GetState();
            _prevState = _curState;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _playerArt = Content.Load<Texture2D>("Character Cat Girl");
            _grassArt = Content.Load<Texture2D>("Grass Block");
        }

        public void DrawElement(Texture2D toDraw, Vector2 position)
        {
            spriteBatch.Draw(toDraw, position, Color.White);
        }

        public void LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                _config.PlayerSpeed = 100f;
            }
            else
            {
                FileStream configFile = File.OpenRead(path);
                _config = (GameConfig)_configSerializer.Deserialize(configFile);
                configFile.Close();
            }
            
        }

        public void SaveConfig(string path)
        {
            FileStream configFile = File.Open(path, FileMode.Create);
            _configSerializer.Serialize(configFile, _config);
            configFile.Close();
        }

        public void ReloadGame()
        {
            LoadConfig("Config.xml");
            _playerPos = Vector2.Zero;
        }

        public bool IsKeyPressed(Keys key)
        {
            return _curState.IsKeyDown(key) && _prevState.IsKeyUp(key);
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
            _curState = Keyboard.GetState();

            if (_curState.IsKeyDown(Keys.Right))
                _playerPos.X += _config.PlayerSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (_curState.IsKeyDown(Keys.Left))
                _playerPos.X -= _config.PlayerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(IsKeyPressed(Keys.F5))
                ReloadGame();
            if(IsKeyPressed(Keys.F4))
                SaveConfig("Config.xml");

            base.Update(gameTime);
            _prevState = _curState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawElement(_grassArt, Vector2.Zero);

            DrawElement(_playerArt, _playerPos);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
