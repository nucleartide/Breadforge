using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
#endif

public class CollectableSignifier : MonoBehaviour
{
#if UNITY_EDITOR
    private void Start()
    {
        Assert.IsNotNull(FindObjectOfType<EventSystem>());
    }
#endif
}
