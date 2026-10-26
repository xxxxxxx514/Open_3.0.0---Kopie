using Open_0._0._0.Combat;
namespace Open_0._0._0.Quests
{
    abstract class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Reward? Reward { get; set; }

        public Quest(string title = "Unkown", string description = "Unkown", Reward? reward = null)
        {
            Title = title;
            Description = description;
            Reward = reward;
        }

        public abstract bool IsCompleted();
        public void SetTitle(string title)
        {
            Title = title;
        }
        public QuestType GetType()
        {
            if (this is KillQuest)
                return QuestType.Kill_Quest;
            return QuestType.None;
        }
    }
}
