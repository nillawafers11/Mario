using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Threading;

public class ContentManager
{
    private readonly Game game;

    // Item textures
    private readonly Texture2D oneUp;
    private readonly Texture2D fireFlower;
    private readonly Texture2D coin;

    // Mario state textures
    private readonly Texture2D smallMario;
    private Texture2D bigMario;
    private readonly Texture2D fireMario;
    private readonly Texture2D starMario1;
    private readonly Texture2D starMario2;
    private readonly Texture2D starMario3;

    // Enemy textures
    private readonly Texture2D goomba;
    private readonly Texture2D koopa;

    // Block textures
    private readonly Texture2D brickBlock;
    private readonly Texture2D questionBlock;
    private Texture2D groundBlock;
    private Texture2D unbreakableBlock;

    private SpriteFont hudfont;

    public ContentManager(Game game)
    {
        this.game = game;

        hudfont = game.Content.Load<SpriteFont>("File.spritefont");

        //Item textures
        oneUp = game.Content.Load<Texture2D>("1UPMushroom");
        fireFlower = game.Content.Load<Texture2D>("FireFlower");
        coin = game.Content.Load<Texture2D>("coin");
        

        // Mario state textures
        smallMario = game.Content.Load<Texture2D>("smallMarioSheet");
        bigMario = game.Content.Load<Texture2D>("superMarioSheet");
        fireMario = game.Content.Load<Texture2D>("fireMarioSheet");
        starMario1 = game.Content.Load<Texture2D>("starmanPalette1");
        starMario2 = game.Content.Load<Texture2D>("starmanPalette2");
        starMario3 = game.Content.Load<Texture2D>("starmanPalette3");
        //deadMario = game.Content.Load<Texture2D>("deadMario");


        // Enemy textures
        goomba = game.Content.Load<Texture2D>("goomba");
        koopa = game.Content.Load<Texture2D>("greenKoopaTroopa");

        // Block textures
        questionBlock = game.Content.Load<Texture2D>("QuestionBlock");
        brickBlock = game.Content.Load<Texture2D>("BrickBlock");
        groundBlock = game.Content.Load<Texture2D>("groundBlock");
        unbreakableBlock = game.Content.Load<Texture2D>("UnbreakableBlock");
        
    }

    public SpriteFont GetHudFont()
    {
        return hudfont;
    }
    public Texture2D GetOneUpTexture()
    {
        return oneUp;
    }

    public Texture2D GetFireFlowerTexture()
    {
        return fireFlower;
    }

    public Texture2D GetCoinTexture()
    {
        return coin;
    }

    public Texture2D GetSmallMarioTexture()
    {
        return smallMario;
    }

    public Texture2D GetBigMarioTexture()
    {
        return bigMario;
    }

    public Texture2D GetFireMarioTexture()
    {
        return fireMario;
    }

    //public Texture2D GetDeadMarioTexture()
    //{
    //    return deadMario;
    //}

    public Texture2D GetBrickBlockTexture()
    {
        return brickBlock;
    }

    public Texture2D GetGoombaTexture()
    {
        return goomba;
    }

    public Texture2D GetKoopaTexture()
    {
        return koopa;
    }

    public Texture2D GetBrickTexture()
    {
        return brickBlock;
    }

    public Texture2D GetQuestionBlockTexture()
    {
        return questionBlock;
    }

    public Texture2D GetGroundBlockTexture()
    {
        return groundBlock;
    }

    public Texture2D GetUnbreakableBlockTexture()
    {
        return unbreakableBlock;
    }

    public void Unload()
    {
        // Unload all content
        game.Content.Unload();
    }
}
