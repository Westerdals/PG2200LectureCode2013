using Microsoft.Xna.Framework;

namespace SolutionLecture5
{
    public class GameObject
    {
        public DrawData Drawable { get; set; }
        protected Point _position;
        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public virtual void Initialize(Game game)
        {
            //Setup your object here!
        }

        public virtual void Update(GameTime gameTime)
        {
            //DO STUFF IN HERE    
        }

    }
}
