using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target, newTarget;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<Player>().isActive)
            {
                target = GameObject.FindGameObjectsWithTag("Player")[i].transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        //si el jugador activo cambia, la camara se mueve al nuevo jugador
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<Player>().isActive)
            {
                if (target != GameObject.FindGameObjectsWithTag("Player")[i].transform)
                {
                    newTarget = GameObject.FindGameObjectsWithTag("Player")[i].transform;
                    ChangePlayer();
                }
            }   
        }
    }   
    void ChangePlayer()
    {
        //lerp the camera to the new player
        transform.position = Vector3.Lerp(transform.position, new Vector3(newTarget.position.x, newTarget.position.y, transform.position.z), 0.1f);
        target = newTarget;
    }
}
