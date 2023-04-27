using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Sprites
{
    public class SmallMarioSprite : PlayerSprite
    {
        public SmallMarioSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("smallMarioSheet"), 1, 7, 3, position, SpriteEffects.None, false)
        {

        }
    }
    public class SuperMarioSprite : PlayerSprite
    {
        public SuperMarioSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("superMarioSheet"), 1, 6, 3, position, SpriteEffects.None, false)
        {

        }
    }
    public class FireMarioSprite : PlayerSprite
    {
        public FireMarioSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("fireMarioSheet"), 1, 6, 3, position, SpriteEffects.None, false)
        {

        }
    }
    public class DeadMarioSprite : PlayerSprite
    {
        public DeadMarioSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("deadMario"), 1, 1, 1, position, SpriteEffects.None, false)
        {

        }
    }
    public class StarMarioSprite : PlayerSprite
    {
        public StarMarioSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("starMario"), 1, 6, 3, position, SpriteEffects.None, false)
        {

        }
    }
}
