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

using System.IO;
using System.Xml.Serialization;

using Lecture7Examples.Animation;
using Lecture7Examples;

namespace Lecture9Examples
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int frame_width = 24;
        public int frame_height = 32;

        private AnimationController _controller;
        private AnimationDrawData _drawable;

        private KeyboardState _prevState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            SpriteComponent spriteDrawer = new SpriteComponent(this);
            Services.AddService(typeof(IDrawSprites), spriteDrawer);
            Components.Add(spriteDrawer);

            AnimationLoader animationLoader = new AnimationLoader(this, null);
            Services.AddService(typeof(AnimationLoader), animationLoader);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _prevState = Keyboard.GetState();

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

            _drawable =
                ((AnimationLoader) Services.GetService(
                    typeof (AnimationLoader))).CreateAnimationDrawable("Purple_Up");
            _controller = new AnimationController(_drawable, 0.2f);
            
            ((IDrawSprites)Services.GetService(typeof(IDrawSprites))
                ).AddDrawable(_drawable);
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
            KeyboardState currentState = Keyboard.GetState();
            _controller.Update(gameTime);

            if(currentState.IsKeyDown(Keys.D) && _prevState.IsKeyUp(Keys.D))
            {
                ((AnimationLoader) Services.GetService(
                    typeof (AnimationLoader))).SetupAnimation(_drawable,
                                                              "Purple_Right");
            }
            if(currentState.IsKeyDown(Keys.A) && _prevState.IsKeyUp(Keys.A))
            {
                ((AnimationLoader) Services.GetService(
                    typeof (AnimationLoader))).SetupAnimation(_drawable,
                                                              "Purple_Left");
            }
            _prevState = currentState;
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

        protected override void EndRun()
        {
            base.EndRun();
            return;
            
            List<AnimationPersistentInfo> animations =
                new List<AnimationPersistentInfo>();
            animations.Add(new AnimationPersistentInfo
                               {
                                   AnimationName = "Purple_Up",
                                   FrameWidth = frame_width,
                                   FrameHeight = frame_height,
                                   NumberOfFrames = 3,
                                   StartOffset = new Point(3*frame_width,0),
                                   TexturePath = "spritesheet"
                               });
            animations.Add(new AnimationPersistentInfo
                            {
                                AnimationName = "Purple_Right",
                                FrameWidth = frame_width,
                                FrameHeight = frame_height,
                                NumberOfFrames = 3,
                                StartOffset = new Point(3 * frame_width, frame_height),
                                TexturePath = "spritesheet"
                            });
            animations.Add(new AnimationPersistentInfo
                        {
                            AnimationName = "Purple_Left",
                            FrameWidth = frame_width,
                            FrameHeight = frame_height,
                            NumberOfFrames = 3,
                            StartOffset = new Point(3 * frame_width, frame_height*3),
                            TexturePath = "spritesheet"
                        });
            FileStream stream = File.Create("Animations.xml");
            XmlSerializer serializer = new XmlSerializer(
                typeof(List<AnimationPersistentInfo>));
            serializer.Serialize(stream, animations);
            stream.Close();
        }
    }
}
