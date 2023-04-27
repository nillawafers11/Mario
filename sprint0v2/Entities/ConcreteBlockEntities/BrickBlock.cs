using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteBlockEntities.BlockStates;
using sprint0v2.Entities.MarioStates.PowerUpStates;
using System.Diagnostics;
using sprint0v2.Sprites;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class BrickBlock : Block
    {
        private ISprite[] blockPieces;
        private Vector2 _bottomPiecesSpeed;
        public override int ItemCount {
            set { _currentItemCount = value; }
        }

        public event Action BumpAction;
        public event Action BreakAction;

        public override BlockState BlockState
        {
            set {
                if (value is BrickBumpState)
                {
                    if (itemType != ItemType.Nothing)
                    {
                        _currentItemCount--;
                    }
                    //Debug.WriteLine("entered bump state");
                }
                else if (value is BrickShatterState)
                {
                    Grid.Instance.RemoveEntity(this);
                    blockPieces = new ISprite[3];
                    Vector2 piece1Position = Sprite.SpritePosition;
                    Vector2 piece2Position = new(piece1Position.X + 8, piece1Position.Y);
                    Vector2 piece3Position = new(piece1Position.X, piece1Position.Y + 8);
                    Vector2 piece4Position = new(piece2Position.X, piece2Position.Y + 8);
                    float velocityX = -32;
                    float velocityY = -48 / (float)0.125;
                    speed = new Vector2(velocityX, velocityY);
                    _bottomPiecesSpeed = new Vector2(velocityX, velocityY + 24);
                    Sprite = BlockSpriteFactory.Instance.CreateSprite(piece1Position, (int)BlockSpriteFactory.eBlockSprite.BrickPiece);
                    blockPieces[0] = BlockSpriteFactory.Instance.CreateSprite(piece2Position, (int)BlockSpriteFactory.eBlockSprite.BrickPiece);
                    blockPieces[1] = BlockSpriteFactory.Instance.CreateSprite(piece3Position, (int)BlockSpriteFactory.eBlockSprite.BrickPiece);
                    blockPieces[2] = BlockSpriteFactory.Instance.CreateSprite(piece4Position, (int)BlockSpriteFactory.eBlockSprite.BrickPiece);
                }
                else if (value is UsedBlockState) {
                    Sprite.CurrentFrame = 2;
                }
                _currentState = value;
            }
            get { return _currentState; }
        }

        public Vector2 BottomPiecesSpeed {
            set { _bottomPiecesSpeed = value; }
            get { return _bottomPiecesSpeed; }
        }

        public BrickBlock(Vector2 position, Vector2 speed, ItemType itemHeld, int itemCount, bool isHidden, string type)
            : base(position, speed, itemHeld, itemCount, isHidden) {

            if (type == "underground")
            {
                Sprite = BlockSpriteFactory.Instance.CreateSprite(Position, (int)BlockSpriteFactory.eBlockSprite.UndergroundBrickBlock);
                BlockState = new BrickBlockState(this);
                AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
                boundingBox = AABB.GetBoundingBox();
            }
            else
            {
                Sprite = BlockSpriteFactory.Instance.CreateSprite(Position, (int)BlockSpriteFactory.eBlockSprite.BrickBlock);
                BlockState = new BrickBlockState(this);
                itemType = itemHeld;
                ItemCount = itemCount;
                AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
                boundingBox = AABB.GetBoundingBox();
            }
        }

        public override void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            if (blockPieces != null)
            {
                blockPieces[1].SpritePosition += BottomPiecesSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                blockPieces[1].Update(gameTime);
                blockPieces[0].SpritePosition += new Vector2(-Speed.X * (float)gameTime.ElapsedGameTime.TotalSeconds, Speed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
                blockPieces[0].Update(gameTime);
                blockPieces[2].SpritePosition += new Vector2(-BottomPiecesSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds, BottomPiecesSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
                blockPieces[2].Update(gameTime);
            }
            Sprite.Update(gameTime);
            AABB = new AABB(Position, new Vector2(16,16));
            boundingBox = AABB.GetBoundingBox();
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            if (other is Mario)
            {
                Mario collidedEntity = (Mario)other;

                float topCollision = collidedEntity.BoundingBox.Bottom - BoundingBox.Top;
                float bottomCollision = BoundingBox.Bottom - collidedEntity.BoundingBox.Top;

                if ((collidedEntity.PowerUpState is StandardMario || ItemHeld != ItemType.Nothing) && other.Position.Y >= Position.Y + 16)
                {
                    BumpAction?.Invoke();
                    _currentState.BrickBump(other);
                }
                else if (other.Position.Y >= Position.Y + 16)
                {
                    BreakAction?.Invoke();
                    _currentState.BrickBreak();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (blockPieces != null)
            {
                foreach (ISprite piece in blockPieces)
                {
                    piece.Draw(spriteBatch);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
