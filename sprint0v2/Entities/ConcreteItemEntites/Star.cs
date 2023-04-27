using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System.Diagnostics;
using sprint0v2.Entities.ConcreteItemEntites.ItemStates;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class Star : Item
    {
        private new Vector2 acceleration = new Vector2(0, 256);
        private bool flipSpeedY;
        public Star( Vector2 position, Vector2 speed) 
            : base(position, speed)
        {
            ItemState = new AppearFromBlockState(this);
            ItemType = ItemType.Star;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.Star);
        }
        public override void Update(GameTime gameTime)
        {
            if (ItemState is not AppearFromBlockState)
            {
                Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                SpeedY = MathHelper.Clamp(Speed.Y, -160, 160);
            }
            if (flipSpeedX)
            {
                SpeedX = -Speed.X;
                flipSpeedX = false;
            }
            if (flipSpeedY) {
                SpeedY = -Speed.Y;
                flipSpeedY = false;
            }
            ItemState.Update(gameTime);
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            if (other is Mario)
            {
                // Call the Destroy method of the EntityManager to delete the item
                EntityManager.Instance.RemoveEntity(this);
                Grid.Instance.RemoveEntity(this);
            }
            else if (other is Block)
            {
                if (other.Position.Y >= Position.Y + 8)
                {
                    SpeedY = -160;
                }
                else if (other.Position.Y <= Position.Y && !flipSpeedY)
                {
                    flipSpeedY = true;
                }
                else if ((LeftCollision < 2 || RightCollision < 2) && other.Position.Y - 16 < Position.Y)
                {
                    flipSpeedX = true;
                }
            }
        }
    }
}
