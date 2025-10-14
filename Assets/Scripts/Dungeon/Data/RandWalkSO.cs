using UnityEngine;


[CreateAssetMenu(fileName = "RandWalkPar_", menuName = "PNG/RandomWalkData")]
public class RandWalkSO : ScriptableObject
{


    public int iter = 10, walkLen = 10;
    public bool startRandEachIter = true;
}
