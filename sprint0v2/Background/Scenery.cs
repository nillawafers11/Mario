using System;
using System.Collections.Generic;
using sprint0v2.View;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using System.Security.Cryptography.X509Certificates;
using sprint0v2.Entities;
using System.Diagnostics;

namespace sprint0v2.Background
{
    public class BackgroundScenery
    {
        private class SortLayerAscendingOrder : IComparer<BackgroundLayer>
        {
            int IComparer<BackgroundLayer>.Compare(BackgroundLayer x, BackgroundLayer y)
            {
                return y.CompareTo(x);
            }
        }
        private List<BackgroundLayer> layers;
        private List<BackgroundLayer> castleLayers;
        private Camera camera;
        private Game game;
        private EntityManager entityManager1;

        public BackgroundScenery(Camera newCamera, Game newGame, EntityManager entityManager)
        {
            layers = new List<BackgroundLayer>();
            castleLayers = new List<BackgroundLayer>();
            camera = newCamera;
            game = newGame;
            entityManager1 = entityManager;

        }
        private void AddLayer(BackgroundLayer newLayer)
        {
            layers.Add(newLayer);
            layers.Sort(new SortLayerAscendingOrder());

        }

        private void AddCastleLayer(BackgroundLayer newLayer)
        {
            castleLayers.Add(newLayer);
            Debug.WriteLine("worked");
            castleLayers.Sort(new SortLayerAscendingOrder());
        }

        public void InitializeBackground()
        {

            BackgroundSprite sky = new BackgroundSprite(game.Content.Load<Texture2D>("sky"), new Vector2(0, 0));
            BackgroundLayer newLayer = new BackgroundLayer(sky, -100, Vector2.Zero, camera);
            AddLayer(newLayer);
            BackgroundSprite bush = new BackgroundSprite(game.Content.Load<Texture2D>("Bushes"), new Vector2(0, 0));
            newLayer = new BackgroundLayer(bush, -103, new Vector2(0.75f, 0), camera);
            AddLayer(newLayer);
            BackgroundSprite hill = new BackgroundSprite(game.Content.Load<Texture2D>("Hills"), new Vector2(0, 0));
            newLayer = new BackgroundLayer(hill, -102, new Vector2(0.675f, 0), camera);
            AddLayer(newLayer);
            BackgroundSprite cloud = new BackgroundSprite(game.Content.Load<Texture2D>("Clouds"), new Vector2(0, 0));
            newLayer = new BackgroundLayer(cloud, -101, new Vector2(0.6f, 0), camera);
            AddLayer(newLayer);

        }

        public void InitializeCastleBackground()
        {

            BackgroundSprite black = new BackgroundSprite(game.Content.Load<Texture2D>("black"), new Vector2(0, 0));
            BackgroundLayer newLayer = new BackgroundLayer(black, -100, Vector2.Zero, camera);
            AddCastleLayer(newLayer);
            BackgroundSprite lava = new BackgroundSprite(game.Content.Load<Texture2D>("Lava"), new Vector2(0, 0));
            newLayer = new BackgroundLayer(lava, -101, new Vector2(0.7f, 0), camera);
            AddCastleLayer(newLayer);


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (entityManager1.GetBowser() != null)
            {
                for (int i = 0; i < castleLayers.Count; i++)
                {
                    BackgroundLayer castleLayer = castleLayers[i];
   
                    castleLayer.Draw(spriteBatch);
                }
            }
            else
            {
                for (int i = 0; i < layers.Count; i++)
                {
                    BackgroundLayer layer = layers[i];
                    layer.Draw(spriteBatch);
                }

            }
        }
    }
}

