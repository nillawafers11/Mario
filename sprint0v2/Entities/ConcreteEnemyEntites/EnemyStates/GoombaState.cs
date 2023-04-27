using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class GoombaNormalState : EnemyState
    {
        public GoombaNormalState(Enemy enemy)
        {
            enemyEntity = enemy;
        }

        public override void GoombaStomp()
        {
            enemyEntity.EnemyState = new GoombaStompedState(enemyEntity);
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new GoombaFlopState(enemyEntity);
        }

        public override void Falling()
        {
            //Debug.WriteLine("enemyEntity fell");
            enemyEntity.EnemyState = new GoombaFallingState(enemyEntity);
        }
    }

    public class GoombaFallingState : EnemyState
    {
        private Vector2 acceleration = new Vector2(0, 64);
        public GoombaFallingState(Enemy enemy)
        {
            enemyEntity = enemy;
        }
        public override void update(GameTime gameTime)
        {
            enemyEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Landing()
        {
            //Debug.WriteLine("enemyEntity landed");
            enemyEntity.Speed = new Vector2(enemyEntity.Speed.X, 0);
            enemyEntity.EnemyState = new GoombaNormalState(enemyEntity);
        }
        public override void GoombaStomp()
        {
            enemyEntity.EnemyState = new GoombaStompedState(enemyEntity);
        }
        public override void Die()
        {
            enemyEntity.EnemyState = new GoombaFlopState(enemyEntity);
        }
    }
    public class GoombaStompedState : EnemyState
    {
        double duration = 0.67;
        public GoombaStompedState(Enemy enemy)
        {
            enemyEntity = enemy;
        }
        public override void update(GameTime gametime)
        {
            duration -= gametime.ElapsedGameTime.TotalSeconds;
            if (duration < 0)
            {
                enemyEntity.IsRemoved = true;
            }
        }
    }
    public class GoombaFlopState : EnemyState
    {
        private double duration = 8;
        private Vector2 acceleration = new Vector2(0, 512);
        public GoombaFlopState(Enemy enemy)
        {
            enemyEntity = enemy;
        }
        public override void update(GameTime gameTime)
        {
            duration -= gameTime.ElapsedGameTime.TotalSeconds;
            enemyEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (duration < 0)
            {
                Grid.Instance.RemoveEntity(enemyEntity);
                EntityManager.Instance.RemoveEntity(enemyEntity);
            }
        }
    }
}
