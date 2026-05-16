using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "InfoPanel/Info")]
public class InfoSO : ScriptableObject
{
    public Sprite sprite;
    public string title;
    public string description;

    public InfoType infoType;
}
