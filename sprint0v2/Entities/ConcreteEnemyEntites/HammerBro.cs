using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class HammerBro : Enemy
    {
        private double timeSinceLastThrow;
        private double throwInterval = 2; // Hammer throwing interval in seconds
        private Random random;

        public override EnemyState EnemyState
        {
            set
            {

                if (value is HammerBroFlopState)
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
        public HammerBro(Vector2 position, Vector2 speed) : base(position, speed)
        {
            random = new Random();

            EnemyState = new HammerBroIdleState(this);
            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.HammerBro);
            scale = 0.875f; // adjust the scale factor as desired
            Vector2 boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 2);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
            Sprite.SpriteFlip = SpriteEffects.FlipHorizontally;

            timeSinceLastThrow = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            timeSinceLastThrow += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastThrow >= throwInterval)
            {
                ThrowHammer();
                timeSinceLastThrow = 0;
            }
         
        }

        private void ThrowHammer()
        {
            // Adjust the initial position and speed of the hammer as needed
            Vector2 hammerPosition = Position + new Vector2(0, -20);
            float speedX = random.Next(30, 70) * (Sprite.SpriteFlip == SpriteEffects.FlipHorizontally ? -1 : 1); // Randomly choose speed in the direction Hammer Bro is facing
            Vector2 hammerSpeed = new Vector2(speedX, -150); // Fixed vertical speed
            EntityManager.Instance.AddHammer(hammerPosition, hammerSpeed);
        }


        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch (other)
            {
                case Mario:
                    double xDifference = Math.Abs(other.Position.X - Position.X);
                    if (xDifference < 16 && other.Position.Y + 14 <= Position.Y)
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
                    // Check if Hammer Bro landed on top of the block
                    if (other.Position.Y >= Position.Y + 10)
                    {
                        isGrounded= true;
                        _currentState.Landing();
                    }
                    // Check if Hammer Bro collided with the side of the block
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

    

