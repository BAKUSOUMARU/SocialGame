using Cysharp.Threading.Tasks;
using UnityEngine;
using Save; // JsonSaveLoadを含む名前空間を追加

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    public UserData userData = new UserData();

    protected override async void Awake()
    {
        base.Awake();
        await LoadDataAsync();
    }
    
    public async UniTask SaveDataAsync()
    {
        await JsonSaveLoad.SaveDataAsync(userData);
    }

    public async UniTask LoadDataAsync()
    {
        UserData loadedData = await JsonSaveLoad.LoadDataAsync();

        if (loadedData != null)
        {
            userData = loadedData;
            Debug.Log("ロード完了");
        }
        else
        {
            Debug.Log("新規セーブデータ作成");
            await SaveDataAsync();
        }
    }

    public async UniTask ResetDataAsync()
    {
        userData = new UserData();
        Debug.Log("セーブデータをリセットしました");
        await SaveDataAsync();
    }
}