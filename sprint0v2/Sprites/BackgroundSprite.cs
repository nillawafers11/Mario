using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sprint0v2.Sprites
{
    public class BackgroundSprite : ISprite
    {
        private Texture2D backgroundTexture;
        public Texture2D Texture{ get { return backgroundTexture;  } set { backgroundTexture = value; } }
        private Vector2 position;
        public Vector2 SpritePosition{ get { return position; } set { position = value; } }
        private SpriteEffects effects;
        public SpriteEffects SpriteFlip{ get { return effects; } set { effects = SpriteEffects.None; } }
        private bool animated;
        public bool IsAnimated { get { return animated; } set { animated = false; } }
        private int currentFrame;
        public int CurrentFrame { get { return currentFrame; } set { currentFrame = 0; } }
        private int rowCount;
        public int Rows { get { return rowCount; } set { rowCount = 1; } }
        private int columnCount;
        public int Columns { get { return columnCount; } set { columnCount = 1; } }

        public BackgroundSprite(Texture2D newTexture, Vector2 newPosition)
        { 
            backgroundTexture = newTexture;
            position = newPosition;
        }

        public void SetTexture(Texture2D newTexture) { }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch) { }
    }
}
