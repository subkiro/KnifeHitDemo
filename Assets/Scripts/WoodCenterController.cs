using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodCenterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject WoodCenter;
    [SerializeField] public GameObject WoodFracturedPrefab;
    [SerializeField] private ArrayScriptable BonusObstacleList;
    [SerializeField] private ArrayScriptable KnifesObstacleList;
    public float fieldOfImpact = 1.22f;
    public WoodCenterObject wood;
    public static WoodCenterController instance;
    private GameObject spawnObj;
    private void Awake()
    {
        if (instance != null) { Destroy(instance.gameObject); }

        instance = this;
        
    }


    void Start()
    {

        EventManagerController.instance.WoodBrokeAction += ExplodeWood;
        EventManagerController.instance.RoundStartAction += Init;
        EventManagerController.instance.LostAction += DeleteWood;
        EventManagerController.instance.RoundFinishedAction += ShowStagePass;
    }

    public void Init()
    {
        
        CreateArray(28);


       
    }


    public void ShowStagePass() {
        if (StageController.instance.stageBullet+1 == 4)
        {
            Camera.main.transform.DOShakeScale(1).OnComplete(() => Menu.instance.ShowUI(1)); //Finish 4th stage
        }
        else
        {
            Camera.main.transform.DOShakeScale(1).OnComplete(() => { EventManagerController.instance.RoundStart(this); });
        }
    }


    public void ExplodeWood(Object sender) {

        Instantiate(WoodFracturedPrefab, this.transform);
        wood.OnDestroyBroke();

        
        


    }

    public void DeleteWood()
    {
        Destroy(wood.gameObject);
        Menu.instance.ShowUI(0);


    }




    public void CreateArray(int numberOfPosition) {
        Vector2[] possitions = new Vector2[numberOfPosition];
        float[] rotations = new float[numberOfPosition];


        float radius = fieldOfImpact;
        float angleStep = (360f / (numberOfPosition));
        Vector2 center = transform.position;
        for (int i = 0; i < numberOfPosition; i++)
            {
                possitions[i] = center + Subkiro.rotate(Vector2.up, angleStep * i)*radius;
                rotations[i] = angleStep * i;
            }

        CreateObstacle(possitions,rotations);

    }


    public void CreateObstacle(Vector2[]  pos, float[] angle) {

        GameObject woodObj = Instantiate(WoodCenter, this.transform);
        wood = woodObj.GetComponent<WoodCenterObject>();

        int minKnifes = GameManager.instance.minKnifeSpawn;
        int minApples = GameManager.instance.minAppleSpawn;

        int maxKnifes = GameManager.instance.maxKnifeSpawn;
        int maxApple = GameManager.instance.maxAppleSpawn;


        int levelOfDifficulty = StageController.instance.stageBullet;
        int[] randArray = Subkiro.GetRandomArray(Random.Range(minApples+minKnifes+ levelOfDifficulty, maxApple+maxKnifes+ levelOfDifficulty*2), pos.Length);
        


        List<ScriptObj> AllObstaclesBonus = this.BonusObstacleList.GetObstacleList();
        List<ScriptObj> AllObstaclesKnifes = this.KnifesObstacleList.GetObstacleList();

        for (int i = 0; i < randArray.Length; i++)
        {
           

            if ( i<maxApple) {
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

        wood.ShowVfx();
    }

    private void OnDisable()
    {
        EventManagerController.instance.WoodBrokeAction -= ExplodeWood;
        EventManagerController.instance.RoundStartAction -= Init;
        EventManagerController.instance.LostAction -= DeleteWood;
        EventManagerController.instance.RoundFinishedAction -= ShowStagePass;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
