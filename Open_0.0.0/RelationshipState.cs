namespace Open_0._0._0
{
     enum RelationshipState : byte
    {
        Stranger,       // Fremder - keine Beziehung
        Acquaintance,   // Bekanntschaft - erstes Gespräch
        Friendly,       // Freundlich - mag Spieler
        Friend,         // Freund - guter Draht
        CloseFriend,    // Enger Freund - vertraut
        Ally,           // Verbündeter - kämpft für Spieler
        Love,           // Verliebt - romantisches Interesse
        Disliked,       // Unbeliebt - mag Spieler nicht
        Hated,          // Gehasst - sehr negativ
        Enemy,          // Feind - greift an
        Neutral         // Neutral - keine Meinung
    }
}