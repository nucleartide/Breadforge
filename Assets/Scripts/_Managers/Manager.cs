using UnityEngine;

/// <summary>
/// An Manager is a reusable gameplay system.
/// </summary>
public abstract class Manager : ScriptableObject
{
    public virtual void OnManualEnable() {}
    public virtual void OnManualDisable() {}
}
