using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : SingletonMonoBehaviour<PartyManager>
{
    public PartyEmpty EmptyPartyData;
    [SerializeField]private IPartyFormationable[] _partyList  = new IPartyFormationable[3];
    public IPartyFormationable[] PartyList => _partyList;


    // private void Start()
    // {
    //     _character = En as Character;
    // }

    public void PartySet(Character character ,int index)
    {
        _partyList[index] = character;
    }
    
    public void EnptyPartySet(int index)
    {
        _partyList[index] = (IPartyFormationable)EmptyPartyData;
    }

    public void PartyRemove(int index)
    {
        _partyList[index] = (IPartyFormationable)EmptyPartyData;
    }
    
    void Update()
    {
        
    }
}
