using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleClick : MonoBehaviour
{
    public int balloonID;

    public bool IsClikable = true;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (IsClikable)
        {
            NetworkedClientProcessing.SendMessageToServer(ClientToServerSignifiers.BalloonClicked + "," + balloonID);
            //Destroy(gameObject);
        }
    }
}
