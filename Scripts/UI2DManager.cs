using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI2DManager : MonoBehaviour {



	public Text Score;

    public Text DebugScore;
    public Text Time;

    // Use this for initialization
    public void Start()
	{
	    GameManagerBase.OnScoreChange += OnScoreChange;

    }
    public void Awake()
	{

    }
    void OnScoreChange()
	{
		Score.text = GameManagerBase.Score.ToString();
	    if (DebugScore)
	    {
		    DebugScore.text = GameManagerBase.Score.ToString();

        }
    }

	void Destroy()
	{
		GameManagerBase.OnScoreChange -= OnScoreChange;
	}
}
