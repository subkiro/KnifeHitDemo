using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Tools/Obstacle Obj")]
public class ScriptObj : ScriptableObject
{
    public string id;
    public string Name;
    public float SpownChance = 100;
    public GameObject Prefab;
    public GameObject VFX_Explosion;


    public GameObject InitializeMyGem(bool useChance = false)
    {
        if (!useChance) {
            SpownChance = 1000;
        }

        if (SpownChance >= Random.Range(0, 100))
        {
            GameObject GemOBJ = Instantiate(Prefab);
            Obstacle tmp = GemOBJ.AddComponent<Obstacle>();
            tmp.InitVisuals(id, Name, VFX_Explosion);

            return GemOBJ;
        }
        else {
            Debug.Log("Return Null");
            return null;
        }
        
    }


}
