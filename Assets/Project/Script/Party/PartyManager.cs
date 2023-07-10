using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

public class PartyManager : SingletonMonoBehaviour<PartyManager>
{
    public PartyEmpty EmptyPartyData;
    [SerializeField] private GetPartyFile _getPartyFile;
    private bool _getData = false;
    
    [SerializeField]
    private IPartyFormationable[] _partyList  = new IPartyFormationable[3];
    public IPartyFormationable[] PartyList => _partyList;

    public void PartySet(Character character ,int index)
    {
        _partyList[index] = character;
        Debug.Log(index);
    }
    
    public void EnptyPartySet(int index)
    {
        _partyList[index] = (IPartyFormationable)EmptyPartyData;
    }

    public void PartyRemove(int index)
    {
        _partyList[index] = (IPartyFormationable)EmptyPartyData;
    }

    public async UniTask FarstPartySet()
    {
        _partyList[0] = (IPartyFormationable)CharacterManager.Instance._getCharacters[0];
        _partyList[1] = (IPartyFormationable)EmptyPartyData;
        _partyList[2] = (IPartyFormationable)EmptyPartyData;
        Debug.Log(_partyList[0].IsCharacter);
        Debug.Log(_partyList[1].IsCharacter);
        Debug.Log(_partyList[2].IsCharacter);
        await UpdateUserData();
    }
    
    
    public async UniTask GetUserData() {
        //GetUserDataRequestのインスタンスを生成
        var request = new GetUserDataRequest();

        //ユーザー(プレイヤー)データの取得
        await UniTask.WhenAll();
        PlayFabClientAPI.GetUserData(request, OnSuccessGettingPlayerData, OnErrorGettingPlayerData);
        await UniTask.WaitUntil(() => _getData, cancellationToken: this.GetCancellationTokenOnDestroy());
        Debug.Log($"プレイヤー(ユーザー)データの取得完了");
        _getData = false;
    }
      
    //=================================================================================
    //取得結果
    //=================================================================================

    //ユーザー(プレイヤー)データの取得に成功
    private async void OnSuccessGettingPlayerData(GetUserDataResult result) {
        Debug.Log($"ユーザー(プレイヤー)データの取得に成功しました");

        //result.Data(Dictionary<string, UserDataRecord>)に全データが入っていて、Keyを文字列で指定すると値が取り出せる
        var  value = result.Data["Party"].Value;
        _getPartyFile = JsonUtility.FromJson<GetPartyFile>(value);
        await GetPartySetr();
        _getData = true;
        Debug.Log("完了");
        //Debug.Log(_getCharacterFile.input_File[0].GetCharacterName);
    }
    
    //ユーザー(プレイヤー)データの取得に失敗
    private void OnErrorGettingPlayerData(PlayFabError error) {
        Debug.LogWarning($"ユーザー(プレイヤー)データの取得に失敗しました : {error.GenerateErrorReport()}");
    }
    
    public async UniTask UpdateUserData()
    {
        int index = 0;
        foreach (var data in _partyList)
        {
            var party =  new GetParty(_partyList[index].Characternum);
            _getPartyFile.input_PartyData.Add(party);
            index++;
        }
        string json = JsonUtility.ToJson(_getPartyFile, true);
        Debug.Log(json);
        var updateData = new Dictionary<string, string>()
        {
            {"Party",json},
        };
        
        var request = new UpdateUserDataRequest {
            Data         = updateData,
            Permission   = UserDataPermission.Private //アクセス許可設定
        };
        
        PlayFabClientAPI.UpdateUserData(request, OnSuccessUpdatingPlayerData, OnErrorUpdatingPlayerData);
        await UniTask.WaitUntil(() => _getData, cancellationToken: this.GetCancellationTokenOnDestroy());
        Debug.Log($"通った");
        _getPartyFile.input_PartyData.Clear();
        _getData = false;
    }

    private void OnErrorUpdatingPlayerData(PlayFabError obj　)
    {
        Debug.LogWarning($"ユーザー(プレイヤー)データの更新に失敗しました");
    }

    private void OnSuccessUpdatingPlayerData(UpdateUserDataResult obj)
    {
        _getData = true;
        Debug.Log($"ユーザー(プレイヤー)データの更新に成功しました");
    }


    private async UniTask GetPartySetr()
    {
        int index = 0;
        foreach (var getPartyData in _getPartyFile.input_PartyData)
        {
            if (getPartyData.Characternuml == -1)
            {
                _partyList[index] = EmptyPartyData;
                Debug.Log("empty");
                index++;
            }
            else
            {
                var Data =CharacterManager.Instance._getCharacters.Find(X => X.Characternum == getPartyData.Characternuml);
                _partyList[index] = Data;
                Debug.Log(getPartyData.Characternuml);
                index++;
            }
        }
    }
    
    
    [Serializable]
    public class GetPartyFile
    {
        public List<GetParty> input_PartyData = new List<GetParty>(3);
    }

    [Serializable]
    public class GetParty
    {
       public int Characternuml;

       public GetParty(int num)
       {
           Characternuml = num;
       }
    }

    
}
