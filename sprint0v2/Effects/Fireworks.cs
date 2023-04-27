using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sprint0v2.Effects
{
    public class Firework
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }
        public bool IsActive { get; set; }
        public Vector2 Velocity { get; set; }


        private int rows;
        private int columns;
        private int currentFrame;
        private int totalFrames;
        private float animationInterval;
        private float elapsedTime;
        private float delay;
        private float delayElapsedTime;
        public bool IsVisible { get; private set; }

        public Firework(Texture2D texture, int rows, int columns, float animationInterval, float delay)
        {
            Texture = texture;
            Scale = 1.0f;
            IsActive = false;
            this.rows = rows;
            this.columns = columns;
            this.animationInterval = animationInterval;
            totalFrames = rows * columns;
            currentFrame = 0;
            elapsedTime = 0;
            this.delay = delay;
            delayElapsedTime = 0;
            Velocity = new Vector2(0, -200);
            IsVisible = false;

        }

        public void Initialize(Vector2 position, Color color, float delay)
        {
            Position = position;
            Color = color;
            IsActive = true;
            this.delay = delay;
            delayElapsedTime = 0;

            // Set initial upward velocity
            Velocity = new Vector2(0, -100);

        }
        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (delay > 0)
                {
                    delayElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (delayElapsedTime >= delay)
                    {
                        delay = 0;
                        IsVisible = true; // Make the firework visible when the delay has passed

                    }
                }
                else
                {
                    // Update position based on velocity
                    Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    // Apply gravity (reduce upward velocity)
                   Velocity = new Vector2(Velocity.X, Velocity.Y + 50 * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (elapsedTime > animationInterval)
                    {
                        currentFrame++;
                        elapsedTime = 0;
                    }

                    if (currentFrame == totalFrames)
                    {
                        IsActive = false;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive && IsVisible)
            {
                int width = Texture.Width / columns;
                int height = Texture.Height / rows;
                int row = currentFrame / columns;
                int column = currentFrame % columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }
    }
}
