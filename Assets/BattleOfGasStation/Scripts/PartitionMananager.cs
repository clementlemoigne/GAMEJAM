using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartitionMananager : MonoBehaviour {

    public GameObject[] partitions;
    public GameObject note;
    public bool creationMode;

	// Use this for initialization
	void Start () {
		
        if(creationMode)
        {
            StartCoroutine(CreationMode());
        }
        else
        {
            GameObject tempPartition = Instantiate(partitions[0]);
            SoundManager.instance.LoadMusic(tempPartition.name,1.0f);
        }

	}

    IEnumerator CreationMode()
    {
        GameObject partition = new GameObject();
        partition.transform.SetParent(transform);
        partition.transform.position = Vector3.zero;
        partition.transform.localScale = Vector3.one;
        GameObject tempNote = null;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space) && creationMode)
            {
                tempNote = Instantiate(note,transform.position,Quaternion.identity);
                tempNote.transform.SetParent(partition.transform);
                tempNote.transform.localScale = Vector3.one;
            }
            yield return null;
        }
    }

}
