using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Sprites
{
    public class GoombaSprite : PlayerSprite
    {
        public GoombaSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("goomba"), 1, 3, 2, position, SpriteEffects.None, true)
        {

        }
    }
    public class GreenKoopaTroopaSprite : PlayerSprite
    {
        public GreenKoopaTroopaSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("greenKoopaTroopa"), 1, 3, 2, position, SpriteEffects.None, true)
        {

        }
    }
    public class RedKoopaTroopaSprite : EnemySprite
    {
        public RedKoopaTroopaSprite(Vector2 position,Game game)
            : base(game.Content.Load<Texture2D>("redKoopaTroopa"), 1, 3, 2, position, SpriteEffects.None, true)
        {

        }
    }
    public class PiranhaPlantSprite : EnemySprite {
        public PiranhaPlantSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("PiranhaPlant"), 1, 2, 2, position, SpriteEffects.None, true) 
        { 
        }
    }
    
    public class BulletBillSprite : EnemySprite
    {
        public BulletBillSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("bulletBill"), 1, 1, 1, position, SpriteEffects.None, false)
        {
        }
    }

    public class LakituSprite : EnemySprite
    {
        public LakituSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("lakitu"), 1, 2, 2, position, SpriteEffects.None, true)
        {
        }
    }

    public class HammerBroSprite : EnemySprite
    {
        public HammerBroSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("hammerBro"), 1, 3, 3, position, SpriteEffects.None, true)
        {
        }
    }

    public class SpinySprite : EnemySprite
    {
        public SpinySprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("spiny"), 1, 4, 2, position, SpriteEffects.None, true)
        {
        }
    }

    public class BowserSprite : EnemySprite
    {
        public BowserSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("bowser"), 1, 4, 4, position, SpriteEffects.None, true)
        {
        }
    }

    public class CastleGoombaSprite : EnemySprite
    {
        public CastleGoombaSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("castleGoomba"), 1, 3, 2, position, SpriteEffects.None, true)
        {
        }
    }

    public class CastleKoopaTroopaSprite : EnemySprite
    {
        public CastleKoopaTroopaSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("castleKoopaTroopa"), 1, 3, 2, position, SpriteEffects.None, true)
        {
        }
    }
}
