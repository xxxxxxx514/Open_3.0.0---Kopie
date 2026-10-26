using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0
{
    class TimeSystem
    {
        private float _timehour;
        public bool IsDaytime;
        public bool IsNighttime;

        private const float _timeMultyplayer = 0.7f; // Adjust this multiplier to control the speed of time progression

        public float TimeHour
        {
            get => _timehour;
            set
            {
                if (value < 0) _timehour = 0;
                else if (value > 24) _timehour = 0;
                else _timehour = value;
            }
        }
        public void Update(float deltatime)
        {
            TimeHour += deltatime * _timeMultyplayer; // Adjust the multiplier to control the speed of time progression
            if (TimeHour >= 6 && TimeHour < 18)
            {
                IsDaytime = true;
                IsNighttime = false;
            }
            else
            {
                IsDaytime = false;
                IsNighttime = true;
            }
            //Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"Time: {TimeHour:00.00}h | Daytime: {IsDaytime} | Nighttime: {IsNighttime}";
        }
    }
}
