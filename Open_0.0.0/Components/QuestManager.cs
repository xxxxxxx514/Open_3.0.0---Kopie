using Open_0._0._0.Core;
using Open_0._0._0.Factories;
using Open_0._0._0.Quests;
namespace Open_0._0._0.Components
{
    class QuestManager
    {
        public Quest? Quest { get; set; }
        public QuestFactory QuestFactory { get; set; }=new QuestFactory();
        public QuestManager()
        {
            Quest = QuestFactory.CreateSmallQuest();
        }

        public void Update()
        {
            if(IsQuestComplete())
            {
                GiveQuestRewards();
            }
        }
        public bool IsQuestComplete()
        {
            if (Quest is KillQuest quest)
            {
                return quest.IsCompleted();
            }
            else { return false; }
        }
        public void GiveQuestRewards()
        {
            var player = GameWorld.Player;
            if (Quest != null)
            {
                player.Level_Component.AddExperience(Quest.Reward.XP_Amount);
                player.Inventory.TryAddItem(Quest.Reward.Item);
                //gold

                Quest=null;
            }
        }
    }
}
