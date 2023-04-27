using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class BulletNormalState : EnemyState
    {
        public BulletNormalState(Enemy enemy)
        {
            enemyEntity = enemy;
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new BulletFlopState(enemyEntity);
        }
    }

    

    public class BulletFlopState : EnemyState
    {
        private double duration = 8;
        private Vector2 acceleration = new Vector2(0, 512);
        public BulletFlopState(Enemy enemy)
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
