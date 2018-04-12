using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviours : MonoBehaviour
{

    public static Behaviours instance;
    // the following variables are references to possible targets
    [HideInInspector]
    public GameObject gazedTarget;
    [HideInInspector]
    public GameObject previousGazedTarget;

    private bool displayMessage;

    void Awake()
    {
        // allows this class instance to behave like a singleton 
        instance = this;
    }

    void Update()
    {
        /*
        // use OnGUI to display a GUI.Box on screen
        if (gazedTarget != null)
        {
            if (!"Plane".Equals(gazedTarget.name))
            {
                displayMessage = true;
            }
            else
            {
                displayMessage = false;
            }
        }
        else
        {
            displayMessage = false;
        }
        */

        // use Tooltip to display on a TextMesh
        if (gazedTarget != null)
        {
            Tooltip tooltip = gazedTarget.GetComponent(typeof(Tooltip)) as Tooltip;
            if (tooltip != null)
            {
                tooltip.ShowTooltip();
                previousGazedTarget = gazedTarget;
            }
            else
            {
                ResetTooltip();
            }
        }
        else
        {
            ResetTooltip();
        }
    }

    void ResetTooltip()
    {
        Tooltip tooltip = previousGazedTarget?.GetComponent(typeof(Tooltip)) as Tooltip;
        if (tooltip != null)
            tooltip.HideTooltip();
    }

    /*
    private void OnGUI()
    {
        if (displayMessage)
        {
            GUIStyle boxStyle = GUI.skin.box;
            boxStyle.fontSize = 30;
            boxStyle.wordWrap = true;
            string text = messageText(gazedTarget.name);
            GUI.Box(new Rect(Screen.width / 2 + 50F, Screen.height / 2 + 50F, 300f, 150f), text, boxStyle);
        }
    }
    */

    private string messageText(string objectName)
    {
        string text = objectName;
        switch (objectName)
        {
            case "Kidney_dish":
                text = "Please put surgical instrument in kidney dish, then pass it to surgeon";
                break;
            case "scissor_forceps":
                text = "Please use scissor forceps to hold needle";
                break;
            default:
                break;
        }
        return text;
    }


    /// <summary> 
    /// Determines which obejct reference is the target GameObject by providing its name 
    /// </summary> 
    private GameObject FindTarget(string name)
    {
        GameObject targetAsGO = null;
        switch (name)
        {
            case "needle":
                break;

            case "holder":
                break;

            case "kidney dish":
                break;

            case "this": // as an example of target words that the user may use when looking at an object 
            case "it":  // as this is the default, these are not actually needed in this example 
            case "that":
            default: // if the target name is none of those above, check if the user is looking at something 
                if (gazedTarget != null)
                {
                    targetAsGO = gazedTarget;
                }
                break;
        }
        return targetAsGO;
    }
}
