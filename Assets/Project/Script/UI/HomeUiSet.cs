using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class HomeUiSet : MonoBehaviour
{
    [SerializeField] private TMP_Text userNameText;
    
    private string _userName;
    // Start is called before the first frame update
    async void Start()
    {
        await GetDisplayName();
        userNameText.text = _userName;
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
                userNameText.text = _userName;
            },
            error => {
                Debug.LogError(error.GenerateErrorReport());
            }
        ); 
    }
    
}
