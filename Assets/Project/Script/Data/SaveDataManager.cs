using System.Threading.Tasks;
using UnityEngine;
using save; // JsonSaveLoadを含む名前空間を追加

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    public UserData userData = new UserData();

    private async void Start()
    {
        await LoadDataAsync();
    }

    public async Task SaveDataAsync()
    {
        await JsonSaveLoad.SaveDataAsync(userData);
    }

    public async Task LoadDataAsync()
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
}