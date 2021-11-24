using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    float durationUntilNextBalloon;

    Sprite circleTexture;
    LinkedList<GameObject> Balloons;

    void Start()
    {
        NetworkedClientProcessing.SetGameLogic(this);
        Balloons = new LinkedList<GameObject>();
    }
    void Update()
    {
        //durationUntilNextBalloon -= Time.deltaTime;

        //if(durationUntilNextBalloon < 0)
        //{
        //    durationUntilNextBalloon = 1f;

        //    float screenPositionXPercent = Random.Range(0.0f, 1.0f);
        //    float screenPositionYPercent = Random.Range(0.0f, 1.0f);
        //    Vector2 screenPosition = new Vector2(screenPositionXPercent * (float)Screen.width, screenPositionYPercent * (float)Screen.height);
        //    SpawnNewBalloon(screenPosition);
        //}
    }
    public void SpawnNewBalloon(Vector2 screenPosition, int BalloonID)
    {
        if(circleTexture == null)
            circleTexture = Resources.Load<Sprite>("Circle");

        GameObject balloon = new GameObject("Balloon");

        balloon.AddComponent<SpriteRenderer>();
        balloon.GetComponent<SpriteRenderer>().sprite = circleTexture;
        balloon.AddComponent<CircleClick>();
        balloon.AddComponent<CircleCollider2D>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
        pos.z = 0;
        balloon.transform.position = pos;
        balloon.GetComponent<CircleClick>().balloonID = BalloonID;
        Balloons.AddLast(balloon);
        //go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
    }

    public void  DestoryBalloon (int BalloonID) 
    {

        GameObject DestroyMe = null;
        foreach (GameObject b in Balloons) 
        {
            if (b.GetComponent<CircleClick>().balloonID == BalloonID)
            {
               DestroyMe = b;
                break;
            }
        }
        if(DestroyMe != null) 
        {
            Balloons.Remove(DestroyMe);
            Destroy(DestroyMe);
        }
    }
}

