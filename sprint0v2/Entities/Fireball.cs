using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System;
using System.Collections;

namespace sprint0v2.Entities
{
    public class Fireball : Entity
    {
        private Vector2 acceleration = new Vector2(0, 512);

        private bool hasExploded = false;
        private float explodeTimer;
        private const float explodeDelaySeconds = 0.45f; // change to desired explosion time before removal
        private float remainingExplodeDelay = explodeDelaySeconds;

        private float onScreenTimer;
        private const float onScreenTimeSeconds = 4f; // change to desired time on screen before removal
        private float remainingOnScreenTime = onScreenTimeSeconds;

        private const int maxFireballAmount = 2;
        private static readonly Queue fireballPool = new Queue(maxFireballAmount);

        private const float screenHeight = 238f; // change to desired screen height

        public Fireball(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            Position = position;
            Speed = speed;
            Sprite = FireballSpriteFactory.Instance.CreateSprite(Position, (int)FireballSpriteFactory.eFireballSprite.Normal);
            AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            BoundingBox = AABB.GetBoundingBox();
            BoundingBoxColor = Color.Indigo;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Speed.Y > 0)
            {
                Speed = new Vector2(Speed.X, Math.Clamp(Speed.Y, 0, 100));
            }
            else if(Speed.Y < 0)
            {
                Speed = new Vector2(Speed.X, Math.Clamp(Speed.Y, -100, 0));
            }

            Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            BoundingBox = AABB.GetBoundingBox();

            if(Position.Y >= screenHeight)
            {
                RemoveFireball();
            }

            #region On Screen Timer
            onScreenTimer = (float)gameTime.ElapsedGameTime.TotalSeconds;
            remainingOnScreenTime -= onScreenTimer;
            if (remainingOnScreenTime <= 0)
            {
                RemoveFireball();
            }
            #endregion
            #region Explode Timer
            if (hasExploded)
            {
                explodeTimer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                remainingExplodeDelay -= explodeTimer;
            }
            if (remainingExplodeDelay <= 0)
            {
                RemoveFireball();
            }
            #endregion


        }

        public override void OnCollision(Entity other)
        {
            float leftCollision = BoundingBox.Right - other.BoundingBox.Left;
            float rightCollision = other.BoundingBox.Right - BoundingBox.Left;
            float topCollision = BoundingBox.Bottom - other.BoundingBox.Top;
            float bottomCollision = other.BoundingBox.Bottom - BoundingBox.Top;

            if (other is Block)
            {
                HandleBlockCollision(other as Block, leftCollision, rightCollision, topCollision, bottomCollision);
            }
            else if (other is Enemy)
            {
                RemoveFireball();
            }
        }

        private void HandleBlockCollision(Block block, float leftCollision, float rightCollision, float topCollision, float bottomCollision)
        {
            float minHorizontalOverlap = Math.Min(leftCollision, rightCollision);
            float minVerticalOverlap = Math.Min(topCollision, bottomCollision);
            float pixelBuffer = 4f;

            // Fireball collides with the top or bottom of a block
            if (minVerticalOverlap < minHorizontalOverlap + pixelBuffer)
            {
                float mtvY = topCollision < bottomCollision ? -minVerticalOverlap : minVerticalOverlap;
                Position += new Vector2(0, mtvY);
                Bounce(block);
            }
            // Fireball collides with the side of a block
            else
            {
                Explode();
            }
        }

        public void Bounce(Block block)
        {
            float threshold = 2f;

            if (Position.Y + BoundingBox.Height <= block.BoundingBox.Top + threshold)
            {
                Speed = new Vector2(Speed.X, -Math.Abs(Speed.Y));
            }
            else if (Position.Y >= block.BoundingBox.Bottom - threshold)
            {
                Speed = new Vector2(Speed.X, Math.Abs(Speed.Y));
            }
        }

        public void SpawnFireball()
        {
            if (fireballPool.Count < maxFireballAmount)
            {
                Fireball newFireball = (Fireball)EntityManager.Instance.AddFireball(Position, Speed);
                Grid.Instance.AddEntity(newFireball);
                fireballPool.Enqueue(this);
            }
        }
        public void RemoveFireball()
        {
            if(fireballPool.Count > 0)
            {
                EntityManager.Instance.RemoveEntity(this);
                Grid.Instance.RemoveEntity(this);
                fireballPool.Dequeue();
                IsRemoved = true;
            }
        }
        public void Explode()
        {
            hasExploded = true;
            Speed = Vector2.Zero;
            Sprite = FireballSpriteFactory.Instance.CreateSprite(Position, (int)FireballSpriteFactory.eFireballSprite.Exploding);
            acceleration = new(0, 0);
            Grid.Instance.RemoveEntity(this);
        }
    }
}
