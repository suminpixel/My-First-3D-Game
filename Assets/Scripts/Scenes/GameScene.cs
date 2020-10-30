using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//인게임 씬 타입의 스크립트
public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        //인게임 씬에서는 UI 팝업창이 열려~
        Managers.UI.ShowSceneUI<UI_Inven>();

          //임시로
        for (int i = 0; i < 5; i++){
            Managers.Resource.Instantiate("UnityChan");
        }
    }
    public override void Clear()
    {
        
    }


}
