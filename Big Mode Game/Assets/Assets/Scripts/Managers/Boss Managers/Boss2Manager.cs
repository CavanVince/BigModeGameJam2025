using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Manager : BossManagerParent
{
    [SerializeField]
    GameObject bricks;

    [SerializeField]
    AudioClip bossMusic;

    void Start()
    {
        BasicLevelManager.Instance.BossManager = this;

        GameObject.Find("Top Wall").SetActive(false);
    }


}
