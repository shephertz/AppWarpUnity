using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

//using UnityEditor;

using AssemblyCSharp;

public class appwarp : MonoBehaviour {

	//please update with values you get after signing up
	public static string apiKey = "00e9e2a781f1b87e3420b29725081305b689a6d151430bd96be33abc84f5db86";
	public static string secretKey = "a6f73a086535d65d12f1e47e9944f8002778a2a6cd63f414e2fb40f68653f037";
	public static string roomid = "313078471";
	public static string username;
	Listener listen = new Listener();
	public static Vector3 newPos = new Vector3(0,0,0);
	
	void Start () {
		WarpClient.initialize(apiKey,secretKey);
		WarpClient.GetInstance().AddConnectionRequestListener(listen);
		WarpClient.GetInstance().AddChatRequestListener(listen);
		WarpClient.GetInstance().AddLobbyRequestListener(listen);
		WarpClient.GetInstance().AddNotificationListener(listen);
		WarpClient.GetInstance().AddRoomRequestListener(listen);
		WarpClient.GetInstance().AddUpdateRequestListener(listen);
		WarpClient.GetInstance().AddZoneRequestListener(listen);
		
		// join with a unique name (current time stamp)
		username = System.DateTime.UtcNow.Ticks.ToString();
		WarpClient.GetInstance().Connect(username);
		
		//EditorApplication.playmodeStateChanged += OnEditorStateChanged;
		addPlayer();
	}
	
	public float interval = 0.1f;
	float timer = 0;

	
	public static GameObject obj;
	
	public static void addPlayer()
	{
		obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
		obj.transform.position = new Vector3(732f,1.5f,500f);
	}
	
	public static void movePlayer(float x, float y, float z)
	{
		newPos = new Vector3(x,y,z);
	}
	
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0)
		{
			string json = "{\"x\":\""+transform.position.x+"\",\"y\":\""+transform.position.y+"\",\"z\":\""+transform.position.z+"\"}";
			
			listen.sendMsg(json);
			
			timer = interval;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
        	Application.Quit();
    	}
		WarpClient.GetInstance().Update();
		obj.transform.position = Vector3.Lerp(obj.transform.position, newPos, Time.deltaTime);
	}
	
	void OnGUI()
	{
		GUI.contentColor = Color.black;
		GUI.Label(new Rect(10,10,500,200), listen.getDebug());
	}
	
	/*void OnEditorStateChanged()
	{
    	if(EditorApplication.isPlaying == false) 
		{
			WarpClient.GetInstance().Disconnect();
    	}
	}*/
	
	void OnApplicationQuit()
	{
		WarpClient.GetInstance().Disconnect();
	}
	
}
