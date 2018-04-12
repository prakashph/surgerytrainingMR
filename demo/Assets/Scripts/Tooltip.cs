using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{

    public string text;
    public TextMesh textMesh;

    public void ShowTooltip()
    {
        // when this method is called
        // the tooltip text should appear in
        // the output text mesh

        if (textMesh != null)
        {
            textMesh.text = text;
        }
    }

    public void HideTooltip()
    {
        if (textMesh != null)
        {
            textMesh.text = "";
        }
    }
}
