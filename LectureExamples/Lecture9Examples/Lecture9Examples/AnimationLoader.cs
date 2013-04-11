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

using System.IO;
using System.Xml.Serialization;

using Lecture7Examples.Animation;

namespace Lecture9Examples
{
    public struct AnimationPersistentInfo
    {
        public string TexturePath;
        public string AnimationName;
        public int NumberOfFrames;
        public Point StartOffset;
        public int FrameWidth;
        public int FrameHeight;
    }

    public class AnimationLoader
    {
        public string[] AnimationLocations;
        private Game _game;
        private Dictionary<string, AnimationPersistentInfo>
            _loadedAnimations = new Dictionary<string, AnimationPersistentInfo>();

        public AnimationLoader(Game game,
            string[] animationLocations)
        {
            _game = game;
            AnimationLocations = animationLocations;
        }


        public bool SetupAnimation(AnimationDrawData toConfigure, 
            string animationName)
        {
            AnimationPersistentInfo animationInfo;
            if (!getAnimationInfo(animationName, out animationInfo))
                return false;

            Rectangle source = new Rectangle(
                animationInfo.StartOffset.X, animationInfo.StartOffset.Y,
                animationInfo.FrameWidth, animationInfo.FrameHeight);
            
            toConfigure.ChangeAnimationData(
                _game.Content.Load<Texture2D>(animationInfo.TexturePath),
                source, animationInfo.NumberOfFrames
                );
            return true;
        }

        public AnimationDrawData CreateAnimationDrawable(string animationName)
        {
            AnimationPersistentInfo animationInfo;
            if (!getAnimationInfo(animationName, out animationInfo))
                return null;
            Texture2D temp =
                _game.Content.Load<Texture2D>(animationInfo.TexturePath);
            Rectangle source = new Rectangle(
                animationInfo.StartOffset.X, animationInfo.StartOffset.Y,
                animationInfo.FrameWidth, animationInfo.FrameHeight);

            return new AnimationDrawData(temp,
                source, Point.Zero,animationInfo.FrameWidth * 4,
                animationInfo.FrameHeight * 4, animationInfo.NumberOfFrames);

        }

        private bool getAnimationInfo(string animationName, out 
            AnimationPersistentInfo animationInfo)
        {
            if (!_loadedAnimations.TryGetValue(animationName, out animationInfo))
            {
                tryReadAnimationsFromFile();
                if (!_loadedAnimations.TryGetValue(animationName, out animationInfo))
                    return false;
            }
            return true;
        }

        private void tryReadAnimationsFromFile()
        {
            List<AnimationPersistentInfo> animations = null;

            FileStream toRead = File.OpenRead("Animations.xml");
            XmlSerializer deserializer = new XmlSerializer(
                typeof(List<AnimationPersistentInfo>));
            animations = (List<AnimationPersistentInfo>)
                         deserializer.Deserialize(toRead);

            foreach (AnimationPersistentInfo animation in animations)
            {
                if(!_loadedAnimations.ContainsKey(animation.AnimationName))
                    _loadedAnimations.Add(animation.AnimationName, animation);
            }
        }


    }
}
