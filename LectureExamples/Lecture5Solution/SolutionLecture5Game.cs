using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SolutionLecture5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SolutionLecture5Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<GameObject> _gameObjects = new List<GameObject>();
        private InputManager _input;
        private PlayerMovementController _player1Controller;
        private PlayerMovementController _player2Controller;


        public SolutionLecture5Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _input = new InputManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            _input.Initialize(this);

            _gameObjects.Add(new PlayerCharacter(new Point(100, 50)));
            _player1Controller = new PlayerMovementController(_gameObjects[0] as PlayerCharacter, Keys.W, Keys.D, Keys.S, Keys.A);
            _gameObjects.Add(new PlayerCharacter(new Point(300, 100)));
            _player2Controller = new PlayerMovementController(_gameObjects[1] as PlayerCharacter, Keys.Up, Keys.Right, Keys.Down, Keys.Left);
            _player1Controller.Initialize(this, _input);
            _player2Controller.Initialize(this, _input);
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

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if(_gameObjects[i] != null)
                    _gameObjects[i].Initialize(this);
            }

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            _input.Update(gameTime);
            _player1Controller.Update(gameTime);
            _player2Controller.Update(gameTime);
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if(_gameObjects[i] != null)
                    _gameObjects[i].Update(gameTime);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Goldenrod);
            spriteBatch.Begin();
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (_gameObjects[i] != null && _gameObjects[i].Drawable != null)
                {
                    drawDrawable(_gameObjects[i].Drawable);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void drawDrawable(DrawData toDraw)
        {
            Rectangle destination = new Rectangle(
                toDraw.WorldPosition.X, toDraw.WorldPosition.Y,
                toDraw.DrawWidth, toDraw.DrawHeight);
            spriteBatch.Draw(toDraw.Art, destination, toDraw.Source, Color.White);
        }
    }
}
