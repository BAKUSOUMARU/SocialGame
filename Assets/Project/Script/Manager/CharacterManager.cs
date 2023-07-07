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
    
    //[SerializeField] private Image _image;

    public int CharacterID;

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

    private bool _getData = false;
    /// <summary>
    /// ユーザー(プレイヤー)データの取得
    /// </summary>
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
        var  value = result.Data["Character"].Value;
        CharacterID = int.Parse(result.Data["CharacterID"].Value);
        _getCharacterFile = JsonUtility.FromJson<GetCharacterFile>(value);
        await GetCharactersSetr();
        _getData = true;
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
        string id = CharacterID.ToString();
        string json = JsonUtility.ToJson(_getCharacterFile, true);
        Debug.Log(json);
        var updateData = new Dictionary<string, string>()
        {
            {"Character",json},
            {"CharacterID",id}
        };
        
        var request = new UpdateUserDataRequest {
            Data         = updateData,
            Permission   = UserDataPermission.Private //アクセス許可設定
        };
        
        PlayFabClientAPI.UpdateUserData(request, OnSuccessUpdatingPlayerData, OnErrorUpdatingPlayerData);
        await UniTask.WaitUntil(() => _getData, cancellationToken: this.GetCancellationTokenOnDestroy());
        Debug.Log($"通った");
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
    

    #endregion
    
    #endregion


    #region PrivateMethods

    private async UniTask GetCharactersSetr()
    {
        foreach (var getChatacterData in _getCharacterFile.input_File)
        {
            var Data =_characterDataAsset.CharacterDatasList.Find(X => X._status.Name == getChatacterData.GetCharacterName);

            var character = new Character(
                                getChatacterData.CharacterID,
                                Data._characterSprite,
                                Data._charactericonSprite,
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
        var data = new GetChatacter(CharacterID,getCharacter.Status.Name,getCharacter.Status.Level);
        _getCharacterFile.input_File.Add(data);
        CharacterID++;
    }

    public void CharacterIDReset()
    {
        CharacterID = 0;
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
    public int CharacterID;
    
    public string GetCharacterName;

    public int CharacterLevel;


    public GetChatacter(int id, string name, int level)
    {
        this.CharacterID = id;
        this.GetCharacterName = name;
        this.CharacterLevel = level;
    }
}
#endregion
