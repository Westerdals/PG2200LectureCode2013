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

namespace Lecture7Examples.Animation
{
    public class AnimationController
    {
        public AnimationDrawData _drawData;
        private float _stepTime;
        private float _timer;

        public AnimationController(AnimationDrawData data,
            float animationStepTime)
        {
            _drawData = data;
            _stepTime = animationStepTime;
        }

        public virtual void Update(GameTime gameTime)
        {
            _timer += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer >= _stepTime)
            {
                _timer -= _stepTime;
                _drawData.CurrentFrame++;
            }
        }
    }
}
