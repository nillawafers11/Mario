using sprint0v2.Sprites;
using sprint0v2.CollisionDetection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using sprint0v2.Entities.MarioStates.PowerUpStates;
using System.Diagnostics;
using sprint0v2.Entities.ConcreteItemEntites;
using sprint0v2.Entities.ConcreteEnemyEntites;
using sprint0v2.Entities.ConcreteBlockEntities;
using sprint0v2.GameState;
using sprint0v2.Controllers;

namespace sprint0v2.Entities
{
    public class Mario : Entity
    {
        ActionState _currentState;
        IPowerUpState _powerUpState;
        public event EventHandler<LivesChangedEventArgs> LivesChanged;
        public event EventHandler<CoinsChangedEventArgs> CoinsChanged;
        public event EventHandler<PointsChangedEventArgs> PointsChanged;
        public event EventHandler Jumped;
        public event EventHandler TookDamage;
        private DateTime _lastDamageTime = DateTime.MinValue;
        private TimeSpan _damageCooldown = TimeSpan.FromSeconds(2);
        private void OnLivesChanged(int lives)
        {
            LivesChanged?.Invoke(this, new LivesChangedEventArgs(lives));
        }

        private void OnCoinsChanged(int coins)
        {
            CoinsChanged?.Invoke(this, new CoinsChangedEventArgs(coins));
        }

        private void OnPointsChanged(int points)
        {
            PointsChanged?.Invoke(this, new PointsChangedEventArgs(points));
        }
        private void OnJumped()
        {
            Jumped?.Invoke(this, EventArgs.Empty);
        }

