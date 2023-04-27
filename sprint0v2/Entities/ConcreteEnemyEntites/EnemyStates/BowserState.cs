using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates
{
    public class BowserNormalState : EnemyState
    {
        private Random random;
        private double jumpCooldown;
        

        public BowserNormalState(Enemy enemy)
        {
            random = new Random();
            jumpCooldown = random.Next(2, 5);
            enemyEntity = enemy;
        }

        public override void update(GameTime gameTime)
        {
            jumpCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            if(jumpCooldown <= 0)
            {
                enemyEntity.EnemyState = new BowserJumpState(enemyEntity);
            }

            
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new BowserDefeatedState(enemyEntity);
        }

        public override void Jump()
        {
            enemyEntity.EnemyState = new BowserJumpState(enemyEntity);
        }

        public override void Falling()
        {
            enemyEntity.Speed = new Vector2(enemyEntity.Speed.X, 5);
        }
    }

    public class BowserJumpState : EnemyState
    {
        private Random random;
        private Vector2 jumpSpeed;
        private Vector2 acceleration;
        public BowserJumpState(Enemy enemy)
        {
            random = new Random();
            float jumpHeight = random.Next(-200, -200); // Randomly choose jump height
            int moveDirection = random.Next(2) == 0 ? -1 : 1; // Randomly choose left or right direction
            float horizontalSpeed = random.Next(10, 60) * moveDirection;

            jumpSpeed = new Vector2(horizontalSpeed, jumpHeight);
            acceleration = new Vector2(0, 500);
            enemyEntity = enemy;
            enemyEntity.Speed = jumpSpeed;
        }

        public override void update(GameTime gameTime)
        {
            enemyEntity.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void Die()
        {
            enemyEntity.EnemyState = new BowserDefeatedState(enemyEntity);
        }

        public override void Landing()
        {
            enemyEntity.Speed = new Vector2(enemyEntity.Speed.X, 0);
            enemyEntity.EnemyState = new BowserNormalState(enemyEntity);
        }
    }

    //state of bowser when defeated with fireballs
    public class BowserDefeatedState : EnemyState
    {
        private double duration = 8;
        private Vector2 acceleration = new Vector2(0, 512);
        public BowserDefeatedState(Enemy enemy)
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
