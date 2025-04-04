using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    public GameObject activePlayer;
    void Awake()
    {
        instance = this;
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActivePlayer(GameObject player)
    {
        if (activePlayer == null)
        {
            activePlayer = player.gameObject;
            activePlayer.GetComponent<Player>().enabled = true;
        }
        else if (activePlayer != player)
        {
            activePlayer.GetComponent<Player>().enabled = false;
            player.GetComponent<Player>().enabled = true;
            activePlayer = player.gameObject;
        }
    }
}
