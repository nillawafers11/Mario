using Microsoft.Xna.Framework;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public interface IEnemyState
    {
        public void Die();
        public void KoopaStomp();
        public void KoopaKick();
        public void GoombaStomp();
        public void Falling();
        public void Landing();
        public void MoveUp();
        public void MoveDown();
        public void Jump();
        public void update(GameTime gameTime);
    }
}
