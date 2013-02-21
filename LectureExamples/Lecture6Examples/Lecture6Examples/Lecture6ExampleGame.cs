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

namespace Lecture6Examples
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Lecture6ExampleGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SpriteFont _arialFont;
        private float _textHeight;

        private MouseState _mouseState;
        private Vector2 _mousePos;
        private string _mouseInfo;
        private Vector2 _mouseInfoTextSize;

        List<GameObject> _gameObjects = new List<GameObject>();
        private AudioListener _listener;

        public Lecture6ExampleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            for (int i = 0; i < 3; i++)
            {
                _gameObjects.Add(new GameObject());
                _gameObjects.Add(new Bonfire());
            }
            _listener = new AudioListener();
            _listener.Position = Vector3.Zero; //It's already set to this but never mind :)
            _listener.Forward = Vector3.Forward;
            _listener.Up = Vector3.Up;
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
            _arialFont = Content.Load<SpriteFont>("Arial");
            _textHeight = _arialFont.MeasureString("H").Y;
            _gameObjects.Add(new PianoPlayer(this));
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _mouseState = Mouse.GetState();
            _mousePos = new Vector2(_mouseState.X, _mouseState.Y);
            _mouseInfo = _mousePos.ToString();
            _mouseInfoTextSize = _arialFont.MeasureString(_mouseInfo);
            _mouseInfoTextSize.X = _mouseInfoTextSize.X / 2.0f;
            _mouseInfoTextSize.Y = 0;

            foreach (GameObject gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
                if (_mousePos.X > 300)
                {
                    ICatchFire burnable = gameObject as ICatchFire;
                    if (burnable != null)
                    {
                        burnable.LightOnFire();
                    }
                }

                IEmitSound player = gameObject as IEmitSound;
                if(player != null)
                    player.PlaySounds(_listener);
            }
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                spriteBatch.DrawString(_arialFont, _gameObjects[i].GetState(),
                    new Vector2(0,i*_textHeight),Color.Red);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
