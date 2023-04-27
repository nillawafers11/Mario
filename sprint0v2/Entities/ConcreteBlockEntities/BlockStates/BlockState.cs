using Microsoft.Xna.Framework;
using System;
using System.Threading;

namespace sprint0v2.Entities.ConcreteBlockEntities.BlockStates{

	public abstract class BlockState : IBlockState
	{
		protected Block blockEntity { get; set; }

		public virtual void BrickBreak() { }

		public virtual void BrickBump() { }

		public virtual void BrickBump(Entity collidedEntity) { }

		public virtual void QuestionBump() { }

		public virtual void QuestionBump(Entity collidedEntity) { }

        public virtual void PlatformStopState(PlatformState originalState, Boolean Reverse) { }

        public virtual void PlatformVerticalState(Boolean Reverse) { }

        public virtual void PlatformHorizontalState(Boolean Reverse) { }

        public virtual void Update(GameTime gameTime) { }

        public void createItem(bool marioContactLeft)
        {
            if (blockEntity.ItemHeld == ItemType.Coin)
            {
                Vector2 itemPosition = new Vector2(blockEntity.Position.X, blockEntity.Position.Y);
                Vector2 itemSpeed = new Vector2(0, -96);
                if (marioContactLeft)
                {
                    itemSpeed.X = -itemSpeed.X;
                }
                EntityManager.Instance.AddItemEntity(itemPosition, "coin", itemSpeed);

            }
            else if (blockEntity.ItemHeld == ItemType.SuperMushroom)
            {
                Vector2 itemPosition = new Vector2(blockEntity.Position.X, blockEntity.Position.Y - 8);
                Vector2 itemSpeed = new Vector2(48, 0);
                if (marioContactLeft)
                {
                    itemSpeed.X = -itemSpeed.X;
                }
                EntityManager.Instance.AddItemEntity(itemPosition, "supermushroom", itemSpeed);
            }
            else if (blockEntity.ItemHeld == ItemType.OneUpMushroom)
            {
                Vector2 itemPosition = new Vector2(blockEntity.Position.X, blockEntity.Position.Y - 8);
                Vector2 itemSpeed = new Vector2(-48, 0);
                if (marioContactLeft)
                {
                    itemSpeed.X = -itemSpeed.X;
                }
                EntityManager.Instance.AddItemEntity(itemPosition, "oneupmushroom", itemSpeed);
            }
            else if (blockEntity.ItemHeld == ItemType.FireFlower)
            {
                Vector2 itemPosition = new Vector2(blockEntity.Position.X, blockEntity.Position.Y - 8);
                Vector2 itemSpeed = new Vector2(-48, 0);
                if (marioContactLeft)
                {
                    itemSpeed.X = -itemSpeed.X;
                }
                EntityManager.Instance.AddItemEntity(itemPosition, "fireflower", itemSpeed);
            }
            else if (blockEntity.ItemHeld == ItemType.Star)
            {
                Vector2 itemPosition = new Vector2(blockEntity.Position.X, blockEntity.Position.Y - 8);
                Vector2 itemSpeed = new Vector2(-48, -160);
                if (marioContactLeft)
                {
                    itemSpeed.X = -itemSpeed.X;
                }
                EntityManager.Instance.AddItemEntity(itemPosition, "star", itemSpeed);
            }
            else if (blockEntity.ItemHeld == ItemType.Vine)
            {
                Vector2 itemPosition = new Vector2(blockEntity.Position.X + 2, blockEntity.Position.Y - 7);
                Vector2 itemSpeed = new Vector2(0, -12);
                EntityManager.Instance.AddItemEntity(itemPosition, "vine", itemSpeed);
            }
        }

    }
}
