using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayFabAccountNameSet : MonoBehaviour
{
    [SerializeField] private TMP_InputField _accountnameSet;
    
    [SerializeField] private TMP_Text _accountNameErrorText;
    
    [SerializeField] string sceneChangeName ="HomeScene";

    public void AccountNameSet()
    {
        if (_accountnameSet == null)
        {
            _accountNameErrorText.text = "アカウントの名前を入れてください";
        }
        string accountName = _accountnameSet.text;
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            new UpdateUserTitleDisplayNameRequest {
                DisplayName = accountName
            }, async result =>
            {
                _accountNameErrorText.text = "アカウントの名前が正常に登録できました";
                await CharacterManager.Instance.UpdateUserDate();
                await CharacterManager.Instance.GetUserData();
                SceneChanger.ChangeScene(sceneChangeName);
            },
            error => {
                _accountNameErrorText.text = "すでにこの名前は使わっれてります";
            }
        );
    }
}
