using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    List<Manager> managers = new List<Manager>();

    private void OnEnable()
    {
        foreach (var manager in managers)
            manager.OnManualEnable();
    }

    private void OnDisable()
    {
        foreach (var manager in managers)
            manager.OnManualDisable();
    }
}
