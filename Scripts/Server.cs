﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
//using Random = UnityEngine.Random;

//public enum RoomsID
//{
//    Debref = 9999,
//    Bref = 9998,
//    Between = 9997,
//    Game = 9996
//}
//public class Team
//{
//    public string TeamName;
//    public int RoomNumber;
//    public int TeamId;
//    public int ScoreRoom;
//    public float RoomTime;
//    public int ScoreAll;
//}

//public class Room
//{
//    public int RoomTime;

//}

public class Server : MonoBehaviour
{


	public Server Instance;
    public  Text RoomTimer;
    public  string TeamName="superTam";
    private   float _localTimer;


	public void Awake()
	{
		if (Instance==null)
		{
			Instance = this;
		}
	}

	public  void IncScore()
    {
		//.text = 
		//(Int32.Parse(Score.text) + 1).ToString();
    }
    void  Start()
    {
        //AndroidNFCReader.enableBackgroundScan();
        //AndroidNFCReader.ScanNFC(gameObject.name, "OnFinishScan");

        CreateTeam();
       // Score.text = "0";
    }

    private int ID = 0;
    public  void CreateTeam()
    {

        //t.teamId = PlayerPrefs.GetInt("teamId", 0); ;
        //if (t.teamId == 0)
        //{
        //    t.teamId = Random.Range(0, 333);
        //}
        //PlayerPrefs.SetInt("teamId", t.teamId);
        string url = "http://192.168.0.151:8000/TeamCreation/0";
        Debug.Log("SADASDAPSKKDPS TEAM  CREATIN");
        WWWForm form = new WWWForm();
        form.AddField("name", TeamName);
        byte[] rawData = form.data;
        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);

        StartCoroutine(RequestTeamCreation(www));
    }


    public void EnterRoom()
    {
        string url = "http://192.168.0.151:8000/Enter/" + ID;

        WWWForm form = new WWWForm();
        form.AddField("", "bla");
        byte[] rawData = form.data;
        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);

        StartCoroutine(RoomEnterRequest(www));



    }

    public void LeaveRoom()
    {
        string url = "http://192.168.0.151:8000/Finish/" + ID;

        WWWForm form = new WWWForm();
        form.AddField("score", GameManagerBase.Score);
        byte[] rawData = form.data;

        // headers.Add("X-Auth-Token", "7da4596d42e24f9798d73ec40bbbbd81");

        var headers = new Dictionary<string, string>();
        WWW www = new WWW(url, rawData, headers);


        StartCoroutine(RoomEndRequest(www));


    }
    IEnumerator GetTimeRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
        
            float t = 0;
            float.TryParse(www.data, NumberStyles.Float, null, out t);
            int time = ((int)t);
            RoomTimer.text = (Math.Floor(time / 60.0f).ToString() + ":" + time % 60);
            //Debug.Log(time);
            if (time == -1)
            {
                LeaveRoom();
            }

            // Debug.Log(r.RoomTime);
            // t.RoomTime = r.RoomTime;
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    void Update()
    {

        _localTimer += Time.deltaTime;
        if (_localTimer >= 0.5f)
        {
            _localTimer = 0;
            string url = "http://192.168.0.151:8000/Time/" + ID;

            WWW www = new WWW(url);

            StartCoroutine(GetTimeRequest(www));
            
        }


    }
    IEnumerator RoomEnterRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW ENTERED!: " + www.data);

     

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }


    IEnumerator RoomEndRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW END!: " + www.data);

            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
    IEnumerator RequestTeamCreation(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW TEAMCREATIOn: " + www.data);

            ID = int.Parse(www.data);
            Debug.Log(ID);
            //Debug.Log(t.TeamId+"----"+t.RoomNumber.ToString());
            EnterRoom();

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

}
