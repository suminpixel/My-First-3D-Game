using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//인게임 씬 타입의 스크립트
public class GameScene : BaseScene
{

    Coroutine co;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        //인게임 씬에서는 UI 팝업창이 열려~
        Managers.UI.ShowSceneUI<UI_Inven>();

        //임시로
        //for (int i = 0; i < 5; i++){
        //    Managers.Resource.Instantiate("UnityChan");
        //}

        //co = StartCoroutine("CoExplodeAfterSeconds", 4.0f);
        //StartCoroutine("CoStopExplode", 3.0f);

        //임시로 
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

    }

    /*
     * 코루틴 테스트 코드 임시로
     * 
    IEnumerator CoExplodeAfterSeconds(float seconds){
        Debug.Log("Explode Enter");
        // WaitForSeconds 를 반환하면 유니티엔진 자체에서 체크하여 n초간 기다리는 녀석이구나를 인지
        yield return new WaitForSeconds(seconds); 
        Debug.Log("Explode Execute ");
        co = null;
    }


    IEnumerator CoStopExplode(float seconds){
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds); 
        Debug.Log("Stop Execute ");
        if( co != null){
            //만약 들고있던 코루틴을 멈추고싶은 경우            
            StopCoroutine(co);
        }
    }
    */
    public override void Clear()
    {
        
    }


}
