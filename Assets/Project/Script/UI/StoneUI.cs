using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class StoneUI : MonoBehaviour
{
    private int _getVirtualCurrency;

    public int GetVirtualCurrency => _getVirtualCurrency;
    
    private bool _getData = false;
    // Start is called before the first frame update
    void Start()
    {
        GetUserInventory();
    }
    /// <summary>
    /// インベントリの情報を取得
    /// </summary>
    public async UniTask GetUserInventory(){
        //GetUserInventoryRequestのインスタンスを生成
        var userInventoryRequest = new GetUserInventoryRequest();
    
        //インベントリの情報の取得
        Debug.Log($"インベントリの情報の取得開始");
        PlayFabClientAPI.GetUserInventory(userInventoryRequest, OnSuccess, OnError);
        await UniTask.WaitUntil(() => _getData, cancellationToken: this.GetCancellationTokenOnDestroy());

        _getData = false;
    }
  
    //=================================================================================
    //取得結果
    //=================================================================================
  
    //インベントリの情報の取得に成功
    private void OnSuccess(GetUserInventoryResult result){
        //result.Inventoryがインベントリの情報
        Debug.Log($"インベントリの情報の取得に成功");
    
        //所持している仮想通貨の情報をログで表示
        foreach(var virtualCurrency in result.VirtualCurrency){
            Debug.Log($"仮想通貨 {virtualCurrency.Key} : {virtualCurrency.Value}");
            _getVirtualCurrency = virtualCurrency.Value;
        }

        Debug.Log("魔法石　" + _getVirtualCurrency);

        _getData = true;
    }

    //インベントリの情報の取得に失敗
    private void OnError(PlayFabError error){
        Debug.LogError($"インベントリの情報の取得に失敗\n{error.GenerateErrorReport()}");
        _getData = true;
    }
}
