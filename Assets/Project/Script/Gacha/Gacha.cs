using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;

public class Gacha : MonoBehaviour
{

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
        Debug.LogWarning($"ガチャが引けませんでした"+ obj);
    }

    private void OnSuccessUpdatingPlayerData(PurchaseItemResult obj)
    {
        Debug.Log(obj.Items[1].DisplayName);
        var data =CharacterManager.Instance._characterDataAsset.CharacterDatasList.Find(X => X._status.Name == obj.Items[1].DisplayName);
        var character = new Character(
            CharacterManager.Instance.CharacterID,
            data._characterSprite,
            data._charactericonSprite,
            data._status.Name,
            data._status.Level,
            data._status.MaxHP,
            data._status.Atk,
            data._status.Speed,
            data._status.Lucky);
        CharacterManager.Instance.AddGetCharacter(character);
        CharacterManager.Instance.UpdateUserDate();
    }
}
