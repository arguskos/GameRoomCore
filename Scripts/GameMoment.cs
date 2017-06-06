using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameMoment", menuName = "Moment", order = 1)]
[System.Serializable]

public class GameMoment : ScriptableObject {

	// Use this for initialization
    //public float MinWalls;
    //public float MaxWalls;
    //public string Walls=Parameters.Names[(int)Parameters.ParamsName.WallsAmount];

    //public float MinTargetSpeed;
    //public float MaxTargetSpeed;
    //public string Target = Parameters.Names[(int)Parameters.ParamsName.TargetSpeed];


	//public float SongTempoMin;
	//public float SongTempoMax;
	//public string SongTempo = Parameters.GetParameter(Parameters.ParamsName.SongTempo);

	//public float ToleranceErrorTimeMin;
	//public float ToleranceErrorTimeMax;
	//public string ToleranceErrorTime = Parameters.GetParameter(Parameters.ParamsName.ToleranceErrorTime);




	public Dictionary<Parameters.ParamsName, MinMaxPair> MomentInfo = new Dictionary<Parameters.ParamsName, MinMaxPair>();
	public List<MomentInfo> MomentInfo2;

	public void Init () {
		foreach (var moment in MomentInfo2)
		{
			MomentInfo.Add(moment.Name, new MinMaxPair(moment.MinMax.Min, moment.MinMax.Max));
		}


	}

	// Update is called once per frame
	void Update () {
	}
}
