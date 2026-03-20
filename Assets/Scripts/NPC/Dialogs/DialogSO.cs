using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Dialog")]
public class DialogSO : ScriptableObject
{
    public string title;
    public SpeachNodeSO first_speachNode;

    public NPCSO dialog_starting_npcSO;
}
