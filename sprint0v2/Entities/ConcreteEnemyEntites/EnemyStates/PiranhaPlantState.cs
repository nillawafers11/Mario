using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class PiranhaUpState : EnemyState
    {
        private Vector2 destination;
        private double duration = 1;
        public PiranhaUpState(Enemy enemy, Vector2 position) {
            destination = enemy.Position;
            destination.Y -= enemy.Sprite.Texture.Height + 2;
            enemyEntity = enemy;
        }

        public override void update(GameTime gameTime)
        {
            if (enemyEntity.Position.Y < destination.Y)
            {
                enemyEntity.Speed = Vector2.Zero;
                enemyEntity.Position = destination;
            }
            if (enemyEntity.Speed == Vector2.Zero) {
                duration -= gameTime.ElapsedGameTime.TotalSeconds;
                MoveDown();
            }
        }
        public override void Die()
        {
            EntityManager.Instance.RemoveEntity(enemyEntity);
            Grid.Instance.RemoveEntity(enemyEntity);
        }

        public override void MoveDown()
        {
            if (duration < 0)
            {
                enemyEntity.SpeedY = 24;
                enemyEntity.EnemyState = new PiranhaDownState(enemyEntity, destination);
            }
        }
    }

    public class PiranhaDownState : EnemyState
    {
        private Vector2 destination;
        private double duration = 1;
        public PiranhaDownState(Enemy enemy, Vector2 position) {
            destination = enemy.Position;
            destination.Y += enemy.Sprite.Texture.Height + 2;
            enemyEntity = enemy;
        }
        public override void update(GameTime gameTime)
        {
            if (enemyEntity.Position.Y > destination.Y) {
                enemyEntity.Speed = Vector2.Zero;
                enemyEntity.Position = destination;
            }
            if (enemyEntity.Speed == Vector2.Zero)
            {
                if (Grid.Instance.GetAllEntities().Contains(enemyEntity))
                {
                    Grid.Instance.RemoveEntity(enemyEntity);
                }
                duration -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void MoveUp()
        {
            if (duration < 0)
            {
                Grid.Instance.AddEntity(enemyEntity);
                enemyEntity.SpeedY = -24;
                enemyEntity.EnemyState = new PiranhaUpState(enemyEntity, destination);
            }
        }

        public override void Die()
        {
            EntityManager.Instance.RemoveEntity(enemyEntity);
            Grid.Instance.RemoveEntity(enemyEntity);
        }
    }
}
