using Microsoft.Xna.Framework;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public abstract class EnemyState : IEnemyState
    {
        protected readonly float acceleration = 3f;
        protected Enemy enemyEntity { get; set; }
        public virtual void Die() { }
        public virtual void KoopaStomp() {  }
        public virtual void KoopaKick() { }
        public virtual void GoombaStomp() { }
        public virtual void BulletStomp() { }
        public virtual void Falling() { }
        public virtual void Landing() { }
        public virtual void MoveUp() { }
        public virtual void MoveDown() { }
        public virtual void Jump() { }
        public virtual void update(GameTime gameTime) { }
    }
}
