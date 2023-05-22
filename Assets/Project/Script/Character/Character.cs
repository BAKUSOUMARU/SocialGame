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

    public Character(Sprite characterSprite,string name,int level, int hp, int atk,int speed, int lucky )
    {
        this._characterSprite = characterSprite;
        this._status.Set(level,hp,atk,speed,lucky,name);
    }
    
    void IBattler.Dead()
    {
        //Destroy(gameObject);
    }
    
    public void Initialize()
    {
        Status.Initialize();
    }
}
