using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class StaticCoin : Item
    {
        private new Vector2 acceleration;
        private Mario _mario;

        public StaticCoin(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            ItemType = ItemType.Coin;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.Coin);
            gravity = 0;
            Speed = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Speed.Y > 0)
            {
                EntityManager.Instance.RemoveEntity(this);
                Grid.Instance.RemoveEntity(this);
            }
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);

            if (other is Mario) {
                _mario = (Mario) other;
                _mario.CollectCoin();
                Grid.Instance.RemoveEntity(this);
                EntityManager.Instance.RemoveEntity(this);

            }
        }
    }
}

