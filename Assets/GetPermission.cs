using UnityEngine;
using UnityEngine.Android;


public class GetPermission : MonoBehaviour
{
    void Start()
    {
    if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // show user the request permission screen
            // on click of the "give Camera Access" button, run the beneath line of code
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
}