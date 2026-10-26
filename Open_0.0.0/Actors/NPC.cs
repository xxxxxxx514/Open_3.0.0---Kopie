using Open_0._0._0.Actions;
using Open_0._0._0.Components;
using Open_0._0._0.Core;
using Open_0._0._0.Enums;
using Open_0._0._0.Items;
using Open_0._0._0.Items.Drinks;
using Open_0._0._0.Items.Food;
using Open_0._0._0.Items.Potions;
using Open_0._0._0.Relationships;
using Open_0._0._0.Relationships.Desires;
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

        public int hungerProSec = 32;
        public int thirstProSec = 2;

        private float _upthungertimer = 1f;
        private float _exeActiontimer = 2f;
        private float _interactiontimer = 2f;
        private float _dmgtimer = 1f;

        private int _dmgCooldown = 1;
        private int _upthungerCooldown = 1;
        private int _exeActionCooldown = 2;
        private float _interactionCooldown = 2f;

        private int _dmgfromDesire = 1;

        private float _detectionRange = 80f;

        public event Action<NPC> OnInteract;

       

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
        public Detector Detector { get; set; }


        public NPC() : base()
        {
            Name = "NPC";
            RestrictAcces = RestrictAccesEnum.None;
           
            Detector = new Detector(this);

            // Actor Konstruktor — diese Zeilen löschen
            RegisterTickable(Decision_Component);
            RegisterTickable(DesireManager);
            RegisterTickable(DesireManager.Desire);

            InitializeFeedback();

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
            Item item = new Apple();
            Item potion = new Juice();

            Inventory.TryAddItem(item);
            Inventory.TryAddItem(potion);

            InitializeFeedback();


        }

        public override void Update(float deltatime)
        {
            base.Update(deltatime);
            if (!IsAlive) return;

            TickHunger(deltatime);
            //UpdateHunger();

            // Bedürfnisse aktualisieren → Actions aktivieren/deaktivieren
            DesireManager.Desire.Update(deltatime,DesireManager);

            // Entscheidung treffen
            var action = Decision_Component.Decide(this);

            ExecuteAction(deltatime,action);

            TickInteract(deltatime);

        }
        public override void FixedUpdate(float fixedDelta)
        {
            // Bewegung
            Movement_Component.UpdatePosition();
        }
        private void ExecuteAction(float dt, IAction action)
        {
            // WanderAction läuft IMMER sofort
            if (action is WanderAction)
            {
                action.Execute(this, dt);
                return;
            }

            // Timer runterzählen
            _exeActiontimer -= dt;

            // Wenn Timer abgelaufen → Action ausführen
            if (_exeActiontimer <= 0f)
            {
                action.Execute(this, dt);
                _exeActiontimer = _exeActionCooldown; // RESET
            }
        }
        public void TickHunger(float deltatime)
        {
            _upthungertimer -= deltatime;//timer runter zählen

            if (_upthungertimer <= 0)
            {
                DesireManager.Desire.DrainNeeds(hungerProSec, thirstProSec);
                _upthungertimer = _upthungerCooldown;

                Console.WriteLine(ToString());

            }
            if (DesireManager.Desire.IsStarving())
            {
                if (_dmgtimer <= 0)
                {
                    Health_Component.ApplyDamage(_dmgfromDesire);
                    _dmgtimer = _dmgCooldown;
                }
            }
        }
        public override void Render(RenderWindow window, Vector2f camaraPos)
        {
            window.Draw(Body_Componente.Sprite);  // Dann den Sprite

        }

        public virtual void TickInteract(float deltatime)
        {
            _interactiontimer -= deltatime;
            if (Detector.IsInRange(GameWorld.Player.Position_Component.PixelPosition, _detectionRange))
            {
                if (_interactiontimer <= 0)
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.E))
                    {
                        Console.WriteLine(Dialogs[DialogKey.Welcome]);
                        _interactiontimer = _interactionCooldown;
                        OnInteract?.Invoke(this);

                    }
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

            if (DesireManager.Desire.State.HasFlag(DesireState.Happy))
                states.Add("Happy");

            return string.Join(", ", states);
        }


    }
}