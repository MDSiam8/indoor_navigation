using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using GoogleARCore;


public class ARControllerScript : MonoBehaviour
{
    public GameObject player;
    public bool readyToTrack;
    public GameObject NavSceneSetup;
    public GameObject ARDevice;
    public GameObject ButtonScriptHolder;
    private int i;
    void Start()
    {
        i = 0;
    }

    void Update()
    {
        readyToTrack = ButtonScriptHolder.GetComponent<GetInput>().readyToTrack;
        if (readyToTrack == true)
        {
            ARDevice.SetActive(true);
            // it takes the camera's current relative position and sets it equal to the position of the gameobject
            // we don't want the camera to record our up and down movements, so some axis are set to 0
            // there is Vector addition; thats the position offset due to the placement of the user in the map

            Vector3 currentPos = new Vector3(Frame.Pose.position.x, 0, Frame.Pose.position.z) + ButtonScriptHolder.GetComponent<GetInput>().officialUserSpawnPoint;
            player.transform.position = currentPos;

            // same with the rotation; we only want the y and w axis

            Quaternion cameraRot = new Quaternion(0, Frame.Pose.rotation.y, 0, Frame.Pose.rotation.w);

            if (i == 0)
            {
                // rotate map opposite to the way of the intended rotation of the user
                // we have to do this to create an illusion, because the tracking does not work for objects that aren't facing straight
                // because you cannot set the rotation or position of Frame.Pose

                // the reason the Navmesh also rotates with this is because I added a NavMeshSurface component 
                // to the gameobject
                // it typically doesn't come w/ unity and should be added manually
                NavSceneSetup.transform.RotateAround(player.transform.position, Vector3.up, -ButtonScriptHolder.GetComponent<GetInput>().officialUserRotation.y);

                // this terminates the update loop
                i = 1;
            }

            player.transform.rotation = cameraRot;
        }
    }
}
