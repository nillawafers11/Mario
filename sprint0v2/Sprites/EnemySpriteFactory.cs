using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Sprites
{
    public class EnemySpriteFactory : SpriteFactory
    {
        public static EnemySpriteFactory Instance { get; private set; }
        public enum eEnemySprite
        {
            Goomba = 0,
            GreenKoopaTroopa = 1,
            RedKoopaTroopa = 2,
            piranha = 3,
            BulletBill = 4,
            Lakitu = 5,
            HammerBro = 6,
            Spiny = 7,
            Bowser = 8,
            CastleGoomba = 9,
            CastleKoopaTroopa = 10,
        }

        public EnemySpriteFactory(Game game)
        {
            _game = game;
            Instance = this;
        }

        public override ISprite CreateSprite(Vector2 position, int type)
        {
            ISprite sprite = null;
            eEnemySprite eType = (eEnemySprite)type;

            switch (eType)
            {
                case eEnemySprite.Goomba:
                    sprite = new GoombaSprite(position, _game);
                    break;
                case eEnemySprite.CastleGoomba:
                    sprite = new CastleGoombaSprite(position, _game);
                    break;
                case eEnemySprite.GreenKoopaTroopa:
                    sprite = new GreenKoopaTroopaSprite(position, _game);
                    break;
                case eEnemySprite.CastleKoopaTroopa:
                    sprite = new CastleKoopaTroopaSprite(position, _game);
                    break;
                case eEnemySprite.piranha:
                    sprite = new PiranhaPlantSprite(position, _game);
                    break;
                case eEnemySprite.BulletBill:
                    sprite = new BulletBillSprite(position, _game);
                    break;
                case eEnemySprite.Lakitu:
                    sprite = new LakituSprite(position, _game);
                    break;
                case eEnemySprite.HammerBro:
                    Debug.WriteLine("Creating HammerBro Sprite");
                    sprite = new HammerBroSprite(position, _game);
                    break;
                case eEnemySprite.Spiny:
                    sprite = new SpinySprite(position, _game);
                    break;
                case eEnemySprite.Bowser:
                    sprite = new BowserSprite(position, _game);
                    break;
            }
            return sprite;
        }
    }
}
