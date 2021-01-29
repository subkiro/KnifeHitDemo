using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodCenterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject WoodCenter;
    [SerializeField] private GameObject WoodFracturedPrefab;
    [SerializeField] private ArrayScriptable BonusObstacleList;
    [SerializeField] private ArrayScriptable KnifesObstacleList;
    public WoodCenterObject wood;
    public static WoodCenterController instance;
    private GameObject spawnObj;
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {

        EventManagerController.instance.RoundFinishedAction += ExplodeWood;
        EventManagerController.instance.RoundStartAction += Init;
        EventManagerController.instance.LostAction += DeleteWood;
    }

    public void Init()
    {
        GameObject woodObj = Instantiate(WoodCenter,this.transform);
        wood = woodObj.GetComponent<WoodCenterObject>();
        CreateArray(28);
        wood.ShowVfx();
    }


   


    public void ExplodeWood() {
        EventManagerController.instance.WoodBroke();
        Instantiate(WoodFracturedPrefab, this.transform);
        wood.Explode();
        

    }

    public void DeleteWood()
    {
        Destroy(wood.gameObject);
        Menu.instance.ShowUI(0);


    }




    public void CreateArray(int numberOfPosition) {
        Vector2[] possitions = new Vector2[numberOfPosition];
        float[] rotations = new float[numberOfPosition];


        float radius = wood.GetComponent<WoodCenterObject>().fieldOfImpact;
        float angleStep = (360f / (numberOfPosition));
        Vector2 center = wood.transform.position;
        for (int i = 0; i < numberOfPosition; i++)
            {
                possitions[i] = center + Subkiro.rotate(Vector2.up, angleStep * i)*radius;
                rotations[i] = angleStep * i;
            }

        CreateObstacle(possitions,rotations);

    }


    public void CreateObstacle(Vector2[]  pos, float[] angle) {

        int[] randArray = Subkiro.GetRandomArray(Random.Range(2, 4), pos.Length);
        


        List<ScriptObj> AllObstaclesBonus = this.BonusObstacleList.GetObstacleList();
        List<ScriptObj> AllObstaclesKnifes = this.KnifesObstacleList.GetObstacleList();

        for (int i = 0; i < randArray.Length; i++)
        {
           

            if ( i==0) {
                spawnObj = AllObstaclesBonus[0].InitializeMyGem(true);
            } else {
                spawnObj = AllObstaclesKnifes[0].InitializeMyGem();
            }


            if (spawnObj != null)
            {
               

                spawnObj.transform.SetParent ( wood.transform);
                spawnObj.transform.position = pos[randArray[i]];
                spawnObj.transform.rotation = Quaternion.identity;
                spawnObj.transform.Rotate(spawnObj.transform.forward, angle[randArray[i]]);

            }

        }

       
       }

    private void OnDisable()
    {
        EventManagerController.instance.RoundFinishedAction -= ExplodeWood;
        EventManagerController.instance.RoundStartAction -= Init;
        EventManagerController.instance.LostAction -= DeleteWood;
    }

}
