using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayFabLogin : MonoBehaviour
{
    private string _id;

    void  Start()
    {
              
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public async void Set()
    {
        if (TestForNullOrEmpty(SaveDataManager.Instance.userData.id))
        {
            _id = CreateID().ToString();
            await CheckAccountExistence(_id);
            
        }
        else
        {
            _id = SaveDataManager.Instance.userData.id;
            await Login();
        }
        
    }

    async UniTask Login()
    {
        bool isAccountCreate = TestForNullOrEmpty(SaveDataManager.Instance.userData.id);
        PlayFabClientAPI.LoginWithCustomID(

            
            new LoginWithCustomIDRequest {
                CustomId = _id, 
                CreateAccount = isAccountCreate, 
            },
            result => {
                // 成功時の処理
                Debug.Log("Login successfully");
            },
            error => {
                // 失敗時の処理
                Debug.LogError(error.GenerateErrorReport());
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

    private void OnAccountExistenceCheckSuccess(LoginResult result)
    {
        //アカウントが存在する場合の処理を書く
        _id = CreateID().ToString();
        CheckAccountExistence(_id);
    }

    private async void OnAccountExistenceCheckFailure(PlayFabError error)
    {
        SaveDataManager.Instance.userData.id = _id;
        await SaveDataManager.Instance.SaveDataAsync();
        Login();
    }

    
    
    void CreateAccount()
    {
        int ID = CreateID();
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
}
