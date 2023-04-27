using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using sprint0v2.Commands;

namespace sprint0v2.Controllers
{
    public class KeyboardController : Controller
    {
        private readonly Game game;
        private readonly Entity entity;
        private KeyboardState previousKeyboardState;
        private readonly Dictionary<Keys, ICommand> playCommands;
        private readonly Dictionary<Keys, ICommand> pauseCommands;
        private readonly Dictionary<Keys, ICommand> startCommands;
        private readonly Dictionary<Keys, ICommand> winCommands;
        private readonly Dictionary<Keys, ICommand> gameOverCommands;
        private Game1 game1;

        public KeyboardController(Game game, Entity entity)
        {
            game1 = (Game1)game;
            this.game = game;
            this.entity = entity;
            previousKeyboardState = Keyboard.GetState();

            // Use the CommandDictionaryBuilder to create the dictionary of key commands
            var commandDictionaryBuilder = new CommandDictionaryBuilder();
            playCommands = commandDictionaryBuilder.BuildCommandDictionary(entity, game);
            pauseCommands = commandDictionaryBuilder.BuildPauseDictionary(entity, game);
            startCommands = commandDictionaryBuilder.BuildStartDictionary(entity, game);
            winCommands = commandDictionaryBuilder.BuildWinDictionary(entity, game);
            gameOverCommands = commandDictionaryBuilder.BuildGameOverDictionary(entity, game);
        }

        public override void UpdateInput()
        {
            KeyboardState currentKeyboardState = GetKeyboardState();
            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();

            switch (game1.GameState)
            {
                case PauseState:
                    // Only accept the P key as input
                    Keys pauseKey = Keys.P;
                    bool isPauseKeyPressed = keysPressed.Contains(pauseKey);
                    bool wasPauseKeyPressed = previousKeyboardState.IsKeyDown(pauseKey);
                    if (isPauseKeyPressed && !wasPauseKeyPressed)
                    {
                        ICommand pauseCommand = pauseCommands[pauseKey];
                        pauseCommand.Execute();
                    }
                    break;

                case WinState:
                    foreach (Keys key in winCommands.Keys)
                    {
                        ICommand command = playCommands[key];
                        bool isKeyPressed = keysPressed.Contains(key);
                        bool wasKeyPressed = previousKeyboardState.IsKeyDown(key);
                        if (isKeyPressed && !wasKeyPressed)
                        {
                            command.Execute();
                        }
                        else if (!isKeyPressed && wasKeyPressed)
                        {
                            command.Stop();
                        }
                    }
                    break;
                case PlayState:
                    
                    foreach (Keys key in playCommands.Keys)
                    {
                        ICommand command = playCommands[key];
                        bool isKeyPressed = keysPressed.Contains(key);
                        bool wasKeyPressed = previousKeyboardState.IsKeyDown(key);
                        if (isKeyPressed && !wasKeyPressed)
                        {
                            command.Execute();
                        }
                        else if (!isKeyPressed && wasKeyPressed)
                        {
                            command.Stop();
                        }
                    }
                    break;
                case GameOverState:
                    foreach (Keys key in gameOverCommands.Keys)
                    {
                        ICommand command = gameOverCommands[key];
                        bool isKeyPressed = keysPressed.Contains(key);
                        bool wasKeyPressed = previousKeyboardState.IsKeyDown(key);
                        if (isKeyPressed && !wasKeyPressed)
                        {
                            command.Execute();
                        }
                        else if (!isKeyPressed && wasKeyPressed)
                        {
                            command.Stop();
                        }
                    }
                    break;
                case StartState:
                    foreach (Keys key in startCommands.Keys)
                    {
                        ICommand command = startCommands[key];
                        bool isKeyPressed = keysPressed.Contains(key);
                        bool wasKeyPressed = previousKeyboardState.IsKeyDown(key);
                        if (isKeyPressed && !wasKeyPressed)
                        {
                            command.Execute();
                        }
                        else if (!isKeyPressed && wasKeyPressed)
                        {
                            command.Stop();
                        }
                    }
                    break;
            }

            previousKeyboardState = currentKeyboardState;
        }




        protected virtual KeyboardState GetKeyboardState()
        {
            return Keyboard.GetState();
        }
    }
}
