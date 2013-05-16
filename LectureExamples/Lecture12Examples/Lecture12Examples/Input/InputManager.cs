using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PG515.Input
{
    /// <summary>
    /// Implements the IInputService interface and the GameComponent class.
    /// For information about functions, read comments in IInputService.
    /// </summary>
    public class InputManager : GameComponent, IInputService
    {

        #region MouseProperties
        public Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouseState.X, currentMouseState.Y);
            }
        }

        public Vector2 MousePreviousPosition
        {
            get
            {
                return new Vector2(previousMouseState.X, previousMouseState.Y);
            }
        }

        public int ScrollWheelChange
        {
            get
            {
                return previousMouseState.ScrollWheelValue - currentMouseState.ScrollWheelValue;
            }
        }

        public int ScrollWheelTotal
        {
            get
            {
                return currentMouseState.ScrollWheelValue;
            }
        }
        #endregion

        #region GamePadProperties
        public bool P1Connected
        {
            get { return currentGPState[0].IsConnected; }
        }
        public GamePadThumbSticks SticksP1
        {
            get { return currentGPState[0].ThumbSticks; }
        }
        public GamePadThumbSticks PrevSticksP1
        {
            get { return previousGPState[0].ThumbSticks; }
        }
        public GamePadTriggers TriggersP1
        {
            get { return currentGPState[0].Triggers; }
        }
        public GamePadTriggers PrevTriggersP1
        {
            get { return previousGPState[0].Triggers; }
        }
        public bool P2Connected
        {
            get { return currentGPState[1].IsConnected; }
        }
        public GamePadThumbSticks SticksP2
        {
            get { return currentGPState[1].ThumbSticks; }
        }
        public GamePadThumbSticks PrevSticksP2
        {
            get { return previousGPState[1].ThumbSticks; }
        }
        public GamePadTriggers TriggersP2
        {
            get { return currentGPState[1].Triggers; }
        }
        public GamePadTriggers PrevTriggersP2
        {
            get { return previousGPState[1].Triggers; }
        }
        public bool P3Connected
        {
            get { return currentGPState[2].IsConnected; }
        }
        public GamePadThumbSticks SticksP3
        {
            get { return currentGPState[2].ThumbSticks; }
        }
        public GamePadThumbSticks PrevSticksP3
        {
            get { return previousGPState[2].ThumbSticks; }
        }
        public GamePadTriggers TriggersP3
        {
            get { return currentGPState[2].Triggers; }
        }
        public GamePadTriggers PrevTriggersP3
        {
            get { return previousGPState[2].Triggers; }
        }
        public bool P4Connected
        {
            get { return currentGPState[3].IsConnected; }
        }
        public GamePadThumbSticks SticksP4
        {
            get { return currentGPState[3].ThumbSticks; }
        }
        public GamePadThumbSticks PrevSticksP4
        {
            get { return previousGPState[3].ThumbSticks; }
        }
        public GamePadTriggers TriggersP4
        {
            get { return currentGPState[3].Triggers; }
        }
        public GamePadTriggers PrevTriggersP4
        {
            get { return previousGPState[3].Triggers; }
        }
        #endregion
        private MouseState previousMouseState;
        private MouseState currentMouseState;
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        private GamePadState[] previousGPState;
        private GamePadState[] currentGPState;

        private readonly PlayerIndex[] playerIndexHelper
            = {PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four};



        public InputManager(Game game) : 
            base(game)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
            currentGPState = new GamePadState[4];
            previousGPState = new GamePadState[4];
            for(int i = 0; i < 4; i++)
            {
                currentGPState[i] = GamePad.GetState(playerIndexHelper[i]);
                previousGPState[i] = currentGPState[i]; 
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            //Update each and every one of the controllers.
            for (int i = 0; i < 4; i++)
            {
                previousGPState[i] = currentGPState[i];
                currentGPState[i] = GamePad.GetState(playerIndexHelper[i]);
            }
            
            base.Update(gameTime);
        }

        public bool IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case(MouseButtons.leftButton):
                    {
                        if(currentMouseState.LeftButton == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case(MouseButtons.rightButton):
                    {
                        if(currentMouseState.RightButton == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case(MouseButtons.middleButton):
                    {
                        if(currentMouseState.MiddleButton == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case(MouseButtons.xButtoneOne):
                    {
                        if(currentMouseState.XButton1 == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case(MouseButtons.xButtonTwo):
                    {
                        if(currentMouseState.XButton2 == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
            }
            return false;
        }

        /// <summary>
        /// Same as IsButtonDown(Buttons, playerIndex), uses the int(-1) directly (faster than playerIndex)
        /// </summary>
        /// <param name="button">the button to check</param>
        /// <param name="playerIndex">Index of the player (1-4)</param>
        /// <returns>true if down</returns>
        public bool IsButtonDown(Buttons button, int playerIndex)
        {
            if(playerIndex > 4) return false;
            return currentGPState[playerIndex - 1].IsButtonDown(button);
        }

        public bool IsButtonDown(Buttons button, PlayerIndex playerIndex)
        {
            for(int i = 0; i < 4; i++)
            {
                if (playerIndexHelper[i] == playerIndex)
                {
                    return currentGPState[i].IsButtonDown(button); 
                }
            }
            return false;
        }
        

        public bool IsButtonUp(MouseButtons button)
        {
            switch (button)
            {
                case (MouseButtons.leftButton):
                    {
                        if (currentMouseState.LeftButton == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.rightButton):
                    {
                        if (currentMouseState.RightButton == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.middleButton):
                    {
                        if (currentMouseState.MiddleButton == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.xButtoneOne):
                    {
                        if (currentMouseState.XButton1 == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.xButtonTwo):
                    {
                        if (currentMouseState.XButton2 == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
            }

            return false;

        }
        /// <summary>
        /// Same as IsButtonUp(Buttons, playerIndex), uses the int(-1) directly (faster than playerIndex)
        /// </summary>
        /// <param name="button">the button to check</param>
        /// <param name="playerIndex">Index of the player (1-4)</param>
        /// <returns>true if up</returns>
        public bool IsButtonUp(Buttons button, int playerIndex)
        {
            if (playerIndex > 4) return false;
            return currentGPState[playerIndex - 1].IsButtonUp(button);
        }

        public bool IsButtonUp(Buttons button, PlayerIndex playerIndex)
        {
            for (int i = 0; i < 4; i++)
            {
                if (playerIndexHelper[i] == playerIndex)
                {
                    return currentGPState[i].IsButtonUp(button);
                }
            }
            return false;
        }

        public bool IsButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case (MouseButtons.leftButton):
                    {
                        if (currentMouseState.LeftButton == ButtonState.Pressed 
                            && previousMouseState.LeftButton == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.rightButton):
                    {
                        if (currentMouseState.RightButton == ButtonState.Pressed
                            && previousMouseState.RightButton == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.middleButton):
                    {
                        if (currentMouseState.MiddleButton == ButtonState.Pressed
                            && previousMouseState.MiddleButton == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.xButtoneOne):
                    {
                        if (currentMouseState.XButton1 == ButtonState.Pressed
                            && previousMouseState.XButton1 == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.xButtonTwo):
                    {
                        if (currentMouseState.XButton2 == ButtonState.Pressed
                            &&previousMouseState.XButton2 == ButtonState.Released)
                        {
                            return true;
                        }
                        break;
                    }
            }

            return false;

        }

        public bool IsButtonReleased(MouseButtons button)
        {
            switch (button)
            {
                case (MouseButtons.leftButton):
                    {
                        if (currentMouseState.LeftButton == ButtonState.Released
                            && previousMouseState.LeftButton == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.rightButton):
                    {
                        if (currentMouseState.RightButton == ButtonState.Released
                            && previousMouseState.RightButton == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.middleButton):
                    {
                        if (currentMouseState.MiddleButton == ButtonState.Released
                            && previousMouseState.MiddleButton == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.xButtoneOne):
                    {
                        if (currentMouseState.XButton1 == ButtonState.Released
                            && previousMouseState.XButton1 == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
                case (MouseButtons.xButtonTwo):
                    {
                        if (currentMouseState.XButton2 == ButtonState.Released
                            && previousMouseState.XButton2 == ButtonState.Pressed)
                        {
                            return true;
                        }
                        break;
                    }
            }

            return false;
        }

        /// <summary>
        /// Same as IsButtonPressed(Buttons, playerIndex), uses the int(-1) directly (faster than playerIndex)
        /// </summary>
        /// <param name="button">the button to check</param>
        /// <param name="playerIndex">Index of the player (1-4)</param>
        /// <returns>true if pressed this frame</returns>
        public bool IsButtonPressed(Buttons button, int playerIndex)
        {
            if (playerIndex > 4) return false;
            return currentGPState[playerIndex - 1].IsButtonDown(button) &&
                previousGPState[playerIndex - 1].IsButtonUp(button);
        }

        public bool IsButtonPressed(Buttons button, PlayerIndex playerIndex)
        {
            for (int i = 0; i < 4; i++)
            {
                if (playerIndexHelper[i] == playerIndex)
                {
                    return currentGPState[i].IsButtonDown(button) && previousGPState[i].IsButtonUp(button);
                }
            }
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key) && previousKeyboardState.IsKeyDown(key);
        }
    }
}
