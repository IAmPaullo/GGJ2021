using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tumba : MonoBehaviour
{
    public TextMeshProUGUI tumbaText;

    public void SetText(string text)
    {
        tumbaText.text = text;
    }
}
