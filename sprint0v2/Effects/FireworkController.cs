using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Effects
{
    public class FireworkController
    {
        private List<Firework> fireworks;
        private Texture2D texture;
        private int rows;
        private int columns;
        private float animationInterval;

        public FireworkController(Texture2D texture, int rows, int columns, float animationInterval)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.animationInterval = animationInterval;
            fireworks = new List<Firework>();
        }

        public void AddMultipleFireworks(int count, Vector2 startPosition)
        {
            Random random = new Random();
            Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Magenta, Color.Cyan, Color.White };

            for (int i = 0; i < count; i++)
            {
                Vector2 position = new Vector2(
                    startPosition.X + random.Next(-100, 100),
                    startPosition.Y - random.Next(50, 100)
                );

                Color color = colors[random.Next(colors.Length)];

                float delay;
                if (i == 0)
                {
                    delay = 0; // No delay for the first firework
                }
                else
                {
                    delay = (float)(random.NextDouble() * 3); // Delay in seconds for the rest of the fireworks
                }

                Firework firework = new Firework(texture, rows, columns, animationInterval, delay);
                firework.Initialize(position, color, delay);
                fireworks.Add(firework);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = fireworks.Count - 1; i >= 0; i--)
            {
                Firework firework = fireworks[i];
                firework.Update(gameTime);
                if (!firework.IsActive)
                {
                    fireworks.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Firework firework in fireworks)
            {
                firework.Draw(spriteBatch);
            }
        }
    }

}
