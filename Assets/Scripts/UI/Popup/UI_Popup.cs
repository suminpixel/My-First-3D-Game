using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{

    public virtual void Init(){ //start() 에서 관리하는것보다 init()함수를 파서 사용하는게 좋음
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI(){ 
        Managers.UI.ClosePopupUI(this);
    }
}
