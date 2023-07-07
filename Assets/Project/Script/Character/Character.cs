using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character : IBattler,IPartyFormationable
{
    [SerializeField] private int _characterID;

    [SerializeField]
    private Status _status = new Status();

    [SerializeField]
    private Sprite _characterSprite;

    [SerializeField] 
    private Sprite _characterIconSprite;

    public int  Characternum => _characterID;
    
    public Status Status => _status;

    public Sprite CharacterSprite => _characterSprite;

    public Sprite Icon => _characterIconSprite;

    private bool isCharacter = true;
    public bool IsCharacter => isCharacter;
    
    public Character(int characterid, Sprite characterSprite, Sprite characterIconSprite, string name,int level, int hp, int atk,int speed, int lucky )
    {
        this._characterID = characterid;
        this._characterSprite = characterSprite;
        this._characterIconSprite = characterIconSprite;
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
