using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using MyMessage1;
using UnityEngine.SceneManagement;

[System.Serializable]

public enum HiveStates
{
	Playing,
	Walking
}
[System.Serializable]

public class ServerMessage
{
	public ServerMessage(int time, HiveStates state)
	{
		Time = time;
		State = state;
	}

	public int Time;
	public HiveStates State;
}
public class Client : MonoBehaviour
{



	public InputField TeamId;
    public Text RoomTimer;




    private int ID = -1;
	// Use this for initialization

	internal bool SocketReady = false;
	private TcpClient _mySocket;
	private NetworkStream _myStream;
	private StreamWriter _myWriter;
	private StreamReader _myReader;
	private const string Host = "192.168.0.150";
	private const int Port = 8000;
	private bool _playing = true;

	void Start()
	{
		//    _client = new TcpClient("192.168.0.151", 8000);
		SetupSocket();
		StartCoroutine(CheckTime());

		GameManager.Instance.OnNewMoment += OnNewMoment;

	}   

	private void OnNewMoment()
	{
		WriteSocket("recieve");
	    OnNewGameMoment lol = new OnNewGameMoment();
		new BinaryFormatter().Serialize(_myStream, lol);
	}
	public void SetupSocket()
	{


		_mySocket = new TcpClient(Host, Port);

		_myStream = _mySocket.GetStream();
		_myWriter = new StreamWriter(_myStream);
		_myReader = new StreamReader(_myStream);
		SocketReady = true;

	}
	public void WriteSocket(string theLine)
	{
		if (!SocketReady)
			return; 
		string foo = theLine + "\r\n";
		_myWriter.Write(foo);
		_myWriter.Flush();
	}
	public string ReadSocket()
	{
		if (!SocketReady)
			return "";
		if (_myStream.DataAvailable)
			return _myReader.ReadLine();
		return "";
	}
 
    public void SendScore()
	{
		WriteSocket("recieve");
		SendScore lol = new SendScore(0, 100);
		new BinaryFormatter().Serialize(_myStream, lol);
	}
	public void CreateTeam()
	{
		WriteSocket("recieve");
	    if (TeamId == null)
	    {
	        ID = 1;
	    }
	    else
	    {
	        try
	        {
	            int.TryParse(TeamId.text, out ID);
	        }
	        catch (Exception e)
	        {
	            ID = 1;
	            print("ERROR PARSING ID ");
	        }
        }
		TeamCreation lol = new TeamCreation(0, ID);
		new BinaryFormatter().Serialize(_myStream, lol);
		_playing = true;
	}

	public void GetServerMessage()
	{

		WriteSocket("Info");
	    try
	    {

	        var  d = new BinaryFormatter().Deserialize(_myStream);
            if ((d as ServerTime) != null)
            {
                RoomTimer.text = (d as ServerTime).Time.ToString();
            }
        }
	    catch (Exception e)
	    {
	        
	    }
		
		
		//print(d.Time);
	}
	public void CloseSocket()
	{
		if (!SocketReady)
			return;
		_myWriter.Close();
		_myReader.Close();
		_mySocket.Close();
		SocketReady = false;
	}


	
	IEnumerator CheckTime()
	{
		while (true)
		{
			if (_playing)
			{

                GetServerMessage();

			}
			yield return new WaitForSeconds(0.5f);


		}
	}
	// Update is called once per frame
	void Update()
	{
	    if (_myStream.DataAvailable)
	    {
	        
	    }
		//if (Input.GetKeyDown(KeyCode.Space))


		//{
		//	SendScore();
		//}
	}

} 

