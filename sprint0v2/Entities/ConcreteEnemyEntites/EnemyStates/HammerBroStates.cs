using Microsoft.Xna.Framework;
using System;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class HammerBroIdleState : EnemyState
    {
        private Random random;
        private double jumpCooldown;

        public HammerBroIdleState(Enemy enemy)
        {
            random = new Random();
            jumpCooldown = random.Next(2, 5); // Randomly choose time between jumps (2 to 4 seconds)
            enemyEntity = enemy;
        }

        public override void update(GameTime gameTime)
        {
            jumpCooldown -= gameTime.ElapsedGameTime.TotalSeconds;

            if (jumpCooldown <= 0)
            {
                enemyEntity.EnemyState = new HammerBroJumpingState(enemyEntity);
            }
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new HammerBroFlopState(enemyEntity);
        }
        public override void Falling()
        {
            //Debug.WriteLine("enemyEntity fell");
            enemyEntity.EnemyState = new HammerBroFallingState(enemyEntity);
        }
    }

    public class HammerBroJumpingState : EnemyState
    {
        private Random random;
        private Vector2 jumpSpeed;
        private Vector2 acceleration;

        public HammerBroJumpingState(Enemy enemy)
        {
            random = new Random();
            float jumpHeight = random.Next(-400, -200); // Randomly choose jump height
            int moveDirection = random.Next(2) == 0 ? -1 : 1; // Randomly choose left or right direction
            float horizontalSpeed = random.Next(10, 60) * moveDirection;

            jumpSpeed = new Vector2(horizontalSpeed, jumpHeight);
            acceleration = new Vector2(0, 600);
            enemyEntity = enemy;
            enemyEntity.Speed = jumpSpeed;
        }

        public override void update(GameTime gameTime)
        {
            enemyEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check if Hammer Bro has landed (you need to implement the collision detection)
        }

        public override void Landing()
        {
            enemyEntity.SpeedY = 0;
            enemyEntity.EnemyState = new HammerBroIdleState(enemyEntity);
        }
    }

    public class HammerBroFlopState : EnemyState
    {
        private double duration = 8;
        private Vector2 acceleration = new Vector2(0, 512);
        public HammerBroFlopState(Enemy enemy)
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
    public class HammerBroFallingState : EnemyState
    {
        private Vector2 acceleration = new Vector2(0, 64);
        public HammerBroFallingState(Enemy enemy)
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
            enemyEntity.EnemyState = new HammerBroIdleState(enemyEntity);
        }
 
        public override void Die()
        {
            enemyEntity.EnemyState = new HammerBroFlopState(enemyEntity);
        }
    }
}

