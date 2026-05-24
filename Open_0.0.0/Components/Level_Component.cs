using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Components
{
     class Level_Component
    {
        public int Experience = 0;
        public int ExperienceToNextLevel = 100;
        public int Level { get; set; } = 1;

        public void AddExperience(int xp)
        {
            Experience += xp;
            Console.WriteLine($"+{xp} XP erhalten. Total: {Experience}/{ExperienceToNextLevel}");

            // HIER Level prüfen
            LevelSystem();
        }
        private void LevelSystem()
        {
            while (Experience >= ExperienceToNextLevel)
            {
                Level++;
                Experience -= ExperienceToNextLevel;
                ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5f);

                Console.WriteLine($"Level Up! Neues Level: {Level}");
                Console.WriteLine($"Nächstes Level bei: {ExperienceToNextLevel} XP");
            }
        }
    }
}
