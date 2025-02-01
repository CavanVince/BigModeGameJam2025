using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeTrinket : TrinketParent
{
    private int bounceCounter = 0;
    private Sprite lightningSprite;
    // Start is called before the first frame update
    public LightningStrikeTrinket(Sprite sprite)
    {
        BallController.ballBounced += TriggerPassive;
        lightningSprite = sprite;
    }

    public override void TriggerPassive(Transform ballTransform)
    {
        bounceCounter++;
        if (bounceCounter >= 10) 
        {
            GameObject strikeOne = new GameObject();
            setStrike(strikeOne);
            GameObject strikeTwo = new GameObject();
            setStrike(strikeTwo);
            GameObject strikeThree = new GameObject();
            setStrike(strikeThree);
            bounceCounter = 0;
        }
    }
    private void setStrike(GameObject strike)
    {
        strike.layer = 6;
        strike.AddComponent<CircleCollider2D>();
        strike.AddComponent<Rigidbody2D>().gravityScale = 0;
        strike.AddComponent<SpriteRenderer>().sprite = lightningSprite;
        strike.GetComponent<SpriteRenderer>().sortingOrder = 5;
        Vector3 randomPosition = new Vector3(Random.Range(-16,0), Random.Range(8,-5), 0);
        strike.transform.position = randomPosition;
        strike.AddComponent<LightningSpawn>();
    }
}
