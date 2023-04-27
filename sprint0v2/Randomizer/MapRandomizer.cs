using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;

namespace sprint0v2.Randomizer
{
    public class Entity
    {
        [JsonProperty("EntityType")]
        public string EntityType { get; set; }

        [JsonProperty("EntitySubItem")]
        public string EntitySubItem { get; set; }

        [JsonProperty("Location")]
        public Location Location { get; set; }

        [JsonProperty("IsCollisionDetectionEnabled")]
        public bool IsCollisionDetectionEnabled { get; set; }

        [JsonProperty("rowloop")]
        public int RowLoop { get; set; }
    }

    public class Location
    {
        [JsonProperty("X")]
        public int X { get; set; }

        [JsonProperty("Y")]
        public int Y { get; set; }
    }

    public class EntityCollection
    {
        [JsonProperty("Entities")]
        public List<Entity> Entities { get; set; }
    }

    public class MapRandomizer
    {
        private readonly EntityCollection _combinedEntities;

        public MapRandomizer()
        {
            string json1 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/start.json"));
            EntityCollection entities1 = JsonConvert.DeserializeObject<EntityCollection>(json1);
            EntityCollection entities2 = SectionTwoGenerator();
            EntityCollection entities3 = SectionThreeGenerator();
            EntityCollection entities4 = SectionFourGenerator();
            EntityCollection entities5 = SectionFiveGenerator();
            string endjson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/end.json"));
            EntityCollection end = JsonConvert.DeserializeObject<EntityCollection>(endjson);

            // Combine the two EntityCollection objects into one
            _combinedEntities = new EntityCollection
            {
                Entities = new List<Entity>(entities1.Entities.Concat(entities2.Entities).Concat(entities3.Entities).Concat(entities4.Entities).Concat(entities5.Entities).Concat(end.Entities))
            };
        }

        public EntityCollection SectionTwoGenerator()
        {
            EntityCollection entities2 = new EntityCollection();
            int num = GenerateRandomNumber();
            string json2;
            switch (num)
            {
                case 1:
                    json2 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section2/sec2num1.json"));
                    entities2 = JsonConvert.DeserializeObject<EntityCollection>(json2);
                    break;
                case 2:
                    json2 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section2/sec2num2.json"));
                    entities2 = JsonConvert.DeserializeObject<EntityCollection>(json2);
                    break;
                case 3:
                    json2 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section2/sec2num3.json"));
                    entities2 = JsonConvert.DeserializeObject<EntityCollection>(json2);
                    break;
            }
            return entities2;
        }

        public EntityCollection SectionThreeGenerator()
        {
            EntityCollection entities3 = new EntityCollection();
            int num = GenerateRandomNumber();
            string json3;
            switch (num)
            {
                case 1:
                    json3 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section3/sec3num1.json"));
                    entities3 = JsonConvert.DeserializeObject<EntityCollection>(json3);
                    break;
                case 2:
                    json3 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section3/sec3num2.json"));
                    entities3 = JsonConvert.DeserializeObject<EntityCollection>(json3);
                    break;
                case 3:
                    json3 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section3/sec3num3.json"));
                    entities3 = JsonConvert.DeserializeObject<EntityCollection>(json3);
                    break;
            }
            return entities3;
        }

        public EntityCollection SectionFourGenerator()
        {
            EntityCollection entities4 = new EntityCollection();
            int num = GenerateRandomNumber();
            string json4;
            switch (num)
            {
                case 1:
                    json4 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section4/sec4num1.json"));
                    entities4 = JsonConvert.DeserializeObject<EntityCollection>(json4);
                    break;
                case 2:
                    json4 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section4/sec4num2.json"));
                    entities4 = JsonConvert.DeserializeObject<EntityCollection>(json4);
                    break;
                case 3:
                    json4 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section4/sec4num3.json"));
                    entities4 = JsonConvert.DeserializeObject<EntityCollection>(json4);
                    break;
            }
            return entities4;
        }

        public EntityCollection SectionFiveGenerator()
        {
            EntityCollection entities5 = new EntityCollection();
            int num = GenerateRandomNumber();
            string json5;
            switch (num)
            {
                case 1:
                    json5 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section5/sec5num1.json"));
                    entities5 = JsonConvert.DeserializeObject<EntityCollection>(json5);
                    break;
                case 2:
                    json5 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section5/sec5num2.json"));
                    entities5 = JsonConvert.DeserializeObject<EntityCollection>(json5);
                    break;
                case 3:
                    json5 = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/section5/sec5num3.json"));
                    entities5 = JsonConvert.DeserializeObject<EntityCollection>(json5);
                    break;
            }
            return entities5;
        }

        public void CombineAndWriteToJsonFile()
        {
            string combinedJson = JsonConvert.SerializeObject(_combinedEntities);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Randomizer/combined.json"), combinedJson);
        }

        public int GenerateRandomNumber()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 4);
            return randomNumber;
        }
    }
}


