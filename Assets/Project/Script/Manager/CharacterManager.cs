using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private GetCharacterFile _getCharacterFile;
    
    [SerializeField] private Image _image;

    public List<Character> _getCharacters; 
    
    public CharacterDataAsset _characterDataAsset;

    private Sprite Test;
    
    
    
    
    private void Start()
    {
        // Test = _characterDataAsset.CharacterDatasList[1]._characterSprite;
        //
        // _image.sprite = Test;    
        //
        // Debug.Log(_characterDataAsset.CharacterDatasList[0]._status.Name);
        
        GetUserData();
        //UpdateUserDate();
    }
    
    /// <summary>
    /// ユーザー(プレイヤー)データの取得
    /// </summary>
    public void GetUserData() {
        //GetUserDataRequestのインスタンスを生成
        var request = new GetUserDataRequest();

        //ユーザー(プレイヤー)データの取得
        PlayFabClientAPI.GetUserData(request, OnSuccessGettingPlayerData, OnErrorGettingPlayerData);
        Debug.Log($"プレイヤー(ユーザー)データの取得開始");
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
        
        Debug.Log("完了");
        //Debug.Log(_getCharacterFile.input_File[0].GetCharacterName);
    }

    //ユーザー(プレイヤー)データの取得に失敗
    private void OnErrorGettingPlayerData(PlayFabError error) {
        Debug.LogWarning($"ユーザー(プレイヤー)データの取得に失敗しました : {error.GenerateErrorReport()}");
    }

    public void UpdateUserDate()
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
        Debug.Log($"プレイヤー(ユーザー)データの更新開始");
    }

    private void OnErrorUpdatingPlayerData(PlayFabError obj)
    {
        Debug.LogWarning($"ユーザー(プレイヤー)データの更新に失敗しました");
    }

    private void OnSuccessUpdatingPlayerData(UpdateUserDataResult obj)
    {
        Debug.Log($"ユーザー(プレイヤー)データの更新に成功しました");
        
    }


    async UniTask GetCharactersSetr()
    {
        foreach (var getChatacterData in _getCharacterFile.input_File)
        {
            var Data =_characterDataAsset.CharacterDatasList.Find(X => X._status.Name == getChatacterData.GetCharacterName);

            var character = new Character();
            
            character.Set(
                        Data._characterSprite,
                        Data._status.Name,
                        getChatacterData.CharacterLevel,
                        Data._status.Hp,
                        Data._status.Atk,
                        Data._status.Speed,
                        Data._status.Lucky);
            
            _getCharacters.Add(character);
        }
    }
}


[Serializable]
public class GetCharacterFile
{
    public GetChatacter[] input_File;
}

[Serializable]
public class GetChatacter
{
    public string GetCharacterName;

    public int CharacterLevel;
}
