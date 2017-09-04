using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script d'instanciation de la road par rapport à une image qui est éclatée 
/// pixel par pixel en profondeur sur des GameObjetcts
/// </summary>
public class RoadManager : MonoBehaviour {

    public GameObject RoadPart;
    public Material material;
    public float deltaRoadScale;
    public int roadLenght;
    public List<GameObject> roads = new List<GameObject>();
    private PlayerStatManager playerStat;
	void Start () {

        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatManager>();

        switch(playerStat.NvOutRun)
        {
            case 1:
                {
                    SoundManager.instance.LoadMusic("F1 Havas Run1", 0.6f);
                    break;
                }
            case 2:
                {
                    SoundManager.instance.LoadMusic("F1 Havas Run 2", 0.6f);
                    break;
                }
        }

        float deltaTextureOffset = material.mainTextureOffset.y;
        roads.Add(RoadPart);

        Material tempMaterial = material;

        int i;
        Vector3 scale = RoadPart.transform.localScale;
        for (i = 1; i < roadLenght; i++)
        {
            GameObject roadPart = Instantiate(RoadPart, RoadPart.transform.localPosition* i * deltaRoadScale, Quaternion.identity);
            roadPart.transform.localScale = new Vector3((scale.x/**0.9975f*/) - deltaRoadScale, scale.y,1.0f);
            roadPart.GetComponent<UpdateRoadPart>().idRoad = i;
            roadPart.GetComponent<Renderer>().material = tempMaterial;
            roadPart.GetComponent<Renderer>().material.mainTextureOffset = Vector2.up * i * deltaTextureOffset;
            scale = roadPart.transform.localScale;
            roadPart.transform.SetParent(transform);
            roadPart.GetComponent<UpdateRoadPart>().idRoad = i;
            roads.Add(roadPart);
        }


    }

}
