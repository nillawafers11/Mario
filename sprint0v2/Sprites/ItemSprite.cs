using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using sprint0v2.Entities;

namespace sprint0v2.Sprites
{
    public class ItemSprite : ISprite
    {
        private int Rows;
        private int Columns;
        public int setColumns
        {
            set => Columns = value;
        }
        private int currentFrame;
        private readonly int totalFrames;
        private Vector2 spritePosition;
        public Vector2 SpritePosition
        {
            get => spritePosition;
            set => spritePosition = value;
        }
        private SpriteEffects spriteFlip;
        private int timeSinceLastFrame;
        readonly int millisecondsPerFrame = 200;
        private bool isAnimated;
        public SpriteEffects SpriteFlip
        {
            get => spriteFlip;
            set => spriteFlip = value;
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
        private Texture2D itemTexture;
        public Texture2D Texture
        {
            get => itemTexture;
            set => itemTexture = value;
        }
        int ISprite.CurrentFrame { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private int AnimationFrames;
        int ISprite.Rows => Rows;
        int ISprite.Columns => Columns;

        private Rectangle frameRectangle;
        private Rectangle locationRectangle;

        public ItemSprite(Texture2D texture, int rows, int columns, int animationFrames, Vector2 position, SpriteEffects flip, bool Animated)
        {
            itemTexture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            spritePosition = position;
            spriteFlip = flip;
            isAnimated = Animated;
            AnimationFrames = animationFrames;

            int width = itemTexture.Width / Columns;
            int height = itemTexture.Height / Rows;
            int frameOffset = currentFrame % Columns;

            frameRectangle = new Rectangle(frameOffset * width, 0, width, height);
            locationRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, width, height);
        }

        public void Update(GameTime gameTime)
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
            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
            }
            locationRectangle.X = (int)spritePosition.X;
            locationRectangle.Y = (int)spritePosition.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = itemTexture.Width / Columns;
            int frameOffset = currentFrame % Columns;
            frameRectangle.X = frameOffset * width;

            spriteBatch.Draw(itemTexture, locationRectangle, frameRectangle, Color.White, 0, Vector2.Zero, spriteFlip, 0);
        }

        public void SetTexture(Texture2D texture)
        {
            throw new NotImplementedException();
        }
    }
}
