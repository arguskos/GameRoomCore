using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MomentInfo

{
	public MomentInfo(Parameters.ParamsName name)
	{
		Name = name;
		MinMax = new MinMaxPair(0, 0);
	}
	public Parameters.ParamsName Name;
	public MinMaxPair MinMax;
}

