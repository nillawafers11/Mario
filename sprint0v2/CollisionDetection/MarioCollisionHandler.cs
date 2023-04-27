using Microsoft.Xna.Framework;
using sprint0v2.Entities.ConcreteBlockEntities;
using sprint0v2.Entities.ConcreteEnemyEntites;
using sprint0v2.Entities.ConcreteItemEntites;
using sprint0v2.Entities.MarioStates.PowerUpStates;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace sprint0v2.Entities
{
    public class MarioCollisionHandler
    {
        private Mario _mario;
        public event Action CastleAxeAction;

        #region Sound Effect Events
        public event Action StompAction;
        public event Action BumpAction;
        public event Action BreakAction;
        public event Action WarpPipeAction;
        public event Action KoopaKickAction;
        public event Action PowerupAppearsAction;
        public event Action FlagpoleAction;
        #endregion

        public MarioCollisionHandler(Mario mario)
        {
            _mario = mario;
            CastleAxeAction += MarioCollisionHandler_CastleAxeAction;
        }

        

        public void OnCollision(Entity other)
        {
            // Get the vertical and horizontal overlap of the AABBs
            float leftCollision = _mario.BoundingBox.Right - other.BoundingBox.Left;
            float rightCollision = other.BoundingBox.Right - _mario.BoundingBox.Left;
            float topCollision = _mario.BoundingBox.Bottom - other.BoundingBox.Top;
            float bottomCollision = other.BoundingBox.Bottom - _mario.BoundingBox.Top;

            if (other is Item)
            {
                HandleItemCollision(other as Item);
            }
            else if (other is Block)
            {
                HandleBlockCollision(other as Block, leftCollision, rightCollision, topCollision, bottomCollision);
            }
            else if (other is Enemy)
            {
                HandleEnemyCollision(other as Enemy, leftCollision, rightCollision, topCollision, bottomCollision);
            }
            else if (other is Fireball || other is BowserFireball)
            {
                HandleFireballCollision(leftCollision, rightCollision, topCollision, bottomCollision);
            }
            else if (other is Hammer)
            {
                _mario.TakeDamage();
            }
        }

        private void HandleFireballCollision(float leftCollision, float rightCollision, float topCollision, float bottomCollision)
        {
            if(bottomCollision < topCollision || topCollision < bottomCollision || rightCollision < leftCollision || leftCollision < rightCollision)
            {
                _mario.TakeDamage();
            }
        }
        private void HandleItemCollision(Item item)
        {
            if (item is FireFlower)
            {
                _mario.Fire();
                _mario.CollectRewards(item as FireFlower);
            }
            else if (item is SuperMushroom)
            {
                _mario.Super();
                _mario.CollectRewards(item as SuperMushroom);
            }
            else if (item is Star)
            {
                _mario.Starman();
                _mario.CollectRewards(item as Star);
            }
            else if (item is Coin || item is StaticCoin)
            {
                _mario.CollectCoin();
            }
            else if (item is Vine)
            {
                _mario.ClimbVine();
            }
            else if (item is CastleAxe)
            {
                CastleAxeAction?.Invoke();
            }

        }

        private void MarioCollisionHandler_CastleAxeAction()
        {
            EntityManager.Instance.RemoveBridge();
            CastleAxeAction -= MarioCollisionHandler_CastleAxeAction;
        }

        private void HandleBlockCollision(Block block, float leftCollision, float rightCollision, float topCollision, float bottomCollision)
        {
           float buffer = 2.0f; 
            if (block is QuestionBlock && block.IsHidden)
            {
                // If Mario collides with the top or sides of a hidden block, do nothing
                if ((topCollision < bottomCollision - buffer && _mario.Speed.Y > 0) ||
                    (leftCollision < rightCollision && leftCollision < topCollision && leftCollision < bottomCollision && _mario.Speed.X > 0) ||
                    (rightCollision < leftCollision && rightCollision < topCollision && rightCollision < bottomCollision && _mario.Speed.X < 0))
                {
                    return;
                }
            }

            if (block is CastleBlock)
            {
                Game1 game = new Game1();
                game.NextLevel();
            }

            // If Mario collides with the bottom of a block, he should fall
            if (bottomCollision < topCollision && _mario.Speed.Y < 0)
            {
                if(block.ItemHeld == ItemType.Coin)
                {
                    _mario.CollectCoin();
                }
                if(block.ItemHeld == ItemType.SuperMushroom || block.ItemHeld == ItemType.FireFlower || block.ItemHeld == ItemType.OneUpMushroom)
                {
                    PowerupAppearsAction?.Invoke();
                }
                _mario.Position = new Vector2(_mario.Position.X, block.BoundingBox.Bottom + 1);
                _mario.Fall();
                if (block.ItemHeld == ItemType.Nothing)
                {
                    if(_mario.PowerUpState is StandardMario)
                    {
                        BumpAction?.Invoke();
                    }
                    else
                    {
                        BreakAction?.Invoke();
                    }
                }
            }
            // If Mario collides with the top of a block
            else if (topCollision < bottomCollision - buffer && _mario.Speed.Y > 0 && topCollision < buffer * 2)
            {
                _mario.Position = new Vector2(_mario.Position.X, block.BoundingBox.Top - _mario.Sprite.Texture.Height);
                //speedY = 0f;

                if (_mario.ActionState is not JumpingState)
                    _mario.IsGrounded = true;
                if (_mario.Speed.X == 0)
                {
                    if (_mario.ActionState is not IdleState && _mario.ActionState is not CrouchingState)
                    {
                        _mario.ActionState.IdleTransition();
                    }
                }
                if (block is Platform)
                {
                    if (_mario.ActionState is IdleState || _mario.ActionState is CrouchingState)
                    {

                        _mario.SpeedX = block.SpeedX/50;
                    }
                }
                if (block is PipeBlock && _mario.ActionState is CrouchingState)
                {
                    PipeBlock pipe = (PipeBlock)block;
                    if(pipe.PipeType == "warppipe")
                    {
                        WarpPipeAction?.Invoke();
                    }
                }
            }
            else
            {
                // Check for more significant side collisions

                if (leftCollision < rightCollision && leftCollision < topCollision && leftCollision < bottomCollision && _mario.Speed.X > 0)
                {
                    _mario.Position = new Vector2(block.BoundingBox.Left - _mario.BoundingBox.Width - 1, _mario.Position.Y);
                    _mario.Speed = new Vector2(0, _mario.Speed.Y);
                    if (_mario.ActionState is not JumpingState)
                    {
                        _mario.ActionState.IdleTransition();
                    }
                    if (block is FlagBlock)
                    {
                        FlagBlock flag = (FlagBlock)block;
                        _mario.Points += flag.GetPoints(_mario);
                        _mario.IsFinished = true;
                        _mario.FlagpoleAnimation(flag);
                        FlagpoleAction?.Invoke();
                    }
                }
                else if (rightCollision < leftCollision && rightCollision < topCollision && rightCollision < bottomCollision && _mario.Speed.X < 0)
                {
                    _mario.Position = new Vector2(block.BoundingBox.Right + 1, _mario.Position.Y);
                    _mario.Speed = new Vector2(0, _mario.Speed.Y);
                    if (_mario.ActionState is not JumpingState)
                    {
                        _mario.ActionState.IdleTransition();
                    }
                }
            }
        }

        private void HandleEnemyCollision(Enemy enemy, float leftCollision, float rightCollision, float topCollision, float bottomCollision)
        {

            // Check if the collision occurred from the sides or top
            float overLapX = Math.Min(leftCollision, rightCollision);
            float overLapY = Math.Min(topCollision, bottomCollision);
            if(_mario.PowerUpState is not StarMario)
            {
                if (enemy is PiranhaPlant || enemy is FireBar)
                {
                    _mario.TakeDamage();
                }
                else if (overLapX < overLapY * 1.25f)
                {
                    // Collision occurred from the sides
                    if ((enemy is GreenKoopaTroopa) && enemy._currentState is KoopaStompedState)
                    {
                        KoopaKickAction?.Invoke();
                    }
                    else
                    {
                        _mario.TakeDamage();
                    }

                    if (leftCollision < rightCollision)
                    {
                        _mario.Position = new Vector2(enemy.BoundingBox.Left - _mario.BoundingBox.Width, _mario.Position.Y);
                    }
                    else
                    {
                        _mario.Position = new Vector2(enemy.BoundingBox.Right, _mario.Position.Y);
                    }
                }
                else
                {

                    if (topCollision < bottomCollision)
                    {
                        _mario.Position = new Vector2(_mario.Position.X, enemy.BoundingBox.Top - _mario.BoundingBox.Height);
                        _mario.Bounce();
                        _mario.CollectRewards(enemy);

                        StompAction?.Invoke();
                    }
                    else
                    {
                        _mario.Speed = new Vector2(0, 0);
                        _mario.Position = new Vector2(_mario.Position.X, enemy.BoundingBox.Bottom);
                        _mario.Fall();
                    }
                }
            }
            else
            {
                enemy.EnemyState.Die();
                StompAction?.Invoke();
            }         
        }
    }
}
