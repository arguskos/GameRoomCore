using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MinMaxPair  {

	public MinMaxPair(float min,float max)
	{
	    Max = max;
	    Min = min;
	}

    public float Max ;
    public float Min;
}
