using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SolutionLecture5
{
    public class DrawData
    {
        public Rectangle Source { get { return _source; } }
        public Point WorldPosition { get; set;}
        public Texture2D Art { get { return _texture; } }
        public int DrawWidth
        {
            get { return _drawWidth; }
            set
            {
                if(value > 0)
                    _drawWidth = value;
            }
        }

        public int DrawHeight
        {
            get { return _drawHeight; }
            set
            {
                if(value > 0)
                    _drawHeight = value;
            }
        }

        private int _drawHeight;
        private int _drawWidth;
        private Rectangle _source;
        private Texture2D _texture;

        public DrawData(Texture2D texture, Rectangle source,
            Point position, int drawWidth, int drawHeight)
        {
            _texture = texture;
            _source = source;
            WorldPosition = position;
            DrawWidth = drawWidth;
            DrawHeight = drawHeight;
        }
    }
}
