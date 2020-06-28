using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Base //UI 베이스를 상속받아 get, bind 를 쓸 수 있게 함
{
  
  
    //[SerializeField]
    //Text _tex;
    
 
    enum Buttons{
        PointButton //객체명, 버튼 종류별로 포함시킴
    }

    enum Texts{
        PointText, //객체명, 텍스트 종류별로 포함시킴
        ScoreText,
    }


    enum GameObjects{
        TestObject,
        
    }
   
    enum Images{
        ItemIcon,
    }

    private void Start(){
        
        Bind<Button>(typeof(Buttons)); //리플렉션을 이용해서 Button의 enum 타입을 넘김(바인딩)
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //GetText((int) Texts.ScoreText).text = "Bind Text";

        GetButton((int) Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked); // 아래 처럼 여러줄로 하지 않고 1줄로 처리 (Util > Extension.cs)

        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // ItemIcon 이라는 이름의 GameImage를 받아옴
        AddUIEvent(go, ((PointerEventData data)=>{  
            go.transform.position = data.position;
        }), Define.UIEvent.Drag);
    

    }


    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {

        Debug.Log("-- UI Button Clicked");
        _score ++;
        GetText((int) Texts.ScoreText).text = $"점수 : {_score}";
   
        //_text.text = $"점수 : {_score}";
    }
}
