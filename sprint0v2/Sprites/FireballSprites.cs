using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Sprites
{
    public class NormalFireballSprite : FireballSprite
    {
        public NormalFireballSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("fireball"), 1, 4, 4, position, SpriteEffects.None, true)
        {

        }
    }
    public class ExplodingFireballSprite : FireballSprite
    {
        public ExplodingFireballSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("fireballExplode"), 1, 3, 3, position, SpriteEffects.None, true)
        {

        }
    }

    public class BowserFireballSprite : FireballSprite
    {
        public BowserFireballSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("bowserFireball"), 1, 2, 2, position, SpriteEffects.FlipHorizontally, true)
        {

        }
    }
        
}
