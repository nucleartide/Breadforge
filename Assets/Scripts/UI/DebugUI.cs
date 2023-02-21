using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text debugText;

    [SerializeField]
    UnityEngine.UI.RawImage debugImage;

    public void SetText(string text, Color color)
    {
        debugText.text = text;
        debugImage.color = color;
    }
}
