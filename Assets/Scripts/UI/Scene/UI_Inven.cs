using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects{
        GridPanel
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(){

        base.Init();
        Bind<GameObject>(typeof(GameObjects));
    
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform){
            Managers.Resource.Destroy(child.gameObject);
        }
        //실제 인벤토리 데이터를 참고하여 Inven Item 삽입
        for(int i = 0; i < 8 ; i++){
            //현재는 임시로 더미 데이터 8개
            //GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            //item.transform.SetParent(gridPanel.transform); //game obj의 부모를 지정해 연결
            
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;         
            //하위 컴포(프리팹인) 인벤 아이템을 바인드    
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>(); 
            invenItem.SetInfo($"집행검{i}번");

        }
    }
}
