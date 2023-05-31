using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerParty : MonoBehaviour
{
    [SerializeField] private IPartyFormationable _character;

    private bool IsParty = false;
    
    public void Set(Character character)
    {
        _character = character;
    }

    public void Click()
    {
        if (IsParty)
        {
            //PartyManager.Instance.PartyRemove(_character);
            IsParty = false;
        }
        else if(!IsParty)
        {
            //PartyManager.Instance.PartySet(_character);
            IsParty = true;
        }
    }
}
