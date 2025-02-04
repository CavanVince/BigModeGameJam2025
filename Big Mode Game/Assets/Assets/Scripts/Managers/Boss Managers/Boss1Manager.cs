using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss1Manager : BossManagerParent
{
    [SerializeField]
    GameObject bricks;

    [SerializeField]
    AudioClip bossMusic;

    private bool canMoveWindow = false;
    private DisplayInfo display;
    private Vector2 directionVector = new Vector2(1, -1f);
    private const int moveSpeed = 150;

    void Start()
    {
        BasicLevelManager.Instance.BossManager = this;
        bricks.SetActive(false);
        display = Screen.mainWindowDisplayInfo;
        Camera.main.GetComponent<AudioSource>().Stop();
        BasicLevelManager.Instance.CanInput = false;
        StartCoroutine(FightOpening());
    }

    private void Update()
    {
        Debug.Log(display.height);
        if (!canMoveWindow) return;
        MoveWindow();
    }

    /// <summary>
    /// Animate the game window
    /// </summary>
    private void MoveWindow()
    {
        // Check collisions
        if (Screen.mainWindowPosition.x <= 0)
        {
            // Bounce right
            directionVector.x = 1;
            BasicLevelManager.Instance.ScreenShake();
        }
        else if (Screen.mainWindowPosition.x + Screen.width >= display.width - 80)
        {
            // Bounce left
            directionVector.x = -1;
            BasicLevelManager.Instance.ScreenShake();
        }

        if (Screen.mainWindowPosition.y <= 0)
        {
            // Bounce down
            directionVector.y = 1;
            BasicLevelManager.Instance.ScreenShake();
        }
        else if (Screen.mainWindowPosition.y + Screen.height >= display.height - 80)
        {
            // Bounce up
            directionVector.y = -1;
            BasicLevelManager.Instance.ScreenShake();
        }

        // Move the window
        Screen.MoveMainWindowTo(display, new Vector2Int((int)(Screen.mainWindowPosition.x + (directionVector.x * Time.deltaTime * moveSpeed)), (int)(Screen.mainWindowPosition.y + (directionVector.y * Time.deltaTime * moveSpeed))));
    }

    IEnumerator FightOpening()
    {
        yield return new WaitForSeconds(2); // We just have to guess the timing
        DialogueManager.Instance.MoveToBottomCenter();
        yield return new WaitForSeconds(2);

        // Enable the wizard and have them talk
        DialogueManager.Instance.StartDialogue("Welcome to the first boss fight.");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.StartDialogue("I will start by taking your screen...");
        yield return new WaitForSeconds(5);

        Screen.SetResolution(960, 540, FullScreenMode.Windowed);
        DialogueManager.Instance.StartDialogue("Good luck!");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.MoveToTopRight();
        yield return new WaitForSeconds(1);

        Camera.main.GetComponent<AudioSource>().clip = bossMusic;
        Camera.main.GetComponent<AudioSource>().Play();
        canMoveWindow = true;
        bricks.SetActive(true);
        BasicLevelManager.Instance.CanInput = true;
    }

    /// <summary>
    /// Logic to perform when boss level ends
    /// </summary>
    public override void EndLevel()
    {
        BasicLevelManager.Instance.DestroyBallsGlobal();
        canMoveWindow = false;
        Screen.SetResolution(1920, 1080, true);
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
        DialogueManager.Instance.StartDialogue("I'm out.");
        yield return new WaitForSeconds(3);
        UiManager.Instance.WizardExit();
        Camera.main.GetComponent<AudioSource>().clip = wizardRunClip;
        Camera.main.GetComponent<AudioSource>().loop = false;
        Camera.main.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(7);
        LevelLoader.Instance.LoadNextLevel();
    }
}
