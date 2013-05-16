using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PG515.Input
{
    /// <summary>
    /// A simple enum used to define buttons on a mouse.
    /// The mouse does not have this functionality from before.
    /// </summary>
    public enum MouseButtons
    {
        leftButton,
        rightButton,
        middleButton,
        xButtoneOne,
        xButtonTwo
    }
    /// <summary>
    /// Defines an interface 
    /// </summary>
    public interface IInputService
    {
        #region Mouse
        /// <summary>
        /// Get the current position of the mouse.
        /// </summary>
        Vector2 MousePosition {get; }
        /// <summary>
        /// Get the previous position of the mouse.
        /// </summary>
        Vector2 MousePreviousPosition { get; }
        /// <summary>
        /// The change in scrollwheel value since last frame.
        /// </summary>
        int ScrollWheelChange { get; }
        /// <summary>
        /// The total scrollwheel value.
        /// </summary>
        int ScrollWheelTotal { get; }
        /// <summary>
        /// Check if the button provided is currently being held down.
        /// </summary>
        /// <param name="button">The button to check for.</param>
        /// <returns>True if the button is down.</returns>
        bool IsButtonDown(MouseButtons button);
        /// <summary>
        /// Check if the button is up.
        /// </summary>
        /// <param name="button">The button to check for.</param>
        /// <returns>True if the button is up.</returns>
        bool IsButtonUp(MouseButtons button);
        /// <summary>
        /// Check if the button was pressed down this frame.
        /// </summary>
        /// <param name="button">The button to check for.</param>
        /// <returns>True if the button was pressed.</returns>
        bool IsButtonPressed(MouseButtons button);
        /// <summary>
        /// Check if the button was released this frame.
        /// </summary>
        /// <param name="button">The button to check for.</param>
        /// <returns>True if the button was released.</returns>
        bool IsButtonReleased(MouseButtons button);
     

        #endregion

        #region Keyboard
        /// <summary>
        /// Check if the key provided is currently being held down.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if the key is down.</returns>
        bool IsKeyDown(Keys key);
        /// <summary>
        /// Check if the key is up.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if the key is up.</returns>
        bool IsKeyUp(Keys key);
        /// <summary>
        /// Check if the key was pressed down this frame.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if the key was pressed.</returns>
        bool IsKeyPressed(Keys key);
        /// <summary>
        /// Check if the key was released this frame.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if the key was released.</returns>
        bool IsKeyReleased(Keys key);

        #endregion

        #region Gamepad

         bool P1Connected { get; }

         GamePadThumbSticks SticksP1 { get; }

         GamePadThumbSticks PrevSticksP1 { get; }

         GamePadTriggers TriggersP1 { get; }

         GamePadTriggers PrevTriggersP1 { get; }

         bool P2Connected { get; }

         GamePadThumbSticks SticksP2 { get; }

         GamePadThumbSticks PrevSticksP2 { get; }

         GamePadTriggers TriggersP2 { get; }

         GamePadTriggers PrevTriggersP2 { get; }

         bool P3Connected { get; }

         GamePadThumbSticks SticksP3 { get; }

         GamePadThumbSticks PrevSticksP3 { get; }

         GamePadTriggers TriggersP3 { get; }

         GamePadTriggers PrevTriggersP3 { get; }

         bool P4Connected { get; }

         GamePadThumbSticks SticksP4 { get; }

         GamePadThumbSticks PrevSticksP4 { get; }

         GamePadTriggers TriggersP4 { get; }

         GamePadTriggers PrevTriggersP4 { get; }

        bool IsButtonDown(Buttons button, int playerIndex);
        bool IsButtonUp(Buttons button, int playerIndex);
        bool IsButtonPressed(Buttons button, int playerIndex);
        bool IsButtonDown(Buttons button, PlayerIndex playerIndex);
        bool IsButtonUp(Buttons button, PlayerIndex playerIndex);
        bool IsButtonPressed(Buttons button, PlayerIndex playerIndex);

        #endregion
    }
}
