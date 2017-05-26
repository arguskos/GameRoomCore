using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{

	public static GameManagerBase Instance;

	public GameMoment[] Moments;
	private int _score ;
	public int Score { get
		{
			return _score;
		}
		set
		{
			_score = value;
			OnScoreChange();
		}
	}
	public float GameTime = 20;
	private float _passedTime;
	private int _currentMoment;
	public List<Dictionary<string, MinMaxPair>> AllData = new List<Dictionary<string, MinMaxPair>>();

	public List<List<MomentInfo>> AllMomentsInfo = new List<List<MomentInfo>>();

	private Dictionary<string, float> ReturnInfo = new Dictionary<string, float>();

	//private List<MomentInfo>  ReturnInfo = new List<MomentInfo>();

	enum Parametrs { TargetSpeed, WallsAmout }
	public float GetParameter(string name)
	{
		return ReturnInfo[name];
	}
	public delegate void ScoreChange();
	public  ScoreChange OnScoreChange;

	/// <summary>
	/// Create serise of parameters with name (moments)
	/// game manager interpolate between time with min max moments
	/// All who needs this info can take it
	/// For every object in a scene with interface bla call function do on spread calls....
	/// 
	/// </summary>
	void Awake()
	{
		Instance = this;

	}
	// Use this for initialization
	protected void Start()
	{
		print("start");
		Score = 0;
		//TargetSpeed = Moments[_currentMoment].MinTargetSpeed;
		//WallsToPlace = Moments[_currentMoment].MinWalls;
		//get all data 
		foreach (var moment in Moments)
		{
			moment.Init();
			print(moment.name);
			AllData.Add(moment.MomentInfo);
		}
		if (AllData.Count > 0)
		{
			foreach (var entery in AllData[0])
			{
				print(entery.Key);
				ReturnInfo.Add(entery.Key, entery.Value.Min);

			}
		}
	}

	private void OnRoomEnter()
	{
		//connect to server 
	}

	private void OnRoomExit()
	{
		//send score...
	}
	public void OnNewMoment()
	{
		//WallsPlacer.Instance.OnNewMoment();
	}

	// Update is called once per frame
	protected void Update()
	{

		
		_passedTime += Time.deltaTime;
		//print((_passedTime - GameTime / Moments.Length * _currentMoment+GameTime/Moments.Length) / 3.333*(_currentMoment+1));
		//print((GameTime / Moments.Length * (_currentMoment + 1)));
		if (_currentMoment < Moments.Length)
		{

			float percentageForCurrentMoment = (_passedTime - (GameTime / Moments.Length * _currentMoment)) / (GameTime / Moments.Length);
			//TargetSpeed = (Moments[_currentMoment].MaxTargetSpeed- Moments[_currentMoment].MinTargetSpeed )* percentageForCurrentMoment;
			//WallsToPlace =(int)((Moments[_currentMoment].MaxWalls - Moments[_currentMoment].MinWalls) * percentageForCurrentMoment );

			foreach (var entery in AllData[_currentMoment])
			{
				if (entery.Value.Max == entery.Value.Min)
				{
					ReturnInfo[entery.Key] = entery.Value.Max;

				}
				else
				{
					ReturnInfo[entery.Key] = entery.Value.Min+(entery.Value.Max - entery.Value.Min) * percentageForCurrentMoment;

				}
			}

			if (_passedTime > GameTime / Moments.Length * (_currentMoment + 1))
			{
				print(_currentMoment);
				_currentMoment++;
				OnNewMoment();
			}
		}
	}
}
