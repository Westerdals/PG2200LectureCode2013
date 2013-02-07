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

namespace LectureExamples5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ObjectOrientedGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<DrawData> _toDraw = new List<DrawData>();

        public ObjectOrientedGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D block = 
                Content.Load<Texture2D>("Brown Block");
            
            for (int i = 0; i < 10; i++)
            {
                _toDraw.Add(new DrawData(block));
                Rectangle temp = _toDraw[i].Destination;
                temp.X += i*_toDraw[i].Destination.Width;
                _toDraw[i].Destination = temp;
            }
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (DrawData toDraw in _toDraw)
            {
                drawElement(toDraw);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Uses spritebatch to draw the provided element.
        /// Note: Assumes spritebatch.Begin() !
        /// </summary>
        /// <param name="toDraw"></param>
        private void drawElement(DrawData toDraw)
        {
            spriteBatch.Draw(
                toDraw.Art, toDraw.Destination,
                toDraw.Source,toDraw.BlendColor);
        }
    }
}
