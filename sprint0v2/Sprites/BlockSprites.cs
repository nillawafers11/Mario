using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Sprites
{
    public class GroundBlockSprite : BlockSprite
    {
        public GroundBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("GroundBlock"), 1, 1, 1, position, false, 1)
        {

        }
    }
    public class UnbreakableBlockSprite : BlockSprite
    {
        public UnbreakableBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("UnbreakableBlock"), 1, 1, 1, position, false, 1)
        {

        }
    }
    public class TallPipeBlockSprite : BlockSprite
    {
        public TallPipeBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("pipe1"), 1, 1, 1, position, false, 0.1f)
        {

        }
    }
    public class CastlePipeBlockSprite : BlockSprite
    {
        public CastlePipeBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("castlePipe"), 1, 1, 1, position, false, 0.1f) 
        {
        
        }
    }
    public class SidePipeBlockSprite : BlockSprite
    {
        public SidePipeBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("sidepipe"), 1, 1, 1, position, false)
        {

        }
    }
    public class FlagBlockSprite : BlockSprite
    {
        public FlagBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("flagpole"), 1, 1, 1, position, false)
        {

        }
    }
    public class CastleBlockSprite : BlockSprite
    {
        public CastleBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("castle"), 1, 1, 1, position, false)
        {

        }
    }
    public class HiddenBlockSprite : BlockSprite
    {
        public HiddenBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("QuestionBlock"), 1, 5, 3, position, false)
        {

        }
    }
    public class BrickBlockSprite : BlockSprite
    {
        public BrickBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("BrickBlock"), 1, 4, 1, position, false, 0.1f)
        {

        }
    }

    public class UndergroundBrickBlockSprite : BlockSprite
    {
        public UndergroundBrickBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("UndergroundBrickBlock"), 1, 1, 1, position, false)
        {

        }
    }

    public class UndergroundGroundBlockSprite : BlockSprite
    {
        public UndergroundGroundBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("UndergroundGroundBlock"), 1, 1, 1, position, false)
        {

        }
    }
    public class QuestionBlockSprite : BlockSprite
    {
        public QuestionBlockSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("QuestionBlock"), 1, 5, 3, position, true, 0.1f)
        {

        }
    }
    public class BrickPieceSprite : BlockPieceSprite
    {
        public BrickPieceSprite(Vector2 position, Game game) 
            : base (game.Content.Load<Texture2D>("BrickBlock"), 1, 2, 2, position, SpriteEffects.None, true){ 
        }
    }

    public class BulletBillLauncherSprite : BlockSprite
    {
        public BulletBillLauncherSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("bulletBillLauncher"), 1, 1, 1, position, false)
        {
        }
    }

    public class UsedBlockSprite : BlockSprite
    {
        public UsedBlockSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("QuestionBlock"), 1, 5, 3, position, false)
        {
            currentFrame = 3;
        }
    }

    public class CastleBrickSprite : BlockSprite {
        public CastleBrickSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("CastleBrick"), 1, 1, 1, position, false) 
        { 
        }
    }

    public class PlatformSprite : BlockSprite
    {
        public PlatformSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("platform"), 1, 1, 1, position, false)
        {
        }
    }
    
    public class BridgeSegmentSprite : BlockSprite
    {
        public BridgeSegmentSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("bridgeSegment"), 1, 1, 1, position, false)
        {

        }
    }

    public class BridgeEndSprite : BlockSprite
    {
        public BridgeEndSprite(Vector2 position, Game game) : base(game.Content.Load<Texture2D>("bridgeEnd"), 1, 1, 1, position, false)
        {

        }
    }

}