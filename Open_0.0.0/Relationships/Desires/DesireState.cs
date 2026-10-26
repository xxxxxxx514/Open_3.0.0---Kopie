namespace Open_0._0._0.Relationships.Desires
{

    enum DesireState : byte
    {
        None = 0,           // Explizit 0
        HungerHigh = 1,     // 2⁰ = 1
        ThirstHigh = 2,     // 2¹ = 2
        Unhappy = 4,
        Happy = 8         // 2³ = 8
    }
}
