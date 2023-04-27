using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Sprites
{
    public class CoinSprite : ItemSprite
    {
        public CoinSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("Coin"), 1, 4, 4, position, SpriteEffects.None, true)
        {

        }
    }
    public class FireFlowerSprite : ItemSprite
    {
        public FireFlowerSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("FireFlower"), 1, 4, 4, position, SpriteEffects.None, true)
        {

        }
    }
    public class OneUpMushroomSprite : ItemSprite
    {
        public OneUpMushroomSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("1UPMushroom"), 1, 1, 1, position, SpriteEffects.None, false)
        {

        }
    }
    public class SuperMushroomSprite : ItemSprite
    {
        public SuperMushroomSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("SuperMushroom"), 1, 1, 1, position, SpriteEffects.None, false)
        {

        }
    }
    public class StarSprite : ItemSprite
    {
        public StarSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("Starman"), 1, 4, 4, position, SpriteEffects.None, true)
        {

        }
    }
    public class AxeSprite : ItemSprite
    {
        public AxeSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("castleAxe"), 1, 3, 3, position, SpriteEffects.None, true)
        {

        }
    }
    public class VineTopSprite : ItemSprite
    {
        public VineTopSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("vineTop"), 1, 1, 1, position, SpriteEffects.None, false)
        {

        }
    }

    public class VineStemSprite : ItemSprite
    {
        public VineStemSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("vineStem"), 1, 1, 1, position, SpriteEffects.None, false) 
        { 
        
        }
    }

    public class HammerSprite : ItemSprite
    {
        public HammerSprite(Vector2 position, Game game)
            : base(game.Content.Load<Texture2D>("hammer"), 1, 1, 1, position, SpriteEffects.None, false)
        {

        }
    }
}