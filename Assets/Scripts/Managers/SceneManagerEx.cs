﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    // 현재 사용중인 씬 리턴
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }


    //씬 로드
    public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        //CurrentScene.Clear(); //현재 씬을 클리어 후에
        SceneManager.LoadScene(GetSceneName(type)); //새로 원하는 씬 로드 
    }

    //씬 종류로 이름 리턴
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }


    public void Clear()
    {
        CurrentScene.Clear();
    }

}
