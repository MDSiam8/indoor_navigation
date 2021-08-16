using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class ShowPath : MonoBehaviour
{
    private LineRenderer line;
    private GameObject target;
    public GameObject[] targetsArray;
    private NavMeshAgent agent;
    public GameObject DestinationScriptHolder;
    public Text systemMessage;
    public  TMP_Text Destination;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DestinationScriptHolder.GetComponent<GetInput>().readyToTrack == true)
        {
            // fyi most, if not all, of the string comparisons have ToUpper() attached to them
            // because it ensures that rooms, regardless of how they're spelled,
            // are discovered by my algorithms
            // i.e. "cafe" and "CAFE" and "cAfE" will all yield the same result
            // as long as everything is tagged and labelled with all CAPS in the unity editor 
            string FinalDestination = DestinationScriptHolder.GetComponent<GetInput>().officialDestination.ToUpper();
            Destination.text = "Destination: " + FinalDestination;
            for (int i = 0; i < targetsArray.Length; i++)
            {
                if (targetsArray[i].CompareTag(FinalDestination))
                {
                    // find the target in the array and initializes it
                    target = targetsArray[i];
                    target.SetActive(true);
                }
            }

            line = GetComponent<LineRenderer>();
            agent = GetComponent<NavMeshAgent>();
            getPath();
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < 0.65)
            {
                systemMessage.text = "You are now at your destination.";
            }
            else if (distance >= 0.65){
                systemMessage.text = "Navigation has started. You may now start moving.";
            }

        }

    }
    void getPath()
    {
        line.SetPosition(0, agent.transform.position);
        agent.SetDestination(target.transform.position);
        DrawPath(agent.path);
        agent.isStopped = true;
    }
    void DrawPath(NavMeshPath path)
    //  this code is a modified piece of code from StackExchange
    // https://gamedev.stackexchange.com/questions/67839/is-there-a-way-to-display-navmesh-agent-path-in-unity

    {
        if (path.corners.Length < 2) //if the path has 1 or no corners, there is no need
        {
            return;
        }

        line.positionCount = path.corners.Length; //set the array of positions to the amount of corners

        for (var i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }
    }
}
