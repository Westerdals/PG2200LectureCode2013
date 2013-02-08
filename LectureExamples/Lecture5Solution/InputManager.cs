using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SolutionLecture5
{
    public class InputManager
    {
        public Vector2 MousePosition
        {
            get
            {
                return new Vector2(_curMouseState.X, _curMouseState.Y);
            }
        }

        public Vector2 MousePreviousPosition
        {
            get
            {
                return new Vector2(_prevMouseState.X, _prevMouseState.Y);
            }
        }

        public int ScrollWheelChange
        {
            get
            {
                return _prevMouseState.ScrollWheelValue - _curMouseState.ScrollWheelValue;
            }
        }

        public int ScrollWheelTotal
        {
            get
            {
                return _curMouseState.ScrollWheelValue;
            }
        }

        private KeyboardState _curKeyState;
        private KeyboardState _prevKeystate;
        private MouseState _curMouseState;
        private MouseState _prevMouseState;

        public void Initialize(Game game)
        {
            _curKeyState = Keyboard.GetState();
            _curMouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            _prevMouseState = _curMouseState;
            _curMouseState = Mouse.GetState();
            _prevKeystate = _curKeyState;
            _curKeyState = Keyboard.GetState();
        }


        public bool IsKeyDown(Keys key)
        {
            return _curKeyState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return _curKeyState.IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return _curKeyState.IsKeyDown(key) && _prevKeystate.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return _curKeyState.IsKeyUp(key) && _prevKeystate.IsKeyDown(key);
        }
    }
}
