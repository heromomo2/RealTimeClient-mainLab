using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkedClientProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg)
    {
        Debug.Log("msg received = " + msg + ".");

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if (signifier == ServerToClientSignifiers.spawnBallon)
        {
            float screenPositionXPercent = float.Parse(csv[1]);

            float screenPositionYPercent = float.Parse(csv[2]);

            int BalloonID = int.Parse(csv[3]);
            Vector2 screenPosition = new Vector2(screenPositionXPercent * (float)Screen.width, screenPositionYPercent * (float)Screen.height);
           gameLogic.SpawnNewBalloon(screenPosition,BalloonID);

        }
        else if (signifier == ServerToClientSignifiers.BalloonPopped)
        {
            gameLogic.DestoryBalloon(int.Parse(csv[1]));
        }
        else if (signifier == ServerToClientSignifiers.ActiviePlayers)
        {
            string Playerdata = "# " + csv[1] + " S: " + csv[2];
            gameLogic.AddPlayerOnTheBoard(Playerdata);
        }
        else if (signifier == ServerToClientSignifiers.RemoveActiviePlayers)
        {
            gameLogic.ClearAllPlayersOnTheBoard();
        }
        //gameLogic.DoSomething();

    }

    static public void SendMessageToServer(string msg)
    {
        networkedClient.SendMessageToServer(msg);
    }

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkedClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkedClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkedClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkedClient networkedClient;
    static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkedClient NetworkedClient)
    {
        networkedClient = NetworkedClient;
    }
    static public NetworkedClient GetNetworkedClient()
    {
        return networkedClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int asd = 1;

    public const int BalloonClicked = 2;
}

static public class ServerToClientSignifiers
{
    public const int asd = 1;

    public const int spawnBallon = 2;

    public const int BalloonPopped = 3;

    public const int ActiviePlayers = 4;

    public const int RemoveActiviePlayers = 5;
}


#endregion

