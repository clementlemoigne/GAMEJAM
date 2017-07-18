using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScenetteImage : MonoBehaviour {

    public bool IsLeft;
    public float timer;
    public Sprite spriteScenette;
    public GameObject DialogueContainer;
    public GameObject ScenetteLeft;
    public GameObject ScenetteRight;
    public GameObject Gradient;
    public GameObject skipDialogue;
    // Use this for initialization
    IEnumerator Start () {

        yield return new WaitForSeconds(timer);

        GetComponent<CanvasGroup>().alpha = 1.0f;
        GetComponent<CanvasGroup>().interactable = true;

        if (IsLeft)
        {
            ScenetteLeft.SetActive(true);
            DialogueContainer.GetComponent<InstantiateTextWithArray>().imageScenette = ScenetteLeft.GetComponent<Animator>();
            if (spriteScenette != null)
                ScenetteLeft.GetComponent<Image>().sprite = spriteScenette;
        }
        else
        {
            ScenetteRight.SetActive(true);
            DialogueContainer.GetComponent<InstantiateTextWithArray>().imageScenette = ScenetteRight.GetComponent<Animator>();
            DialogueContainer.transform.localScale = Vector3.left + Vector3.up + Vector3.forward;
            Gradient.transform.localScale = Vector3.left + Vector3.up + Vector3.forward;
            skipDialogue.transform.localScale = Vector3.left + Vector3.up + Vector3.forward;
            if (spriteScenette != null)
                ScenetteRight.GetComponent<Image>().sprite = spriteScenette;
        }
        DialogueContainer.SetActive(true);
        Gradient.SetActive(true);
    }
	
}
