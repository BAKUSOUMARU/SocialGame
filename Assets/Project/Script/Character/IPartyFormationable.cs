
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPartyFormationable
{
    int Characternum { get; }
    
    Sprite Icon { get; }
    
    bool IsCharacter{ get; }
}
