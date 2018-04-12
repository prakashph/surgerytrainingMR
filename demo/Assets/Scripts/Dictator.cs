using HoloToolkit.Unity.InputModule;
using Microsoft.MR.LUIS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Dictator : AttachToController {

    public LuisDictationManager manager;
    protected override void OnAttachToController()
    {
        base.OnAttachToController();
        InteractionManager.InteractionSourceUpdated += InteractionSourceUpdated;
        Debug.Log("Attached to controler");
    }

    private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        if (obj.state.touchpadPressed)
        {
            manager.StartListening();
        }
    }

    protected override void OnDetachFromController()
    {
        base.OnAttachToController();
        InteractionManager.InteractionSourceUpdated -= InteractionSourceUpdated;
    }
}
