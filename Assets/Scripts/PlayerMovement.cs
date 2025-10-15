using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerControls;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 10f;

    void Awake()
    {
        playerControls = new PlayerController();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        playerControls.PlayerControl.Enable();

    }

    void OnDisable()
    {
        playerControls.PlayerControl.Disable();
    }

    void FixedUpdate()
    {
        moveInput = playerControls.PlayerControl.Move.ReadValue<Vector2>();
        Vector2 delta = moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + delta);
    }
}
