using Open_0._0._0.Actors;
using Open_0._0._0.Core;
using Open_0._0._0.Items;
using Open_0._0._0.UI;
using Open_0._0._0.Utils;
using Open_0._0._0.World;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard;

namespace Open_0._0._0.Scenen
{
    class GameScene : Scene
    {
        private Map _currentMap;
        private Player _player;
        private List<Actor> _entities = new List<Actor>();
        private List<WorldItem> _worldItems = new List<WorldItem>();

        private Monster_Factory _monsterFactory = new Monster_Factory();
        private Random _random = new Random();

        private float _spawnCooldown = 2f;
        private Clock _enemyClock = new Clock();

        private MapDesigner _mapDesigner;

        private BasicHealthUI _healthUI;
       

        private int t = 0;

        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {

            _currentMap = new Map();
            _player = new Player(10, 30);
            _healthUI=new BasicHealthUI(_player);

            GameWorld.Player = _player;
            GameWorld.Entities = _entities;
            GameWorld.CurrentMap = _currentMap;
            GameWorld.WorldItems = _worldItems;

            KeyBoard_Input.OnKeyPressed += OnIKeyClick;
            KeyBoard_Input.OnKeyPressed += OnOKeyClick;
            KeyBoard_Input.OnKeyPressed += OnEscClick; ;


        }

        // ---------------------------------------------------------
        // UPDATE (FPS-abhängig)
        // ---------------------------------------------------------
        public override void Update(float deltaTime)
        {
            OnPlayerDeath();
            // Items droppen
            if (_player.dropItem != null)
            {
                _worldItems.Add(_player.dropItem);
                _player.dropItem = null;
            }

            // Gegner spawnen
            SpawnEnemies(5);

            // Player Update
            _player.Update(deltaTime);
            _healthUI.UpdatePlayerInfo();

            // Gegner Update
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                if (!_entities[i].IsAlive)
                    _entities.RemoveAt(i);
                else
                    _entities[i].Update(deltaTime);
            }
        }

        // ---------------------------------------------------------
        // FIXED UPDATE (Physik)
        // ---------------------------------------------------------
        public override void FixedUpdate(float fixedDelta)
        {
            _player.FixedUpdate(fixedDelta);

            foreach (var entity in _entities)
                entity.FixedUpdate(fixedDelta);
        }

        // ---------------------------------------------------------
        // RENDER
        // ---------------------------------------------------------
        public override void Render()
        {
            _currentMap.RenderMap(Window);
            _player.Render(Window);

            foreach (var enemy in _entities)
                enemy.Render(Window);

            foreach (var item in _worldItems)
                Window.Draw(item.Item.Shape);

            _healthUI.Render(Window);
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
        private void OnIKeyClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.I)
            {
                Console.WriteLine("Pressed Key I");

                SceneManager.ChangeScene(new InventoryScene(SceneManager,_player.Inventory.Slots));
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
            if(!GameWorld.Player.isAlive)
            {
                SceneManager.ChangeScene(new DeathScene(SceneManager));
            }
        }
        private void OnEscClick(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Escape)
            {
                Console.WriteLine("Pressed Escape");
                SceneManager.ChangeScene(new MainMenueScene(SceneManager));
            }
        }
    }

}
