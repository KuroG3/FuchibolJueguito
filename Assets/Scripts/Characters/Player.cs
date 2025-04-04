using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;

public class Player : MonoBehaviour, InputController.IPlayerActions
{
    Rigidbody2D rb;

    //stats
    public float speed, shootForce;
    public bool isActive;
    public GameObject ball;
    public InputController ic;
    void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        rb = GetComponent<Rigidbody2D>();
        ic = new InputController();
        ic.Player.SetCallbacks(this);
        enabled = isActive;
    }
    private void OnEnable()
    {
        ic.Player.Enable();
        isActive = true;
    }
    private void OnDisable()
    {
        ic.Player.Disable();
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        enabled = isActive;
        //look at the mouse
        Vector3 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.transform.position = transform.position;
        rb.transform.rotation = transform.rotation;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            rb.velocity = direction * speed/2;
        }
        else if (context.canceled)
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Vector3.Distance(transform.position, ball.transform.position) < 1.5f)
            {
                ShootBall();
            }
        }
    }
    public void OnPass(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Vector2.Distance(transform.position, ball.transform.position) < 1.5f)
            {
                PassBall();
            }
        }
    }
    void ShootBall()
    {
        if (GetComponentInChildren<Ball>() != null)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            ball.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce * 2, ForceMode2D.Impulse);
            GameManager.instance.ball.transform.parent = null;
        }
    }
    void PassBall()
    {
        if (rb != null)
        {
            //the ball will be passed to the player that is closest to the mouse position
            GameObject closestPlayer = null;
            float minDistance = Mathf.Infinity;
            for (int i = 0; i < FindObjectsOfType<Player>().Length; i++)
            {
                if (FindObjectsOfType<Player>()[i].isActive)
                {
                    continue;
                }
                float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), FindObjectsOfType<Player>()[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlayer = FindObjectsOfType<Player>()[i].gameObject;
                }
            }
            if (closestPlayer != null)
            {
                Vector2 direction = closestPlayer.transform.position - transform.position;
                ball.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shootForce, ForceMode2D.Impulse);
            }
            GameManager.instance.ball.transform.parent = null;
        }
    }
}
