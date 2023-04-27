using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Sprites;
using sprint0v2.View;
using System;

namespace sprint0v2.Background
{
    public class BackgroundLayer : IComparable<BackgroundLayer>
    {
        private ISprite sprite;
        private SamplerState layerState;
        private int zLayerDepth;
        private Vector2 Parallax;
        private Camera camera;
        public BackgroundLayer(ISprite newSprite, int depthOfLayer, Vector2 newParallax, Camera gameCamera) {
            sprite = newSprite;
            layerState = SamplerState.LinearWrap;
            zLayerDepth = depthOfLayer;
            Parallax = newParallax;
            camera = gameCamera;
        }
        public void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.BackToFront, null, layerState, null, null, null, camera.GetViewMatrix(Parallax));
            Rectangle frameRectangle = new Rectangle((int)(camera.Position.X * 0.5), (int)(camera.Position.Y * 0.5), (int)camera.Position.X + 350, sprite.Texture.Height);
            spriteBatch.Draw(sprite.Texture, sprite.SpritePosition, frameRectangle, Color.White);
            spriteBatch.End();
        }

        public int CompareTo(BackgroundLayer comparison) {
            if (zLayerDepth > comparison.zLayerDepth)
            {
                return 1;
            }
            else if (zLayerDepth < comparison.zLayerDepth) {
                return -1;
            }
            return 0;
        }
    }
}
