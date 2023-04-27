using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using sprint0v2.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class Bowser : Enemy
    {
        private Random random;

        private float hammerTimer;
        private int hammerDelay = 250;
        private int hammerCount = 4;

        private int bridgeLeft = 4608;
        private int bridgeRight = 4816;

        private int maxFireballAmount = 2;

        private int health;
        public int Health
        {
            get => health;
            set => health = value;
        }
        private Queue<Action> actionQueue;
        private float actionTimer;

        public override EnemyState EnemyState
        {
            set
            {
                if (value is BowserDefeatedState)
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

        public Bowser(Vector2 position, Vector2 speed) : base(position, speed)
        {
            random = new Random();

            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.Bowser);
            _currentState = new BowserNormalState(this);
            scale = 0.875f;
            Vector2 boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 2);
            AABB = new CollisionDetection.AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
            Sprite.SpriteFlip = SpriteEffects.FlipHorizontally;
            actionQueue = new Queue<Action>();
            actionTimer = 0;
            health = 10;
            hammerTimer = 4;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            hammerTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position = new Vector2(Math.Clamp(Position.X, bridgeLeft, bridgeRight), Position.Y);

            /*if(EntityManager.Instance.GetPlayer().Position.X <= Position.X - 300)
            {
                ShootFireballs();
            }*/
            if(hammerTimer <= 0)
            {
                ThrowHammers();
                hammerTimer = 4;
            }
            if (actionQueue.Count > 0 && actionTimer <= 0)
            {
                var nextAction = actionQueue.Dequeue();
                nextAction();
                actionTimer = random.Next(hammerDelay, hammerDelay * 2); // Adjust the range of the random delay as needed
            }

            if (actionTimer > 0)
            {
                actionTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch (other)
            {
                case Fireball:
                    TakeDamage();
                    break;
                case Block:
                    // Check if Bowser landed on top of the block
                    if (other.Position.Y >= Position.Y + 10)
                    {
                        isGrounded = true;
                        _currentState.Landing();
                    }
                    // Check if Bowser collided with the side of the block
                    else if ((LeftCollision < 2 || RightCollision < 2) && other.Position.Y - 16 < Position.Y)
                    {
                        flipSpeedX = true;
                    }
                    break;
                default:
                    flipSpeedX = true;
                    break;
            }
        }

        public void TakeDamage()
        {
            health--;
            if (health <= 0)
            {
                _currentState.Die();
            }
        }

        private void ThrowHammers()
        {
            for (int i = 0; i < 5; i++) // Throw 5 hammers
            {
                actionQueue.Enqueue(() =>
                {
                    // Adjust the initial position and speed of the hammer as needed
                    Vector2 hammerPosition = Position + new Vector2(0, -20);
                    float speedX = random.Next(30, 70) * (Sprite.SpriteFlip == SpriteEffects.FlipHorizontally ? -1 : 1); // Randomly choose speed in the direction Hammer Bro is facing
                    Vector2 hammerSpeed = new Vector2(speedX, -150); // Fixed vertical speed
                    EntityManager.Instance.AddHammer(hammerPosition, hammerSpeed);
                });
            }
        }

        private void ShootFireballs()
        {
            EntityManager.Instance.AddBowserFireball(Position, new Vector2(-10, 0));
            maxFireballAmount--;
        }
    }
}
