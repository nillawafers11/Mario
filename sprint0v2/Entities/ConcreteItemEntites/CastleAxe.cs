using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class CastleAxe : Item
    {
        private new Vector2 acceleration;
        private Mario _mario;

        public CastleAxe(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            ItemType = ItemType.Axe;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.Axe);
            gravity = 0;
            Speed = Vector2.Zero;
            acceleration = Vector2.Zero;
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

            if (other is Mario)
            {
                _mario = (Mario)other;
                _mario.CollectCoin();
                Grid.Instance.RemoveEntity(this);
                EntityManager.Instance.RemoveEntity(this);
            }
        }
    }
}
