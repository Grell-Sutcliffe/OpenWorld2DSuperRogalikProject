using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorPanelScript : MonoBehaviour
{
    public TextMeshProUGUI errorTMP;

    public Dictionary<ErrorType, string> dict_errorType_to_error;

    private void Awake()
    {
        dict_errorType_to_error = new Dictionary<ErrorType, string>();

        dict_errorType_to_error[ErrorType.NotEnoughMaterials] = "Ќедостаточно материалов дл€ совершени€ операции :(";
    }

    public void ShowError(ErrorType errorType)
    {
        ShowError(dict_errorType_to_error[errorType]);
    }

    public void ShowError(string text)
    {
        gameObject.SetActive(true);
        errorTMP.text = text;
    }
}

public enum ErrorType
{
    NotEnoughMaterials = 0,

}
