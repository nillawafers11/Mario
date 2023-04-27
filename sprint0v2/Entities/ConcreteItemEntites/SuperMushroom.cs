using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using sprint0v2.Entities.ConcreteItemEntites.ItemStates;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class SuperMushroom : Item
    {
        public SuperMushroom(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            ItemState = new AppearFromBlockState(this);
            ItemType = ItemType.SuperMushroom;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.SuperMushroom);
        }

        public override void Update(GameTime gameTime)
        {
            if (ItemState is not AppearFromBlockState && !IsColliding) {
                ItemState.EnterFallingState();
            }
            if (flipSpeedX) {
                SpeedX = -Speed.X;
                flipSpeedX = false;
            }
            ItemState.Update(gameTime);
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch (other)
            {
                case Mario:
                    // Call the Destroy method of the EntityManager to delete the item
                    EntityManager.Instance.RemoveEntity(this);
                    Grid.Instance.RemoveEntity(this);
                    break;
                case Block:
                    if (other.Position.Y >= Position.Y + 12)
                    {
                        ItemState.EnterNormalState();
                    }
                    else if ((LeftCollision < 2 || RightCollision < 2) && other.Position.Y - 16 < Position.Y)
                    {
                        flipSpeedX = true;
                    }
                    break;
            }
        }
    }
}
