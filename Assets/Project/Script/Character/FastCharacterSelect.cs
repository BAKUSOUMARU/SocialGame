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
        var data =CharacterManager.Instance._characterDataAsset.CharacterDatasList.Find(X => X._status.Name == _selectCharacterName);
        var character = new Character(
            data._characterSprite,
            data._status.Name,
            data._status.Level,
            data._status.MaxHP,
            data._status.Atk,
            data._status.Speed,
            data._status.Lucky);
        CharacterManager.Instance.AddGetCharacter(character);
        await CharacterManager.Instance.UpdateUserDate();
        SceneChanger.ChangeScene("HomeScene");
    }
}
