using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class Client2 : MonoBehaviour
{

    public TcpClient TcpClient;
    // Use this for initialization
    void Start()
    {
        Connect();
        StartCoroutine(GetServerTime());
        GameManager.Instance.OnNewMoment += SendMessage;
    }

    public void Connect()
    {
        TcpClient = new TcpClient();
        TcpClient.Connect("192.168.0.150", 8000);
        // use the ipaddress as in the server program
        print("Connected");
    }

    public void SendMessage()
    {

        Stream stm = TcpClient.GetStream();

        MemoryStream ms = new MemoryStream();
        new BinaryFormatter().Serialize(ms,
            new Message.Message(2.ToString(), Message.Message.MessageType.OnNewMoment));

        byte[] size = BitConverter.GetBytes(ms.ToArray().Length);
        // Console.WriteLine("Transmitting.....");
        stm.Write(size, 0, 4);
        stm.Write(ms.ToArray(), 0, ms.ToArray().Length);
    }
    public IEnumerator GetServerTime()
    {

        while (true)
        {
            Stream stm = TcpClient.GetStream(); 


            //MemoryStream ms = new MemoryStream();
            //new BinaryFormatter().Serialize(ms,
            //    new Message.Message(2.ToString(), Message.Message.MessageType.OnGameFinished));

            //byte[] size = BitConverter.GetBytes(ms.ToArray().Length);
            //// Console.WriteLine("Transmitting.....");
            //stm.Write(size, 0, 4);
            //stm.Write(ms.ToArray(), 0, ms.ToArray().Length);



            if (TcpClient.Client.Available > 0)
            {
                byte[] b = new byte[4];
                int k = TcpClient.Client.Receive(b);


                //int word = s.Receive(byteForWord);
                Console.WriteLine("bytes to read " + BitConverter.ToInt32(b, 0));

                Console.WriteLine("Recieved..." + k);

                byte[] byteForWord = new byte[BitConverter.ToInt32(b, 0)];
                TcpClient.Client.Receive(byteForWord);

                var ms1 = new MemoryStream(byteForWord);
                var obj = new BinaryFormatter().Deserialize(ms1) as Message.Message;
                ms1.Close();
                if (obj.Type == Message.Message.MessageType.OnNewMoment)
                {

                    print(" moment");

                }
                else
                {
                    print(obj.Data + " TIme");

                }

                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.01f);

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
