using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup 
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
        Init();
        
    }


    public override void Init(){
        base.Init(); //부모의 init 까지 호출

        //base.Init();
        
        Bind<Button>(typeof(Buttons)); //리플렉션을 이용해서 Button의 enum 타입을 넘김(바인딩)
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //GetText((int) Texts.ScoreText).text = "Bind Text";

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked); // 아래 처럼 여러줄로 하지 않고 1줄로 처리 (Util > Extension.cs)
        //GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // ItemIcon 이라는 이름의 GameImage를 받아옴
        
        AddUIEvent(go, ((PointerEventData data)=>{  
            go.transform.position = data.position;
        }), Define.UIEvent.Drag);
        
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);


    }


    int _score = 0;


    public void OnButtonClicked(PointerEventData data)
    {
        _score ++;

        Debug.Log($"-- UI Button Clicked : {_score}");

        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
   
        //_text.text = $"점수 : {_score}";
    }
}
