/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController cc;
    private float speed = 6f;
    private float gravity = -9.81f;
    private float yVelocity;
    private float yVelocityWhenGrounded = -4;   
    private float rotationSpeed = 360f; // essentially mouse x sensitivity
    private float jumpHeight = 3.0f;    // jump height in units
    private float jumpTime = 0.5f;      // jump air time in seconds
    private float initialJumpVelocity;   // how much force to jump with


    // Start is called before the first frame update
    void Start()
    {

        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    // Update is called once per frame
    void Update()
    {
       
        // determine XZ movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // convert from local to world coordinates
        movement = transform.TransformDirection(movement);
        // ensure diagonal movement doesn't exceed horiz/vert movement speed
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        // determine XZ movement speed
        movement *= speed;

        // Determine Y movement (based on gravity [g])
        // formula for instantaneous velocity [v] of a falling object after time [t]:
        // v = gt (from https://en.wikipedia.org/wiki/Equations_for_a_falling_body)
        yVelocity += gravity * Time.deltaTime;

        // if we are on the ground and we were descending
        if( cc.isGrounded && yVelocity < 0.0)
        {
            yVelocity = yVelocityWhenGrounded;
        }
        
        // check if we jumped.
        if( Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            yVelocity = initialJumpVelocity;
        }

        // make yVelocity part of the movement vector
        movement.y += yVelocity;

        // move the player via the CharacterController
        cc.Move(movement * Time.deltaTime);


        // rotate the player
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
    }
}
*/