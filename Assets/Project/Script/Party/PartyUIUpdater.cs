using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIUpdater : SingletonMonoBehaviour<PartyUIUpdater>
{
    [SerializeField] private GameObject _Partymember;
    
    private async void OnDisable()
    {
        await PartyManager.Instance.UpdateUserData();
    }

    public void PartymemberUITrue()
    {
        _Partymember.SetActive(true);
    }
    
    public void PartymemberUIfalse()
    {
        _Partymember.SetActive(false);
    }
}
