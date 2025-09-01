using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Value")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpHoldForce = 0.5f;
    [SerializeField] private float jumpHoldDuration = 0.2f;

    private bool isJumping;
    private float jumpTimeCounter;

    [Header("Movement Value")]
    [SerializeField] private float speed;

    private float movementvalue;

    [Header("Layer Movement")]
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Components")]
    [SerializeField] private GunFireAction gunFireAction;
    private Rigidbody2D PlayerBody;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        PlayerBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        PlayerJump();
        PlayerCanMove();
        gunFireAction.PlayerShooting();
    }

    private void PlayerCanMove() {
        movementvalue = Input.GetAxis("Horizontal");
        if ((movementvalue > 0 && isOnWallRight()) || (movementvalue > 0 && isGroundedRight()))
        {
            movementvalue = 0;
        }
        if ((movementvalue < 0 && isOnWallLeft()) || (movementvalue < 0 && isGroundedLeft()))
        {
            movementvalue = 0;
        }
        PlayerBody.linearVelocity = new Vector2(movementvalue * speed, PlayerBody.linearVelocity.y);
    }

    private void PlayerJump() {

        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpHoldDuration;
            PlayerBody.linearVelocity = new Vector2(PlayerBody.linearVelocity.x, jumpPower);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                PlayerBody.linearVelocity = new Vector2(PlayerBody.linearVelocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    public void ThrowUpByTornado(){
        PlayerBody.linearVelocity = new Vector2(PlayerBody.linearVelocity.x, 30f);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }

    private bool isGroundedLeft()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
                                 Vector2.left, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }

    private bool isGroundedRight()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
                                 Vector2.right, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }

    private bool isOnWallRight()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
                                 Vector2.right, 0.1f, wallLayer);
    }

    private bool isOnWallLeft()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
                                 Vector2.left, 0.1f, wallLayer);
    }

}
