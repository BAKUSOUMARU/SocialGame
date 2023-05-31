using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PartyButtonSetter : MonoBehaviour
{
    [SerializeField] private GameObject _partyButton;

    [SerializeField] private GameObject _rootObject;

    private List<GameObject> _buttonList  = new List<GameObject>();

    private void OnEnable()
    {
        int index = 0;
        foreach (var character in CharacterManager.Instance._getCharacters)
        {
            var obj = Instantiate(_partyButton, _rootObject.transform);
            obj.GetComponent<Image>().sprite = CharacterManager.Instance._getCharacters[index].CharacterSprite;
            obj.GetComponent<SerParty>().Set(character);
            _buttonList.Add(obj);
            index++;
        }
    }

    private void OnDisable()
    {
        foreach (var obj in _buttonList)
        {
            Destroy(obj);
        }
    }
}
