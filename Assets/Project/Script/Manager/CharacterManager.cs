using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{
    public GetCharacterFile _getCharacterFile;
    
    [SerializeField] private Image _image;

    public List<Character> _getCharacters = new List<Character>(); 
    
    public CharacterDataAsset _characterDataAsset;

    private Sprite Test;
    
    
    private void Start()
    {
        // Test = _characterDataAsset.CharacterDatasList[1]._characterSprite;
        //
        // _image.sprite = Test;    
        //
        // Debug.Log(_characterDataAsset.CharacterDatasList[0]._status.Name);
        
        //GetUserData();
        //UpdateUserDate();
    }


    #region PlayFab

    #region GetUderData

    private bool GetData = false;
    /// <summary>
    /// ユーザー(プレイヤー)データの取得
    /// </summary>
    public async UniTask GetUserData() {
        //GetUserDataRequestのインスタンスを生成
        var request = new GetUserDataRequest();

        //ユーザー(プレイヤー)データの取得
        await UniTask.WhenAll();
        PlayFabClientAPI.GetUserData(request, OnSuccessGettingPlayerData, OnErrorGettingPlayerData);
        await UniTask.WaitUntil(() => GetData, cancellationToken: this.GetCancellationTokenOnDestroy());
        Debug.Log($"プレイヤー(ユーザー)データの取得完了");
        GetData = false;
    }
      
    //=================================================================================
    //取得結果
    //=================================================================================

    //ユーザー(プレイヤー)データの取得に成功
    private async void OnSuccessGettingPlayerData(GetUserDataResult result) {
        Debug.Log($"ユーザー(プレイヤー)データの取得に成功しました");

        //result.Data(Dictionary<string, UserDataRecord>)に全データが入っていて、Keyを文字列で指定すると値が取り出せる
        var  value = result.Data["Character"].Value;
        _getCharacterFile = JsonUtility.FromJson<GetCharacterFile>(value);
        await GetCharactersSetr();
        GetData = true;
        Debug.Log("完了");
        //Debug.Log(_getCharacterFile.input_File[0].GetCharacterName);
    }
    
    //ユーザー(プレイヤー)データの取得に失敗
    private void OnErrorGettingPlayerData(PlayFabError error) {
        Debug.LogWarning($"ユーザー(プレイヤー)データの取得に失敗しました : {error.GenerateErrorReport()}");
    }

    #endregion

    #region UpdateUserData
    
    public async UniTask UpdateUserDate()
    {
        string json = JsonUtility.ToJson(_getCharacterFile, true);
        Debug.Log(json);
        var updateData = new Dictionary<string, string>()
        {
            {"Character",json}
        };
        
        var request = new UpdateUserDataRequest {
            Data         = updateData,
            Permission   = UserDataPermission.Private //アクセス許可設定
        };
        
        PlayFabClientAPI.UpdateUserData(request, OnSuccessUpdatingPlayerData, OnErrorUpdatingPlayerData);
        await UniTask.WaitUntil(() => GetData, cancellationToken: this.GetCancellationTokenOnDestroy());
        Debug.Log($"通った");
        GetData = false;
    }

    private void OnErrorUpdatingPlayerData(PlayFabError obj　)
    {
        Debug.LogWarning($"ユーザー(プレイヤー)データの更新に失敗しました");
    }

    private void OnSuccessUpdatingPlayerData(UpdateUserDataResult obj)
    {
        GetData = true;
        Debug.Log($"ユーザー(プレイヤー)データの更新に成功しました");
    }
    

    #endregion
    
    #endregion


    #region PrivateMethods

    private async UniTask GetCharactersSetr()
    {
        foreach (var getChatacterData in _getCharacterFile.input_File)
        {
            var Data =_characterDataAsset.CharacterDatasList.Find(X => X._status.Name == getChatacterData.GetCharacterName);

            var character = new Character(
                                Data._characterSprite,
                                Data._status.Name,
                                getChatacterData.CharacterLevel,
                                Data._status.MaxHP,
                                Data._status.Atk,
                                Data._status.Speed,
                                Data._status.Lucky);
            
            _getCharacters.Add(character);
        }
    }
    #endregion

    #region PublicMethods

    public void AddGetCharacter(Character getCharacter)
    {
        _getCharacters.Add(getCharacter);
        var data = new GetChatacter(getCharacter.Status.Name,getCharacter.Status.Level);
        _getCharacterFile.input_File.Add(data);
    }
    
    #endregion
    
}

#region Data

[Serializable]
public class GetCharacterFile
{
    public List<GetChatacter> input_File;
}

[Serializable]
public class GetChatacter
{
    public string GetCharacterName;

    public int CharacterLevel;

    public GetChatacter(string name, int level)
    {
        this.GetCharacterName = name;
        this.CharacterLevel = level;
    }
}
#endregion
