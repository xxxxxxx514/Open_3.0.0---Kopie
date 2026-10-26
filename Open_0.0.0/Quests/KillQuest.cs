using Open_0._0._0.Enums;

namespace Open_0._0._0.Quests
{
    internal class KillQuest:Quest
    {
        public int required_Kills;
        public int current_Kills=0;

        public MonsterType target_Type;

        public KillQuest(int req_Kills, MonsterType Type):base()
        {
            required_Kills=req_Kills;
            target_Type=Type;
        }

        public override bool IsCompleted()=> current_Kills>=required_Kills;
        public override string ToString()
        {
            return $"[KILL QUEST]\n" +
                   $"- Title: {Title}\n" +
                   $"- Description: {Description}\n" +
                   $"- Target: {target_Type}\n" +
                   $"- Progress: {current_Kills}/{required_Kills}\n" +
                   $"- Reward: {(Reward != null ? Reward : "None")}";
        }
        public void IncreaseKills(int kills=1)
        {
            current_Kills += kills;
        }
         

    }
}