        public bool IsCollidingTop(Rectangle box)
        {
            return BoundingBox.Bottom >= box.Top &&
                   BoundingBox.Bottom <= box.Top + 5 &&
                   BoundingBox.Right >= box.Left + 2 &&
                   BoundingBox.Left <= box.Right - 2;
        }
        public bool IsCollidingLeft(Rectangle box)
        {
            return BoundingBox.Right >= box.Left &&
                   BoundingBox.Right <= box.Left + 5 &&
                   BoundingBox.Bottom >= box.Top + 2 &&
                   BoundingBox.Top <= box.Bottom - 2;
        }
        public bool IsCrouching()
        {
            if (_currentState is CrouchingState)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public ActionState ActionState
        {
            get => _currentState;
            set => _currentState = value;
        }
        public IPowerUpState PowerUpState
        {
            get => _powerUpState;
            set => _powerUpState = value;
        }
        public float GroundY { get; private set; }
        
        private int lives;
        private int points;
        private int coins;
        public int Lives
        {
            get => lives;
            set
            {
                if (lives != value)
                {
                    lives = value;
                    OnLivesChanged(lives);
                }
            }
        }

        public int Coins
        {
            get => coins;
            set
            {
                if (coins != value)
                {
                    coins = value;
                    OnCoinsChanged(coins);
                }
            }
        }

        public int Points
        {
            get => points;
            set
            {
                if (points != value)
                {
                    points = value;
                    OnPointsChanged(points);
                }
            }
        }

        public bool IsGrounded
        { 
            get => isGrounded; 
            set => isGrounded = value; 
        }

        private bool isFinished = false;
        public bool IsFinished
        {
            get => isFinished;
            set => isFinished = value;
        }

        private bool alive;
        private MarioCollisionHandler _collisionHandler;
        public MarioCollisionHandler CollisionHandler
        {
            get => _collisionHandler;
            set => _collisionHandler = value;
        }
        private double deathDuration;
        private double starDuration = 12;

        #region Sound Effect Events
        public event Action SmallJumpAction;
        public event Action SuperJumpAction;
        public event Action DeadAction;
        public event Action FireballAction;
        public event Action PowerupAction;
        public event Action OneUpAction;
        public event Action StageClearAction;
        public event Action StarmanBeginAction;
        public event Action StarmanEndAction;
        //public event Action StarmanAction;
        #endregion

        public Mario(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            Sprite = PlayerSpriteFactory.Instance.CreateSprite(Position, (int)PlayerSpriteFactory.ePlayerSprite.SmallMario);
            _powerUpState = new StandardMario(this);
            _currentState = new IdleState(this);
            AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            _collisionHandler = new MarioCollisionHandler(this);
            isGrounded = false;
            alive = true;
            Position = position;
            Speed = speed;
            lives = 3;
            coins = 0;
            points = 0;
            BoundingBox = AABB.GetBoundingBox();
            BoundingBoxColor = Color.White;
            GroundY = 400;
        }

        public override void Update(GameTime gameTime)
        {
            /*Debug.WriteLine("" + _powerUpState.ToString());*/
            if (alive)
            {
                _currentState.Update(gameTime);

                //Debug.WriteLine(gameTime.ElapsedGameTime.TotalSeconds);
                Position += Speed * 50 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
                BoundingBox = AABB.GetBoundingBox();
                Sprite.SpritePosition = Position;
                if(Position.Y + BoundingBox.Height > 238)
                {
                    Dead();
                }
                Sprite.Update(gameTime);                
                if (_currentState is not JumpingState && !isGrounded)
                {
                    Fall();
                }
                //Debug.WriteLine("Points = " + Points);
            }
            else
            {
                SpeedY += acceleration * 6.4f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                deathDuration -= gameTime.ElapsedGameTime.TotalSeconds;
                Sprite.SpritePosition = Position;
                Sprite.Update(gameTime);
                if (lives > 0 && deathDuration < 0)
                {
                    //lives--;
                    GameStateSnapshot.Instance.ReturnToSnapshot();
                }
                else
                {
                    //game over
                }
            }

            if(PowerUpState is StarMario)
            {
                starDuration -= gameTime.ElapsedGameTime.TotalSeconds;
                if(starDuration <= 0)
                {
                    Super();
                    StarmanEndAction?.Invoke();
                }
            }
        }

        public override void OnCollision(Entity other)
        {
            if (alive)
            {
                // Check if Mario is within the cooldown period
                if (other is Enemy && DateTime.UtcNow - _lastDamageTime < _damageCooldown)
                {
                    return; // Ignore collision
                }
           
                _collisionHandler.OnCollision(other);
            }
        }

        public void Jump()
        {
            if (_powerUpState is not DeadMario)
            {
                if (!isGrounded)
                {
                    return;
                }
                if (_powerUpState is StandardMario)
                {
                    SmallJumpAction?.Invoke();
                }
                else
                {
                    SuperJumpAction?.Invoke();
                }
                _currentState.JumpingTransition();
                isGrounded = false;
                OnJumped();
            }
        }

        public void Bounce()
        {
            _currentState.BounceTransition();
            isGrounded = false;
        }

        public void Fall()
        {
            if (alive)
            {
                if (_currentState is not FallingState && _currentState is not FallingLeftState && _currentState is not FallingRightState && _currentState is not BouncingState && _currentState is not FlagFallState)
                {
                    _currentState.FallingTransition();
                }
            }
        }

        public void MoveLeft()
        {
            if (_powerUpState is not DeadMario)
            {
                if (Speed.X > 0 && _currentState is not JumpingState && _currentState is not FallingState && _currentState is not IdleState)
                {
                    _currentState.IdleTransition();
                }
                else if (Sprite.SpriteFlip != SpriteEffects.FlipHorizontally)
                {
                    _currentState.FaceLeftTransition();
                }
                else if (_currentState is JumpingState)
                {
                    _currentState.JumpingLeftTransition();
                }
                else if (_currentState is FallingState)
                {
                    _currentState.FallingLeftTransition();
                }
                else if (_currentState is IdleState)
                {
                    _currentState.RunningTransition();
                }
            }
        }

        public void StopMovingLeft()
        {
            if (_currentState is FallingLeftState)
            {
                _currentState.FallingTransition();
            }
            else if (_currentState is not IdleState && _currentState is not FallingState && _currentState is not JumpingState)
            {
                _currentState.IdleTransition();
            }
        }

        public void MoveRight()
        {
            if (_powerUpState is not DeadMario)
            {
                if (Speed.X < 0 && _currentState is not JumpingState && _currentState is not FallingState && _currentState is not IdleState)
                {
                    _currentState.IdleTransition();
                }
                else if (Sprite.SpriteFlip != SpriteEffects.None)
                {
                    _currentState.FaceRightTransition();
                }
                else if (_currentState is JumpingState)
                {
                    _currentState.JumpingRightTransition();
                }
                else if (_currentState is FallingState && _currentState is not FallingRightState)
                {
                    _currentState.FallingRightTransition();
                }
                else if (_currentState is IdleState && _currentState is not JumpingState && _currentState is not FallingState)
                {
                    _currentState.RunningTransition();
                }
            }
        }

        public void StopMovingRight()
        {
            if (_currentState is FallingRightState)
            {
                _currentState.FallingTransition();
            }
            else if (_currentState is not IdleState && _currentState is not FallingState && _currentState is not JumpingState)
            {
                _currentState.IdleTransition();
            }
        }

        public void Crouch()
        {
            if (_powerUpState is not DeadMario)
            {
                //change to idle state if jumping
                if (_currentState is JumpingState)
                {
                    _currentState.IdleTransition();
                }
                //undo crouching
                else if (_currentState is CrouchingState)
                {
                    _currentState.IdleTransition();
                }
                //change to crouch state if idle
                else if (_currentState is IdleState)
                {
                    _currentState.CrouchingTransition();
                }
            }
        }

        public void Standard()
        {
            SpriteEffects previousFlip = Sprite.SpriteFlip;
            _powerUpState.StandardTransition();
            Sprite = PlayerSpriteFactory.Instance.CreateSprite(Position, (int)PlayerSpriteFactory.ePlayerSprite.SmallMario);
            Sprite.SpriteFlip = previousFlip;
            alive = true;
        }

        public void Super()
        {
            Position = new(Position.X, Position.Y - 8);
            SpriteEffects previousFlip = Sprite.SpriteFlip;
            _powerUpState.SuperTransition();
            Sprite = PlayerSpriteFactory.Instance.CreateSprite(Position, (int)PlayerSpriteFactory.ePlayerSprite.SuperMario);
            Sprite.SpriteFlip = previousFlip;
            alive = true;
            PowerupAction?.Invoke();
        }

        public void Fire()
        {
            Position = new(Position.X, Position.Y - 8);
            SpriteEffects previousFlip = Sprite.SpriteFlip;
            FireMario fireState = new FireMario(this);
            fireState.Enter(_powerUpState);
            Sprite = PlayerSpriteFactory.Instance.CreateSprite(Position, (int)PlayerSpriteFactory.ePlayerSprite.FireMario);
            Sprite.SpriteFlip = previousFlip;
            alive = true;
            PowerupAction?.Invoke();
        }

        public void Starman()
        {
            Position = new(Position.X, Position.Y - 8);
            SpriteEffects previousFlip = Sprite.SpriteFlip;
            _powerUpState.StarTransition();
            Sprite = PlayerSpriteFactory.Instance.CreateSprite(Position, (int)PlayerSpriteFactory.ePlayerSprite.StarMario);
            Sprite.SpriteFlip = previousFlip;
            alive = true;
            StarmanBeginAction?.Invoke();
        }

        public void Dead()
        {
            Grid.Instance.RemoveEntity(this);
            deathDuration = 2;
            alive = false;
            if (_powerUpState is SuperMario || _powerUpState is FireMario)
            {
                Position = new(Position.X, Position.Y + 16);
            }
            _powerUpState = new DeadMario(this);
            Speed = new Vector2(0, -96);
            Sprite = PlayerSpriteFactory.Instance.CreateSprite(Position, (int)PlayerSpriteFactory.ePlayerSprite.Dead);
            Lives--;
            Points = 0;
            DeadAction?.Invoke();
                
        }

        public void TakeDamage()
        {
            if ((DateTime.UtcNow - _lastDamageTime) < _damageCooldown)
            {
                // The cooldown period has not elapsed yet, so do not take damage.
                return;
            }

            // The cooldown period has elapsed, so take damage.
            Speed = new Vector2(0, 0);
            if (_powerUpState is FireMario or SuperMario)
            {
                
                Standard();
                _lastDamageTime = DateTime.UtcNow;
                Position = new(Position.X, Position.Y + 16);
            }
            else Dead();

            
        }

        public void ShootFireball()
        {
            if (_powerUpState is FireMario)
            {
                Fireball fireball = new Fireball(Position, Speed);
                if (Sprite.SpriteFlip == SpriteEffects.None)
                {
                    fireball.Position = new Vector2(Position.X + 20, Position.Y + 10);
                    fireball.Speed = new Vector2(100f, 20f);
                }
                else
                {
                    fireball.Position = new Vector2(Position.X - 17, Position.Y + 10);
                    fireball.Speed = new Vector2(-100f, 20f);
                }
                fireball.SpawnFireball();
                FireballAction?.Invoke();
            }
        }

        public void CollectCoin()
        {
            Coins++;
            Points += 200;
            if(Coins % 100 == 0)
            {
                Lives++;
            }
        }

        public void CollectRewards(Entity other)
        {
            switch (other)
            {
                case SuperMushroom:
                    Points += 1000;
                    break;
                case FireFlower:
                    Points += 1000;
                    break;
                case Star:
                    Points += 1000;
                    break;
                case OneUpMushroom:
                    Lives++;
                    OneUpAction?.Invoke();
                    break;
                case Goomba:
                    Points += 100;
                    break;
                case GreenKoopaTroopa:
                    Points += 100;
                    break;
            }
        }

        public void ClimbVine()
        {
            _currentState.ClimbingTransition();
        }

        public void FlagpoleAnimation(FlagBlock flagBlock)
        {
            //do
            //{
            //    Position = new Vector2(flagBlock.AABB.Position.X, Position.Y);
            //    Speed = new Vector2(0, 0.5f);
            //    Position += Speed;
            //} while (Position.Y < flagBlock.BottomCollision);
            IsFinished = false;
            Grid.Instance.RemoveEntity(this);

            _currentState.FlagFallTransition();
            StageClearAction?.Invoke();
        }

    }
    public class LivesChangedEventArgs : EventArgs
    {
        public int Lives { get; set; }

        public LivesChangedEventArgs(int lives)
        {
            Lives = lives;
        }
    }

    public class CoinsChangedEventArgs : EventArgs
    {
        public int Coins { get; set; }

        public CoinsChangedEventArgs(int coins)
        {
            Coins = coins;
        }
    }

    public class PointsChangedEventArgs : EventArgs
    {
        public int Points { get; set; }

        public PointsChangedEventArgs(int points)
        {
            Points = points;
        }
    }
    public class Jumped : EventArgs
    {
        
    }
}

