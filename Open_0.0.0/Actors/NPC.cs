using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
using System.Text;

namespace Open_0._0._0.Actors
{
     class NPC : Actor
    {
        public const int NPC_SIZE = 28;

        private Clock timer;
        public NPCState CurrentState { get; set; } = NPCState.Idle;
        public RelationshipState Relationship { get; set; } = RelationshipState.Stranger;

        public NPC() : base()
        {
            Name = "NPC";
            RestrictAcces = RestrictAccesEnum.None;
            Body_Componente.ChangeBodyColor(CustomColors.BabyBlue);

            timer = new Clock();

        }

        public NPC(int gridX, int gridY) : this()
        {

            Console.WriteLine($"NPC Constructor: gridX={gridX}, gridY={gridY}");
            Console.WriteLine($"Before SetPosition - PixelPosition: ({Position_Component.PixelPosition.X}, {Position_Component.PixelPosition.Y})");

            Vector2f newPos = Position_Component.ToPixelPosition(gridX, gridY);
            Console.WriteLine($"ToPixelPosition returned: ({newPos.X}, {newPos.Y})");

            Position_Component.SetPosition(newPos);

            Console.WriteLine($"After SetPosition - PixelPosition: ({Position_Component.PixelPosition.X}, {Position_Component.PixelPosition.Y})");
            Console.WriteLine($"After SetPosition - GridPosition: ({Position_Component.GridPosition.X}, {Position_Component.GridPosition.Y})");
        }

        public override void Update(float deltatime, Map map)
        {
            if (!IsAlive) return;

            UpdateHunger();

            // Bedürfnisse aktualisieren → Actions aktivieren/deaktivieren
            DesireManager.Desire.Update(DesireManager);

            // Entscheidung treffen
             var action = Decision_Component.Decide(this);

            // Action ausführen
             action.Execute(this,deltatime,map);

            // Bewegung
            Movement_Component.UpdatePosition();
      

            // 1) Input
            Vector2f dir = Movement_Component.GetInputDirection();
            Vector2f movement = dir * deltatime * Movement_Component.MoveSpeed;

            // 2) Bewegung
            Movement_Component.TryMove(movement, map, NPC_SIZE);

            // 3) Position aktualisieren
            Movement_Component.UpdatePosition();

            // 4) Items aufsammeln
        

        }

        public void UpdateHunger()
        {
            if (timer.ElapsedTime.AsSeconds() >= 1)
            {
                DesireManager.Desire.DrainNeeds(20, 20);
                DesireManager.Desire.Update(DesireManager);
                timer.Restart();
                Console.WriteLine(ToString());

            }
            if (DesireManager.Desire.IsStarving())
            {
                Health_Component.TakeDamage(0);
            }
        }

        public override void Render(RenderWindow window)
        {
            window.Draw(Body_Componente.Sprite);  // Dann den Sprite
            window.Draw(Body_Componente.Shape);

        }

        public virtual void Interact()
        {

        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"NPC: {Name}");
            sb.AppendLine($"State: {CurrentState}");
            sb.AppendLine($"DesireState: {DesireStateToString()}");
            sb.AppendLine($"Relationships: {RealtionsToString()}");
            sb.AppendLine($"Relationship with Player: {RelationshipWithPlayer.RelationshipState}");
            sb.AppendLine($"Actions: {ActionsToString()}");
            sb.AppendLine($"Health: {Health_Component.Health}");
            sb.AppendLine($"Hunger: {DesireManager.Desire.Hunger}, Thirst: {DesireManager.Desire.Thirst}, Happiness: {DesireManager.Desire.Happines}");
            sb.AppendLine($"Pos: {Position_Component.PixelPosition.X:F0}, {Position_Component.PixelPosition.Y:F0}");
            sb.AppendLine($"Grid: {Position_Component.GridPosition.X}, {Position_Component.GridPosition.Y}");

            return sb.ToString();
        }

        private string RealtionsToString()
        {
            if (Relationships.Count == 0)
                return "[]";

            string result = "[";

            for (int i = 0; i < Relationships.Count; i++)
            {
                var relation = Relationships[i];

                result += Relationships[i].ToString();

                if (i < Relationships.Count - 1)
                    result += ", ";
            }

            result += "]";
            return result;
        }
        private string DesireStateToString()
        {
            List<string> states = new List<string>();

            if (DesireManager.Desire.State.HasFlag(DesireState.HungerHigh))
                states.Add("HungerHigh");

            if (DesireManager.Desire.State.HasFlag(DesireState.ThirstHigh))
                states.Add("ThirstHigh");

            if (DesireManager.Desire.State.HasFlag(DesireState.Unhappy))
                states.Add("Unhappy");

            return string.Join(", ", states);
        }


    }
}