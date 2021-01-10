using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stat 이 아니라 Data.Stat 과같이 맨든 이유 =>
//Stat 이나 Contents 이런 흔한이름의 다른 클래스와 중첩될까봐 구분을 위해
namespace Data
{
	//데이터처럼 긴 파일은 리전으로 구분하여 접었다 폈다하게 관리
	#region Stat  

	[Serializable]
	public class Stat
	{
		public int level;
		public int maxHp;
		public int attack;
		public int totalExp;
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
}
