using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//데이터처럼 긴 파일은 리전으로 구분하여 접었다 폈다하게 관리
#region Stat 

[Serializable]
public class Stat
{
	public int level;
	public int hp;
	public int attack;
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
	public List<Stat> stats = new List<Stat>();

	public Dictionary<int, Stat> MakeDict()
	{
		Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
		foreach (Stat stat in stats)
			dict.Add(stat.level, stat);
		return dict;
	}
}

#endregion