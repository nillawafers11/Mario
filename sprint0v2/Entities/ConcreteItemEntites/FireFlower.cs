using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System.Diagnostics;
using sprint0v2.Entities.ConcreteItemEntites.ItemStates;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    public class FireFlower : Item
    {
        public FireFlower(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            ItemState = new AppearFromBlockState(this);
            ItemType = ItemType.FireFlower;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.FireFlower);
            Gravity = 0;
        }

        public override void Update(GameTime gameTime)
        {
            ItemState.Update(gameTime);
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            // Delete the item if it collides with Mario
            base.OnCollision(other);
            switch (other)
            {
                case Mario:
                    // Call the Destroy method of the EntityManager to delete the item
                    EntityManager.Instance.RemoveEntity(this);
                    Grid.Instance.RemoveEntity(this);
                    break;
                case Block:
                    if (other.Position.Y >= Position.Y)
                    {
                        ItemState.EnterNormalState();
                    }
                    break;
            }
        }
    }
}