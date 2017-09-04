using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour 
{
	public static ScoreManager instance = null;

    public Text scoreUI;
    public GameObject IOScontrols;
    private const string scoreText = " Score: ";
    private int score = 0;


	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a ScoreManager.
			Destroy(gameObject);

#if UNITY_IOS && !UNITY_EDITOR
       IOScontrols.SetActive((true));
#endif
	}

    //appelé lorsque qu'on mange un sandwich
    public void addPoint(int points)
    {
        score += points;
        if(score <= 0)
        {
            score = 0;
        }

        scoreUI.text = scoreText + (score) ;
    }
}
