using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SolutionLecture5
{
    public class PlayerCharacter : GameObject
    {
        public PlayerCharacter(Point position)
        {
            this.Position = position;
        }

        public override void Initialize(Game game)
        {
            base.Initialize(game);
            Texture2D playerTexture = game.Content.Load<Texture2D>(
                "Character Horn Girl");
            Drawable = new DrawData(
                playerTexture,
                playerTexture.Bounds, Position, 
                playerTexture.Bounds.Width, playerTexture.Bounds.Height 
                );
        }

        public override void Update(GameTime gameTime)
        {
            Drawable.WorldPosition = _position;
            base.Update(gameTime);
        }
    }
}
