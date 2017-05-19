using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameMoment", menuName = "Moment", order = 1)]
public class GameMoment : ScriptableObject {

	// Use this for initialization
    //public float MinWalls;
    //public float MaxWalls;
    //public string Walls=Parameters.Names[(int)Parameters.ParamsName.WallsAmount];

    //public float MinTargetSpeed;
    //public float MaxTargetSpeed;
    //public string Target = Parameters.Names[(int)Parameters.ParamsName.TargetSpeed];


	public float SongTempoMin;
	public float SongTempoMax;
	public string SongTempo = Parameters.GetParameter(Parameters.ParamsName.SongTempo);

	public float ToleranceErrorTimeMin;
	public float ToleranceErrorTimeMax;
	public string ToleranceErrorTime = Parameters.GetParameter(Parameters.ParamsName.ToleranceErrorTime);




	public Dictionary<string, MinMaxPair> MomentInfo = new Dictionary<string, MinMaxPair>();    
    public void Init () {
		MomentInfo.Add(SongTempo, new MinMaxPair(SongTempoMin, SongTempoMax));
		MomentInfo.Add(ToleranceErrorTime, new MinMaxPair(ToleranceErrorTimeMin, ToleranceErrorTimeMax));



	}

	// Update is called once per frame
	void Update () {
	}
}
