using Open_0._0._0.Actors;
using SFML.System;
using System.Numerics;

namespace Open_0._0._0
{
    internal class Monster_Factory
    {
        private Random random = new Random();
        private MonsterType Enemy;
        public Monster_Factory()
        {
        }
        public Enemy GetRandomMonster()
        {
            int randomMonster = random.Next(0, Enum.GetValues(typeof(MonsterType)).Length);//anazhl der Enumtypen
            MonsterType monsterType = (MonsterType)randomMonster;
            return CreateMonsterByType(monsterType);
        }
        public Enemy GetRandomMonster(int gridX, int gridY)
        {
            int randomMonster = random.Next(1, Enum.GetValues(typeof(MonsterType)).Length);//anazhl der Enumtypen
            MonsterType monsterType = (MonsterType)randomMonster;
            Enemy enemy = CreateMonsterByType(monsterType);

            enemy.Position_Component.SetPosition(enemy.Position_Component.ToPixelPosition(gridX, gridY));
            return enemy;
        }
        private Enemy CreateMonsterByType(MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.Goblin:
                    {
                        return new Goblin();
                    }
                case MonsterType.Slime:
                    {
                        return new Slime();
                    }
                case MonsterType.Oark:
                    {
                        return new Oark();
                    }
            }
            Console.WriteLine("Warnung kein Valider MonsterTyp Null zurückgegeben");
            return null;
        }

    }
}