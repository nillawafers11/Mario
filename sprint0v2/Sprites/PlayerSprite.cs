using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;

namespace sprint0v2.Sprites
{
    public class PlayerSprite : ISprite
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
        private Texture2D playerTexture;
        public Texture2D Texture
        {
            get => playerTexture;
            set => playerTexture = value;
        }
        public Texture2D PlayerTexture
        {
            get => playerTexture;
            set => playerTexture = value;
        }
        private int AnimationFrames;
        public int CurrentFrame
        {
            get => currentFrame;
            set => currentFrame = value;
        }
        int ISprite.Rows => Rows;
        int ISprite.Columns => Columns;
        bool BBoxVisable = true;

        private Rectangle frameRectangle;
        private Rectangle locationRectangle;

        public PlayerSprite(Texture2D texture, int rows, int columns, int animationFrames, Vector2 position, SpriteEffects flip, bool Animated)
        {
            playerTexture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            spritePosition = position;
            spriteFlip = flip;
            isAnimated = Animated;
            AnimationFrames = animationFrames;

            int width = playerTexture.Width / Columns;
            int height = playerTexture.Height / Rows;
            int frameOffset = currentFrame % Columns;

            frameRectangle = new Rectangle(frameOffset * width, 0, width, height);
            locationRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, width, height);
        }

        public void Update(GameTime gameTime)
        {
            if (IsAnimated)
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
                //This is a dope line of code. 
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
            int width = playerTexture.Width / Columns;
            int frameOffset = currentFrame % Columns;
            frameRectangle.X = frameOffset * width;

            spriteBatch.Draw(playerTexture, locationRectangle, frameRectangle, Color.White, 0, Vector2.Zero, spriteFlip, 1);

        }

        public void SetTexture(Texture2D texture)
        {
            throw new System.NotImplementedException();
        }
    }

}
