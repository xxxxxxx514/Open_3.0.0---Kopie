using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.Enums
{
     enum NPCState : byte
    {
        Idle,        // Steht rum
        Wandering,   // Wandert umher
        Interacting, // Interagiert mit Spieler
        Trading,     // Handelt
        Sleeping,    // Schläft
        Working,     // Arbeitet
        Following,   // Folgt Spieler
        Fleeing      // Rennt weg (z.B. bei Gefahr)
    }
}