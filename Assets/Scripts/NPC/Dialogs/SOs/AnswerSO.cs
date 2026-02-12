using UnityEngine;

[CreateAssetMenu(fileName = "Answer", menuName = "Dialog/Answer")]
public class AnswerSO : ScriptableObject
{
    public string answer_text;
    // public AnswerType type_of_answer_text;
    public SpeachNodeSO next_speachNode;
}
