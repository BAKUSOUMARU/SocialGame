using Unity.Collections;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_EDITOR
        Debug.Log("デバックモード発動");
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
        
#endif
    }
}
