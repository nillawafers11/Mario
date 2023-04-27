using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using sprint0v2.Entities;
using static sprint0v2.Entities.Mario;
using System.Diagnostics;

namespace sprint0v2
{
    public class HUD
    {
        public static HUD Instance;
        private SpriteFont hudFont;
        private Vector2 position;
        private Mario mario;
        private int score;
        private int coins;
        private string world;
        private int time;
        private int lives;

        public int[] Snapshot 
        { 
            get { int[] snapshotArr = { score, coins, time }; return snapshotArr; } 
            set { score = value[0]; coins = value[1]; time = value[2]; } 
        }

        public HUD(Vector2 initialPosition, Mario mario, SpriteFont font)
        {
            Instance = this;
            position = initialPosition;
            hudFont = font;
            this.mario = mario;
            lives = mario.Lives;
            mario.LivesChanged += OnLivesChanged;
            mario.CoinsChanged += OnCoinsChanged;
            mario.PointsChanged += OnPointsChanged;
        }
      

        private void OnLivesChanged(object sender, LivesChangedEventArgs e)
        {
            lives = e.Lives;
        }

        private void OnCoinsChanged(object sender, CoinsChangedEventArgs e)
        {
            coins = e.Coins;
        }

        private void OnPointsChanged(object sender, PointsChangedEventArgs e)
        {
            score = e.Points;
        }

        public void Draw(SpriteBatch spriteBatch,string world, int time, Vector2 cameraPosition)
        {
            Vector2 screenPosition = position + cameraPosition;
            float horizontalSpacing = 50f;
            float verticalSpacing = 10f;
            int timeLeft = 300 - time;

            spriteBatch.DrawString(hudFont, "Score:", screenPosition, Color.White);
            spriteBatch.DrawString(hudFont, $"{score}", screenPosition + new Vector2(0, verticalSpacing), Color.White);

            spriteBatch.DrawString(hudFont, "Coins:", screenPosition + new Vector2(horizontalSpacing, 0), Color.White);
            spriteBatch.DrawString(hudFont, $"{coins}", screenPosition + new Vector2(horizontalSpacing, verticalSpacing), Color.White);

            spriteBatch.DrawString(hudFont, "World:", screenPosition + new Vector2(horizontalSpacing * 2, 0), Color.White);
            spriteBatch.DrawString(hudFont, $"{world}", screenPosition + new Vector2(horizontalSpacing * 2, verticalSpacing), Color.White);

            spriteBatch.DrawString(hudFont, "Time:", screenPosition + new Vector2(horizontalSpacing * 3, 0), Color.White);
            spriteBatch.DrawString(hudFont, $"{timeLeft}", screenPosition + new Vector2(horizontalSpacing * 3, verticalSpacing), Color.White);

            spriteBatch.DrawString(hudFont, "Lives:", screenPosition + new Vector2(horizontalSpacing * 4, 0), Color.White);
            spriteBatch.DrawString(hudFont, $"{lives}", screenPosition + new Vector2(horizontalSpacing * 4, verticalSpacing), Color.White);
        }

    }
}
