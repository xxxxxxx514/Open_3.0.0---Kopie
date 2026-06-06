using Open_0._0._0.Actions;
using Open_0._0._0.Components;
using Open_0._0._0.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Text;

namespace Open_0._0._0.Actors
{
    class NPC : Actor
    {
        public const int NPC_SIZE = 28;

        public int hungerProSec = 2;
        public int thirstProSec = 2;

        private float _upthungertimer = 1f;
        private float _exeActiontimer = 2f;

        private int _upthungerCooldown = 1;
        private int _exeActionCooldown = 2;

        private int _dmgfromDesire = 0;


        private float _detectionRange = 50f;

        private Dictionary<DialogKey, string> Dialogs = new()
        {
            { DialogKey.Welcome, "Hey! Schön dich zu sehen." },
            { DialogKey.Goodbye, "Danke fürs Spielen. Bis bald!" },
            { DialogKey.Error, "Es ist ein Fehler aufgetreten." },
            { DialogKey.ConfirmExit, "Bist du sicher, dass du das Spiel verlassen möchtest?" },
            { DialogKey.InvalidInput, "Diese Eingabe ist ungültig." }
        };

        public NPCState CurrentState { get; set; } = NPCState.Idle;
        public RelationshipState Relationship { get; set; } = RelationshipState.Stranger;
        public PlayerDetector Detector { get; set; }


        public NPC() : base()
        {
            Name = "NPC";
            RestrictAcces = RestrictAccesEnum.None;
            Body_Componente.ChangeBodyColor(CustomColors.BabyBlue);

            timer = new Clock();
            _timer = new Clock();
            Detector = new PlayerDetector(this);

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
            base.Update(deltatime, map);
            if (!IsAlive) return;

            TickHunger(deltatime, map);
            //UpdateHunger();

            // Bedürfnisse aktualisieren → Actions aktivieren/deaktivieren
            DesireManager.Desire.Update(deltatime,DesireManager);

            // Entscheidung treffen
            var action = Decision_Component.Decide(this);

            ExecuteAction(deltatime, map,action);

            // Bewegung
            Movement_Component.UpdatePosition();

            Interactt();

        }
        private void ExecuteAction(float dt, Map map, IAction action)
        {
            // WanderAction läuft IMMER sofort
            if (action is WanderAction)
            {
                action.Execute(this, dt, map);
                return;
            }

            // Timer runterzählen
            _exeActiontimer -= dt;

            // Wenn Timer abgelaufen → Action ausführen
            if (_exeActiontimer <= 0f)
            {
                action.Execute(this, dt, map);
                _exeActiontimer = _exeActionCooldown; // RESET
            }
        }
        public void TickHunger(float deltatime, Map map)
        {
            _upthungertimer -= deltatime;//timer runter zählen

            if (_upthungertimer <= 0)
            {
                DesireManager.Desire.DrainNeeds(hungerProSec, thirstProSec);
                DesireManager.Desire.Update(deltatime,DesireManager);

                _upthungertimer = _upthungerCooldown;

                Console.WriteLine(ToString());

            }
            if (DesireManager.Desire.IsStarving())
            {
                Health_Component.TakeDamage(_dmgfromDesire);
            }
        }
        public override void Render(RenderWindow window)
        {
            window.Draw(Body_Componente.Sprite);  // Dann den Sprite
            window.Draw(Body_Componente.Shape);

        }

        public virtual void Interactt()
        {
            if (Detector.IsInRange(Game.Player.Position_Component.PixelPosition, _detectionRange))
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                {
                    Console.WriteLine(Dialogs[DialogKey.Welcome]);
                }
            }
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