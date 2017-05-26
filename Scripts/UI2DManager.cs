using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI2DManager : MonoBehaviour {



	public Text Score;
	// Use this for initialization
	public void Start()
	{
		
	}
	public void Awake()
	{
		GameManagerBase.Instance.OnScoreChange += OnScoreChange;
	}
	void OnScoreChange()
	{
		Score.text = GameManagerBase.Instance.Score.ToString();
	}

	void Destroy()
	{
		GameManagerBase.Instance.OnScoreChange -= OnScoreChange;
	}
}
