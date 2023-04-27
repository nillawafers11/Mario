using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class Coin : Item
    {
        private new Vector2 acceleration;

        public Coin(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            ItemType = ItemType.Coin;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.Coin);
            gravity = 0;
            acceleration = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!Grid.Instance.GetAllEntities().Contains(this)) {
                acceleration = new Vector2(0, 112);
            }
            if (Speed.Y > 0) {
                EntityManager.Instance.RemoveEntity(this);
            }
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            if (other is Mario) {
                EntityManager.Instance.RemoveEntity(this);
                Grid.Instance.RemoveEntity(this);
            }
        }
    }
}
