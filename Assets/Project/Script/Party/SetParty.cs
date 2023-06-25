using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParty : MonoBehaviour
{
    [SerializeField] private IPartyFormationable _character;

    private int index;

    private bool IsParty = false;
    
    public void CharacterSet(Character character)
    {
        _character = character;
    }
    
    public void IndexSet(int i)
    {
        index = i;
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
