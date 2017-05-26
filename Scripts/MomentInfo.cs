using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MomentInfo

{
	public MomentInfo(string name)
	{
		Name = name;
		MinMax = new MinMaxPair(0, 0);
	}
	public string Name;
	public MinMaxPair MinMax;
}

