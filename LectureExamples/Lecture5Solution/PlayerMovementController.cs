using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SolutionLecture5
{
    public class PlayerMovementController
    {
        private PlayerCharacter _controlled;
        private Keys _left;
        private Keys _right;
        private Keys _up;
        private Keys _down;
        private InputManager _input;

        public PlayerMovementController(PlayerCharacter toControl, Keys up, Keys right, Keys down, Keys left)
        {
            _controlled = toControl;
            _down = down;
            _left = left;
            _up = up;
            _right = right;
        }

        public void Initialize(Game game, InputManager input)
        {
            _input = input;
        }

        public void Update(GameTime gameTime)
        {
            Point positionChange = Point.Zero;
            if (_input.IsKeyDown(_right))
            {
                positionChange.X += 4;
            }
            if (_input.IsKeyDown(_left))
            {
                positionChange.X -= 4;
            }
            if (_input.IsKeyDown(_down))
            {
                positionChange.Y += 4;
            }
            if (_input.IsKeyDown(_up))
            {
                positionChange.Y -= 4;
            }

            _controlled.Position = new Point(
                _controlled.Position.X + positionChange.X, 
                _controlled.Position.Y + positionChange.Y);
        }
    }
}
