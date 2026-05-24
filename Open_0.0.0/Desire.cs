using Open_0._0._0.Actions;
using SFML.System;

namespace Open_0._0._0
{
    class Desire
    {
        private int _hunger = 0;
        private int _thirst = 0;
        private int _happiness = 0;

        private const int _hungerReq = 75;
        private const int _thirstReq = 75;
        private const int _happinessReq = 85;
        public DesireState State { get; set; }
        private Clock _clock=new Clock();

        public Desire()
        {
            State = DesireState.None;
        }
        public int Hunger
        {
            get => _hunger;
            set
            {
                if (value < 0) _hunger = 0;
                else if (value > 100) _hunger = 100;
                else _hunger = value;
            }
        }

        public int Thirst
        {
            get => _thirst;
            set
            {
                if (value < 0) _thirst = 0;
                else if (value > 100) _thirst = 100;
                else _thirst = value;
            }
        }

        public int Happines
        {
            get => _happiness;
            set
            {
                if (value < 0) _happiness = 0;
                else if (value > 100) _happiness = 100;
                else _happiness = value;
            }
        }
        public void Update(DesireManager manager)
        {
            DeterminateDesireStates();
            if (_clock.ElapsedTime.AsSeconds() >= 1)
            {
                DeterminateHappiness();
                _clock.Restart();
            }
            if (State.HasFlag(DesireState.HungerHigh))
                manager.AddAction(new EatAction());
            else
                manager.RemoveAction<EatAction>();

            if (State.HasFlag(DesireState.ThirstHigh))
                manager.AddAction(new DrinkAction());
            else
                manager.RemoveAction<DrinkAction>();

        }
        public void DrainNeeds(int hungerDrain, int thirstDrain)
        {
            Hunger += hungerDrain;
            Thirst += thirstDrain;
        }
        public void RestoreNeeds(int hungerAmount = 0, int thirstAmount = 0)
        {
            Hunger -= hungerAmount;
            Thirst -= thirstAmount;
        }
        public bool IsStarving() => Hunger >= 100 || Thirst >= 100;



        public void DeterminateDesireStates()
        {
            if (Hunger > _hungerReq)
            {
                State |= DesireState.HungerHigh;
            }
            else
            {
                if (State.HasFlag(DesireState.HungerHigh))
                {
                    State &= ~DesireState.HungerHigh;
                }
            }
            if (Thirst > _thirstReq)
            {
                State |= DesireState.ThirstHigh;
            }
            else
            {
                if (State.HasFlag(DesireState.ThirstHigh))
                {
                    State &= ~DesireState.ThirstHigh;
                }
            }
                if (Happines > _happinessReq)
            {
                State |= DesireState.Unhappy;
            }
            else
            {
                if (State.HasFlag(DesireState.Unhappy))
                {
                    State &= ~DesireState.Unhappy;
                }
            }
        }
        private void DeterminateHappiness()
        {
            int needScore = (Hunger + Thirst) / 2;

            if (needScore >= 80)
            {
                // Sehr gute Bedürfnisse → sehr glücklich
                Happines += 2;
            }
            else if (needScore >= 50)
            {
                // Gute Bedürfnisse → leicht glücklich
                Happines += 1;
            }
            else if (needScore >= 20)
            {
                // Schlechte Bedürfnisse → unglücklich
                Happines -= 1;
            }
            else
            {
                // Sehr schlechte Bedürfnisse → stark unglücklich
                Happines -= 2;
            }
        }
    }
}