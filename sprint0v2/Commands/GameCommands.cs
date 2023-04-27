using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0v2.Sprites;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using sprint0v2.Commands;
using System.Collections.Generic;
using sprint0v2.View;
using sprint0v2.Tiles;
using sprint0v2.Entities;
using System.Linq;
using sprint0v2.Entities.ConcreteBlockEntities;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using sprint0v2.Randomizer;

namespace sprint0v2.Commands
{
    class ExitCommand : GameCommand
    {
        private Game game;
        public ExitCommand(Game game) : base(game)
        {
            this.game = game;
        }
        public override void Execute()
        {
            game.Exit();
        }
    }
    class Pause : GameCommand
    {
        Game1 game;

        public Pause(Game game) : base(game)
        {
            this.game = (Game1)game;
        }

        public override void Execute()
        {
            if (game.GameState is PlayState)
            {
                game.GameState = game.PausedState;
            }
            else if (game.GameState is PauseState)
            {
                game.GameState = game.PlayState;
            }
        }
    }

    class WinReplay : GameCommand
    {
        Game1 game;
        public WinReplay(Game game) : base(game)
        {
            this.game = (Game1)game;
        }
        public override void Execute()
        {
            ResetCommand reset = new ResetCommand(game);
            reset.Execute();
        }
    }

    class WinQuit : GameCommand
    {
        Game1 game;
        public WinQuit(Game game) : base(game)
        {
            this.game = (Game1)game;
        }
        public override void Execute()
        {
            game.Exit();
        }
    }

    class Mute : GameCommand
    {
        public Mute(Game game) : base(game) { }
        public override void Execute()
        {
            if(SoundEffect.MasterVolume == 0.0f)
            {
                SoundEffect.MasterVolume = 0.25f;
                MediaPlayer.Volume = 0.3f;
            }
            else
            {
                SoundEffect.MasterVolume = 0.0f;
                MediaPlayer.Volume = 0.0f;
            }
            
        }
    }
    class ShowBoundingBoxesCommand : GameCommand
    {
        public ShowBoundingBoxesCommand(Game game) : base(game) { }

        public override void Execute()
        {
            //Debug.WriteLine("SHowing BoudingBoxes");
            // Get the collision grid from the game's level object
            List<Entity> entities = Grid.Instance.GetAllEntities();

            // Loop over all entities in the grid and set their bBoxVisible property to true
            foreach (Entity entity in entities)
            {
                entity.BBoxVisible = !entity.BBoxVisible ;
            }
        }
    }

    class ResetCommand : GameCommand
    {
        Game1 game;
        public ResetCommand(Game game) : base(game) 
        { 
            this.game = (Game1) game;
        }

        public override void Execute()
        {
            TileMapManager tileMapManager = new TileMapManager();
            List<Entity> gridEntities = Grid.Instance.GetAllEntities();
            List<Entity> managerEntities = EntityManager.Instance.GetEntities();

            EntityManager.Instance.Clear();

            foreach (Entity entity in gridEntities)
            {
                Debug.WriteLine("Removing " + entity.GetType().Name);
                Grid.Instance.RemoveEntity(entity);
            }

            tileMapManager.Create(game.Path, EntityManager.Instance);
            game.Reset = true;

            foreach (Entity entity in managerEntities.ToList())
            {
                Debug.WriteLine("Adding " + entity.GetType().Name);
                Grid.Instance.AddEntity(entity);
            }
            game.GameState = game.PlayState;
            game.ElapsedGameTime = 0;

            Camera.Instance.LookAt(new Vector2(10, 186));
            Camera.Instance.Limits = null;
        }
    }
    class WorldOneCommand : GameCommand
    {
        Game1 game;
        public WorldOneCommand(Game game) : base(game)
        {
            this.game = (Game1) game;
        }
        public override void Execute()
        {
            Debug.WriteLine("World 1-1");
            game.Path = " ../../../Tiles/tilemap.json";
            game.GameState = game.PlayState;
            game.NextLevel();

        }
    }
    class CastleCommand : GameCommand
    {
        Game1 game;
        public CastleCommand(Game game) : base(game)
        {
            this.game = (Game1)game;
        }
        public override void Execute()
        {
            game.Path = "../../../Tiles/castleTilemap.json";
            game.GameState = game.PlayState;
            game.NextLevel();
        }
    }
    class RandomWorld : GameCommand
    {
        Game1 game;
        public RandomWorld(Game game) : base(game)
        {
            this.game = (Game1)game;
        }
        public override void Execute()
        {
            MapRandomizer randomizer = new MapRandomizer();
            randomizer.CombineAndWriteToJsonFile();
            game.Path = "Randomizer/combined.json";
            game.GameState = game.PlayState;
            game.NextLevel();
        }
    }
    class StartScreen : GameCommand
    {
        Game1 game;

        public StartScreen(Game game) : base(game)
        {
            this.game = (Game1)game;
        }
        public override void Execute()
        {
            game.GameState = game.StartState;
        }
    }
}

