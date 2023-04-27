using Microsoft.Xna.Framework;

namespace sprint0v2.Sprites
{
    public class BlockSpriteFactory : SpriteFactory
    {
        public static BlockSpriteFactory Instance { get; private set; }
        public enum eBlockSprite
        {
            GroundBlock = 0,
            UnbreakableBlock = 1,
            HiddenBlock = 2,
            BrickBlock = 3,
            QuestionBlock = 4,
            BrickPiece = 5,
            CastlePipeBlock = 7,
            TallPipeBlock = 8,
            FlagBlock = 9,
            CastleBlock = 10,
            SidePipeBlock = 11,
            UndergroundBrickBlock = 12,
            UndergroundGroundBlock = 13,
            BulletBillLauncher = 14,
            UsedBlock = 15,
            CastleBrick = 16,
            Platform = 17,
            BridgeSegment = 18,
            BridgeEnd = 19,
        }

        public BlockSpriteFactory(Game game)
        {
            _game = game;
            Instance = this;
        }

        public override ISprite CreateSprite(Vector2 position, int type)
        {
            ISprite sprite = null;
            eBlockSprite eType = (eBlockSprite)type;

            switch (eType)
            {
                case eBlockSprite.GroundBlock:
                    sprite = new GroundBlockSprite(position, _game);
                    break;
                case eBlockSprite.UnbreakableBlock:
                    sprite = new UnbreakableBlockSprite(position, _game);
                    break;
                case eBlockSprite.HiddenBlock:
                    sprite = new HiddenBlockSprite(position, _game);
                    break;
                case eBlockSprite.BrickBlock:
                    sprite = new BrickBlockSprite(position, _game);
                    break;
                case eBlockSprite.QuestionBlock:
                    sprite = new QuestionBlockSprite(position, _game);
                    break;
                case eBlockSprite.BrickPiece:
                    sprite = new BrickPieceSprite(position, _game);
                    break;
                case eBlockSprite.CastlePipeBlock:
                    sprite = new CastlePipeBlockSprite(position, _game);
                    break;
                case eBlockSprite.TallPipeBlock:
                    sprite = new TallPipeBlockSprite(position, _game);
                    break;
                case eBlockSprite.SidePipeBlock:
                    sprite = new SidePipeBlockSprite(position, _game);
                    break;
                case eBlockSprite.FlagBlock:
                    sprite = new FlagBlockSprite(position, _game);
                    break;
                case eBlockSprite.CastleBlock:
                    sprite = new CastleBlockSprite(position, _game);
                    break;
                case eBlockSprite.UndergroundBrickBlock:
                    sprite = new UndergroundBrickBlockSprite(position, _game);
                    break;
                case eBlockSprite.UndergroundGroundBlock:
                    sprite = new UndergroundGroundBlockSprite(position, _game);
                    break;
                case eBlockSprite.BulletBillLauncher:
                    sprite = new BulletBillLauncherSprite(position, _game);
                    break;
                case eBlockSprite.UsedBlock:
                    sprite = new UsedBlockSprite(position, _game);
                    break;
                case eBlockSprite.CastleBrick:
                    sprite = new CastleBrickSprite(position, _game);
                    break;
                case eBlockSprite.Platform:
                    sprite = new PlatformSprite(position, _game);
                    break;
                case eBlockSprite.BridgeSegment:
                    sprite = new BridgeSegmentSprite(position, _game);
                    break;
                case eBlockSprite.BridgeEnd:
                    sprite = new BridgeEndSprite(position, _game);
                    break;
            }
            return sprite;
        }
    }
}
