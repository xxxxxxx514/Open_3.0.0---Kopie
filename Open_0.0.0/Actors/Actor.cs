using Open_0._0._0.Components;
using SFML.Graphics;

namespace Open_0._0._0.Actors
{
    abstract class Actor : ICollidable
    {
        public Tag Tag { get; private set; } = Tag.Untagged;
        public Actor Owner { get; init; }

        public string Name;
        public bool IsAlive { get; set; } = true;
        public Body_Componente Body_Componente { get; set; }
        public Health_Component Health_Component { get; private set; }
        public Position_Component Position_Component { get; set; }
        public Level_Component Level_Component { get; private set; }
        public Collison2D Collison2D { get; set; }
        public Movement_Component Movement_Component { get; set; }
        public Attack_Component Attack_Component { get; set; }
        public RestrictAccesEnum RestrictAcces { get; init; }
        public List<Relationship> Relationships { get; set; }
        public Relationship RelationshipWithPlayer { get; set; }
        public DecisionComponent Decision_Component { get; set; }
        public DesireManager DesireManager { get; set; }
        public Inventory Inventory { get; private set; }

        public List<ITickable> TickComponents = new();

        public Actor(RestrictAccesEnum restrictAcces = RestrictAccesEnum.None)
        {
            Position_Component = new Position_Component();
            Body_Componente = new Body_Componente(Position_Component);
            Health_Component = new Health_Component(this);

            Level_Component = new Level_Component();
            Movement_Component = new Movement_Component(this);
            Collison2D = new Collison2D();
            Attack_Component = new Attack_Component();
            RestrictAcces = restrictAcces;

            Relationships = new List<Relationship>();
            RelationshipWithPlayer = new Relationship();

            Decision_Component = new DecisionComponent();
            DesireManager = new DesireManager();

            Inventory = new Inventory();


            RegisterTickable(Movement_Component);
            RegisterTickable(Attack_Component);
            RegisterTickable(Decision_Component);
            RegisterTickable(DesireManager);
            RegisterTickable(Level_Component);
            RegisterTickable(DesireManager.Desire);
        }
        public virtual void Update(float deltatime, Map map)
        {
            foreach (var component in TickComponents)
            {
                component.Tick(deltatime);
            }
        }
        private void RegisterTickable(object component)
        {
            if (component is ITickable tickable)
            {
                TickComponents.Add(tickable);
            }
        }
        public abstract void Render(RenderWindow window);
        public virtual void OnDeath()
        {
            IsAlive = false;
            Console.WriteLine($"{Name} ist gestorben.");
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
