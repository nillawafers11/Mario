using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities.BlockStates
{
    public class PlatformState : BlockState
    {
        protected PlatformState originalState;
        protected Boolean reverse;
        protected Vector2 originalPosition;
        protected float distance = 100;
        protected float acceleration = 3000;
        protected double duration = 2.5;

        public PlatformState(Block Block)
        {
            blockEntity = Block;
        }

        public override void PlatformStopState(PlatformState OriginalState, Boolean Reverse)
        {
            blockEntity.BlockState = new PlatformStopState(blockEntity, OriginalState, Reverse);
        }

        public override void PlatformVerticalState(Boolean Reverse)
        {
            blockEntity.BlockState = new PlatformVerticalState(blockEntity, Reverse);
        }

        public override void PlatformHorizontalState(Boolean Reverse)
        {
            blockEntity.BlockState = new PlatformHorizontalState(blockEntity, Reverse);
        }
    }

    public class PlatformStopState : PlatformState
    {
        public PlatformStopState(Block block, PlatformState OriginalState, Boolean Reverse) : base(block) 
        {
            reverse = Reverse;
            originalState = OriginalState;
            blockEntity = block;
            blockEntity.Speed = new Vector2(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            duration -= gameTime.ElapsedGameTime.TotalSeconds;
            if (duration < 0)
            {
                if (originalState is PlatformVerticalState)
                {
                    PlatformVerticalState(reverse);
                }
                else if (originalState is PlatformHorizontalState)
                {
                    PlatformHorizontalState(reverse);
                }
            }
        }
    }

    public class PlatformVerticalState : PlatformState
    {
        public PlatformVerticalState(Block block, Boolean Reverse) : base (block)
        {
            reverse = !Reverse;
            originalState = this;
            blockEntity = block;
            originalPosition = block.Position;
        }

        public override void Update(GameTime gameTime)
        {
            if (!reverse)
            {
                if (blockEntity.SpeedY == 0)
                {
                    blockEntity.SpeedY = acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (blockEntity.Position.Y >= originalPosition.Y + distance)
                {
                    PlatformStopState(originalState, reverse);
                }
            }
            else
            {
                if (blockEntity.SpeedY == 0)
                {
                    blockEntity.SpeedY = -acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (blockEntity.Position.Y <= originalPosition.Y - distance)
                {
                    PlatformStopState(originalState, reverse);
                }
            }
        }
    }

    public class PlatformHorizontalState : PlatformState
    {
        public PlatformHorizontalState(Block block, Boolean Reverse) : base(block)
        {
            reverse = !Reverse;
            originalState = this;
            blockEntity = block;
            originalPosition = block.Position;
        }

        public override void Update(GameTime gameTime)
        {
            if (!reverse)
            {
                if (blockEntity.SpeedX == 0)
                {
                    blockEntity.SpeedX = acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (blockEntity.Position.X >= originalPosition.X + distance)
                {
                    PlatformStopState(originalState, reverse);
                }
            }
            else
            {
                if (blockEntity.SpeedX == 0)
                {
                    blockEntity.SpeedX = -acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (blockEntity.Position.X <= originalPosition.X - distance)
                {
                    PlatformStopState(originalState, reverse);
                }
            }
        }
    }
}
