using Open_0._0._0.Actors;
using Open_0._0._0.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace Open_0._0._0
{
    internal class Game
    {
        private BasicGameStart_UI _basicGameStartUI = new BasicGameStart_UI();
        private MapDesigner _mapDesigner;
        private Monster_Factory _monsterFactory = new Monster_Factory();
        private RenderWindow _window;
        private Map _currentMap;
        public static Player Player;
        private StatWindowUI _statWindow;
        private Random _random = new Random();
        private bool _isGameRunning = false;

        public static List<WorldItem> WorldItems = new List<WorldItem>();
        public static List<Actor> Entities = new List<Actor>();

        private Clock _clock;
        private Clock _enemyClock = new Clock();
        private float _spawnCooldown = 5f;

        int t = 0;

        public Game()
        {
            _window = new RenderWindow(new VideoMode(1920, 1080), "My Game");
            _currentMap = new Map();
            _clock = new Clock();
            Player = new Player(10, 30);
            _statWindow = new StatWindowUI(Player);

            OnButtonClick();
            RunMenu();
        }

        private void RunMenu()
        {
            _basicGameStartUI.toggle = true;

            while (_window.IsOpen && !_isGameRunning)
            {
                _window.DispatchEvents();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    _window.Close();

                _window.Clear();
                _basicGameStartUI.Render(_window);
                _window.Display();
            }
        }

        public void RunGame()
        {
            _isGameRunning = true;

            while (_window.IsOpen)
            {
                if (!Player.isAlive)
                {
                    _window.Close();
                    break;
                }

                float deltaTime = _clock.Restart().AsSeconds();
                _window.DispatchEvents();

                if (Keyboard.IsKeyPressed(Keyboard.Key.M))
                {
                    _isGameRunning = false;
                    RunMenu();
                }

                Update(deltaTime);

                _window.Clear();
                Render(_window);
                _window.Display();
            }
        }

        private void RunMapDesign()
        {
            _mapDesigner = new MapDesigner();
            Room room = _mapDesigner.Run();
            // TODO: Map speichern Logik anpassen
            RunMenu();
        }

        private void Render(RenderWindow window)
        {
            _currentMap.RenderMap(window);
            Player.Render(window);

            foreach (Actor enemy in Entities)
                enemy.Render(window);

            Player.inventoryUI.Render(window);
            _statWindow.Render(window);

            foreach (WorldItem item in WorldItems)
                window.Draw(item.Item.Shape);
        }

        private void Update(float deltatime)
        {
            if (Player.dropItem != null)
            {
                WorldItems.Add(Player.dropItem);
                Player.dropItem = null;
            }

            // zum Testen: regelmäßig Humans spawnen
            // SpawnHuman(1);
            //SpawnEnemies(5);


            if (t == 0)
            {
                int gridX = _random.Next(1, Map.MAXWIDTH - 1);
                int gridY = _random.Next(1, Map.MAXHEIGHT - 1);
                Human human = new Human(gridX, gridY);

                Console.WriteLine(human.ToString());

                // Startposition im Weltkoordinatensystem
                human.Body_Componente.SetBodyPostion(new Vector2f(300, 212));

                Entities.Add(human);
                t++;
            }

            Player.Update(deltatime, _currentMap);

            for (int i = Entities.Count - 1; i >= 0; i--)
            {
                if (!Entities[i].IsAlive)
                    Entities.RemoveAt(i);
                else
                    Entities[i].Update(deltatime, _currentMap);
            }
        }

        private void SpawnHuman(int maxAttempts)
        {
            if (t == 0)
            {
                if (maxAttempts <= 0) return;

                int gridX = _random.Next(1, Map.MAXWIDTH - 1);
                int gridY = _random.Next(1, Map.MAXHEIGHT - 1);

                if (_currentMap.IsTileAtWalkable(gridX, gridY))
                {
                    if (_enemyClock.ElapsedTime.AsSeconds() >= _spawnCooldown)
                    {
                        Human human = new Human(gridX, gridY);

                        Console.WriteLine(human.ToString());

                        // Startposition im Weltkoordinatensystem
                        human.Body_Componente.SetBodyPostion(new Vector2f(300, 212));

                        Entities.Add(human);
                        _enemyClock.Restart();

                        Console.WriteLine($"Human gespawnt bei Grid: ({gridX},{gridY})");
                    }
                }
                else
                {
                    SpawnHuman(maxAttempts - 1);
                }
            }
        }

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
                    Entities.Add(enemy);
                    _enemyClock.Restart();
                    Console.WriteLine($"Enemy gespawnt bei Grid: ({gridX},{gridY})");
                }
            }
            else
            {
                SpawnEnemies(maxAttempts - 1);
            }
        }

        private void OnButtonClick()
        {
            _window.MouseButtonPressed += (sender, e) =>
            {
                if (!_basicGameStartUI.toggle) return;
                if (e.Button != Mouse.Button.Left) return;

                Vector2i mousePos = Mouse.GetPosition(_window);

                if (_basicGameStartUI.StartButton.GetGlobalBounds().Contains(mousePos))
                {
                    _basicGameStartUI.toggle = false;
                    RunGame();
                }
                else if (_basicGameStartUI.MapDesignButton.GetGlobalBounds().Contains(mousePos))
                {
                    _basicGameStartUI.toggle = false;
                    RunMapDesign();
                }
                else if (_basicGameStartUI.SettingsButton.GetGlobalBounds().Contains(mousePos))
                {
                    Console.WriteLine("Settings wurde geklickt");
                }
            };

            Player.OnMouseKlick(_window);

            _window.KeyPressed += (sender, e) =>
            {
                if (e.Code == Keyboard.Key.O)
                    _statWindow.toggle = !_statWindow.toggle;

                if (e.Code == Keyboard.Key.I)
                {
                    Player.inventoryUI.toggle = !Player.inventoryUI.toggle;
                    if (!Player.inventoryUI.toggle)
                        Player.inventoryUI.TurnOffAllToggle();
                }
            };
        }
    }
}
