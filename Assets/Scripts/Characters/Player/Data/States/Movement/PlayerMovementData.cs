using System;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(1f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}
