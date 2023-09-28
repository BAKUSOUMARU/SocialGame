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
    
    public Character(int characterid, Sprite characterSprite, Sprite characterIconSprite, string englishname,string Japanesename,int level, int hp, int atk,int speed, int lucky )
    {
        this._characterID = characterid;
        this._characterSprite = characterSprite;
        this._characterIconSprite = characterIconSprite;
        this._status.Set(level,hp,atk,speed,lucky,englishname, Japanesename);
    }
    public Character(GetChatacter _getChatacter ,CharacterData data){
        this._characterID = _getChatacter.CharacterID;
        this._characterSprite = data._characterSprite;
        this._characterIconSprite = data._charactericonSprite;
        this._status.Set(data._status.Level,data._status.MaxHP,data._status.Atk,data._status.Speed,data._status.Lucky,data._status.Englishname ,data._status.JapaneseName);
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
