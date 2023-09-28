using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FastCharacterSelect : MonoBehaviour
{
    
    [SerializeField]
    private string _selectCharacterName;
    
    public async void SelectCharacter()
    {
        CharacterManager.Instance.CharacterIDReset();
        var data =CharacterManager.Instance._characterDataAsset.CharacterDatasList.Find(X => X._status.Englishname == _selectCharacterName);
        var character = new Character(
            CharacterManager.Instance.CharacterID,
            data._characterSprite,
            data._charactericonSprite,
            data._status.Englishname,
            data._status.JapaneseName,
            data._status.Level,
            data._status.MaxHP,
            data._status.Atk,
            data._status.Speed,
            data._status.Lucky);
        CharacterManager.Instance.AddGetCharacter(character);
        await CharacterManager.Instance.UpdateUserDate();
        await PartyManager.Instance.FarstPartySet();
        SceneChanger.ChangeScene("HomeScene");
    }
}
