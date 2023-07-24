using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParty : MonoBehaviour
{
    [SerializeField] private GameObject _hostList;
    [SerializeField] private GameObject _gestList;
    
    private Character _character;

    private int index;

    public void CharacterSet(Character character)
    {
        _character = character;
    }
    
    public void IndexSet(int i , GameObject hostlist,GameObject gestlist)
    {
        _hostList = hostlist;
        _gestList = gestlist;
        index = i;
    }
    public void Click()
    {
        if (index == 0)
        {
            _hostList.SetActive(false);
        }
        else
        {
            _gestList.SetActive(false);
        }
        PartyManager.Instance.PartySet(_character, index);
        PartyUIUpdater.Instance.PartymemberUITrue();
    }
}
