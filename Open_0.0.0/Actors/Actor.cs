using Open_0._0._0.Components;
using Open_0._0._0.Enums;
using Open_0._0._0.Items;
using Open_0._0._0.Relationships;
using SFML.Graphics;
using SFML.System;

namespace Open_0._0._0.Actors
{
    abstract class Actor : ICollidable
    {
        public event Action<Actor> Died;

        public Tag Tag { get; private set; } = Tag.Untagged;
        public Actor Owner { get; init; }

        public string Name;
        public bool IsAlive { get; set; } = true;
        public Body_Component Body_Componente { get; set; }
        public Health_Component Health_Component { get; private set; }
        public Position_Component Position_Component { get; set; }
        public Level_Component Level_Component { get; private set; }
        public Collision2D Collison2D { get; set; }
        public Movement_Component Movement_Component { get; set; }
        public Attack_Component Attack_Component { get; set; }
        public CombatFeedbackComponent CombatFeedbackComponent { get; set; }
        public RestrictAccesEnum RestrictAcces { get; init; }
        public List<Relationship> Relationships { get; set; }
        public Relationship RelationshipWithPlayer { get; set; }
        public DecisionComponent Decision_Component { get; set; }
        public DesireManager DesireManager { get; set; }
        public Inventory Inventory { get; private set; }
        public Equipment Equipment { get; set; }

        public List<ITickable> TickComponents = new();
        public List<IAbility> Abilities { get; set; } = new();

        public Actor(RestrictAccesEnum restrictAcces = RestrictAccesEnum.None)
        {
            Position_Component = new Position_Component();
            Body_Componente = new Body_Component(Position_Component);
            Health_Component = new Health_Component(this);

            Level_Component = new Level_Component();
            Movement_Component = new Movement_Component(this);
            Collison2D = new Collision2D();
            Attack_Component = new Attack_Component();

            RestrictAcces = restrictAcces;

            Relationships = new List<Relationship>();
            RelationshipWithPlayer = new Relationship();

            Decision_Component = new DecisionComponent();
            DesireManager = new DesireManager();

            Inventory = new Inventory();

            RegisterTickable(Movement_Component);
            RegisterTickable(Attack_Component);
            RegisterTickable(Level_Component);
        }

        // ---------------------------------------------------------
        // UPDATE (FPS-abhängig)
        // ---------------------------------------------------------
        public virtual void Update(float deltatime)
        {
            foreach (var component in TickComponents)
                component.Tick(deltatime);
        }

        // ---------------------------------------------------------
        // FIXED UPDATE (Physik, 50 Hz)
        // ---------------------------------------------------------
        public virtual void FixedUpdate(float fixedDelta)
        {
            // Noch leer – aber EngineLoop ruft es auf.
            // Movement_Component kann später hier rein, wenn du willst.
        }
        public void InitializeFeedback()
        {
            CombatFeedbackComponent = new CombatFeedbackComponent(this);
            RegisterTickable(CombatFeedbackComponent);
        }
        public void RegisterTickable(object component)
        {
            if (component is ITickable tickable)
                TickComponents.Add(tickable);
        }

        public abstract void Render(RenderWindow window, Vector2f camaraPos);

        public virtual void OnDeath()
        {
            IsAlive = false;
            Console.WriteLine($"{Name} ist gestorben.");
            Died?.Invoke(this);
        }
        public void Consume(Item item)
        {
            Inventory.Remove(item);
        }

        public void Equip(Item item)
        {
            if (item == null) return;

            switch (item.EquipmentType)
            {
                case EquipmentType.Helm:
                    Equipment.Helm = item;
                    break;

                case EquipmentType.Chestplate:
                    Equipment.Chestplate = item;
                    break;

                case EquipmentType.Armguard:
                    Equipment.Armguard = item;
                    break;

                case EquipmentType.Pants:
                    Equipment.Pants = item;
                    break;

                case EquipmentType.Shoes:
                    Equipment.Shoes = item;
                    break;

                case EquipmentType.Lefthand:
                    Equipment.Lefthand = item;
                    break;

                case EquipmentType.Righthand:
                    Equipment.Righthand = item;
                    break;

                default:
                    return;
            }
        }
        public void SetTag(Tag tag)
        {
            Tag = tag;
        }

        public string ActionsToString()
        {
            if (DesireManager.Actions == null || DesireManager.Actions.Count == 0)
                return "Keine Actions vorhanden.";

            return string.Join(", ", DesireManager.Actions.Select(a => a.GetType().Name));
        }

        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}
