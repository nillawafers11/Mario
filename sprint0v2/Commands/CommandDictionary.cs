using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using sprint0v2.Entities;

namespace sprint0v2.Commands
{
    public class CommandDictionaryBuilder
    {
        public Dictionary<Keys, ICommand> BuildCommandDictionary(Entity entity, Game game)
        {
            Dictionary<Keys, ICommand> commandDictionary = new Dictionary<Keys, ICommand>
            {
                // Add commands to the dictionary
                { Keys.Q, new ExitCommand(game) },
                { Keys.R, new ResetCommand(game) },
                { Keys.Up, new JumpCommand((Mario)entity) },
                { Keys.W, new JumpCommand((Mario)entity) },
                { Keys.Left, new Left((Mario)entity) },
                { Keys.A, new Left((Mario)entity) },
                { Keys.Right, new Right((Mario)entity) },
                { Keys.D, new Right((Mario)entity) },
                { Keys.Down, new Crouch((Mario)entity) },
                { Keys.Space, new ShootFireball((Mario)entity) },
                { Keys.S, new Crouch((Mario)entity) },
                { Keys.Y, new Standard((Mario)entity) },
                { Keys.U, new Super((Mario)entity) },
                { Keys.I, new Fire((Mario)entity) },
                { Keys.O, new TakeDamage((Mario)entity) },
                { Keys.C, new ShowBoundingBoxesCommand(game)},
                { Keys.P, new Pause(game) },
                { Keys.M, new Mute(game) },
                { Keys.G, new StarMan((Mario)entity) },

            };
            return commandDictionary;
        }
        public Dictionary<Keys, ICommand> BuildPauseDictionary(Entity entity, Game game)
        {
            Dictionary<Keys, ICommand> commandDictionary = new Dictionary<Keys, ICommand>
            {
                // Add commands to the dictionary
                { Keys.Q, new ExitCommand(game) },
                { Keys.P, new Pause(game) },
                { Keys.M, new Mute(game) },

            };
            return commandDictionary;
        }
        public Dictionary<Keys, ICommand> BuildStartDictionary(Entity entity, Game game)
        {
            Dictionary<Keys, ICommand> commandDictionary = new Dictionary<Keys, ICommand>
            {
                // Add commands to the dictionary
                { Keys.Q, new WorldOneCommand(game) },
                { Keys.A, new CastleCommand(game) },
                { Keys.Z, new RandomWorld(game) },

            };
            return commandDictionary;
        }
        public Dictionary<Keys, ICommand> BuildWinDictionary(Entity entity, Game game)
        {
            Dictionary<Keys, ICommand> commandDictionary = new Dictionary<Keys, ICommand>
            {
                // Add commands to the dictionary
                { Keys.S, new StartScreen(game) },
                { Keys.Q, new ExitCommand(game) },

            };
            return commandDictionary;
        }
        public Dictionary<Keys, ICommand> BuildGameOverDictionary(Entity entity, Game game)
        {
            Dictionary<Keys, ICommand> commandDictionary = new Dictionary<Keys, ICommand>
            {
                // Add commands to the dictionary
                { Keys.S, new StartScreen(game) },
                { Keys.Q, new ExitCommand(game) },

            };
            return commandDictionary;
        }
        public Dictionary<Buttons, ICommand> BuildGamepadCommandDictionary(Entity entity, Game game)
        {
            Dictionary<Buttons, ICommand> commandDictionary = new Dictionary<Buttons, ICommand>
            {
                // Add commands to the dictionary
                { Buttons.Start, new ExitCommand(game) },
                { Buttons.DPadUp, new JumpCommand((Mario)entity) },
                { Buttons.DPadLeft, new Left((Mario)entity) },
                { Buttons.DPadRight, new Right((Mario)entity) },
                { Buttons.DPadDown, new Crouch((Mario)entity) },
                { Buttons.B, new ShootFireball((Mario)entity) },
                { Buttons.A, new JumpCommand((Mario)entity) },
                { Buttons.X, new ShootFireball((Mario)entity) },
            };
            return commandDictionary;
        }
    }
}
