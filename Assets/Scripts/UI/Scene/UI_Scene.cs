using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base //UI 베이스를 상속받아 get, bind 를 쓸 수 있게 함
{

    void Start()
    {
       Init();
    }

    public virtual void Init(){ //start() 에서 관리하는것보다 init()함수를 파서 사용하는게 좋음
        Managers.UI.SetCanvas(gameObject, false);
    }

}
