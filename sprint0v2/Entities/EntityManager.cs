using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Entities;
using sprint0v2.Entities.ConcreteBlockEntities;
using sprint0v2.Entities.ConcreteEnemyEntites;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;

namespace sprint0v2
{
    public class EntityManager
    {
        private List<Entity> entities;
        private Entity entity;
        private MarioFactory marioFactory = new MarioFactory();
        private ItemFactory itemFactory = new ItemFactory();
        private BlockFactory blockFactory = new BlockFactory();
        private EnemyFactory enemyFactory = new EnemyFactory();
        private FireballFactory fireballFactory = new FireballFactory();

        private List<Entity> bridge;

        public static PlayerSpriteFactory PlayerInstance { get; private set; }
        public static ItemSpriteFactory ItemInstance { get; private set; }
        public static BlockSpriteFactory BlockInstance { get; private set; }
        public static EnemySpriteFactory EnemyInstance { get; private set; }
        public static FireballSpriteFactory FireballInstance { get; private set; }

        public static EntityManager Instance { get; internal set; }

        public EntityManager(Game game)
        {
            Instance = this;
            PlayerInstance = new PlayerSpriteFactory(game);
            ItemInstance = new ItemSpriteFactory(game);
            BlockInstance = new BlockSpriteFactory(game);
            EnemyInstance = new EnemySpriteFactory(game);
            FireballInstance = new FireballSpriteFactory(game);
            marioFactory = new MarioFactory();
            itemFactory = new ItemFactory();
            blockFactory = new BlockFactory();
            enemyFactory = new EnemyFactory();
            fireballFactory = new FireballFactory();


            entities = new List<Entity>
            {

            };

            bridge = new List<Entity>
            {

            };
        }

        public Enemy AddEnemyEntity(Vector2 position, string type)
        {
            if (type == "goomba")
            {
                entity = enemyFactory.CreateGoomba(position, new Vector2(-24, 0));
                entities.Add(entity);
                
            }
            else if (type == "castleGoomba")
            {
                entity = enemyFactory.CreateCastleGoomba(position, new Vector2(-24, 0));
                entities.Add(entity);
            }
            else if (type == "greenkoopa")
            {
                entity = enemyFactory.CreateGreenKoopaTroopa(position, new Vector2(-24, 0));
                entities.Add(entity);
            }
            else if (type == "castleKoopaTroopa")
            {
                entity = enemyFactory.CreateCastleKoopaTroopa(position, new Vector2(-24, 0));
                entities.Add(entity);
            }
            else if (type == "piranhaplant")
            {
                entity = enemyFactory.CreatePiranhaPlant(position, Vector2.Zero);
                entities.Add(entity);
            }
            else if (type == "lakitu")
            {
                entity = enemyFactory.CreateLakitu(position, Vector2.Zero);
                entities.Add(entity);
            }
            else if (type == "hammerbro")
            {
                entity = enemyFactory.CreateHammerBro(position, Vector2.Zero);
                entities.Add(entity);
            }
            else if (type == "spiny")
            {
                entity = enemyFactory.CreateSpiny(position, new Vector2(0, 30));
                entities.Add(entity);
            }
            else if (type == "bulletbill")
            {
        
                entity = enemyFactory.CreateBulletBill(position, new Vector2(-20, 0));
                entities.Add(entity);
            }
            else if (type.Contains("firebar"))
            {
                if (type.Contains("long"))
                {
                    entity = enemyFactory.CreateLongFireBar(position);

                }
                else
                {
                    entity = enemyFactory.CreateFireBar(position);
                }
                FireBar fireBar = (FireBar)entity;
                entities.AddRange(fireBar.Fireballs);
                entities.Add(entity);
            }
            else if(type == "bowser")
            {
                entity = enemyFactory.CreateBowser(position, Vector2.Zero);
                entities.Add(entity);
            }
            return (Enemy)entity;
        }
        public void AddPipeEntity(Vector2 position, string type, string subType)
        {
            switch (type) {
                case "pipe":
                    entity = blockFactory.CreatePipeBlock(position, subType);
                    entities.Add(entity);
                    break;
                case "castlepipe":
                    entity = blockFactory.CreateCastlePipeBlock(position, subType);
                    entities.Add(entity);
                    break;
            }
            
        }

        public void AddFlagEntity(Vector2 position, string type)
        {
            entity = blockFactory.CreateFlagBlock(position);
            entities.Add(entity);
        }

