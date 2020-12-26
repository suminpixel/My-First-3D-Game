using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    int _layerMask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    Texture2D _attackIcon;
    Texture2D _handIcon;


    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }
        //몬스터냐, 땅이냐에 따라 커서 텍스쳐 변화
        // 마우스 클릭시 레이 캐스팅
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, _layerMask))
        {

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }

            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_attackIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;

                }
            }
        }
    }
}
