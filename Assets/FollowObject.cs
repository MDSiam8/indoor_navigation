using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject user;
    public Camera mainCamera;
    public float forwardUserInput;
    public float rotationInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        forwardUserInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        // sets the camera to follow the gameobject that represents the user
        mainCamera.transform.position = new Vector3(user.transform.position.x, mainCamera.transform.position.y, user.transform.position.z);

        // these are user input, done for developmental testing
        user.transform.Translate(Vector3.forward * forwardUserInput * 15 * Time.deltaTime);
        user.transform.Rotate(Vector3.up * rotationInput * 100 * Time.deltaTime);        
    }
}
