using sprint0v2.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Controllers;
using sprint0v2.Entities;
using sprint0v2.Tiles;
using sprint0v2.Background;
using sprint0v2.SoundManager;
using sprint0v2.GameState;

using System.Diagnostics;
using sprint0v2.Commands;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using sprint0v2.Randomizer;
using System.IO;
using sprint0v2.Effects;

namespace sprint0v2
{
    public class Game1 : Game
    {
        public static bool nextLevel = false;

        public static Game1 Instance { get; set; }
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private KeyboardController _keyboardController;
        public KeyboardController KeyboardController { get => _keyboardController; }
        private GamepadController _gamepadController;
        public GamepadController GamepadController { get => _gamepadController; }
        public TileMapManager tileMapManager;
        private EntityManager entityManager;
        public EntityManager EntityManager { get => entityManager; }

        private SoundEventManager soundEventManager;
        public SoundEventManager SoundEventManager { get => soundEventManager; }
        
        private Camera camera;
        public Camera Camera { get => camera; }
        private BackgroundScenery scenery;
        public BackgroundScenery Scenery
        {
            get => scenery;
        }
        private Grid collsionGrid;
        public Grid CollisionGrid { get => collsionGrid; }

        private bool reset = false;
        private Vector2 centerOfScreen;
        public Vector2 CenterOfScreen
        {
            get => centerOfScreen;
            set => centerOfScreen = value;
        }
        public bool Reset
        {
          get => reset; 
          set => reset = value;
        }
        private Mario player;
        public Mario Player { get => player; }
        private SpriteFont font;
        public SpriteFont Font
        {
            get => font;
        }
        private string world;
        public string World
        {
            get => world;
        }

        private HUD hud;
        public HUD Hud
        {
            get => hud;
        }

        private Texture2D fireworkTexture;
        private FireworkController fireworkController;

        public FireworkController FireworkController
        {
            get => fireworkController;
            set => fireworkController = value;
        }

        private IGameState gameState;
        public IGameState GameState { get => gameState; set => gameState = value; }

        private IGameState playState;
        public IGameState PlayState
        {
            get => playState;
        }
        private IGameState pauseState;
        private GameOverState gameOverState;
        public GameOverState GameOverState
        {
            get => gameOverState;
        }

        private WinState winState;
        public WinState WinState
        {
            get => winState;
        }
        private StartState startState;
        public StartState StartState
        {
            get => startState;
        }
        public IGameState PausedState
        {
            get => pauseState;
        }
        private float elapsedGameTime;
        public float ElapsedGameTime {
            get => elapsedGameTime;
            set => elapsedGameTime = value; }

        private string path;
        public string Path
        {
            get => path;
            set => path = value;
        }

        #region Sound Effect Events
        public event Action GameOverAction;
        public event Action PauseAction;
        public event Action StageClearAction;
        public event Action HurryUpAction;
        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            if (GraphicsDevice == null)
            {
                _graphics.ApplyChanges();
            }
            _graphics.PreferredBackBufferWidth = 256; 
            _graphics.PreferredBackBufferHeight = 238;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            centerOfScreen = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            playState = new PlayState();
            pauseState = new PauseState();
            winState = new WinState(Content);
            gameOverState = new GameOverState(Content);
            startState = new StartState(Content);



        }

        protected override void Initialize()
        {
            fireworkTexture = Content.Load<Texture2D>("Firework");
            int rows = 5;
            int columns = 6; 
            float animationInterval = 0.125f; 
            fireworkController = new FireworkController(fireworkTexture, rows, columns, animationInterval);

            gameState = startState;
            camera = new Camera(GraphicsDevice.Viewport);
            camera.LookAt(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            entityManager = new EntityManager(this);
            path = " ../../../Tiles/tilemap.json";
            MapRandomizer randomizer = new MapRandomizer();
            randomizer.CombineAndWriteToJsonFile();
            world = "1-1";
            tileMapManager = new TileMapManager();
            tileMapManager.Create(path, entityManager);

            player = entityManager.GetPlayer();
            collsionGrid = new Grid();
            _keyboardController = new KeyboardController(this, player);
            _gamepadController = new GamepadController(this, player);

            scenery = new BackgroundScenery(camera, this, entityManager);


            scenery.InitializeCastleBackground();
            scenery.InitializeBackground();


            font = Content.Load<SpriteFont>("Font");
            hud = new HUD(camera.Position, player, font);
            
            soundEventManager = new SoundEventManager(this);
            soundEventManager.PlayBackgroundSong(path);
            soundEventManager.PlayerSubscribe(player);
            soundEventManager.GameSubscribe(this);

            collsionGrid.AddEntities(entityManager.GetEntities());
            new GameStateSnapshot();

            base.Initialize();
        }

        public void NextLevel()
        {
            Debug.WriteLine("Loading next level");

            // Clear the entityManager
            if (entityManager is not null)
            entityManager.Clear();

 

            tileMapManager = new TileMapManager();
            tileMapManager.Create(path, entityManager);
            collsionGrid.Clear();
            collsionGrid.AddEntities(entityManager.GetEntities());
            player = entityManager.GetPlayer();
            _keyboardController = new KeyboardController(this, player);
            _gamepadController = new GamepadController(this, player);
            soundEventManager = new SoundEventManager(this);
            soundEventManager.PlayBackgroundSong(path);
            soundEventManager.PlayerSubscribe(player);
            soundEventManager.GameSubscribe(this);
        }




        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update(this, gameTime);
            if (GameState is PlayState)
            {
                ElapsedGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if((int)ElapsedGameTime == 200)
            {
                HurryUpAction?.Invoke();
            }
            if((int)ElapsedGameTime <= 0 || player.Lives == 0)
            {
                ResetGame();
            }
            if(!Path.Contains("castle") && (int)player.Position.X == 2968) //x position of castle - 16
            {
                WinGame();
            }
            else if (Path.Contains("castle") && (int)player.Position.X == 4850) 
            {
                WinGame();
            }
            if (player.IsFinished)
            {
                int lastDigitOfTime = (int)ElapsedGameTime % 10;
                Vector2 startPosition = new Vector2(player.Position.X-100, _graphics.PreferredBackBufferHeight -100);
                fireworkController.AddMultipleFireworks(lastDigitOfTime, startPosition);
                player.IsFinished = false;

            }
            fireworkController.Update(gameTime);

            base.Update(gameTime);
        }
        public void ResetGame()
        {
            player = EntityManager.Instance.GetPlayer();
            _keyboardController = new KeyboardController(this, player);
            _gamepadController = new GamepadController(this, player);
            soundEventManager = new SoundEventManager(this);
            soundEventManager.PlayBackgroundSong(path);
            soundEventManager.PlayerSubscribe(player);
            soundEventManager.GameSubscribe(this);
            reset = false;
            if(player.Lives == 0)
            {
                gameState = gameOverState;
                player.Lives = 3;
            }
        }

        public void WinGame()
        {
            gameState = winState;
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
           
            GameState.Draw(this, gameTime);

            base.Draw(gameTime);


        }
    }
}