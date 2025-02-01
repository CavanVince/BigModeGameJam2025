using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour
{
    [SerializeField]
    GameObject bricks;

    private bool canMoveWindow = false;
    private Vector2Int targetScreenPos;
    private DisplayInfo display;
    private Vector2 directionVector = new Vector2(1, -1f);
    private const int moveSpeed = 250;

    void Start()
    {
        bricks.SetActive(false);
        display = Screen.mainWindowDisplayInfo;
        StartCoroutine(FightOpening());
    }

    private void Update()
    {
        if (!canMoveWindow) return;
        MoveWindow();
    }

    private void MoveWindow()
    {
        // Check collisions
        if (Screen.mainWindowPosition.x <= 0)
        {
            // Bounce right
            directionVector.x = -directionVector.x;
        }
        else if (Screen.mainWindowPosition.x + Screen.width >= display.width)
        {
            // Bounce left
            directionVector.x = -directionVector.x;
        }

        if (Screen.mainWindowPosition.y <= 0)
        {
            // Bounce down
            directionVector.y = -directionVector.y;
        }
        else if (Screen.mainWindowPosition.y + Screen.height >= display.height - 50)
        {
            // Bounce up
            directionVector.y = -directionVector.y;
        }

        // Move the window
        Screen.MoveMainWindowTo(display, new Vector2Int((int)(Screen.mainWindowPosition.x + (directionVector.x * Time.deltaTime * moveSpeed)), (int)(Screen.mainWindowPosition.y + (directionVector.y * Time.deltaTime * moveSpeed))));
    }

    IEnumerator FightOpening()
    {
        // Enable the wizard and have them talk
        DialogueManager.Instance.EnableWizardSpeak();
        DialogueManager.Instance.StartDialogue("Welcome to the first boss fight.");
        yield return new WaitForSeconds(5); // We just have to guess the timing

        DialogueManager.Instance.StartDialogue("I will start by taking your screen...");
        yield return new WaitForSeconds(5);

        Screen.SetResolution(960, 540, FullScreenMode.Windowed);
        DialogueManager.Instance.StartDialogue("Now, let the fun begin!");
        yield return new WaitForSeconds(5);

        canMoveWindow = true;
        bricks.SetActive(true);
    }
}
