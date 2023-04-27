using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;

namespace sprint0v2.Sprites
{
    public class EnemySprite : ISprite
    {
        private int Rows;
        private int Columns;
        private int currentFrame;
        private readonly int totalFrames;
        private Vector2 spritePosition;
        private SpriteEffects spriteFlip;
        private int timeSinceLastFrame;
        readonly int millisecondsPerFrame = 200;
        private bool isAnimated;
        private int animationFrames;
        private Texture2D enemyTexture;
        private AABB aabb;
        public AABB BoundingBox { get => aabb; set => aabb = value; }
        public Texture2D Texture { get => enemyTexture; set => enemyTexture = value; }
        public Vector2 SpritePosition { get => spritePosition; set => spritePosition = value; }
        public SpriteEffects SpriteFlip { get => spriteFlip; set => spriteFlip = value; }
        public bool IsAnimated { get => isAnimated; set => isAnimated = value; }
        public int CurrentFrame { get => currentFrame; set => currentFrame = value; }
        private Rectangle boundingBox;
        int ISprite.Rows => Rows;
        int ISprite.Columns => Columns;

        private Rectangle frameRectangle;
        private Rectangle locationRectangle;

        public EnemySprite(Texture2D texture, int rows, int columns, int animationFrames, Vector2 position, SpriteEffects flip, bool animated)
        {
            enemyTexture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            spritePosition = position;
            spriteFlip = flip;
            isAnimated = animated;
            this.animationFrames = animationFrames;

            int width = enemyTexture.Width / Columns;
            int height = enemyTexture.Height / Rows;
            int frameOffset = currentFrame % Columns;

            frameRectangle = new Rectangle(frameOffset * width, 0, width, height);
            locationRectangle = new Rectangle((int)spritePosition.X, (int)spritePosition.Y, width, height);

            aabb = new AABB(new Vector2(position.X, position.Y), new Vector2(texture.Width / columns, texture.Height / rows));
            boundingBox = aabb.GetBoundingBox();
            //scale bounding box down by 30%
        }

        public void Update(GameTime gameTime)
        {
            if (isAnimated)
            {
                UpdateAnimation(gameTime);
            }
            locationRectangle.X = (int)spritePosition.X;
            locationRectangle.Y = (int)spritePosition.Y;

            aabb.Position = spritePosition;
            boundingBox = aabb.GetBoundingBox();
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                int animationDirection = spriteFlip == SpriteEffects.None ? -1 : -1;
                currentFrame += animationDirection;
                if (currentFrame >= animationFrames)
                {
                    currentFrame = 0;
                }
                if (currentFrame < 0)
                {
                    currentFrame = animationFrames - 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = enemyTexture.Width / Columns;
            int frameOffset = currentFrame % Columns;
            frameRectangle.X = frameOffset * width;

            spriteBatch.Draw(enemyTexture, locationRectangle, frameRectangle, Color.White, 0, Vector2.Zero, spriteFlip, 0);
        }

        public void SetTexture(Texture2D texture)
        {
            throw new System.NotImplementedException();
        }
    }

}