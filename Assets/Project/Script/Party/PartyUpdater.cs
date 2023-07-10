using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUpdater : MonoBehaviour
{
    private async void OnDisable()
    {
        await PartyManager.Instance.UpdateUserData();
    }
}
