using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum Layer {
        Monster = 8,
        Ground = 9,
        Block = 10,
        
    }

    public enum Scene{
        Unknown,
        Login,
        Lobby,
        Game,

    }
    public enum Sound{
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent{
        Click,
        Drag, 
    }
    public enum MouseEvent{
        Press,
        Click,
        PointerDown, // 뗀 상태에서 마우스를 처음 누름
        PointerUp, // 마우스를 한번 누른상태(몇초간)에서 마우스를 처음 뗌 => 클릭과의 차이륻
    }
    public enum CameraMode{
        QuaterView,
    }
}
