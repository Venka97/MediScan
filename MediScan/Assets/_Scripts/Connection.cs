﻿using UnityEngine;
using System.Collections;
using System.Net.Sockets;

public class Connection : MonoBehaviour {

	public static Connection conn;
	// Use this for initialization
	static System.IO.StreamWriter streamWriter;
	static System.IO.StreamReader streamReader;
	static NetworkStream network;
	public static string ailment;
	public static string recCheck;
	public static string homeRemedies;
	public static string suggestions;
	public static string recd;
	public static string recd2;
	void Start () {
		Debug.Log ("initConnection is called");
	}

	public static void initConnection(int ch, string str){



		if (ch == 1) {
			Connection.connectToSocket ();
			streamWriter.WriteLine (str);
			streamWriter.Flush ();
			network.Close ();
		} else if (ch == 2) {
			Debug.Log ("2 IS SELECTED ");
			Connection.connectToSocket ();
			Debug.Log ("Connected");
			if (str == "1")
				ailment = "loose motions";
			else if (str == "2")
				ailment = "sinusitus";
			else if (str == "3")
				ailment = "nausea";
			else if (str == "4")
				ailment = "strains or sprains";
			else if (str == "5")
				ailment = "allergy";
			else if (str == "6")
				ailment = "cold and flu";
			else if (str == "7")
				ailment = "headache";
			else if (str == "8")
				ailment = "acid reflux";
			recd = concatHomeRem (ailment);
			Debug.Log (recd);
			streamWriter.WriteLine (recd);
			streamWriter.Flush ();   
			homeRemedies = streamReader.ReadLine ();
			network.Close ();
		} else if (ch == 3) {
			Connection.connectToSocket ();
			streamWriter.WriteLine ("check;" + ailment + ";" + str);
			streamWriter.Flush();
			recd = streamReader.ReadLine ();
//			recCheck = System.Int32.Parse (recd);
			Debug.Log (recCheck);
			network.Close ();
		} else if (ch == 4) {
			Connection.connectToSocket ();
			streamWriter.WriteLine ("our suggestions;" + ailment);
			streamWriter.Flush ();
			suggestions = streamReader.ReadLine ();
			network.Close ();

		}
			
	}
	//concatHomeRem concatenates the string for when ch == 2
	public static string concatHomeRem(string ail){
		string a;

		a = "home remedies;" + ail;
		return a;
	}

	public static void connectToSocket(){
		TcpClient socket = new TcpClient ();
		Debug.Log ("New TCP Client init");
		socket.Connect ("10.4.59.49", 8154);				//Change IP Address and Port to that of the Server Computer
		network = socket.GetStream ();
 		streamWriter = new System.IO.StreamWriter (network); 
		streamReader = new System.IO.StreamReader (network);

	}

	public static string returnHomeRem(){
		return homeRemedies;
	}

	public static string returnRecomCheck(){
		return recd;
	}

	public static string[] returnSuggestions(){
		string[] s = suggestions.Split (';');
		Debug.Log (s);
		return s;
	}

	void OnApplicationQuit() {
		streamWriter.WriteLine ("exit");
		network.Close ();
	}

	/*
	 * 1. SOS
	 * 2. home remedies
	 * 3. check
	 * 4. our suggestions
	 * 5. exit
	 */


	// Update is called once per frame
}
