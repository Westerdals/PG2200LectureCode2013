using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace LectureExamples5
{
    public class PlayerCharacter : GameObject
    {
        private KeyboardState _currentState;

        public override void Initialize(Game game)
        {
            _currentState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            _currentState = Keyboard.GetState();
            if (_currentState.IsKeyDown(Keys.Right))
            {
                Position = new Point(
                    Position.X + 4, Position.Y);
            }
            base.Update(gameTime);
            
        }
    }
}
