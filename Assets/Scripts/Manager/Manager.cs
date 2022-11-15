using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private bool debugManager = false;
    private void Start()
    {
        if (!debugManager)
            DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR
        if(debugManager)
            DestroyImmediate(gameObject);
#endif
    }
}
