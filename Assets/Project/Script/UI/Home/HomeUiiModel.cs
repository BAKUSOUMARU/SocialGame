using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class HomeUiiModel : MonoBehaviour
{
    private string _userName;
    
    public string UserName => _userName;

    private  int _virtualCurrency;

    public int VirtualCurrency => _virtualCurrency;
    
    // Start is called before the first frame update
     void Start()
    {
         GetDisplayName().Forget();
         GetvirtualCurrency().Forget();
    }

    async UniTask GetDisplayName()
    {
        PlayFabClientAPI.GetPlayerProfile(
            new GetPlayerProfileRequest {
                PlayFabId = SaveDataManager.Instance.userData.playFabId,
                ProfileConstraints = new PlayerProfileViewConstraints {
                    ShowDisplayName = true
                }
            },
            result => {
                _userName = result.PlayerProfile.DisplayName;
            },
            error => {
                Debug.LogError(error.GenerateErrorReport());
            }
        ); 
    }

    async UniTask GetvirtualCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest()
            {
            }
            , (result) =>
            {
                result.VirtualCurrency.TryGetValue("MS", out int value);
                _virtualCurrency = value;
                
            },
            (error => { Debug.Log(error.GenerateErrorReport()); }));
    }
    
}
