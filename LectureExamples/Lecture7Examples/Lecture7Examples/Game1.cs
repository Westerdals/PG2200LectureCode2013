using System;
using System.Collections.Generic;
using System.Linq;
using LectureExamples5;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Lecture7Examples.Animation;

namespace Lecture7Examples
{
    public delegate void EmptyDelegate();
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public event EmptyDelegate OnEndRun;
        public event EmptyDelegate OnBeginRun;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _spriteSheet;
        private DrawData _sheet;
        private float _animationTimer;
        private int _frame;
        private int _currentAnim = 1;

        List<AnimationController> _controllers = new List<AnimationController>(); 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DrawableGameComponent renderer = new WorldMapManager(this);
            Components.Add(renderer);
            Services.AddService(typeof(IDrawSprites),renderer);
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

            base.Initialize();
            OnEndRun += endRunEventHandler;
            OnEndRun += Exit;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Texture2D blockArt = Content.Load<Texture2D>("Brown Block");
            _spriteSheet = Content.Load<Texture2D>("spritesheet");


            IDrawSprites renderer = (IDrawSprites)
                                    Services.GetService(typeof (IDrawSprites));
            for (int i = 0; i < 20; i++)
            {
                renderer.AddDrawable(new DrawData(
                    blockArt, new Rectangle(
                        blockArt.Bounds.Width * (i%4), //X
                        (i/4)*blockArt.Bounds.Height / 2, //Y
                        blockArt.Bounds.Width,//Width
                        blockArt.Bounds.Height)));//Height
            }
            _sheet = new DrawData(_spriteSheet);
            _sheet.Source = new Rectangle(0,0,24,32);
            _sheet.Destination = new Rectangle(0,0,24*4,32*4);
            renderer.AddDrawable(_sheet);

           AnimationDrawData animated = new AnimationDrawData(
               _spriteSheet, new Rectangle(0,0, 24, 32),
               new Point(100,100),24*4,32*4,3);
            renderer.AddDrawable(animated);
            _controllers.Add(
                new AnimationController(animated, 0.3f));
        }

        public void endRunEventHandler()
        {
            Console.WriteLine("End run!");
        }

        public void quitGameEventHandler()
        {
            Exit();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _animationTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (_animationTimer >= 0.5f)
            {
                _frame++;
                _animationTimer = 0f;
                if (_frame == 3)
                    _frame = 0;
                _sheet.Source = new Rectangle(_frame * 24,_currentAnim*32,24,32);
            }

            foreach (AnimationController controller in _controllers)
            {
                controller.Update(gameTime);
            }

            KeyboardState keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.Space))
            {
                if(OnEndRun != null)
                    OnEndRun();
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
