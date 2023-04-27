using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using sprint0v2.Tiles;
using sprint0v2.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class Lakitu : Enemy
    {
        private float clampedXPos;
        private double throwTimer;
        private double throwInterval = 3000; // Interval in milliseconds between throwing Spinies
        private Mario mario;
        EnemyFactory enemyFactory;
        public override EnemyState EnemyState
        {
            set
            {
                if (value is LakituFlopState)
                {
                    Grid.Instance.RemoveEntity(this);
                    Sprite.SpriteFlip = SpriteEffects.FlipVertically;

                    float xVelocity = 32;
                    if (flopToLeft)
                    {
                        xVelocity = -xVelocity;
                    }
                    float yVelocity = -160;
                    Speed = new Vector2(xVelocity, yVelocity);
                }
                _currentState = value;
            }
        }
        public Lakitu(Vector2 position, Vector2 speed) : base(position, speed)
        {
            this.mario = mario;
            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.Lakitu);
            scale = 0.875f; // adjust the scale factor as desired
            Vector2 boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 2);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
            throwTimer = 0;
            enemyFactory = new EnemyFactory();
            EnemyState = new LakituState(this);
        }

        // Add a new class variable to control the chasing speed
        private float chasingSpeed = 2.0f;

        // Add a new class variable to control the interpolation speed
        private float interpolationSpeed = 0.005f;
        private float targetHorizontalSpeed = 5f;

        public override void Update(GameTime gameTime)
        {
            clampedXPos = Math.Clamp(Position.X, Camera.Instance.Position.X, Camera.Instance.Position.X + 256);
            Vector2 marioPosition = EntityManager.Instance.GetPlayer().Position;
            Vector2 targetPosition = new Vector2(marioPosition.X, marioPosition.Y - 100);
            float horizontalDiff = targetPosition.X - Position.X;
            float targetSpeed = Math.Sign(horizontalDiff) * targetHorizontalSpeed;
            chasingSpeed = MathHelper.Lerp(chasingSpeed, targetSpeed, interpolationSpeed);
            float newXPos = Math.Clamp(Position.X + chasingSpeed, Camera.Instance.Position.X, Camera.Instance.Position.X + 256);
            
            Position = new Vector2(newXPos, Position.Y);

            throwTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (throwTimer >= throwInterval)
            {
                Debug.WriteLine("Spawning Spiny");
                SpawnSpiny();
                throwTimer = 0;
            }
            base.Update(gameTime);
        }

        private void SpawnSpiny()
        {
            Entity spiny = EntityManager.Instance.AddEnemyEntity(Position, "spiny");
            Grid.Instance.AddEntity(spiny);
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch (other)
            {
                case Mario:
                    double xDifference = Math.Abs(other.Position.X - Position.X);
                    if (xDifference < 16 && other.Position.Y + 8 <= Position.Y)
                    {
                        EnemyState.Die();
                    }
                    break;
                case Fireball:
                    if (other.Position.X > Position.X)
                    {
                        flopToLeft = true;
                    }
                    EnemyState.Die();
                    break;
                case Block:
                    if (other.Speed.Y < 0)
                    {
                        if (other.Position.X > Position.X)
                        {
                            flopToLeft = true;
                        }
                        EnemyState.Die();
                    }
                    else if (other.Position.Y >= Position.Y + 10)
                    {
                        //_currentState.Landing();
                    }
                    else if ((LeftCollision < 2 || RightCollision < 2) && other.Position.Y - 16 < Position.Y)
                    {
                        flipSpeedX = true;
                    }
                    break;
                case Enemy:
                    Enemy collidedEntity = (Enemy)other;
                    if (collidedEntity.EnemyState is KoopaZoomState)
                    {
                        if (other.Position.X > Position.X)
                        {
                            flopToLeft = true;
                        }
                        EnemyState.Die();
                    }
                    else
                    {
                        flipSpeedX = true;
                    }
                    break;
                default:
                    flipSpeedX = true;
                    break;
            }
        }
    }
}
