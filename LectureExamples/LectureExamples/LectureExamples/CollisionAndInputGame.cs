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

namespace Lecture4Examples
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CollisionAndInputGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int player_movement_speed = 4;
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        private Texture2D _characterArt;
        private Texture2D _stoneArt;

        private Rectangle _characterBox;
        private Rectangle _previousCharacterBox;
        private List<Rectangle> _stones = new List<Rectangle>();
        

        public CollisionAndInputGame()
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
            base.Initialize();
            _currentState = Keyboard.GetState();

            _stones.Add(new Rectangle(200, 140, 80,80));
            _stones.Add(new Rectangle(110, 300, 80,80));
            _stones.Add(new Rectangle(400, 0, 80,80));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
          
            _characterArt = Content.Load<Texture2D>("Character Boy");
            _stoneArt = Content.Load<Texture2D>("Stone Block");
            _characterBox = _characterArt.Bounds;
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
            _previousState = _currentState;
            _currentState = Keyboard.GetState();

            _previousCharacterBox = _characterBox;

            if(_currentState.IsKeyDown(Keys.LeftShift) && 
                _currentState.IsKeyDown(Keys.Escape))
                Exit();

            if (_currentState.IsKeyDown(Keys.Up))
                _characterBox.Y -= player_movement_speed;
            if (_currentState.IsKeyDown(Keys.Right))
                _characterBox.X += player_movement_speed;
            if (_currentState.IsKeyDown(Keys.Left))
                _characterBox.X -= player_movement_speed;
            if (_currentState.IsKeyDown(Keys.Down))
                _characterBox.Y += player_movement_speed;

            doCollisionDetection();

            
            base.Update(gameTime);
        }


        void doCollisionDetection()
        {
            foreach (Rectangle stone in _stones)
            {
                if (stone.Intersects(_characterBox))
                {
                    _characterBox = _previousCharacterBox;
                }
            }
        }

        /// <summary>
        /// Check if key is pressed down this exact frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyPressed(Keys key)
        {
            if (_currentState.IsKeyDown(key) && _previousState.IsKeyUp(key)) 
                return true;
            return false;
        }

        /// <summary>
        /// Checks if key is released this exact frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyReleased(Keys key)
        {
            if (_currentState.IsKeyUp(key) && _previousState.IsKeyDown(key))
                return true;
            return false; 
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);

            spriteBatch.Begin();
            spriteBatch.Draw(_characterArt, _characterBox, Color.White);
            foreach (Rectangle stone in _stones)
            {
                spriteBatch.Draw(_stoneArt, stone, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
