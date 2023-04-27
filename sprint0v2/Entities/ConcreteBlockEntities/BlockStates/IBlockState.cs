using Microsoft.Xna.Framework;

namespace sprint0v2.Entities.ConcreteBlockEntities.BlockStates
{
    public interface IBlockState
    {
        public void BrickBump();

        public void QuestionBump();

        public void Update(GameTime gameTime);
    }
}
