using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyPartyDataAsset", menuName = "ScriptableObjects/EmptyPartyDataAsset")]
public class EmptyCharacterData : ScriptableObject
{
    public PartyEmpty PartyEmpty;
}

[System.Serializable]
public class PartyEmpty:IPartyFormationable
{
    [SerializeField] private int _characternum = -1;
    public int Characternum => _characternum;
    
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    private bool isCharacter = false;
    public bool IsCharacter => isCharacter;
}
