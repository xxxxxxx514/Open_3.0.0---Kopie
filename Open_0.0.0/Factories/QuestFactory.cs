using Open_0._0._0.Combat;
using Open_0._0._0.Enums;
using Open_0._0._0.Quests;

namespace Open_0._0._0.Factories
{
    class QuestFactory
    {
        private Random _random = new Random();

        public Quest CreateSmallQuest()
        {
            int targetMonstertype = _random.Next(1, Enum.GetValues(typeof(MonsterType)).Length);//anazhl der Enumtypen
            MonsterType targetMonster = (MonsterType)targetMonstertype;

            int killReq = _random.Next(5, 11);

            KillQuest killQuest = new KillQuest(killReq, targetMonster);

            Reward reward = new Reward();
            reward.XP_Amount = 3;

            killQuest.Reward=reward;

            return killQuest;

        }
        public Quest CreateMediumQuest()
        {
            int targetMonstertype = _random.Next(1, Enum.GetValues(typeof(MonsterType)).Length);//anazhl der Enumtypen
            MonsterType targetMonster = (MonsterType)targetMonstertype;

            int killReq = _random.Next(11, 25);

            KillQuest killQuest = new KillQuest(killReq, targetMonster);

            Reward reward = new Reward();
            reward.XP_Amount = 100;

            killQuest.Reward = reward;

            return killQuest;

        }
    }
}
