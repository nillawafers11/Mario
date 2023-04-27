using sprint0v2.Entities.ConcreteBlockEntities;
using sprint0v2.Entities.ConcreteItemEntites;
using sprint0v2.Entities;
using sprint0v2.Sprites;
using Microsoft.Xna.Framework;
using sprint0v2.Entities.ConcreteEnemyEntites;
using System.Diagnostics;
using sprint0v2;

public class MarioFactory
{
    public Mario Create(Vector2 position, Vector2 speed)
    {
        return new Mario(position, speed);
    }
}

public class ItemFactory
{
    public Coin CreateCoin(Vector2 position, Vector2 speed)
    {
        return new Coin(position, speed);
    }
    public StaticCoin CreateStaticCoin(Vector2 position, Vector2 speed)
    {
        return new StaticCoin(position, speed);
    }

    public Star CreateStar(Vector2 position, Vector2 speed)
    {
        return new Star(position, speed);
    }

    public SuperMushroom CreateSuperMushroom(Vector2 position, Vector2 speed)
    {
        return new SuperMushroom(position, speed);
    }

    public FireFlower CreateFireFlower(Vector2 position, Vector2 speed)
    {
        return new FireFlower(position, speed);
    }

    public OneUpMushroom CreateOneUpMushroom(Vector2 position, Vector2 speed)
    {
        return new OneUpMushroom(position, speed);
    }

    public CastleAxe CreateCastleAxe(Vector2 position, Vector2 speed)
    {
        return new CastleAxe(position, speed);
    }

    public Hammer CreateHammer(Vector2 position, Vector2 speed)
    {
        return new Hammer(position, speed);
    }

    public Vine CreateVine(Vector2 position, Vector2 speed)
    {
        return new Vine(position, speed);
    }
}

public class BlockFactory
{

    public BrickBlock CreateBrickBlock(Vector2 position, ItemType item, int itemTotal) {
        return new BrickBlock(position, Vector2.Zero, item, itemTotal, false, "normal");
    }

    public BrickBlock CreateUndergroundBrickBlock(Vector2 position, ItemType item, int itemTotal)
    {
        return new BrickBlock(position, Vector2.Zero, item, itemTotal, false, "underground");
    }

    public UnbreakableBlock CreateUnbreakableBlock(Vector2 position)
    {
        return new UnbreakableBlock(position, Vector2.Zero, ItemType.Nothing, 0, false);
    }
    public GroundBlock CreateUndergroundGroundBlock(Vector2 position, Vector2 speed)
    {
        return new GroundBlock(position, speed, ItemType.Nothing, 0, false, "underground");
    }

    public PipeBlock CreatePipeBlock(Vector2 position, string subType)
    {
        return new PipeBlock(position, Vector2.Zero, subType, false);
    }

    public PipeBlock CreateCastlePipeBlock(Vector2 position, string subType) 
    {
        return new PipeBlock(position, Vector2.Zero, true);
    }

    public FlagBlock CreateFlagBlock(Vector2 position)
    {
        return new FlagBlock(position, Vector2.Zero, ItemType.Nothing, 0, false);
    }

    public CastleBlock CreateCastleBlock(Vector2 position)
    {
        return new CastleBlock(position, Vector2.Zero, ItemType.Nothing, 0, false);
    }

    public QuestionBlock CreateQuestionBlock(Vector2 position, ItemType item, int itemTotal)
    {
        return new QuestionBlock(position, Vector2.Zero, item, itemTotal, false);
    }

    public QuestionBlock CreateHiddenBlock(Vector2 position, ItemType item, int itemTotal) {
        return new QuestionBlock(position, Vector2.Zero, item, itemTotal, true);
    }

    public GroundBlock CreateGroundBlock(Vector2 position, Vector2 speed)
    {
        return new GroundBlock(position, speed, ItemType.Nothing, 0, false, "normal");
    }

    public CheckpointBlock CreateCheckPointBlock(Vector2 position) {
        return new CheckpointBlock(position);
    }
    public BulletBillLauncher CreateBulletBillLauncher(Vector2 position)
    {
        return new BulletBillLauncher(position);
    }

    public CastleBrick CreateCastleBrick(Vector2 position) { 
        return new CastleBrick(position);
    }

    public UsedBlock CreateUsedBlock(Vector2 position) { 
        return new UsedBlock(position);
    }

    public Platform CreatePlatform(Vector2 position, string type)
    {
        return new Platform(position, new Vector2(0, 0), ItemType.Nothing, 0, false, type);
    }

    public BridgeSegment CreateBridgeSegment(Vector2 position)
    {
        return new BridgeSegment(position, Vector2.Zero, ItemType.Nothing, 0, false);
    }

    public BridgeEnd CreateBridgeEnd(Vector2 position)
    {
        return new BridgeEnd(position, Vector2.Zero, ItemType.Nothing, 0, false);
    }
}

public class EnemyFactory
{
    public Enemy CreateGoomba(Vector2 position, Vector2 speed)
    {
        return new Goomba(position, speed);
    }

    public Enemy CreateCastleGoomba(Vector2 position, Vector2 speed)
    {
        return new CastleGoomba(position, speed);
    }

    public Enemy CreateGreenKoopaTroopa(Vector2 position, Vector2 speed) { 
        return new GreenKoopaTroopa(position, speed);
    }

    public Enemy CreateCastleKoopaTroopa(Vector2 position, Vector2 speed)
    {
        return new CastleKoopaTroopa(position, speed);
    }
    
    public Enemy CreatePiranhaPlant(Vector2 position, Vector2 speed) {
        return new PiranhaPlant(position, speed);
    }
    public Enemy CreateLakitu(Vector2 position, Vector2 speed)
    {
        Debug.WriteLine("Creating Lakitu");
        return new Lakitu(position, speed);
    }
    public Enemy CreateHammerBro(Vector2 position, Vector2 speed)
    {
        return new HammerBro(position, speed);
    }
    public Enemy CreateSpiny(Vector2 position, Vector2 speed)
    {
        return new Spiny(position, speed);
    }
    public Enemy CreateBulletBill(Vector2 position, Vector2 speed)
    {
        return new BulletBill(position, speed);
    }

    public Enemy CreateFireBar(Vector2 position)
    {
        return new FireBar(position, 6);
    }

    public Enemy CreateLongFireBar(Vector2 position)
    {
        return new FireBar(position, 12);
    }

    public Enemy CreateBowser(Vector2 position, Vector2 speed)
    {
        return new Bowser(position, speed);
    }
}

public class FireballFactory
{
    public Fireball CreateFireball(Vector2 position, Vector2 speed)
    {
        return new Fireball(position, speed);
    }
    public BowserFireball CreateBowserFireball(Vector2 position, Vector2 speed)
    {
        return new BowserFireball(position, speed);
    }
}
