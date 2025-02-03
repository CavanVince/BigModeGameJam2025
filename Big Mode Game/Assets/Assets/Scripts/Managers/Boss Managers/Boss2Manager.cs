using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Manager : BossManagerParent
{
    [SerializeField]
    GameObject bricks;

    [SerializeField]
    AudioClip bossMusic;

    [SerializeField]
    List<GameObject> gemPatterns;

    private int health;
    private bool canSpawnBricks;
    private float brickSpawnTimer;

    void Start()
    {
        BasicLevelManager.Instance.BossManager = this;

        GameObject.Find("Top Wall").SetActive(false);
        health = 3;
        canSpawnBricks = false;
        brickSpawnTimer = Random.Range(10, 16);
        Camera.main.GetComponent<AudioSource>().Stop();
        BasicLevelManager.Instance.CanInput = false;
        StartCoroutine(FightOpening());
    }

    private void Update()
    {
        if (canSpawnBricks && bricks.transform.childCount == 1)
        {
            if (brickSpawnTimer > 0)
            {
                brickSpawnTimer -= Time.deltaTime;
            }
            else
            {
                brickSpawnTimer = Random.Range(10, 16);
                // Spawn and attach bricks to 
                GameObject newGemPattern = Instantiate(gemPatterns[Random.Range(0, gemPatterns.Count)]);
                int childCount = newGemPattern.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    newGemPattern.transform.GetChild(childCount - i - 1).SetParent(bricks.transform);
                }
                Destroy(newGemPattern);
            }
        }
    }

    IEnumerator FightOpening()
    {
        yield return new WaitForSeconds(1); // We just have to guess the timing
        DialogueManager.Instance.MoveToBottomCenter();
        yield return new WaitForSeconds(2);

        // Enable the wizard and have them talk
        DialogueManager.Instance.StartDialogue("Welcome to the second boss fight.");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.StartDialogue("This time will be different...");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.StartDialogue("We will settle this with PONG");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.MoveToTopRight();
        yield return new WaitForSeconds(1);

        Camera.main.GetComponent<AudioSource>().clip = bossMusic;
        Camera.main.GetComponent<AudioSource>().Play();
        canSpawnBricks = true;
        BasicLevelManager.Instance.CanInput = true;
    }

    /// <summary>
    /// Reduce the boss' health
    /// </summary>
    public void ReduceHealth()
    {
        health--;
        UiManager.Instance.TakeDamage();


        if (health <= 0)
        {
            EndLevel();
        }
    }

    /// <summary>
    /// Logic to perform when boss level ends
    /// </summary>
    public override void EndLevel()
    {
        BasicLevelManager.Instance.DestroyBallsGlobal();
        BasicLevelManager.Instance.ScreenShake();
        Camera.main.GetComponent<AudioSource>().Stop();
        StartCoroutine(WizardExit());
    }

    IEnumerator WizardExit()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.MoveToBottomCenter();
        yield return new WaitForSeconds(2);
        DialogueManager.Instance.StartDialogue("Uh oh.");
        yield return new WaitForSeconds(3);
        DialogueManager.Instance.StartDialogue("Not again...");
        yield return new WaitForSeconds(3);
        UiManager.Instance.WizardExit();
        Camera.main.GetComponent<AudioSource>().clip = wizardRunClip;
        Camera.main.GetComponent<AudioSource>().loop = false;
        Camera.main.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(7);
        LevelLoader.Instance.LoadNextLevel();
    }
}
