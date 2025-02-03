using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Manager : BossManagerParent
{
    [SerializeField]
    private AudioClip bossMusic;

    [SerializeField]
    private List<GameObject> initialWalls;

    [SerializeField]
    private List<GameObject> patterns;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private List<GameObject> movingSegments;

    [SerializeField]
    private float levelTimer;

    private bool startTimer = false;


    void Start()
    {
        BasicLevelManager.Instance.BossManager = this;
        Camera.main.GetComponent<AudioSource>().Stop();
        GameObject.Find("Top Wall").transform.position += Vector3.up * 2;
        BasicLevelManager.Instance.CanInput = false;
        StartCoroutine(FightOpening());
    }

    private void Update()
    {
        if (!startTimer) return;
        levelTimer -= Time.deltaTime;
        DialogueManager.Instance.DisplayPlainText(levelTimer.ToString("0.00"));
        if (levelTimer <= 0)
        {
            startTimer = false;
            levelTimer = 0;

            // Stop level
            DOTween.Kill("Boss 3");
            EndLevel();
        }
    }


    IEnumerator FightOpening()
    {
        yield return new WaitForSeconds(1); // We just have to guess the timing
        DialogueManager.Instance.MoveToBottomCenter();
        yield return new WaitForSeconds(2);

        // Enable the wizard and have them talk
        DialogueManager.Instance.StartDialogue("Welcome to the final fight!");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.StartDialogue("I took out a mortgage for these gems.");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.StartDialogue("Let's see how long you can last.");
        yield return new WaitForSeconds(5);

        DialogueManager.Instance.MoveToTopRight();
        yield return new WaitForSeconds(2);

        Camera.main.GetComponent<AudioSource>().clip = bossMusic;
        Camera.main.GetComponent<AudioSource>().Play();

        DialogueManager.Instance.EnableWizardSpeak();
        DialogueManager.Instance.DisplayPlainText(levelTimer.ToString("0.00"));
        startTimer = true;
        BasicLevelManager.Instance.CanInput = true;

        // Jank to move the first two segments at a similar speed
        moveSpeed /= 2;
        MoveSegment(movingSegments[0].transform);
        moveSpeed *= 2;
        MoveSegment(movingSegments[1].transform);
    }

    /// <summary>
    /// Helper function to interpolate a segment
    /// </summary>
    /// <param name="segment">Segment to move</param>
    private void MoveSegment(Transform segment)
    {
        segment.DOMoveY(-20.5f, moveSpeed).SetEase(Ease.Linear).SetId("Boss 3").OnComplete(() =>
        {
            Destroy(segment.gameObject);
            Transform newSegmentRef = patterns[Random.Range(0, patterns.Count)].transform;
            MoveSegment(Instantiate(newSegmentRef, new Vector3(newSegmentRef.position.x, 24, newSegmentRef.position.z), Quaternion.identity).transform);
        });
    }


    /// <summary>
    /// Logic to perform when boss level ends
    /// </summary>
    public override void EndLevel()
    {
        BasicLevelManager.Instance.DestroyBallsGlobal();
        StartCoroutine(WizardExit());
    }

    IEnumerator WizardExit()
    {
        yield return new WaitForSeconds(15);
        DialogueManager.Instance.MoveToBottomCenter();
        yield return new WaitForSeconds(2);
        DialogueManager.Instance.StartDialogue("Uh oh.");
        yield return new WaitForSeconds(3);
        DialogueManager.Instance.StartDialogue("I'm all out...");
        yield return new WaitForSeconds(3);
        DialogueManager.Instance.StartDialogue("My wife is not going to like this.");
        yield return new WaitForSeconds(3);
        UiManager.Instance.WizardExit();
        Camera.main.GetComponent<AudioSource>().clip = wizardRunClip;
        Camera.main.GetComponent<AudioSource>().loop = false;
        Camera.main.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(7);
        LevelLoader.Instance.LoadNextLevel();
    }
}
