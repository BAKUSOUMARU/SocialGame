using UnityEngine;

public class ButtonSceneChanger : MonoBehaviour
{
    private void ChangeScene(string sceneName)
    {
        // SceneChangerクラスのChangeSceneメソッドを呼び出す
        SceneChanger.ChangeScene(sceneName);
    }
}