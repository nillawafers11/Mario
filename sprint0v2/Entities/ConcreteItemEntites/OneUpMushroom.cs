using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using sprint0v2.Entities.ConcreteItemEntites.ItemStates;
using System;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class OneUpMushroom : Item
    {

        public OneUpMushroom(Vector2 position, Vector2 speed)
            : base(position, new Vector2(speed.X, Math.Abs(speed.Y)))
        {
            ItemState = new AppearFromBlockState(this);
            ItemType = ItemType.OneUpMushroom;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.OneUpMushroom);

        }

        public override void Update(GameTime gameTime)
        {
            if (ItemState is not AppearFromBlockState && !IsColliding)
            {
                ItemState.EnterFallingState();
            }
            if (flipSpeedX)
            {
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
                    if (TopCollision < BottomCollision)
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
