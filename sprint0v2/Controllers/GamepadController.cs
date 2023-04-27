using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using sprint0v2.Commands;
using sprint0v2.Controllers;

namespace sprint0v2
{
    public class GamepadController : Controller
    {
        private readonly Game game;
        private readonly Entity entity;
        private GamePadState previousGamePadState;
        private readonly Dictionary<Buttons, ICommand> buttonCommands;

        public GamepadController(Game game, Entity entity)
        {
            this.game = game;
            this.entity = entity;
            previousGamePadState = GamePad.GetState(PlayerIndex.One);

            // Use the CommandDictionaryBuilder to create the dictionary of button commands
            var commandDictionaryBuilder = new CommandDictionaryBuilder();
            buttonCommands = commandDictionaryBuilder.BuildGamepadCommandDictionary(entity, game);
        }

        public override void UpdateInput()
        {
            GamePadState currentGamePadState = GetGamePadState();
            if (currentGamePadState.IsConnected)
            {
                Buttons[] buttonsPressed = (Buttons[])Enum.GetValues(typeof(Buttons));
                foreach (var button in buttonsPressed)
                {
                    if (buttonCommands.ContainsKey(button))
                    {
                        ICommand command = buttonCommands[button];
                        bool isButtonPressed = currentGamePadState.IsButtonDown(button);
                        bool wasButtonPressed = previousGamePadState.IsButtonDown(button);
                        if (isButtonPressed && !wasButtonPressed)
                        {
                            command.Execute();
                        }
                        else if (!isButtonPressed && wasButtonPressed)
                        {
                            command.Stop();
                        }
                    }
                }
            }

            previousGamePadState = currentGamePadState;
        }

        protected virtual GamePadState GetGamePadState()
        {
            return GamePad.GetState(PlayerIndex.One);
        }
    }
}
