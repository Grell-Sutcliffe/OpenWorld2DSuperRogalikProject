using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [Header("State Group Parameter Names")]
    [SerializeField] private string groundedParameterName = "Grounded";
    [SerializeField] private string movingParameterName = "Moving";

    [Header("Grounded Parameter Names")]
    [SerializeField] private string idleParameterName = "isIdling";
    [SerializeField] private string runParameterName = "isRunning";

    [Header("Moving Input Parameter Names")]
    [SerializeField] private string moveXParameterName = "MoveX";
    [SerializeField] private string moveYParameterName = "MoveY";


    public int GroundedParameterHash { get; private set; }
    public int MovingParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int MoveXParameterHash { get; private set; }
    public int MoveYParameterHash { get; private set; }

    public void Initialize()
    {
        GroundedParameterHash = Animator.StringToHash(groundedParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        MoveXParameterHash = Animator.StringToHash(moveXParameterName);
        MoveYParameterHash = Animator.StringToHash(moveYParameterName);
    }

}
