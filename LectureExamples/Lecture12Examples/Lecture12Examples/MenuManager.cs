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
using PG515.Input;


namespace Lecture12Examples
{
    public delegate void MenuTriggeredDelegate(object source);

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public event MenuTriggeredDelegate StartGameSelected;
        public event MenuTriggeredDelegate OptionsSelected;
        public event MenuTriggeredDelegate QuitSelected;

        private SpriteFont _font;
        private IInputService _input;
        private SpriteBatch _spriteBatch;

        private int _selection;
        private string[] _menuItems = {"Start", "Options", "Quit"};
        private Vector2[] _menuItemSizes;
        private float _spacing = 18;

        public MenuManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            _input = (IInputService)
                Game.Services.GetService(typeof (IInputService));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _font = Game.Content.Load<SpriteFont>("Arial");
            _menuItemSizes = new Vector2[_menuItems.Length];

            for (int i = 0; i < _menuItems.Length; i++)
            {
                _menuItemSizes[i] = _font.MeasureString(_menuItems[i]);
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (_input.IsKeyPressed(Keys.S))
            {
                _selection++;
                if (_selection >= _menuItems.Length)
                    _selection = 0;
            }
            if (_input.IsKeyPressed(Keys.W))
            {
                _selection--;
                if (_selection < 0)
                    _selection = _menuItems.Length - 1;
            }

            if (_input.IsKeyPressed(Keys.Enter))
                doSelection();

            base.Update(gameTime);
        }

        private void doSelection()
        {
            switch (_selection)
            {
                case (0):
                    {
                        if (StartGameSelected != null)
                            StartGameSelected(this);
                        break;
                    }
                case (2):
                    {
                        if (QuitSelected != null) QuitSelected(this);
                        break;
                    }
            }
                
        }

        public override void Draw(GameTime gameTime)
        {
            float totalOffset = _spacing*4;
            base.Draw(gameTime);
            _spriteBatch.Begin();
            for (int i = 0; i < _menuItems.Length; i++)
            {
                _spriteBatch.DrawString(_font, _menuItems[i],
                    new Vector2(Game.Window.ClientBounds.Width / 2 
                        - _menuItemSizes[i].X / 2, totalOffset),
                        (i == _selection ? Color.Black : Color.Beige)
                    );
                totalOffset += _menuItemSizes[i].Y + _spacing;
            }

            _spriteBatch.End();
        }
    }
}
