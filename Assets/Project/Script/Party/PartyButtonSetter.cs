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

    public void GestCharacterSelectOn(int Partyindex){
        int index = 0;
        _gestrootObject.SetActive(true);
        var en = Instantiate(_emptyButton, _gestrootObject.transform);
            en.GetComponent<SetPartyEmpty>().IndexSet(Partyindex,_gestrootObject);
            en.GetComponent<Image>().sprite = PartyManager.Instance.EmptyPartyData.Icon;
            _buttonList.Add(en);
        foreach (var character in CharacterManager.Instance._getCharacters)
        {
            if (character.Characternum == PartyManager.Instance.PartyList[0].Characternum)
            {
                index++;
            }
            else if (character.Characternum == PartyManager.Instance.PartyList[1].Characternum)
            {
                index++;
            }
            else if (character.Characternum == PartyManager.Instance.PartyList[2].Characternum)
            {
                index++;
            }
            else
            {
                var obj = Instantiate(_partyButton, _gestrootObject.transform);
                obj.GetComponent<SetParty>().IndexSet(Partyindex,_hostrootObject,_gestrootObject);
                obj.GetComponent<Image>().sprite = CharacterManager.Instance._getCharacters[index].Icon;
                obj.GetComponent<SetParty>().CharacterSet(character);
                _buttonList.Add(obj);
                index++;
            }
        }
    }
    
    public void HostCharacterSelectOn(int Partyindex){
        int index = 0;
        _hostrootObject.SetActive(true);
        foreach (var character in CharacterManager.Instance._getCharacters)
        {
            if (character.Characternum == PartyManager.Instance.PartyList[0].Characternum)
            {
                index++;
            }
            else if (character.Characternum == PartyManager.Instance.PartyList[1].Characternum)
            {
                index++;
            }
            else if (character.Characternum == PartyManager.Instance.PartyList[2].Characternum)
            {
                index++;
            }
            else
            {
                var obj = Instantiate(_partyButton, _hostrootObject.transform);
                obj.GetComponent<SetParty>().IndexSet(Partyindex,_hostrootObject,_gestrootObject);
                obj.GetComponent<Image>().sprite = CharacterManager.Instance._getCharacters[index].Icon;
                obj.GetComponent<SetParty>().CharacterSet(character);
                _buttonList.Add(obj);
                index++;
            }

        }
        if (_buttonList.Count == 0)
        {
            _hostrootObject.SetActive(false);
            Debug.Log("メンバーがいないよ");
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
