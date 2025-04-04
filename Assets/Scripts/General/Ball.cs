using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //move along with its parent if it has one
        if (transform.parent != null)
        {
            RotateBall();
            FollowPlayer();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.SetActivePlayer(collision.gameObject);
            AttachToPlayer();
        }
    }
    void AttachToPlayer()
    {
        transform.parent = GameManager.instance.activePlayer.transform;
        rb.velocity = Vector2.zero;
    }
    void RotateBall()
    {
        Vector3 direction = GameManager.instance.activePlayer.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void FollowPlayer()
    {
        transform.position = GameManager.instance.activePlayer.transform.position + GameManager.instance.activePlayer.transform.right * 0.75f;
    }
}
