using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVLookAtPlayer : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        //Doesn't work with the cameras we have. Use a different model with the lens as a moveable object, and the mounting as unmoving

        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        transform.LookAt(target);
    }
}
