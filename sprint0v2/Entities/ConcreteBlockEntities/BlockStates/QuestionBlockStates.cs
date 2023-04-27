using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities.BlockStates
{
	public class HiddenQuestionBlockState : BlockState
	{
		public HiddenQuestionBlockState(Block block)
		{
			blockEntity = block;
		}

		public override void QuestionBump(Entity collidedEntity)
		{
			blockEntity.BlockState = new QuestionBlockBumpState(blockEntity, collidedEntity);
		}
	}

	public class QuestionBlockState : BlockState
	{
		public QuestionBlockState(Block Block)
		{
			blockEntity = Block;
		}

		public override void QuestionBump(Entity collidedEntity)
		{
			blockEntity.BlockState = new QuestionBlockBumpState(blockEntity, collidedEntity);
		}
	}

	public class UsedBlockState : BlockState
	{
		public UsedBlockState(Block Block)
		{
			blockEntity = Block;
		}
	}

	public class QuestionBlockBumpState : BlockState
	{
		private Vector2 acceleration = new(0, 16 / (float)0.015625);

		private double duration = 0.250;

		private Vector2 originalLoc;

		public QuestionBlockBumpState(Block block, Entity collidedEntity)
		{
			//////Debug.WriteLine("Exited question bump state");
			blockEntity = block;
			blockEntity.Speed = new Vector2(0, -16 / (float)0.125);
			originalLoc = blockEntity.Position;
			createItem(collidedEntity.Position.X < blockEntity.Position.X);
		}

		public override void QuestionBump()
		{
			if (duration < 0)
			{
				blockEntity.Position = originalLoc;
				blockEntity.Speed = Vector2.Zero;
				if (blockEntity.ItemCount > 0)
				{
					blockEntity.BlockState = new QuestionBlockState(blockEntity);
				}
				else
				{
					blockEntity.BlockState = new UsedBlockState(blockEntity);
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			blockEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
			duration -= gameTime.ElapsedGameTime.TotalSeconds;
			QuestionBump();
		}
	}
}
