using Open_0._0._0.Actors;

namespace Open_0._0._0.Actions
{
    interface IEatable
    {
        int EatAmount { get; set; }
        void Eat(Actor actor);
    }
}
