using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class BaseParameters
{

    public  static  List<Parameters.ParamsName> Names= new List<Parameters.ParamsName>();

	
	
	public static  Parameters.ParamsName GetParameter(Parameters.ParamsName paramName)
	{
		return Names[(int)paramName];
	}
	static BaseParameters()
    {

	}
}
