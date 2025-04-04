using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    public string team;
    Rigidbody2D rb;
    public float speed;
    public GameObject goalZone, target;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetTarget();
        FollowBall();
    }
    void SetTarget()
    {
        GameObject ball = GameManager.instance.ball;
        if (ball != null && ball.transform.parent != null && ball.transform.parent.gameObject == gameObject)
        {
            target = ball;
        }
        else if (ball != null && ball.transform.parent != null && ball.transform.parent.gameObject != gameObject)
        {
            target = ball.transform.parent.gameObject;
        }
    }
    void FollowBall()
    {
        if (target != null)
        {
            Vector2 targetPos = new Vector2(target.transform.position.x, rb.position.y);
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, speed * Time.deltaTime));
        }
    }
}
