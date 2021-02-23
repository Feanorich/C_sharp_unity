using UnityEngine;
using System.Collections;
 
public static class CoroutineExtensions
{
    private static AsyncOperationBehaviour _asyncOperationBehaviour = null;
    
    public static Coroutine StartCoroutine(this IEnumerator task, out CoroutineController coroutineController)
    {
        Initialize();
        if (task == null)
        {
            throw new System.ArgumentNullException(nameof(task));
        }
 
        coroutineController = new CoroutineController(task);
        return _asyncOperationBehaviour.StartCoroutine(coroutineController.Start());
        
    }

    private static void Initialize()
    {
        if (_asyncOperationBehaviour != null)
        {
            return;
        }

        GameObject g = new GameObject();
        Object.DontDestroyOnLoad(g);
        g.name = "AsyncOperationExtensionCoroutine";
        g.hideFlags = HideFlags.HideAndDontSave;

        _asyncOperationBehaviour = g.AddComponent<AsyncOperationBehaviour>();
    }
}
