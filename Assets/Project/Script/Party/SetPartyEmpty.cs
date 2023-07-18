using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SetPartyEmpty : MonoBehaviour
{
    [SerializeField] private GameObject _gestGameObject;
    
    [SerializeField]
    private int _index;
    
    public void IndexSet(int i,GameObject gestGameObject)
    {
        _gestGameObject = gestGameObject;
        _index = i;
    }
    public void Click()
    {
        _gestGameObject.SetActive(false);
        PartyManager.Instance.PartyRemove(_index);
        PartyUIUpdater.Instance.PartymemberUITrue();
    }
}
