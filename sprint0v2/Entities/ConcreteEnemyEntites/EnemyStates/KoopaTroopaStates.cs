using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class KoopaNormalState : EnemyState
    {
        public KoopaNormalState(Enemy enemy)
        {
            //Debug.WriteLine("Koopa entered normal state");
            enemy.Speed = new Vector2(-24, 0);
            if (EntityManager.Instance.GetPlayer() is not null && EntityManager.Instance.GetPlayer().Position.X > enemy.Position.X)
            {
                enemy.DirectionXToggle = true;
            }
            if (enemy.EnemyState is KoopaStompedState)
            {
                enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y - 8);
            }
            enemyEntity = enemy;
        }

        public override void KoopaStomp()
        {
            enemyEntity.EnemyState = new KoopaStompedState(enemyEntity, this);
        }

        public override void Falling()
        {
            enemyEntity.EnemyState = new KoopaFallingState(enemyEntity, this);
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new KoopaFlopState(enemyEntity);
        }
    }
    public class KoopaFallingState : EnemyState
    {
        private EnemyState previousState;
        private Vector2 acceleration = new Vector2(0, 128);
        public KoopaFallingState(Enemy enemy, EnemyState pastState) {
            //Debug.WriteLine("koopa entered falling state");
            previousState = pastState;
            enemyEntity = enemy;
        }

        public override void update(GameTime gameTime)
        {
            enemyEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Landing()
        {
            enemyEntity.SpeedY = 0;
            if (previousState is KoopaNormalState)
            {
                enemyEntity.EnemyState = new KoopaNormalState(enemyEntity);
            }
            else
            {
                enemyEntity.EnemyState = new KoopaZoomState(enemyEntity);
            }
        }

        public override void KoopaStomp()
        {
            enemyEntity.EnemyState = new KoopaStompedState(enemyEntity, this);
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new KoopaFlopState(enemyEntity);
        }
    }

    public class KoopaFlopState : EnemyState
    {
        private double duration = 7;
        Vector2 acceleration = new Vector2(0, 512);
        public KoopaFlopState(Enemy enemy) {
            enemyEntity = enemy;
        }

        public override void update(GameTime gameTime)
        {
            duration -= gameTime.ElapsedGameTime.TotalSeconds;
            enemyEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (duration < 0) { 
                Grid.Instance.RemoveEntity(enemyEntity);
                EntityManager.Instance.RemoveEntity(enemyEntity);
            }
        }
    }

    public class KoopaStompedState : EnemyState 
    {
        private double duration = 8;

        public KoopaStompedState(Enemy enemy, EnemyState pastState) {
            if (pastState is KoopaNormalState)
            {
                enemy.Position = new Vector2(enemy.Position.X, enemy.Position.Y + 8);
            }
            enemy.Speed = new Vector2(0, 0);
            enemyEntity = enemy;
        }
        public override void update(GameTime gameTime)
        {
            duration -= gameTime.ElapsedGameTime.TotalSeconds;
            if (duration <= 0) {
                //Debug.WriteLine("should turn back to turtle");
                enemyEntity.EnemyState = new KoopaNormalState(enemyEntity);
            }
        }
        public override void Die()
        {
            enemyEntity.EnemyState = new KoopaFlopState(enemyEntity);
        }
        public override void KoopaKick()
        {
            if (duration <= 7.9)
            {
                enemyEntity.EnemyState = new KoopaZoomState(enemyEntity);
            }
        }
    }

    public class KoopaZoomState : EnemyState {

        private double waitTransition = 0.1;

        public KoopaZoomState(Enemy enemy) {
            enemyEntity = enemy;
        }
        public override void update(GameTime gameTime)
        {
            waitTransition -= gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void KoopaStomp()
        {
            if (waitTransition < 0)
            {
                enemyEntity.EnemyState = new KoopaStompedState(enemyEntity, this);
            }
        }

        public override void Falling()
        {
            enemyEntity.EnemyState = new KoopaFallingState(enemyEntity, this);
        }
        public override void Die()
        {   
            enemyEntity.EnemyState = new KoopaFlopState(enemyEntity);
        }
    }
}
