using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;

namespace sprint0v2.Sprites
{
    public class FireballSprite : ISprite
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
        public Vector2 SpritePosition
        {
            get => spritePosition;
            set => spritePosition = value;
        }
        private Texture2D fireballTexture;
        public Texture2D Texture
        {
            get => fireballTexture;
            set => fireballTexture = value;
        }
        private int AnimationFrames;
        public int CurrentFrame
        {
            get => currentFrame;
            set => currentFrame = value;
        }
        int ISprite.Rows => Rows;
        int ISprite.Columns => Columns;
        bool BBoxVisible = true;

        private Rectangle frameRectangle;
        private Rectangle locationRectangle;

        public FireballSprite(Texture2D texture, int rows, int columns, int animationFrames, Vector2 position, SpriteEffects flip, bool Animated)
        {
            fireballTexture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            spritePosition = position;
            spriteFlip = flip;
            isAnimated = Animated;
            AnimationFrames = animationFrames;

            int width = fireballTexture.Width / Columns;
            int height = fireballTexture.Height / Rows;
            int frameOffset = currentFrame % Columns;

            frameRectangle = new Rectangle(frameOffset * width, 0, width, height);
            locationRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, width, height);
        }

        public void Update(GameTime gameTime)
        {
            if (isAnimated)
            {
                UpdateAnimation(gameTime);
            }

            locationRectangle.X = (int)spritePosition.X;
            locationRectangle.Y = (int)spritePosition.Y;
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                int animationDirection = spriteFlip == SpriteEffects.None ? -1 : -1;
                currentFrame += animationDirection;
                if (currentFrame >= AnimationFrames)
                {
                    currentFrame = 0;
                }
                if (currentFrame < 0)
                {
                    currentFrame = AnimationFrames - 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = fireballTexture.Width / Columns;
            int frameOffset = currentFrame % Columns;
            frameRectangle.X = frameOffset * width;

            spriteBatch.Draw(fireballTexture, locationRectangle, frameRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.2f);
        }

        public void SetTexture(Texture2D texture)
        {
            throw new System.NotImplementedException();
        }
    }
}
