using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using sprint0v2.View;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class Goomba : Enemy
    {
        public override EnemyState EnemyState {
            set {
                if (value is GoombaStompedState)
                {
                    Grid.Instance.RemoveEntity(this);
                    Sprite.IsAnimated = false;
                    Sprite.CurrentFrame = 2;
                    Speed = new Vector2(0, 0);
                }
                else if (value is GoombaFlopState) {
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

        public Goomba(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.Goomba);
            EnemyState = new GoombaNormalState(this);
            scale = 0.875f; // adjust the scale factor as desired
            Vector2 boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 2);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch(other){
                case Mario:
                    double xDifference = Math.Abs(other.Position.X - Position.X);
                    if (xDifference < 16 && other.Position.Y + 14 <= Position.Y) {
                        EnemyState.GoombaStomp();
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
                        _currentState.Landing();
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
