using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static IEnumerator SlowPrint(string text,float waitTime)
	{
		print(text);
		yield return new WaitForSeconds(waitTime);
	}
	public static IEnumerator SlowPrint(int text, float waitTime)
	{
		print(text);
		yield return new WaitForSeconds(waitTime);
	}
}
