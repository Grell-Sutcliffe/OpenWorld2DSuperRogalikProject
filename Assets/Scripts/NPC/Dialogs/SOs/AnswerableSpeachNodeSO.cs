using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnswerableSpeachNode", menuName = "Dialog/AnswerableSpeachNode")]
public class AnswerableSpeachNodeSO : SpeachNodeSO
{
    public List<AnswerSO> answerSOs;
}
