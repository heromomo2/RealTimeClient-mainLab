using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    float durationUntilNextBalloon;

    Sprite circleTexture;

    LinkedList<GameObject> Balloons;

    LinkedList<GameObject> PlayerSlots;

   public GameObject Content;

    public GameObject TextPrefab;
    void Start()
    {
        NetworkedClientProcessing.SetGameLogic(this);

        Balloons = new LinkedList<GameObject>();

        PlayerSlots= new LinkedList<GameObject>();

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "Content")
                Content = go;
        }

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



    public void AddPlayerOnTheBoard(string s)
    {
      //  Text prefabTextObject = (Text)Resources.Load("Prefab/Text.prefab", typeof(Text));

        if (TextPrefab != null)
        {
            GameObject newObj = (GameObject)Instantiate(TextPrefab, Content.transform);
            newObj.GetComponent<Text>().text = s;
            PlayerSlots.AddLast(newObj);
        }
    }

    public void ClearAllPlayersOnTheBoard()
    {
        if (PlayerSlots.Count != 0)
        {
            foreach (GameObject p in PlayerSlots)
            {
                Destroy(p);
            }
            PlayerSlots.Clear();
        }
    }

    public void SetBalloonToOld (int BalloonID)
    {
        GameObject SearchBalloon = null;
        foreach (GameObject b in Balloons)
        {
            if (b.GetComponent<CircleClick>().balloonID == BalloonID)
            {
                SearchBalloon = b;
                break;
            }
        }
        if (SearchBalloon != null)
        {
            SearchBalloon.GetComponent<CircleClick>().IsClikable = false;
            SearchBalloon.GetComponent<SpriteRenderer>().color = Color.cyan;
            
        }
    }
}

