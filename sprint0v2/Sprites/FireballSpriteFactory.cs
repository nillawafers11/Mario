using Microsoft.Xna.Framework;

namespace sprint0v2.Sprites
{
    public class FireballSpriteFactory : SpriteFactory
    {
        public static FireballSpriteFactory Instance { get; private set; }

        public enum eFireballSprite
        {
            Normal = 0,
            Exploding = 1,
            Bowser = 2,
        }

        public FireballSpriteFactory(Game game)
        {
            _game = game;
            Instance = this;
        }

        public override ISprite CreateSprite(Vector2 position, int type)
        {
            ISprite sprite = null;
            eFireballSprite eType = (eFireballSprite)type;

            switch (eType)
            {
                case eFireballSprite.Normal:
                    sprite = new NormalFireballSprite(position, _game);
                    break;
                case eFireballSprite.Exploding:
                    sprite = new ExplodingFireballSprite(position, _game);
                    break;
                case eFireballSprite.Bowser:
                    sprite = new BowserFireballSprite(position, _game);
                    break;
            }
            return sprite;
        }
    }
}
