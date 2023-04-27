using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities.BlockStates
{
	public class BrickBlockState : BlockState
	{

		public BrickBlockState(Block Block)
		{
			blockEntity = Block;
		}


        public override void BrickBump(Entity collidedEntity)
		{
			blockEntity.BlockState = new BrickBumpState(blockEntity, collidedEntity);
		}

        public override void BrickBreak()
        {
			blockEntity.BlockState = new BrickShatterState(blockEntity);
        }
    }

	public class BrickBumpState : BlockState
	{

		private Vector2 acceleration = new (0, 16 / (float)0.015625);

		private double duration = 0.250;

		private Vector2 originalLoc;

		public BrickBumpState(Block block, Entity collidedEntity)
		{
			blockEntity = block;
			blockEntity.Speed = new Vector2(0, -16/(float)0.125);
			originalLoc = blockEntity.Position;
			if (blockEntity.ItemHeld != ItemType.Nothing) {
				createItem(collidedEntity.Position.X < blockEntity.Position.X);
			}
		}

		public override void BrickBump()
		{
			if (duration < 0) { 
			//Debug.WriteLine("exiting bumped state");
			blockEntity.Position = originalLoc;
			blockEntity.Speed = Vector2.Zero;
				if (blockEntity.ItemCount == 0 && blockEntity.ItemHeld != ItemType.Nothing)
				{
					blockEntity.BlockState = new UsedBlockState(blockEntity);
				}
				else 
				{ 
					blockEntity.BlockState = new BrickBlockState(blockEntity); 
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			blockEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
			duration -= gameTime.ElapsedGameTime.TotalSeconds;
			BrickBump();
		}
	}

	public class BrickShatterState : BlockState {

		private double duration = 8;
		private Vector2 acceleration = new Vector2(0, 16/ (float)0.015625);

		public BrickShatterState(Block block) {
			blockEntity = block;
		}

        public override void Update(GameTime gameTime)
		{
			duration -= gameTime.ElapsedGameTime.TotalSeconds;
            blockEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
			BrickBlock brickEntity = (BrickBlock)blockEntity;
			brickEntity.BottomPiecesSpeed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
			if (duration < 0) {
				EntityManager.Instance.RemoveEntity(blockEntity);
			}
        }
    }

}