        public void AddCastleEntity(Vector2 position, string type)
        {
            entity = blockFactory.CreateCastleBlock(position);
            entities.Add(entity);
        }
        public void AddBlockEntity(Vector2 position, string type, ItemType item, int itemTotal)
        {
            switch (type)
            {
                case "brickblock":
                    entity = blockFactory.CreateBrickBlock(position, item, itemTotal);
                    entities.Add(entity);
                    break;
                case "undergroundbrickblock":
                    entity = blockFactory.CreateUndergroundBrickBlock(position, item, itemTotal);
                    entities.Add(entity);
                    break;
                case "questionblock":
                    entity = blockFactory.CreateQuestionBlock(position, item, itemTotal);
                    entities.Add(entity);
                    break;
                case "groundblock":
                    entity = blockFactory.CreateGroundBlock(position, new Vector2(0, 0));
                    entities.Add(entity);
                    break;
                case "undergroundgroundblock":
                    entity = blockFactory.CreateUndergroundGroundBlock(position, new Vector2(0, 0));
                    entities.Add(entity);
                    break;
                case "unbreakableblock":
                    entity = blockFactory.CreateUnbreakableBlock(position);
                    entities.Add(entity);
                    break;
                case "hiddenblock":
                    entity = blockFactory.CreateHiddenBlock(position, item, itemTotal);
                    entities.Add(entity);
                    break;
                case "checkpointblock":
                    entity = blockFactory.CreateCheckPointBlock(position);
                    entities.Add(entity);
                    break;
                case "bulletlauncher":
                    entity = blockFactory.CreateBulletBillLauncher(position);
                    entities.Add(entity);
                    break;
                case "castleBrick":
                    entity = blockFactory.CreateCastleBrick(position);
                    entities.Add(entity);
                    break;
                case "usedblock":
                    entity = blockFactory.CreateUsedBlock(position);
                    entities.Add(entity);
                    break;
                case "verticalPlatform":
                    entity = blockFactory.CreatePlatform(position, type);
                    entities.Add(entity);
                    break;
                case "horizontalPlatform":
                    entity = blockFactory.CreatePlatform(position, type);
                    entities.Add(entity);
                    break;
                case "bridgeSegment":
                    entity = blockFactory.CreateBridgeSegment(position);
                    entities.Add(entity);
                    bridge.Add(entity);
                    break;
                case "bridgeEnd":
                    entity = blockFactory.CreateBridgeEnd(position);
                    entities.Add(entity);
                    bridge.Add(entity);
                    break;
            }
        }

        public async void RemoveBridge()
        {
            for (int i = 0; i < bridge.Count; i++)
            {
                await Task.Delay(100);
                RemoveEntity(bridge[i]);
                Grid.Instance.RemoveEntity(bridge[i]);
            }
            if (GetBowser() != null)
            {
                GetBowser().EnemyState.Die();
            }
        }

        public void AddItemEntity(Vector2 position, string type, Vector2 speed)
        {
            
            switch (type)
            {
                case "supermushroom":
                    entity = itemFactory.CreateSuperMushroom(position, speed);
                    entities.Add(entity);
                    break;
                case "oneupmushroom":
                    entity = itemFactory.CreateOneUpMushroom(position, speed);
                    entities.Add(entity);
                    break;
                case "star":
                    entity = itemFactory.CreateStar(position, speed);
                    entities.Add(entity);
                    break;
                case "fireflower":
                    entity = itemFactory.CreateFireFlower(position, Vector2.Zero);
                    entities.Add(entity);
                    break;
                case "coin":
                    entity = itemFactory.CreateCoin(position, speed);
                    entities.Add(entity);
                    break;
                case "castleAxe":
                    entity = itemFactory.CreateCastleAxe(position, speed);
                    entities.Add(entity);
                    break;
                case "vine":
                    entity = itemFactory.CreateVine(position, speed);
                    entities.Add(entity);
                    break;
            }
           /* if (Grid.Instance.GetAllEntities()[0].BBoxVisible)
            {
                entity.BBoxVisible = true;
            }*/
        }

        public Entity AddMario(Vector2 position, Vector2 speed)
        {
            entity = marioFactory.Create(position, speed);
            entities.Add(entity);
         /*   Grid.Instance.AddEntity(entity);*/
            return entity;
        }

        public Entity AddHammer(Vector2 position, Vector2 speed)
        {
            Entity hammer = itemFactory.CreateHammer(position, speed);
            Grid.Instance.AddEntity(hammer);
            entities.Add(hammer);
            return hammer;
        }

        public Entity AddFireball(Vector2 position, Vector2 speed)
        {
            Entity fireball = fireballFactory.CreateFireball(position, speed);
            Grid.Instance.AddEntity(fireball);

            entities.Add(fireball);
            return fireball;
        }

        public Entity AddBowserFireball(Vector2 position, Vector2 speed)
        {
            Entity bowserFireball = fireballFactory.CreateBowserFireball(position, speed);
            Grid.Instance.AddEntity(bowserFireball);

            entities.Add(bowserFireball);
            return bowserFireball;
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }
        private void RemoveOffScreenEntities()
        {
            const int offScreenThreshold = 333;

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (entity.Position.Y > offScreenThreshold)
                {
                    // Remove the entity from the EntityManager and the Grid
                    entities.RemoveAt(i);
                    Grid.Instance.RemoveEntity(entity);
                    EntityManager.Instance.RemoveEntity(entity);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
           RemoveOffScreenEntities();

            foreach (Entity entity in entities.ToArray())
            {
                entity.Update(gameTime);
                // if (entity.IsRemoved)
                //     RemoveEntity(entity);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Sort the entities based on the DrawOrder property
            List<Entity> sortedEntities = entities.OrderBy(e => e.DrawOrder).ToList();

            // Draw the sorted entities
            foreach (Entity entity in sortedEntities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public void Clear()
        {
            entities.Clear();
        }
        public List<Entity> GetEntities()
        {
            return entities;
        }

        public void SnapshotRepopulation(List<Entity> EntityManagerSnapshot) {
            entities = EntityManagerSnapshot;
        }

        public Mario GetPlayer()
        {
            foreach (Entity entity in entities)
            {
                if (entity is Mario mario)
                {
                    return mario;
                }
            }
            return null; // Return null if no Mario entity is found
        }

        public Bowser GetBowser()
        {
            foreach(Entity entity in entities)
            {
                if(entity is Bowser bowser)
                {
                    return bowser;
                }
            }
            return null;
        }

    }
}
