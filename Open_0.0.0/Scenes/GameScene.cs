using Open_0._0._0.Actors;
using Open_0._0._0.Actors.FriendlyNpc;
using Open_0._0._0.Components;
using Open_0._0._0.Core;
using Open_0._0._0.Factories;
using Open_0._0._0.Items;
using Open_0._0._0.UI;
using Open_0._0._0.Utils;
using Open_0._0._0.World;
using SFML.System;
using SFML.Window;

namespace Open_0._0._0.Scenes
{
    class GameScene : Scene
    {

        private float _hitresponseTimer = 1f;
        private int _restartTimer = 1;

        private Map _currentMap;
        private Player _player;
        private List<Actor> _entities = new List<Actor>();
        private List<WorldItem> _worldItems = new List<WorldItem>();

        private Monster_Factory _monsterFactory = new Monster_Factory();
        private Random _random = new Random();

        private float _spawnCooldown = 5f;
        private Clock _enemyClock = new Clock();

        private MapDesigner _mapDesigner;

        private BasicHealthUI _healthUI;
        private Camera Camara;
        private ChunkManager _chunkManager;


        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {


            _player = new Player(130, 130);

            GameWorld.Player = _player;


            KeyBoard_Input.OnKeyPressed += OnIKeyClick;
            KeyBoard_Input.OnKeyPressed += OnOKeyClick;
            KeyBoard_Input.OnKeyPressed += OnEscClick;
            KeyBoard_Input.OnKeyPressed += OnLClick;

            Camara = new Camera(sceneManager.Window);

            Human enemy = new Human(15, 30);

           // NPC enemy = new NPC(15, 30);
            AddEntity(enemy);

        }
        public void AddEntity(Actor entity)
        {
            _entities.Add(entity);

            if (entity is NPC npc)
            {
                SceneManager.RegisteNpc(npc);
            }
        }
       //---------------------------------------------------------
        // UPDATE (FPS-abhängig)
        // ---------------------------------------------------------
        public override void Update(float deltaTime)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            // ... ganzer Update Code ...

            sw.Stop();
           
            GameWorld.World.Update(deltaTime);
            Camara.Update();
            _player.Update(deltaTime);

            if (sw.ElapsedMilliseconds > 5)
                Console.WriteLine($"Slow frame: {sw.ElapsedMilliseconds}ms");
        }

        // ---------------------------------------------------------
        // FIXED UPDATE (Physik)
        // ---------------------------------------------------------
        public override void FixedUpdate(float fixedDelta)
        {
            _player.FixedUpdate(fixedDelta);
            //Console.WriteLine(_player.Position_Component.PixelPosition);
        }

        // ---------------------------------------------------------
        // RENDER
        // ---------------------------------------------------------
        public override void Render()
        {
            Window.Clear();
           
            GameWorld.World.Render(Window, Camara.lastvalidposition);

            _player.Render(Window, Camara.lastvalidposition);

        }


        // ---------------------------------------------------------
        // SPAWNS
        // ---------------------------------------------------------
        private void SpawnEnemies(int maxAttempts)
        {
            if (maxAttempts <= 0) return;

            int gridX = _random.Next(1, Map.MAXWIDTH - 1);
            int gridY = _random.Next(1, Map.MAXHEIGHT - 1);

            if (_currentMap.IsTileAtWalkable(gridX, gridY))
            {
                if (_enemyClock.ElapsedTime.AsSeconds() >= _spawnCooldown)
                {
                    Enemy enemy = _monsterFactory.GetRandomMonster(gridX, gridY);
                    _entities.Add(enemy);
                    _enemyClock.Restart();
                }
            }
            else
            {
                SpawnEnemies(maxAttempts - 1);
            }

        }
        private void OnLClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.L)
            {
                Console.WriteLine("Pressed L");
                _player.DashAbility.TryExecute(_player, _player);
            }
        }
        private void OnIKeyClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.I)
            {
                Console.WriteLine("Pressed Key I");

                SceneManager.ChangeScene(new InventoryScene(SceneManager, _player.Inventory.Slots));
            }
        }
        private void OnOKeyClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.O)
            {
                Console.WriteLine("Pressed Key O");

                SceneManager.ChangeScene(new StatScene(SceneManager));
            }
        }
        private void OnPlayerDeath()
        {
            if (!GameWorld.Player.isAlive)
            {
                SceneManager.ChangeScene(new DeathScene(SceneManager));
            }
        }
        private void OnEscClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Escape)
            {
                Console.WriteLine("Pressed Escape zu Menü von maingame");
                SceneManager.ChangeScene(new MainMenuScene(SceneManager));

            }
        }
    }

}
