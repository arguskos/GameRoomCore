using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{


	public GameMoment[] Moments;
	private static int _score ;
	public static int Score { get
		{
			return _score;
		}
		set
		{
			_score = value;
			if (OnScoreChange!=null)
				OnScoreChange();
		}
	}
	public float GameTime = 20;
	private float _passedTime;
	private int _currentMoment;
	public List<Dictionary<Parameters.ParamsName, MinMaxPair>> AllData = new List<Dictionary<Parameters.ParamsName, MinMaxPair>>();

	public List<List<MomentInfo>> AllMomentsInfo = new List<List<MomentInfo>>();

	private Dictionary<Parameters.ParamsName, float> ReturnInfo = new Dictionary<Parameters.ParamsName, float>();

	//private List<MomentInfo>  ReturnInfo = new List<MomentInfo>();
	public Action OnNewMoment;
	
	public float GetParameter(Parameters.ParamsName name)
	{
		
		return ReturnInfo[Parameters.GetParameter(name)];
	}


	public delegate void ScoreChange();
	public  static  ScoreChange OnScoreChange;

	/// <summary>
	/// Create serise of parameters with name (moments)
	/// game manager interpolate between time with min max moments
	/// All who needs this info can take it
	/// For every object in a scene with interface bla call function do on spread calls....
	/// 
	/// </summary>

	// Use this for initialization
	protected virtual void Start()
	{
		Score = 0;
		//TargetSpeed = Moments[_currentMoment].MinTargetSpeed;
		//WallsToPlace = Moments[_currentMoment].MinWalls;
		//get all data 
		foreach (var moment in Moments)
		{
			moment.Init();

			AllData.Add(moment.MomentInfo);
		}
		if (AllData.Count > 0)
		{
			foreach (var entery in AllData[0])
			{

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


	// Update is called once per frame
	protected virtual void Update()
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
				print("Current moment"+_currentMoment+" of all "+ Moments.Length);
				_currentMoment++;
			    if (OnNewMoment != null)
			    {
			        OnNewMoment();
                }
            }
		}
	}
}
