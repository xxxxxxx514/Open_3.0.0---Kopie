using Open_0._0._0.Actors;
using SFML.System;

namespace Open_0._0._0
{
    // ===============================
    // ENGINE TODO – GENAU WAS ZU TUN IST
    // ===============================

    // 1) Core Engine Struktur verbessern
    //    - EngineLoop-Klasse erstellen (Update, FixedUpdate, Render trennen)
    //    - DeltaTime zentral berechnen und an alle Systeme verteilen
    //    - SceneManager einführen (Menu, Game, MapDesigner als Scenes)
    //    - Lifecycle: OnEnter(), OnExit(), OnPause(), OnResume()

    // 2) ECS / System-Trennung einführen
    //    - Actor nicht mehr alles selbst machen lassen
    //    - MovementSystem: bewegt ALLE Entities
    //    - AISystem: verarbeitet NPC/Enemy AI
    //    - RenderSystem: zeichnet alles in richtiger Reihenfolge
    //    - CollisionSystem: Kollisionen zentral prüfen
    //    - Components dynamisch machen (AddComponent<T>(), GetComponent<T>())

    // 3) ResourceManager erstellen
    //    - Texturen, Fonts, Sounds zentral laden & cachen
    //    - Keine Texturen mehr im Konstruktor laden
    //    - Beispiel: TextureManager.Get("Goblin.png")

    // 4) Input-System bauen
    //    - KeyDown/KeyUp Events statt dauernd Keyboard.IsKeyPressed()
    //    - Rebindable Controls (z. B. WASD ändern)
    //    - InputActions: Move, Attack, Interact, OpenInventory

    // 5) EventBus einführen (globales Messaging)
    //    - Publish/Subscribe System
    //    - Beispiele:
    //        EventBus.Publish(new ItemPickedEvent(player, item));
    //        EventBus.Subscribe<ItemPickedEvent>(OnItemPicked);
    //    - Entkoppelt Player, UI, Inventory, World

    // 6) Rendering Engine erweitern
    //    - Camera-System (Follow Player, Zoom, Shake)
    //    - AnimationSystem (SpriteSheets, FrameTime)
    //    - RenderQueue (Z-Sorting: Hintergrund → Entities → UI)

    // 7) Physics/Collision Engine erweitern
    //    - Layer/Masken (Player, Enemy, Item, Wall)
    //    - Trigger-Events (OnEnter, OnExit)
    //    - Raycasting (für AI-Sicht, Schüsse, Interaktionen)

    // 8) Debug-Tools hinzufügen
    //    - FPS Counter
    //    - Collision-Box Visualizer
    //    - Debug Console (z. B. "spawn goblin", "tp 10 20")
    //    - Entity Counter, Memory Usage

    // 9) Save/Load System erweitern
    //    - Player Stats speichern
    //    - Inventory speichern
    //    - Entities speichern
    //    - WorldItems speichern
    //    - MapDesigner Output integrieren

    // 10) Settings-System
    //    - Auflösung, VSync, Volume, Keybinds
    //    - Settings speichern & laden

    // ===============================
    // Das ist die vollständige ENGINE-PHASE Roadmap.
    // ===============================


    internal class Program
    {
        static void Main(string[] args)
        {
            // Game game = new Game();
            Input_System.OnKeyPressed += (key) => Console.WriteLine("Key gedrückt"+key);
            Input_System.OnKeyReleased += (key) => Console.WriteLine("Key losgelasen"+key);

            while (true) { Input_System.Update(); }
        }
    }
}
