using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Lecture6Examples
{
    public class PianoPlayer : GameObject ,IEmitSound
    {
        private SoundEffectInstance _tuneInstance;
        private SoundEffect _tune;
        private AudioEmitter _emitter;

        #region Mathy things to make piano player orbit
        /// <summary>
        /// The sample moves the emitter around in a circle with radius away from the listener.
        /// </summary>
        private float _radiusFromListener = 12000f;
        private float _currentRotation = 0f;
        /// <summary>
        /// rotations per second.
        /// </summary>
        private float _rotationSpeed = (float)Math.PI / 12; 
        #endregion

        public PianoPlayer(Game game)
        {
            _tune = game.Content.Load<SoundEffect>("piano");
            _tuneInstance = _tune.CreateInstance();
            _tuneInstance.IsLooped = true;

            _emitter = new AudioEmitter();
            _emitter.Forward = Vector3.Backward;
            _emitter.Up = Vector3.Up;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            #region Mathy things to make piano player orbit
            _currentRotation += _rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector3 newPos = new Vector3(
                (float)Math.Sin(_currentRotation), 0f,
                (float)Math.Cos(_currentRotation));
            _emitter.Position = newPos;

            #endregion
        }

        public AudioEmitter Emitter
        {
            get { return _emitter; }
        }

        public void PlaySounds(AudioListener listener)
        {
            //If you do not apply 3D before using play, the sound will not be flagged as 3D.
            _tuneInstance.Apply3D(listener, _emitter);
            if (_tuneInstance.State == SoundState.Stopped)
            {
                _tuneInstance.Play();
            }
        }

        public override string GetState()
        {
            return "I'm playing music!, my emitter is at" + _emitter.Position.ToString();
        }

        
    }
}
