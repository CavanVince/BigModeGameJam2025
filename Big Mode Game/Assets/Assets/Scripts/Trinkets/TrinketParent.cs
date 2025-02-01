using UnityEngine;

/// <summary>
/// The parent class for trinkets
/// </summary>
public abstract class TrinketParent
{
    /// <summary>
    /// Trigger the passive of the trinket
    /// </summary>
    public virtual void TriggerPassive() { }

    /// <summary>
    /// Trigger the passive of the trinket
    /// </summary>
    /// <param name="trans">Transform parameter for more information</param>
    public virtual void TriggerPassive(Transform trans) { }

    /// <summary>
    /// Function to unsubscribe/revert logic when trinket is removed
    /// </summary>
    public virtual void RemoveTrinket() { }
}
