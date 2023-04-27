using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using System.Collections.Generic;

public abstract class SpriteFactory
{
    public Game _game;
    protected SpriteFactory()
    {

    }
    public abstract ISprite CreateSprite(Vector2 position, int type);
}
