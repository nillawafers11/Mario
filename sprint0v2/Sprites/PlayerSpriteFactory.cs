    using Microsoft.Xna.Framework;

namespace sprint0v2.Sprites
{
    public class PlayerSpriteFactory : SpriteFactory
    {
        public static PlayerSpriteFactory Instance{ get; private set; }
        public enum ePlayerSprite
        {
            Dead = 0,
            SmallMario = 1,
            SuperMario = 2,
            FireMario = 3,
            StarMario = 4
        }
        
        public PlayerSpriteFactory(Game game) 
        {
            _game = game;
            Instance = this;
        }

        public override ISprite CreateSprite(Vector2 position, int type)
        {
            ISprite sprite = null;
            ePlayerSprite eType = (ePlayerSprite)type;

            switch (eType)
            {
                case ePlayerSprite.SmallMario:
                    sprite = new SmallMarioSprite(position, _game);
                    break;
                case ePlayerSprite.SuperMario:
                    sprite = new SuperMarioSprite(position, _game);
                    break;
                case ePlayerSprite.FireMario:
                    sprite = new FireMarioSprite(position, _game);
                    break;
                case ePlayerSprite.Dead:
                    sprite = new DeadMarioSprite(position, _game);
                    break;
                case ePlayerSprite.StarMario:
                    sprite = new StarMarioSprite(position, _game);
                    break;
            }
            return sprite;
        }
    }
}
