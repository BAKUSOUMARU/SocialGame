using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CreateCharacterData")]
public class CharacterDataAsset : ScriptableObject
{
    public List<CharacterData> CharacterDatasList = new List<CharacterData>();
}

[System.Serializable]
public class CharacterData
{
    public Status _status = new Status();
    public Sprite _characterSprite;

}
