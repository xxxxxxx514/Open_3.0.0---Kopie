using Open_0._0._0.Core;
using Open_0._0._0.Factories;
using Open_0._0._0.Quests;
using Open_0._0._0.Utils;
using Open_0._0._0.World;
namespace Open_0._0._0
{
//Visuelles Combat Feedback — Hit-Flash, Schadensanzeige
//Knockback einbauen
//Stamina System
//Tag/Nacht beeinflusst NPCs — schlafen nachts

//Später 📋

//Shop System
//NPC nimmt selbst Quests an
//NPC levelt auf
//NPCs interagieren miteinander
//Pathfinding
//Sounds
//Speichersystem

    internal class Program
    {
        static void Main(string[] args)
        {
          //  MapDesigner mapDesigner = new MapDesigner();
            //mapDesigner.Run();
            Game game = new Game();
            KeyBoard_Input.OnKeyPressed += (key) => Console.WriteLine("Key gedrückt" + key);
            KeyBoard_Input.OnKeyReleased += (key) => Console.WriteLine("Key losgelasen" + key);

            QuestFactory questFactory = new QuestFactory();
            Quest quest = questFactory.CreateSmallQuest();
            Console.WriteLine(quest.ToString());
        }
    }
}
