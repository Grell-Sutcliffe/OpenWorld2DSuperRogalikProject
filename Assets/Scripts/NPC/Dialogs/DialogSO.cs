using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog/Dialog")]
public class DialogSO : ScriptableObject
{
    public string dialog_name;
    public SpeachNodeSO first_speachNode;
}
