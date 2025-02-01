using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for boss managers
/// </summary>
public abstract class BossManagerParent : MonoBehaviour
{
    [SerializeField]
    protected AudioClip wizardRunClip;

    public virtual void EndLevel() { }
}
