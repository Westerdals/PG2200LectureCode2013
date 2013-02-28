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
using LectureExamples5;

namespace Lecture7Examples
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class WorldMapManager : DrawableGameComponent, IDrawSprites
    {
        private SpriteBatch _drawer;
        protected List<DrawData> _toDraw = new List<DrawData>();

        private Point _cameraPosition;

        public WorldMapManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void LoadContent()
        {
            base.LoadContent();
    
            _drawer = new SpriteBatch(this.Game.GraphicsDevice);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            _cameraPosition = new Point(0,0);

            base.Initialize();
        }
        public void AddDrawable(DrawData drawable)
        {
            if (drawable == null || _toDraw.Contains(drawable))
            {
                Console.WriteLine("Unable to add drawable!");
                return;
            }
            _toDraw.Add(drawable);
        }

        public void RemoveDrawable(DrawData toRemove)
        {
            _toDraw.Remove(toRemove);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _cameraPosition.X += 4;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _cameraPosition.X -= 4;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _cameraPosition.Y -= 4;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
                _cameraPosition.Y += 4;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _drawer.Begin();
            foreach (DrawData drawData in _toDraw)
            {
                drawElement(drawData);
            }
            _drawer.End();
        }

        protected virtual void drawElement(DrawData drawable)
        {
            Rectangle worldDestination = drawable.Destination;
            worldDestination.X -= _cameraPosition.X;
            worldDestination.Y -= _cameraPosition.Y;

            _drawer.Draw(drawable.Art, worldDestination,
                drawable.Source, Color.White);
        }
    }
}
