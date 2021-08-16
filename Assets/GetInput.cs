using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GetInput : MonoBehaviour
{
    public GameObject user;
    public TMP_InputField inputRoomID;
    public InputField userPositionID;
    public Button continueButton;
    public Button cancelNavigationButton;
    public Button startButton;
    public Button continueUserPositionScreenButton;
    public Button backToMenu;
    public Button moreInfo;
    public Button continueEnterCodeButton;
    public GameObject setDestinationScene;
    public GameObject navigationScene;
    public GameObject startScene;
    public GameObject UserPosInstrScreen;
    public GameObject MoreInfoScreen;
    public GameObject EnterCodeScreen;
    public Text warning;
    public Text warningCodeWrong;

    public string[] rooms;
    public string[] userPositionCodes;
    public Vector3[] userCoordinates;
    public Vector3[] userRotations;
    public Vector3 officialUserSpawnPoint;
    public Vector3 officialUserRotation;
    public string officialDestination;
    public bool readyToTrack = false;

    // Start is called before the first frame update
    void Start()
    {
        moreInfo.onClick.AddListener(OnPressMoreInfo);
        backToMenu.onClick.AddListener(onPressBackToMenu);
        continueButton.onClick.AddListener(onPressContinueButton);
        cancelNavigationButton.onClick.AddListener(onPressCancelButton);
        startButton.onClick.AddListener(onPressStartButton);
        continueEnterCodeButton.onClick.AddListener(onPressEnterCodeContinueButton);
        continueUserPositionScreenButton.onClick.AddListener(onPressInstructionContinueButton);
    }

    // Update is called once per frame
    void Update()
    {
        // userCurrentRot = user.transform.rotation;
        // Debug.Log(inputRoomID.text);
    }
    void onPressContinueButton()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].ToString().ToUpper().Equals(inputRoomID.text.ToUpper()))
            {
                officialDestination = inputRoomID.text;
                setDestinationScene.SetActive(false);
                UserPosInstrScreen.SetActive(true);
                return;
            }
            else
            {
                warning.text = "Room not found. Please try again.";
            }
        }
    }

    void onPressCancelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void onPressStartButton()
    {
        startScene.SetActive(false);
        setDestinationScene.SetActive(true);
    }
    void onPressInstructionContinueButton()
    {
        UserPosInstrScreen.SetActive(false);
        EnterCodeScreen.SetActive(true);
    }
    void onPressBackToMenu(){
        MoreInfoScreen.SetActive(false);
        startScene.SetActive(true);
    }
    void OnPressMoreInfo(){
        startScene.SetActive(false);
        MoreInfoScreen.SetActive(true);
    }
    void onPressEnterCodeContinueButton()
    {
        for (int i = 0; i < userPositionCodes.Length; i++)
        {
            if (userPositionCodes[i].ToString().ToUpper().Equals(userPositionID.text.ToUpper()))
            {
                // there are three arrays regarding this
                // first array has the position IDs
                // second array corresponds completely, 1 to 1, with the first array
                // but instead of position ID, it has the position coordinates
                // third array is for rotation

                // any and all additions, i.e. adding more destination points or initialization points
                // should be done through the Unity inspector screen that this script is attached to
                // app will adjust accordingly
                int UserPositionCodeIndex = i;
                warningCodeWrong.text = "";
                officialUserSpawnPoint = userCoordinates[UserPositionCodeIndex];
                officialUserRotation = userRotations[UserPositionCodeIndex];
                EnterCodeScreen.SetActive(false);
                navigationScene.SetActive(true);
                user.transform.position = officialUserSpawnPoint;
                user.transform.eulerAngles = officialUserRotation;
                readyToTrack = true;
                return;
            }
            else
            {
                warningCodeWrong.text = "Code not found. Please check to make sure you entered it correctly.";
            }
        }

    }
}
