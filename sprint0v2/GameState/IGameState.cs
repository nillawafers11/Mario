using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sprint0v2.Commands;
using sprint0v2.Effects;
using sprint0v2.View;
using System;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace sprint0v2
{
    public interface IGameState
    {
        void Update(Game1 game, GameTime gameTime);
        void Draw(Game1 game, GameTime gameTime);
    }

    public class StartState : IGameState
    {
        Texture2D startTexture;
        public StartState(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            LoadContent(content);
        }
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            startTexture = content.Load<Texture2D>("startScreen");
        }
        public void Update(Game1 game, GameTime gameTime)
        {
            game.KeyboardController.UpdateInput();

        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            game._spriteBatch.Begin();
            Vector2 position = new Vector2(0, 0);
            game._spriteBatch.Draw(startTexture, position, Color.White);
            game._spriteBatch.End();
        }
    }
    public class PlayState : IGameState
    {
        public void Update(Game1 game, GameTime gameTime)
        {
            if (game.Reset) game.ResetGame();
            game.EntityManager.Update(gameTime);
            game.KeyboardController.UpdateInput();
            game.GamepadController.UpdateInput();
            if (game.Player.Position.X > game.CenterOfScreen.X)
                game.Camera.LookAt(game.Player.Position);
            else
                game.Camera.LookAt(game.CenterOfScreen);
            game.CollisionGrid.Update();
            if (game.Player.Lives < 1)
            {
                game.GameState = game.GameOverState;
            }
        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            if (game.Player.Position.X < 7000)
            {
                game.Scenery.Draw(game._spriteBatch);
            }
            game._spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, transformMatrix: game.Camera.GetViewMatrix(Vector2.One));
      
            game.EntityManager.Draw(game._spriteBatch);
            game.Hud.Draw(game._spriteBatch, game.World, (int)game.ElapsedGameTime, game.Camera.Position);
            game.FireworkController.Draw(game._spriteBatch);

            game._spriteBatch.End();
        }


    }
    public class PauseState : IGameState
    {
        public void Update(Game1 game, GameTime gameTime)
        {

            game.KeyboardController.UpdateInput();
        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            game.PlayState.Draw(game, gameTime);

            game._spriteBatch.Begin();
            string message = "GAME PAUSED";
            Vector2 messageSize = game.Font.MeasureString(message);
            Vector2 messagePosition = new Vector2(
                game.GraphicsDevice.Viewport.Width / 2 - messageSize.X / 2,
                game.GraphicsDevice.Viewport.Height / 2 - messageSize.Y / 2
            );
            game._spriteBatch.DrawString(game.Font, message, messagePosition, Color.White);
            game._spriteBatch.End();
        }


    }

    public class WinState : IGameState
    {
        private Texture2D winTexture;

        public WinState(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            LoadContent(content);
        }
        public void Update(Game1 game, GameTime gameTime)
        {
            game.KeyboardController.UpdateInput();
        }
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            winTexture = content.Load<Texture2D>("winScreen");
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            game._spriteBatch.Begin();
            Vector2 position = new Vector2(0, 0);
            game._spriteBatch.Draw(winTexture, position, Color.White);
            game.Hud.Draw(game._spriteBatch, game.World, (int)game.ElapsedGameTime, game.Camera.Position);

            game._spriteBatch.End();
        }
    }

    public class GameOverState : IGameState
    {
        private Texture2D gameOverTexture;

        public GameOverState(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            LoadContent(content);
        }
        public void Update(Game1 game, GameTime gameTime)
        {
            game.KeyboardController.UpdateInput();

        }
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            gameOverTexture = content.Load<Texture2D>("GameOver");
        }
        public void Draw(Game1 game, GameTime gameTime)
        {
            game._spriteBatch.Begin();
            Vector2 position = new Vector2(0, 0);
            game._spriteBatch.Draw(gameOverTexture, position, Color.White);
            game.Hud.Draw(game._spriteBatch, game.World, (int)game.ElapsedGameTime, game.Camera.Position);

            game._spriteBatch.End();
        }
    }
}
