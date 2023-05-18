using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : IBattler
{
    [SerializeField]
    private Status _status = new Status();

    [SerializeField]
    private Sprite _characterSprite;

    public Sprite CharacterSprite => _characterSprite;
    
    public Status Status => _status;

    private void Awake()
    {
    }

    void IBattler.Dead()
    {
        //Destroy(gameObject);
    }
    
    public void Initialize()
    {
        Status.Initialize();
    }

    public void Set(Sprite characterSprite,string name,int level, int hp, int atk,int speed, int lucky )
    {
        _characterSprite = characterSprite;
        _status.Set(level,hp,atk,speed,lucky,name);
    }
}
