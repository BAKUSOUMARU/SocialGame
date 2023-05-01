using System;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayFabcustomidLogin : MonoBehaviour
{
    [SerializeField]
    private GameObject _accountnameSetUI;

    [SerializeField] 
    private TMP_Text _text;

    [SerializeField] 
    private string sceneChangeName ="HomeScene";
    
    private string _id;
    bool _isAccountCreate;
    
    public async void Set()
    {
        if (TestForNullOrEmpty(SaveDataManager.Instance.userData.id))
        {
            _isAccountCreate = true;
            _id = CreateID().ToString();
            await CheckAccountExistence(_id);
            
        }
        else
        {
            _isAccountCreate = false;
            _id = SaveDataManager.Instance.userData.id;
            await CustomIDLogin();
        }
        
    }

    async UniTask CustomIDLogin()
    {
        PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest {
                CustomId = _id, 
                CreateAccount = _isAccountCreate, 
            }, async result => {
                Debug.Log("ログイン成功");
                SaveDataManager.Instance.userData.playFabId = result.PlayFabId;
                await SaveDataManager.Instance.SaveDataAsync();
                if (_isAccountCreate)
                {
                    UiUP();
                    return;
                }
                SceneChanger.ChangeScene(sceneChangeName);
                _text.text = "すでにアカウントが作られているためログインが完了しました";
            },
            error => {
                // 失敗時の処理
                _text.text = "アカウント作成に失敗しました";
            }
        );
    }

    public async UniTask CheckAccountExistence(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = false,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnAccountExistenceCheckSuccess, OnAccountExistenceCheckFailure);
    }

    
    private async void OnAccountExistenceCheckSuccess(LoginResult result)
    {
        //アカウントが存在する場合の処理を書く
        _id = CreateID().ToString();
        await CheckAccountExistence(_id);

    }
    private async void OnAccountExistenceCheckFailure(PlayFabError error)
    {
        SaveDataManager.Instance.userData.id = _id;
        await CustomIDLogin();
        Debug.Log("Check完了");
    }
    
     int CreateID()
    {
        int min = 100000000;
        int max = 999999999;
        int randomNumber = Random.Range(min, max + 1);

        return randomNumber;
    }
     
     bool TestForNullOrEmpty(string s)
     {
         bool result;
         result = s == null || s == string.Empty;
         return result;
     }

     void UiUP()
     {
         _accountnameSetUI.SetActive(true);
         this.gameObject.SetActive(false);
     }
     
     /// <summary>
     /// メアドとパスワードでログインした場合のcustomIDをセーブデータmanagerに入れる関数
     /// (saveは別途してください)
     /// </summary>
     private void GetCustomID()
     {
         var request = new GetAccountInfoRequest();

         PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailure);
     }

     // 取得成功時のコールバックメソッド
     private void OnGetAccountInfoSuccess(GetAccountInfoResult result)
     {
         SaveDataManager.Instance.userData.id = result.AccountInfo.CustomIdInfo.CustomId;
         SaveDataManager.Instance.userData.accountName = result.AccountInfo.TitleInfo.DisplayName;
     }

     // 取得失敗時のコールバックメソッド
     private void OnGetAccountInfoFailure(PlayFabError error)
     {
         Debug.LogError("Failed to get custom ID: " + error.ErrorMessage);
     }
     
}
