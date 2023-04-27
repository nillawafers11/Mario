using Microsoft.Xna.Framework;

namespace sprint0v2.Sprites
{
    public class ItemSpriteFactory : SpriteFactory
    {
        public static ItemSpriteFactory Instance { get; private set; }
        public enum eItemSprite
        {
            Coin = 0,
            FireFlower = 1,
            OneUpMushroom = 2,
            SuperMushroom = 3,
            Star = 4,
            Axe = 5,
            VineTop = 6,
            VineStem = 7,
            Hammer = 8,
        }

        public ItemSpriteFactory(Game game)
        {
            _game = game;
            Instance = this;
        }

        public override ISprite CreateSprite(Vector2 position, int type)
        {
            ISprite sprite = null;
            eItemSprite eType = (eItemSprite)type;

            switch (eType)
            {
                case eItemSprite.Coin:
                    sprite = new CoinSprite(position, _game);
                    break;
                case eItemSprite.FireFlower:
                    sprite = new FireFlowerSprite(position, _game);
                    break;
                case eItemSprite.OneUpMushroom:
                    sprite = new OneUpMushroomSprite(position, _game);
                    break;
                case eItemSprite.SuperMushroom:
                    sprite = new SuperMushroomSprite(position, _game);
                    break;
                case eItemSprite.Star:
                    sprite = new StarSprite(position, _game);
                    break;
                case eItemSprite.Axe:
                    sprite = new AxeSprite(position, _game);
                    break;
                case eItemSprite.VineTop:
                    sprite = new VineTopSprite(position, _game);
                    break;
                case eItemSprite.VineStem:
                    sprite = new VineStemSprite(position, _game);
                    break;
                case eItemSprite.Hammer:
                    sprite = new HammerSprite(position, _game);
                    break;
            }
            return sprite;
        }



    }
}
