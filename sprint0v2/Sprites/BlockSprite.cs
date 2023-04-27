using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using System;

namespace sprint0v2.Sprites
{
    public class BlockSprite : ISprite
    {

        protected int Rows;
        protected int Columns;
        public int SetColumns
        {
            set => Columns = value;
        }
        protected int currentFrame;
        protected readonly int totalFrames;
        protected Vector2 spritePosition;
        public Vector2 SpritePosition
        {
            get => spritePosition;
            set => spritePosition = value;
        }
        protected SpriteEffects spriteFlip;
        protected int timeSinceLastFrame;
        protected readonly int millisecondsPerFrame = 200;
        protected bool isAnimated;
        public SpriteEffects SpriteFlip
        {
            get { return SpriteEffects.None; }
            set { }
        }
        public bool IsAnimated
        {
            get => isAnimated;
            set => isAnimated = value;
        }
        public int CurrentFrame
        {
            set => currentFrame = value;
        }
        public float SpritePositionY
        {
            get => spritePosition.Y;
            set => spritePosition.Y = value;
        }
        protected Texture2D texture;
        public Texture2D Texture
        {
            get => texture;
            set {
                texture = value;
            }
        }
        Texture2D ISprite.Texture { get => Texture; set => Texture = value; }
        SpriteEffects ISprite.SpriteFlip { get => SpriteFlip; set => SpriteFlip = value; }
        bool ISprite.IsAnimated { get => isAnimated; set => isAnimated = value; }
        int ISprite.CurrentFrame { get => currentFrame; set => currentFrame = value; }
        int ISprite.Rows => Rows;
        int ISprite.Columns => Columns;
        protected int AnimationFrames;

        private Rectangle frameRectangle;
        private Rectangle locationRectangle;
        private float zDepth;

        public BlockSprite(Texture2D texture, int rows, int columns, int animationFrames, Vector2 position, bool Animated)
        {
            this.texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            spritePosition = position;
            isAnimated = Animated;
            AnimationFrames = animationFrames;
            zDepth = 0;

            int width = this.texture.Width / Columns;
            int height = this.texture.Height / Rows;
            int frameOffset = currentFrame % Columns;

            frameRectangle = new Rectangle(frameOffset * width, 0, width, height);
            locationRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, width, height);
        }

        public BlockSprite(Texture2D texture, int rows, int columns, int animationFrames, Vector2 position, bool Animated, float newDepth)
        {
            this.texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            spritePosition = position;
            isAnimated = Animated;
            AnimationFrames = animationFrames;
            zDepth = newDepth;

            int width = this.texture.Width / Columns;
            int height = this.texture.Height / Rows;
            int frameOffset = currentFrame % Columns;

            frameRectangle = new Rectangle(frameOffset * width, 0, width, height);
            locationRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, width, height);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (isAnimated)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame -= millisecondsPerFrame;
                    ++currentFrame;
                    if (currentFrame >= AnimationFrames)
                    {
                        currentFrame = 0;
                    }
                }
            }

            locationRectangle.X = (int)spritePosition.X;
            locationRectangle.Y = (int)spritePosition.Y;


        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Columns;
            int frameOffset = currentFrame % Columns;
            frameRectangle.X = frameOffset * width;

            if (texture.Name == "groundblock")
            {
                int testInt = 1; //this is a breakpoint insert, can be deleted later
            }
            spriteBatch.Draw(texture, locationRectangle, frameRectangle, Color.White, 0, Vector2.Zero, spriteFlip, zDepth);

        }
        

        void ISprite.SetTexture(Texture2D texture)
        {
            throw new NotImplementedException();
        }
    }

    public class BlockPieceSprite : BlockSprite {
        int _currentFrame;
        int width = 8;
        int height = 8;
        private Rectangle frameRectangle;
        private Rectangle locationRectangle;

        public int DisplayFrame
        {
            set { _currentFrame = value; }
            get { return _currentFrame; }
        }
        public BlockPieceSprite(Texture2D newTexture, int rows, int columns, int animationFrames, Vector2 newPosition, SpriteEffects flip, bool animated) 
            : base(newTexture, rows, columns, animationFrames, newPosition, animated) {
            texture = newTexture;
            Rows = rows;
            Columns = columns;
            AnimationFrames = animationFrames;
            spritePosition = newPosition;
            spriteFlip = flip;
            currentFrame = 0;
            IsAnimated = animated;

            int xOffset = 16 + width * currentFrame;
            frameRectangle = new Rectangle(xOffset, 0, width, height);
            locationRectangle = new Rectangle((int)SpritePosition.X, (int)SpritePosition.Y, width, height);
        }
        public override void Update(GameTime gameTime) {
            if (IsAnimated) {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame) {
                    timeSinceLastFrame -= millisecondsPerFrame;
                    currentFrame++;
                    currentFrame %= totalFrames;
                }
            }
            locationRectangle.X = (int)spritePosition.X;
            locationRectangle.Y = (int)spritePosition.Y;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            
            int xOffset = 16 + width * currentFrame;
            
            frameRectangle.X = xOffset;
            spriteBatch.Draw(texture, locationRectangle, frameRectangle, Color.White, 0, Vector2.Zero, spriteFlip,0);
        }
    }
}
