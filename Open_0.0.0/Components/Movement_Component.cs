using Open_0._0._0.Actors;
using Open_0._0._0.Utils;
using SFML.System;
using SFML.Window;

namespace Open_0._0._0.Components
{
     class Movement_Component
    {
        public Actor Character;
        public float MoveSpeed { get; private set; } = 210f;

        public Movement_Component(Actor character)
        {
            Character = character;
        }

        // ---------------------------------------------------------
        // 1) Allgemeine Bewegung (für Enemy, Human, NPC)
        // ---------------------------------------------------------
        public bool TryMove(Vector2f movement, Map map, float size)
        {
            CollisionInfo collision = Character.Collison2D.CollisionMap(
                Character.Position_Component.PixelPosition,
                map,
                movement,
                size
            );

            if (!collision.HasCollision)
            {
                Character.Position_Component.PixelPosition += movement;
                return true;
            }

            return false;
        }

        // ---------------------------------------------------------
        // 2) Position aktualisieren (für alle Actors)
        // ---------------------------------------------------------
        public void UpdatePosition()
        {
            Character.Body_Componente.Shape.Position = Character.Position_Component.PixelPosition;
            Character.Body_Componente.Sprite.Position = Character.Position_Component.PixelPosition;

            Character.Position_Component.GridPosition = Character.Position_Component.ToGridPosition();
            Character.Position_Component.PixelPosition = Character.Body_Componente.Shape.Position;
        }

        // ---------------------------------------------------------
        // 3) Input für Player (wird NUR vom Player benutzt)
        // ---------------------------------------------------------
        public Vector2f GetInputDirection()
        {
            Vector2f direction = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) direction.Y = -1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) direction.Y = 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) direction.X = -1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) direction.X = 1;

            return MathHelper.Normalize(direction);
        }

        // ---------------------------------------------------------
        // 4) Speed erhöhen (für Buffs, Level-Ups, Items)
        // ---------------------------------------------------------
        public void IncreaseMoveSpeed(float amount)
        {
            if (amount > 0)
                MoveSpeed += amount;
        }
    }
}