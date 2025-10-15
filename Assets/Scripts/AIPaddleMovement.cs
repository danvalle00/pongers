using System;
using UnityEngine;

public class AIPaddleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f; // menos velocidade que o player pq esse daqui tem hack
    [SerializeField] private float deadZone = 0.05f;
    private Rigidbody2D rb;
    private Transform ballTransform;
    private Vector2 targetPosition;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ballTransform = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        targetPosition = new Vector2(rb.position.x, ballTransform.position.y);
        if (Math.Abs(targetPosition.y - rb.position.y) > deadZone)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            float newY = Mathf.MoveTowards(rb.position.y, targetPosition.y, step);
            rb.MovePosition(new Vector2(rb.position.x, newY));
        }
    }


}
