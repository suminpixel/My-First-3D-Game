using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
//UI 이벤트를 받아 처리하는 핸들러
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler // IBeginDragHandler, IDragHandler : 드래그 관련 인터페이스 
{
        public Action<PointerEventData> OnBeginDragHandler = null;
        public Action<PointerEventData> OnDragHandler = null;

        public Action<PointerEventData> OnClickHandler = null;

        public void OnPointerClick(PointerEventData eventData){
            if(OnClickHandler !=null){
                OnClickHandler.Invoke(eventData);
            }
        }
        public void OnBeginDrag(PointerEventData eventData){ 
            //Debug.Log("OnBeginDrag");

            if(OnBeginDragHandler != null){
                OnBeginDragHandler.Invoke(eventData);  //Invoke: 대리자 실행
            }
            //throw new System.NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData){ //드래그 이벤트 받기
            //transform.position = eventData.position;
            // Debug.Log("OnDrag");
             if(OnDragHandler != null){
                OnDragHandler.Invoke(eventData);  //Invoke: 대리자 실행
            }
            //throw new System.NotImplementedException();
        }
}
