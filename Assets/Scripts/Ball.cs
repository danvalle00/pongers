using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Transform ballTransform;
    [SerializeField] private float impulseForce = 10f;
    [SerializeField] private Vector2 lastVelocity;
    [Header("Audio")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource audioSource;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        ballTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        float randomX = Random.value < 0.5f ? -1.0f : 1.0f;
        float randomY = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        Vector2 impulseDirection = new Vector2(randomX, randomY).normalized;
        rigidBody.AddForce(impulseDirection * impulseForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        lastVelocity = rigidBody.linearVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Field"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflect = Vector2.Reflect(lastVelocity, normal);
            rigidBody.linearVelocity = reflect;
        }
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 reflect = Vector2.Reflect(lastVelocity, normal);
            rigidBody.linearVelocity = reflect * 1.2f; // 20% de aumento qnd hita uma paddle
        }
        PlayHitSound();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadzoneLeft"))
        {

            ResetBallPosition();
            if (GameManager.Instance != null) GameManager.Instance.AddAIScore(1);
        }
        else if (collision.gameObject.CompareTag("DeadzoneRight"))
        {
            ResetBallPosition();
            if (GameManager.Instance != null) GameManager.Instance.AddPlayerScore(1);
        }



    }
    void ResetBallPosition()
    {
        ballTransform.position = Vector2.zero; // reseta pro meio
        rigidBody.linearVelocity = Vector2.zero; // zera a velocidade
        Start();
    }
    void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

}
