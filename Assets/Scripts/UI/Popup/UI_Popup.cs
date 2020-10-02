using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    void Start()
    {
        Managers.UI.SetCanvas(GamObject, true);
    }

}
