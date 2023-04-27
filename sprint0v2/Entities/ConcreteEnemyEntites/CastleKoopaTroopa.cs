using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using System;
using System.Diagnostics;
using System.Threading;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class CastleKoopaTroopa : Enemy
    {
        public override EnemyState EnemyState
        {
            get
            {
                return _currentState;
            }
            set
            {
                if (value is KoopaNormalState)
                {
                    Sprite.IsAnimated = true;
                    Sprite.CurrentFrame = 0;
                    boxOffset = new Vector2(1, 3);
                    AABB.Size = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
                    AABB.Position = Position + boxOffset;
                }
                else if (value is KoopaStompedState)
                {
                    Sprite.IsAnimated = false;
                    Sprite.CurrentFrame = 2;
                    boxOffset = new Vector2(1, 2);
                    AABB.Size = new Vector2(16 * scale - 1, 16 * scale);
                    AABB.Position = Position + boxOffset;
                }
                else if (value is KoopaFlopState)
                {
                    Grid.Instance.RemoveEntity(this);
                    Sprite.IsAnimated = false;
                    Sprite.CurrentFrame = 2;
                    Sprite.SpriteFlip = SpriteEffects.FlipVertically;

                    float xVelocity = 32;
                    if (flopToLeft)
                    {
                        xVelocity = -xVelocity;
                    }
                    float yVelocity = -160;
                    Speed = new Vector2(xVelocity, yVelocity);
                }
                else if (value is KoopaZoomState)
                {
                    //Debug.WriteLine("zoomed");
                    float xVelocity = 96;
                    if (flopToLeft)
                    {
                        xVelocity = -xVelocity;
                    }
                    Speed = new Vector2(xVelocity, 0);
                }
                _currentState = value;
            }

        }
        public CastleKoopaTroopa(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            _currentState = new KoopaNormalState(this);
            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.CastleKoopaTroopa);
            //Debug.WriteLine("Resizing AABB");
            scale = 0.875f;
            boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 3);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
            if (Speed.X < 0)
            {
                Sprite.SpriteFlip = SpriteEffects.FlipHorizontally;
            }
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch (other)
            {
                case Mario:
                    if (other.Position.X > Position.X)
                    {
                        flopToLeft = true;
                    }
                    else
                    {
                        flopToLeft = false;
                    }
                    EnemyState.KoopaKick();
                    double xDifference = Math.Abs(other.Position.X - Position.X);
                    if (xDifference < 16 && other.Position.Y + 12 <= Position.Y)
                    {
                        EnemyState.KoopaStomp();
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
                    else if (other.Position.Y >= Position.Y + 14)
                    {
                        _currentState.Landing();
                    }
                    else if (other.Position.Y - Sprite.Texture.Height < Position.Y)
                    {
                        flipSpeedX = true;
                        ToggleSpriteFlip();
                    }
                    break;
                case Enemy:
                    if (EnemyState is not KoopaZoomState)
                    {
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
                            ToggleSpriteFlip();
                        }
                    }
                    break;
                default:
                    flipSpeedX = true;
                    ToggleSpriteFlip();
                    break;
            }
        }
    }
}
