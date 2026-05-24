using Open_0._0._0.Actors;
using SFML.System;

namespace Open_0._0._0
{
    // TODO: Phase 1 - Spiel fertig
    // [x] Basic Inventory
    // [x] Items können aufgenommen werden  
    // [ ] Kampf-System verbessern
    // [ ] Mehr Map-Vielfalt

    // TODO: Phase 2 - Refactoring
    // [ ] RenderManager einführen
    // [ ] Event-System für Input
    // [ ] UI-State-Manager

    // TODO: Phase 3 - Erweiterungen
    // [ ] Skills/Level-System
    // [ ] Quest-System
    // [ ] Multiplayer-Vorbereitun
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Clock timer = new Clock();

            Human human = new Human();
            human.ToString();

            // Test-Relationships hinzufügen
            human.Relationships.Add(new Relationship()
            {
                RelationWith = "Bob",
                RelationshipState = RelationshipState.Friend
            });

            human.Relationships.Add(new Relationship()
            {
                RelationWith = "Alice",
                RelationshipState = RelationshipState.Enemy
            });

            human.Relationships.Add(new Relationship()
            {
                RelationWith = "Trader",
                RelationshipState = RelationshipState.Neutral
            });

            human.RelationshipWithPlayer.RelationWith = "Player";
            human.RelationshipWithPlayer.RelationshipState = RelationshipState.Friend;

            // Loop
            while (true)
            {
                if (timer.ElapsedTime.AsSeconds() >= 1)
                {
                    human.UpdateHunger();   // oder UpdateNeeds()
                    human.DesireManager.Actions.Add(human.Decision_Component.Decide(human));
                    Console.WriteLine(human.ToString());
                    timer.Restart();
                }
            }
        }
    }
}
