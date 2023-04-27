using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Tiles
{
    public class Entity
    {
        public string EntityType { get; set; }
        public string EntitySubItem { get; set; }
        public Location Location { get; set; }
        public bool IsCollisionDetectionEnabled { get; set; }
        public int RowLoop { get; set; }


    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class TileMap
    {
        public List<Entity> Entities { get; set; }
        
        public static TileMap Read(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<TileMap>(json);
        }

    }

    public class TileMapManager
    {
        public const int tileSize = 16;
        public void Create(string path, EntityManager manager)
        {
            TileMap tileMap = TileMap.Read(path);


            foreach (Entity entity in tileMap.Entities)
            {
                string entityType = entity.EntityType;
                string entitySubType = entity.EntitySubItem;
                int x = entity.Location.X;
                int y = entity.Location.Y;
                bool isCollisionDetectionEnabled = entity.IsCollisionDetectionEnabled;
                int rowloop = entity.RowLoop;
                Vector2 position = GetTilePosition(x, y);
                CreateEntity(entityType, entitySubType, rowloop, position, isCollisionDetectionEnabled, manager);
            }
        }

        public void CreateEntity(string entityType, string entitySubType, int rowloop, Vector2 position, bool isCollisionDetectionEnabled, EntityManager manager)
        {
            if (rowloop == 0)
            {
                switch (entityType)
                {
                    case "ground":
                        manager.AddBlockEntity(position, "groundblock", Entities.ItemType.Nothing, 0);
                        break;
                    case "mario":
                        manager.AddMario(position, new Vector2(0, 0));
                        break;
                    case "goomba":
                        manager.AddEnemyEntity(position, "goomba");
                        break;
                    case "castleGoomba":
                        manager.AddEnemyEntity(position, "castleGoomba");
                        break;
                    case "castleKoopaTroopa":
                        manager.AddEnemyEntity(position, "castleKoopaTroopa");
                        break;
                    case "flagpole":
                        manager.AddFlagEntity(position, "flagpole");
                        break;
                    case "unbreakableblock":
                        manager.AddBlockEntity(position, "unbreakableblock", Entities.ItemType.Nothing, 0);
                        break;
                    case "undergroundgroundblock":
                        manager.AddBlockEntity(position, "undergroundgroundblock", Entities.ItemType.Nothing, 0);
                        break;
                    case "undergroundbrickblock":
                        manager.AddBlockEntity(position, "undergroundbrickblock", Entities.ItemType.Nothing, 0);
                        break;
                    case "castle":
                        manager.AddCastleEntity(position, "castle");
                        break;
                    case "greenkoopatroopa":
                        Debug.WriteLine("adding greenkoopatroopa");
                        manager.AddEnemyEntity(position, "greenkoopa");
                        break;
                    case "lakitu":
                        Debug.WriteLine("adding lakitu");
                        manager.AddEnemyEntity(position, "lakitu");
                        break;
                    case "hammerbro":
                        Debug.WriteLine("adding hammerbro");
                        manager.AddEnemyEntity(position, "hammerbro");
                        break;
                    case "spiny":
                        Debug.WriteLine("adding spiny");
                        manager.AddEnemyEntity(position, "spiny");
                        break;
                    case "bulletbill":
                        Debug.WriteLine("adding bulletbill");
                        manager.AddEnemyEntity(position, "bulletbill");
                        break;
                    case "brick":
                        switch (entitySubType)
                        {
                            case "none":
                                manager.AddBlockEntity(position, "brickblock", Entities.ItemType.Nothing, 0);
                                break;
                            case "supermushroom":
                                manager.AddBlockEntity(position, "brickblock", Entities.ItemType.SuperMushroom, 1);
                                break;
                            case "oneupmushroom":
                                manager.AddBlockEntity(position, "brickblock", Entities.ItemType.OneUpMushroom, 1);
                                break;
                            case "fireflower":
                                manager.AddBlockEntity(position, "brickblock", Entities.ItemType.FireFlower, 1);
                                break;
                            case "star":
                                manager.AddBlockEntity(position, "brickblock", Entities.ItemType.Star, 1);
                                break;
                            case "vine":
                                manager.AddBlockEntity(position, "brickblock", Entities.ItemType.Vine, 1);
                                break;
                        }
                        if (entitySubType == "coin")
                        {
                            manager.AddBlockEntity(position, "brickblock", Entities.ItemType.Coin, 1); //single coin
                        }
                        else if (entitySubType.Contains("coin")) {
                            int coinCount = int.Parse(entitySubType.Substring(4));
                            manager.AddBlockEntity(position, "brickblock", Entities.ItemType.Coin, coinCount);
                        }
                        break;
                    case "questionblock":
                        switch (entitySubType)
                        {
                            case "none":
                                manager.AddBlockEntity(position, "questionblock", Entities.ItemType.Coin, 1); //Default behavior for questionblock with 1 coin
                                break;
                            case "supermushroom":
                                manager.AddBlockEntity(position, "questionblock", Entities.ItemType.SuperMushroom, 1);
                                break;
                            case "oneupmushroom":
                                manager.AddBlockEntity(position, "questionblock", Entities.ItemType.OneUpMushroom, 1);
                                break;
                            case "fireflower":
                                manager.AddBlockEntity(position, "questionblock", Entities.ItemType.FireFlower, 1);
                                break;
                            case "star":
                                manager.AddBlockEntity(position, "questionblock", Entities.ItemType.Star, 1);
                                break;

                        }
                        if (entitySubType.Contains("coin"))
                        {
                            int coinCount = int.Parse(entitySubType.Substring(4));
                            manager.AddBlockEntity(position, "questionblock", Entities.ItemType.Coin, coinCount);
                        }
                        break;
                    case "hiddenblock":
                        switch (entitySubType) {
                            case "oneupmushroom":
                                manager.AddBlockEntity(position, "hiddenblock", Entities.ItemType.OneUpMushroom, 1);
                                break;
                            case "vine":
                                manager.AddBlockEntity(position, "hiddenblock", Entities.ItemType.Vine, 1);
                                break;
                            case "star":
                                manager.AddBlockEntity(position, entityType, Entities.ItemType.Vine, 1);
                                break;
                            case "fireflower":
                                manager.AddBlockEntity(position, entityType, Entities.ItemType.FireFlower, 1);
                                break;
                        }
                        break;
                    case "pipe":
                        manager.AddPipeEntity(position, entityType, entitySubType);
                        if (entitySubType == "piranhaplant") {
                            Vector2 piranhaPosition = position;
                            piranhaPosition.X += 8;
                            piranhaPosition.Y += 2;
                            manager.AddEnemyEntity(piranhaPosition, entitySubType);
                        }
                        break;
                    case "castlepipe":
                        manager.AddPipeEntity(position, entityType, entitySubType);
                        if (entitySubType == "piranhaplant")
                        {
                            Vector2 piranhaPosition = position;
                            piranhaPosition.X += 8;
                            piranhaPosition.Y += 2;
                            manager.AddEnemyEntity(piranhaPosition, entitySubType);
                        }
                        break;
                    case "checkpointblock":
                        manager.AddBlockEntity(position, "checkpointblock", Entities.ItemType.Nothing, 0);
                        break;
                    case "bulletlauncher":
                        manager.AddBlockEntity(position, "bulletlauncher", Entities.ItemType.Nothing, 0);
                        break;
                    case "usedblock":
                        manager.AddBlockEntity(position, "usedblock", Entities.ItemType.Nothing, 0);
                        if (entitySubType.Contains("firebar")) {
                            Vector2 firebarPosition = position;
                            firebarPosition.X += 4;
                            firebarPosition.Y += 4;
                            manager.AddEnemyEntity(firebarPosition, entitySubType);
                        }
                        break;
                    case "castleBrick":
                        manager.AddBlockEntity(position, "castleBrick", Entities.ItemType.Nothing, 0);
                        break;
                    case "verticalPlatform":
                        manager.AddBlockEntity(position, "verticalPlatform", Entities.ItemType.Nothing, 0);
                        break;
                    case "horizontalPlatform":
                        manager.AddBlockEntity(position, "horizontalPlatform", Entities.ItemType.Nothing, 0);
                        break;
                    case "bowser":
                        manager.AddEnemyEntity(position, "bowser");
                        break;
                    case "castleAxe":
                        manager.AddItemEntity(position, "castleAxe", Vector2.Zero);
                        break;
                    case "bridgeEnd":
                        manager.AddBlockEntity(position, "bridgeEnd", Entities.ItemType.Nothing, 0);
                        break;
                }
            }
            else
            {
                switch (entityType)
                {
                    case "coin":
                        EntityLoop(position, "coin", rowloop, manager);
                        break;
                    case "ground":
                        EntityLoop(position, "groundblock", rowloop, manager);
                        break;
                    case "brick":
                        EntityLoop(position, "brickblock", rowloop, manager);
                        break;
                    case "unbreakableblock":
                        EntityLoop(position, "unbreakableblock", rowloop, manager);
                        break;
                    case "undergroundbrickblock":
                        EntityLoop(position, "undergroundbrickblock", rowloop, manager);
                        break;
                    case "undergroundgroundblock":
                        EntityLoop(position, "undergroundgroundblock", rowloop, manager);
                        break;
                    case "castleBrick":
                        EntityLoop(position, entityType, rowloop, manager);
                        break;
                    case "bridgeSegment":
                        EntityLoop(position, "bridgeSegment", rowloop, manager);
                        break;
                }
            }
        }

        public Vector2 GetTilePosition(int x, int y)
        {
            float xPos = x;
            float yPos = y;
            return new Vector2(xPos, yPos);
        }

        public void EntityLoop(Vector2 position, string entityName, int rowloop, EntityManager manager)
        {
            int i = 0;
            float offset = 16f;
            if (entityName == "coin")
            {
                while (i < rowloop)
                {
                    manager.AddItemEntity(position, entityName, new Vector2(0,0));
                    position.X += offset;
                    i++;
                }
            }
            else
            {
                while (i < rowloop)
                {
                    manager.AddBlockEntity(position, entityName, Entities.ItemType.Nothing, 0);
                    position.X += offset;
                    i++;
                }
            }
        }
    }
}