using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    [SerializeField]
    CharacterController cc;
    private float speed = 9.0f;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    private float gravity;
    private float YVelocity = 0.0f;
    private float YVelocityWhenGrounded = -4.0f;
    public float jumpHeight = 4.0f;
    public float jumpTime = 1.6f;
    private float initialJumpVelocity;
    private bool hasJumped;
    private bool doubleJumped;
    private float pushForce = 5.0f;
    void Start()
    {
        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / (Mathf.Pow(timeToApex, 2));
        initialJumpVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        hasJumped = Input.GetButtonDown("Jump");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        movement = transform.TransformDirection(movement);
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movement *= speed;
        YVelocity += gravity * Time.deltaTime;
        //transform.Translate(movement * speed * Time.deltaTime, Space.Self);

        if (cc.isGrounded && YVelocity < 0.0f)
        {
            YVelocity = YVelocityWhenGrounded;

        }
        if (hasJumped && cc.isGrounded)
        {
            YVelocity = initialJumpVelocity;
        }
        movement.y = YVelocity;
        cc.Move(movement * Time.deltaTime);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        // does it have a rigidbody and is Physics enabled?
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
