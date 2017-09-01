using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstantiateTextWithArray : MonoBehaviour {

    public string[] dialogues;
    public bool UseNextTravellingAnimation;
    public bool UseNextMainAnimation;
    public bool IsLastScenette;
	public bool CanClick;
    public string sceneName;
    public Animator imageScenette;
    public bool automaticAction;
    public ActivateAnimator activeAnimator;
    public Animator gradient;
    public GameObject nextScenette;
    public GameObject text;
    public RectTransform fleche;
    private List<GameObject> texts = new List<GameObject>();
    public int id;
    private Animator main;
    // Use this for initialization
    void Start () {
        automaticAction = false;
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        activeAnimator = GameObject.FindGameObjectWithTag("animator").GetComponent<ActivateAnimator>();
        GameObject tempText = null;

        if (transform.localScale.x < 0)
        {

            text.transform.localScale = Vector3.left + Vector3.up + Vector3.forward;
            transform.localPosition = Vector3.left * transform.localPosition.x + Vector3.up * transform.localPosition.y + Vector3.forward * transform.localPosition.z;
        }
        else
        {
            text.transform.localScale = Vector3.one;
        }

        text.GetComponent<Text>().text = dialogues[0];
        texts.Add(text);
        id = 0;
        for (int i = 1; i < dialogues.Length; i++)
        {        
            tempText = Instantiate(text, (text.transform as RectTransform).localPosition,Quaternion.identity);
            tempText.GetComponent<Text>().text = dialogues[i];
            tempText.transform.SetParent(transform,false);

            if (transform.localScale.x < 0)
                tempText.transform.localScale = Vector3.left + Vector3.up + Vector3.forward;
            else
                tempText.transform.localScale = Vector3.one;

            tempText.SetActive(false);
            texts.Add(tempText);
        }

        StartCoroutine(UpdateDialogueScenette());
        StartCoroutine(AutomaticAction(Random.Range(5.0f,8.0f)));

    }
    
    IEnumerator UpdateDialogueScenette()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
			if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || automaticAction)&&CanClick)
            {
				CanClick = false;
                StartCoroutine(AutomaticAction(Random.Range(5.0f, 8.0f)));
                if (id == (dialogues.Length - 1))
                {
                    if(UseNextMainAnimation)
                    {
                        activeAnimator.PlayAnimator1("animation intro");
                    }

                    if (IsLastScenette)
                    {
                        SceneManager.LoadScene(sceneName);
                    }

                    imageScenette.Play("Back", 0, 0.0f);
                    transform.GetComponent<Animator>().Play("ReverseFade", 0, 0.0f);
                    gradient.Play("ReverseGradient", 0, 0.0f);
                    yield return new WaitForSeconds(0.2f);

                    if(UseNextTravellingAnimation)
                    {
                        main.SetBool("Next", !main.GetBool("Next"));
                    }

                    if (nextScenette != null)
                    InstantiateNextScenette(nextScenette);

                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    ChangeDialogue();
                }
            }
            yield return null;
        }
    }

    IEnumerator AutomaticAction(float timer)
    {
        automaticAction = false;
        yield return new WaitForSeconds(timer);
        automaticAction = true;
        yield break;
    }

    public void ChangeDialogue()
    {
        id++;

        for (int i = 0; i < dialogues.Length; i++)
        {
            texts[i].SetActive(false);
        }

        texts[id].SetActive(true); 
    }

    public void InstantiateNextScenette(GameObject _Scenette)
    {
        GameObject Scenette = Instantiate(_Scenette);
        Scenette.transform.SetParent(transform.parent.parent,false);
        Scenette.transform.localScale = Vector3.one;
    }

}
