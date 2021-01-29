using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tools/Oblstacle List")]
public class ArrayScriptable : ScriptableObject
{
    [SerializeField]
    List<ScriptObj> AllObstacles;

    public List<ScriptObj> GetObstacleList() {
        return AllObstacles;
    }

}
