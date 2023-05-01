using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour,IBattler
{
    [SerializeField] private Status _status = new Status();
    public Status Status => _status;

    private void Awake()
    {
        Status.Initialize();
    }

    void IBattler.Dead()
    {
        Destroy(gameObject);
    }
}
