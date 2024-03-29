using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using TMPro;

public class Gacha : MonoBehaviour
{

    [SerializeField] 
    private TMP_Text _text;
    public void GachaGO()
    {
        var request = new PurchaseItemRequest
        {
            StoreId = "Gachastore",
            ItemId = "Gacha",
            VirtualCurrency = "MS",
            Price = 1
        }; 
        
        PlayFabClientAPI.PurchaseItem(request,OnSuccessUpdatingPlayerData,OnErrorUpdatingPlayerData);
        
    }
    private void OnErrorUpdatingPlayerData(PlayFabError obj)
    {
        _text.text = "がチャ石が足りないよ";
        //Debug.LogWarning($"ガチャが引けませんでした"+ obj);
    }

    private void OnSuccessUpdatingPlayerData(PurchaseItemResult obj)
    {
		string getCharacter　= obj.Items[1].DisplayName;
        Debug.Log(getCharacter);
        var data =CharacterManager.Instance._characterDataAsset.CharacterDatasList.Find(X => X._status.Englishname == obj.Items[1].DisplayName);
        var character = new Character(
            CharacterManager.Instance.CharacterID,
            data._characterSprite,
            data._charactericonSprite,
            data._status.Englishname,
            data._status.JapaneseName,
            data._status.Level,
            data._status.MaxHP,
            data._status.Atk,
            data._status.Speed,
            data._status.Lucky);
        CharacterManager.Instance.AddGetCharacter(character);
        _text.text = data._status.JapaneseName + "をゲットした";
        CharacterManager.Instance.UpdateUserDate();
    }
}
