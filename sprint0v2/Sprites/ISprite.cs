using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

//create in interface for a sprite with the methods update and draw
namespace sprint0v2.Sprites
{
    public interface ISprite
    {
        Texture2D Texture { get; set; }
        Vector2 SpritePosition { get; set; }
        SpriteEffects SpriteFlip { get; set; }
        bool IsAnimated { get; set; }
        int CurrentFrame { get; set; }
        int Rows { get; }
        int Columns { get; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void SetTexture(Texture2D texture);
    }

}