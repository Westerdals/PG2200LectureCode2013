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
using LectureExamples5;

namespace Lecture7Examples.Animation
{
    public class AnimationDrawData : DrawData
    {
        public int NumberOfFrames { get; set; }
        public int CurrentFrame
        {
            get { return _currentFrame; }
            set 
            { 
                _currentFrame = value;
                if (_currentFrame >= NumberOfFrames)
                    _currentFrame = 0;
                Rectangle newSource = Source;
                newSource.X = _currentFrame*newSource.Width;
                Source = newSource;
            }

        }
        private int _currentFrame;

        public AnimationDrawData(Texture2D animationSheet,
            Rectangle source,Point position, 
            int drawWidth, int drawHeight, int numFrames) : base(
            animationSheet, 
            new Rectangle(position.X,position.Y,drawWidth, drawHeight)
            )
        {
            Source = source;
            NumberOfFrames = numFrames;
        }

    }
}
