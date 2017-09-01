using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManagerInputField : MonoBehaviour {

    public Animator animator;
    public InputField inputfield;
    public GetAndPushLeaderBoardJson LeaderBoardJson;
    public Text textNext;
	public Button restartButton;
	public Button gameJamButton;
	public Button gitHubButton;
    void Start()
    {
#if UNITY_ANDROID || UNITY_IOS

        textNext.text = "appuie pour continuer";

#endif

    }

    // Update is called once per frame
    void Update () {


        if(Input.GetKeyDown(KeyCode.Space))
        {
            inputfield.enabled = false;
        }

        if (Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space))
        {
			restartButton.enabled = true;
			gameJamButton.enabled = true;
			gitHubButton.enabled = true;
            LeaderBoardJson.BeginTestConnection();
            animator.enabled = true;
			Debug.Log("EndEnd");
			GetComponent<InputManagerInputField> ().enabled = false;
        }

	}
}
