using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base //UI 베이스를 상속받아 get, bind 를 쓸 수 있게 함
{

    void Start()
    {
        Managers.UI.SetCanvas(GameObejct, false);
    }

}
