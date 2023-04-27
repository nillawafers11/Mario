using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class SpinyShellState : EnemyState
    {
        public SpinyShellState(Enemy enemy) 
        {
            enemyEntity = enemy;
        }

        public override void Landing()
        {
            enemyEntity.EnemyState = new SpinyNormalState(enemyEntity);
        }
        public override void Die()
        {
            enemyEntity.EnemyState = new SpinyFlopState(enemyEntity);
        }

    }

    public class SpinyNormalState : EnemyState 
    {
        public SpinyNormalState(Enemy enemy)
        {
            enemy.Speed = new Vector2(24, 0);
            if (enemy.Sprite.SpriteFlip is SpriteEffects.FlipHorizontally) {
                enemy.Speed = new Vector2(-24, 0);
            }
            enemyEntity = enemy;
            
        }
        public override void Die()
        {
            enemyEntity.EnemyState = new SpinyFlopState(enemyEntity);
        }
        public override void Falling()
        {
            //Debug.WriteLine("enemyEntity fell");
            enemyEntity.EnemyState = new SpinyFallingState(enemyEntity);
        }
    }
    
       public class SpinyFallingState : EnemyState
    {
        private Vector2 acceleration = new Vector2(0, 64);
        public SpinyFallingState(Enemy enemy)
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
            enemyEntity.EnemyState = new SpinyNormalState(enemyEntity);
        }
        public override void Die()
        {
            enemyEntity.EnemyState = new SpinyFlopState(enemyEntity);
        }
    }
     

    public class SpinyFlopState : EnemyState
    {
        private double duration = 8;
        private Vector2 acceleration = new Vector2(0, 512);
        public SpinyFlopState(Enemy enemy)
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
