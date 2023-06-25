using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PartyButtonSetter : MonoBehaviour
{
    [SerializeField] private GameObject _partyButton;

    [SerializeField] private GameObject _emptyButton;

    [SerializeField] private GameObject _gestrootObject;
    
    [SerializeField] private GameObject _hostrootObject;

    private List<GameObject> _buttonList  = new List<GameObject>();

    public void CharacterSelectOn(int Partyindex){
        int index = 0;
        if (_gestrootObject.activeInHierarchy)
        {
            var obj = Instantiate(_emptyButton, _gestrootObject.transform);
            obj.GetComponent<SetPartyEmpty>().IndexSet(Partyindex);
            obj.GetComponent<Image>().sprite = PartyManager.Instance.EmptyPartyData.Icon;
            _buttonList.Add(obj);
            index++;
        }
        foreach (var character in CharacterManager.Instance._getCharacters)
        {
            var obj = Instantiate(_partyButton, _gestrootObject.transform);
            obj.GetComponent<SetParty>().IndexSet(Partyindex);
            obj.GetComponent<Image>().sprite = CharacterManager.Instance._getCharacters[index].CharacterSprite;
            obj.GetComponent<SetParty>().CharacterSet(character);
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
