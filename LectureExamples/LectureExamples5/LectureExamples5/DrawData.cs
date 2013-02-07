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
    public class DrawData
    {
        public Texture2D Art { get; set; }
        public Rectangle Destination { get; set; }
        public Rectangle Source { get; set; }
        public Color BlendColor { get; set; }

        public DrawData(Texture2D art)
        {
            Art = art;
            Destination = art.Bounds;
            Source = art.Bounds;
            BlendColor = Color.White;
        }
    }
}
