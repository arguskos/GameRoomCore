using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class Parameters
{

    public static List<string> Names= new List<string>();

    public enum ParamsName
    {
        WallsAmount,
        TargetSpeed
    };
	public static string GetParameter(int index)
	{
		return Names[index];
	}
	public static string GetParameter(ParamsName paramName)
	{
		return Names[(int)paramName];
	}
	static Parameters()
    {
        Names.Add("WallsAmmount");
        Names.Add("TargetSpeed");

    }
}
