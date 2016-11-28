using UnityEngine;
using System.Collections;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

using System;
using System.Text;

using System.Collections.Generic;
//using AssemblyCSharp;
using UnityEngine.UI;
using System.IO;
public class AppWarpManager : MonoBehaviour, ConnectionRequestListener, ZoneRequestListener, ChatRequestListener, LobbyRequestListener, NotifyListener, RoomRequestListener, UpdateRequestListener {



	private int viewHeight = 25;

	private int viewWidth = (Screen.width *3)/4 ;
	private int leftGap = 10;
	private int topGap = 30;
	private int fontSize=Screen.width / 45;

    public  string ApiKey ="ApI Key";
	public  string SecretKey="Secret Key";

	private string userName = "userName";
	public string appWarpResponse="";
	public string notifications="";
	private string roomName = "testRoom";
	private string maxusers = "3";
	private string toUser = "1";
	public string roomId = "";
	private string message="chat message";
	private WarpClient theClient;
//	MyListener listen;
	Rect myRect;
	private int widthGap=110;
	// Use this for initialization
	void Start () {
		
		Debug.Log ("Start called of AppWarpManager");

	}

	//Update
	void Update(){
		if (theClient != null)
			theClient.Update ();
	}

	private void initAppWarp(){
		if (theClient != null)
			return;
		WarpClient.initialize(ApiKey,SecretKey);
		WarpClient.setRecoveryAllowance (60);

		WarpClient.GetInstance().AddConnectionRequestListener(this);
		WarpClient.GetInstance().AddChatRequestListener(this);
		WarpClient.GetInstance().AddLobbyRequestListener(this);
		WarpClient.GetInstance().AddNotificationListener(this);
		WarpClient.GetInstance().AddRoomRequestListener(this);
		WarpClient.GetInstance().AddUpdateRequestListener(this);
		WarpClient.GetInstance().AddZoneRequestListener(this);
		theClient = WarpClient.GetInstance ();
	}

//	OnGUI
	void OnGUI()
	{
		GUIStyle style=new GUIStyle();
		style.fontSize = fontSize;

		//Row One
		userName = GUI.TextField (new Rect (leftGap, topGap, 100, viewHeight),userName);
		if (GUI.Button(new Rect (leftGap+widthGap*1, topGap, 100, viewHeight), "Connect"))
		{
			initAppWarp ();
			theClient.Connect (userName);
		}
		if (GUI.Button(new Rect (leftGap+widthGap*2, topGap, 100, viewHeight), "DisConnect"))
		{
			theClient.Disconnect ();
		}
		if (GUI.Button(new Rect (leftGap+widthGap*3, topGap, 130, viewHeight), "GetOnLIneUsers"))
		{
			theClient.GetOnlineUsers ();
		}
		if (GUI.Button(new Rect (leftGap+widthGap*4+30, topGap, 130, viewHeight), "GetUser Info"))
		{
			theClient.GetLiveUserInfo (userName);
		}

		//Row Two
		roomName = GUI.TextField (new Rect (leftGap, topGap*2, 100, viewHeight),roomName);
		maxusers = GUI.TextField (new Rect (leftGap+widthGap*1, topGap*2, 100, viewHeight),maxusers);
		if (GUI.Button(new Rect (leftGap+widthGap*2, topGap*2, 130, viewHeight), "Create Room"))
		{
			
			theClient.CreateRoom (roomName,userName,int.Parse(maxusers),null);
		}
		if (GUI.Button(new Rect (leftGap+widthGap*3+30, topGap*2, 130, viewHeight), "Get All Rooms"))
		{
			theClient.GetAllRooms ();
		}
		//Row 3 For Lobby
		roomId = GUI.TextField (new Rect (leftGap, topGap*3, 100, viewHeight),roomId);

		if (GUI.Button(new Rect (leftGap+widthGap*1, topGap*3, 100, viewHeight), "JoinRoom"))
		{
			theClient.JoinRoom (roomId);
		}
		if (GUI.Button(new Rect (leftGap+widthGap*2, topGap*3, 100, viewHeight), "LeaveRoom"))
		{
			theClient.LeaveRoom (roomId);
		}
		if (GUI.Button(new Rect (leftGap+widthGap*3, topGap*3, 130, viewHeight), "SubscribeRoom"))
		{
			theClient.SubscribeRoom (roomId);
		}
		if (GUI.Button(new Rect (leftGap+widthGap*4+30, topGap*3, 130, viewHeight), "GetRoomInfo"))
		{
			theClient.GetLiveRoomInfo (roomId);
		}
		if (GUI.Button(new Rect (leftGap+widthGap*5+50, topGap*3, 130, viewHeight), "UnSubscribeRoom"))
		{
			theClient.UnsubscribeRoom (roomId);
		}

		//Row 4
		if (GUI.Button(new Rect (leftGap, topGap*4, 100, viewHeight), "SubLobby"))
		{
			theClient.SubscribeLobby ();
		}
		if (GUI.Button(new Rect (leftGap+widthGap*1, topGap*4, 100, viewHeight), "JoinLooby"))
		{
			theClient.JoinLobby ();
		}
		if (GUI.Button(new Rect (leftGap+widthGap*2, topGap*4, 130, viewHeight), "GetLobby Info"))
		{
			theClient.GetLiveLobbyInfo ();
		}
		if (GUI.Button(new Rect (leftGap+widthGap*3+30, topGap*4, 130, viewHeight), "LeaveLobby"))
		{
			theClient.LeaveLobby ();
		}
		// Row 5

		message = GUI.TextField (new Rect (leftGap, topGap*5, 100, viewHeight),message);
		toUser = GUI.TextField (new Rect (leftGap+widthGap*1, topGap*5, 100, viewHeight),toUser);
		if (GUI.Button(new Rect (leftGap+widthGap*2, topGap*5, 100, viewHeight), "Send Chat"))
		{
			theClient.SendChat (message);
		}
		
		if (GUI.Button(new Rect (leftGap+widthGap*3, topGap*5, 130, viewHeight), "Private Chat"))
		{
			theClient.sendPrivateChat (toUser,message);
		}
		

		//API Key and Secret Key

		ApiKey = GUI.TextField (new Rect (leftGap, topGap*6, 600, 20),ApiKey);
		SecretKey = GUI.TextField (new Rect (leftGap, topGap*7, 600, 20),SecretKey);
		//Row 6
		GUI.Label(new Rect (leftGap, topGap*7+20, 100, viewHeight),"Responses");
		appWarpResponse = GUI.TextField (new Rect (leftGap, topGap*8+10, 600, 180),appWarpResponse);	

		GUI.Label(new Rect (leftGap, topGap*8+200, 100, viewHeight),"Notifications");

		if (GUI.Button(new Rect (leftGap+widthGap*6, topGap*7, 80, viewHeight), "Clear"))
		{
			appWarpResponse = "";
			notifications = "";
			notifications = "";
		}

	
		notifications = GUI.TextField (new Rect (leftGap, topGap*9+200, 600, 180),notifications);

	
		//Row 7

	}

	//Application Quit
	void OnApplicationQuit()
	{
		//WarpClient.GetInstance().Disconnect();
	}


	
	public void onConnectDone(ConnectEvent eventObj){
		
		Debug.Log ("onConnectDone : " + eventObj.getResult());
		if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {
//			WarpClient.GetInstance ().CreateRoom ("Testwww", "Himawwnshu", 5, null);
			appWarpResponse += "onConnected Successfully " + eventObj.getResult ()+"\n";
		} else if (eventObj.getResult () == WarpResponseResultCode.CONNECTION_ERROR_RECOVERABLE) {
			//			WarpClient.GetInstance ().CreateRoom ("Testwww", "Himawwnshu", 5, null);
			theClient.RecoverConnection();
			appWarpResponse += "Coonection error Recoverable " + eventObj.getResult ()+"\n";
		} else if (eventObj.getResult () == WarpResponseResultCode.SUCCESS_RECOVERED) {
			//			WarpClient.GetInstance ().CreateRoom ("Testwww", "Himawwnshu", 5, null);
			appWarpResponse += "Coonection Recoverd "+ "\n";
		} 
		else if (eventObj.getResult () == WarpResponseResultCode.AUTH_ERROR) {
			//			WarpClient.GetInstance ().CreateRoom ("Testwww", "Himawwnshu", 5, null);
			appWarpResponse += "Authentication Error" +"\n";
		} 
		else
		{
			appWarpResponse+="onConnectDone Error "+eventObj.getResult()+"\n";
		}
		//JoinRoom ();
		//Log ("onConnectDone : " + eventObj.getResult());
	}
	
	public void onDisconnectDone (ConnectEvent eventObj){
		appWarpResponse+="onDisconnectDone"+eventObj.getResult()+"\n";
		Debug.Log ("onDisConnectDone : " + eventObj.getResult());
	}
	
	public void onJoinRoomDone (RoomEvent eventObj){
		Debug.Log ("onJoinRoomDone : " + eventObj.getResult());
		appWarpResponse+="onJoinRoomDone"+eventObj.getResult()+"\n";
	}
	
	public void onGetLiveRoomInfoDone (LiveRoomInfoEvent eventObj){
		appWarpResponse+="onGetLiveRoomInfoDone : " + eventObj.getResult()  + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId() + ", RoomOwner : " + eventObj.getData().getRoomOwner() + ", MaxUsers : " + eventObj.getData().getMaxUsers()+"\n";
		Debug.Log ("onGetLiveRoomInfoDone : " + eventObj.getResult()  + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId() + ", RoomOwner : " + eventObj.getData().getRoomOwner() + ", MaxUsers : " + eventObj.getData().getMaxUsers());
	}
	
	public void onInitUDPDone (byte resultCode){
		appWarpResponse += "onInitUDPDone " + resultCode + "/n";
	}
	
	public void onSendChatDone (byte result){
		notifications += "onSendChatDone " + result + "/n";
		Debug.Log ("onSendChatDone : " + result);
	}
	
	public void onSendPrivateChatDone (byte result){
		notifications += "onSendPrivateChatDone " + result + "/n";
		Debug.Log ("onSendPrivateChatDone : " + result);
	}
	
	public void onGetLiveLobbyInfoDone (LiveRoomInfoEvent eventObj){
		string result = "";
		if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {
			string[] users = eventObj.getJoinedUsers();
			for (int i = 0; i < users.Length; i++) {
				result += " " + users [i];                
			}
		}
		appWarpResponse+="onJoinLobbyDone : " + eventObj.getResult()  + ", Name : " + eventObj.getData().getName() + ", Id : " + eventObj.getData().getId()+" Users "+result+"\n";

		Debug.Log ("onGetLiveLobbyInfoDone : " + eventObj.getResult()  + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId());
	}
	
	public void onJoinLobbyDone (LobbyEvent eventObj){
		appWarpResponse+="onJoinLobbyDone : " + eventObj.getResult()  + ", Name : " + eventObj.getInfo().getName() + ", Id : " + eventObj.getInfo().getId()+" owner "+eventObj.getInfo().getRoomOwner()+"\n";
		Debug.Log ("onJoinLobbyDone : " + eventObj.getResult()  + ", Name : " + eventObj.getInfo().getName() + ", Id : " + eventObj.getInfo().getId()+" owner "+eventObj.getInfo().getRoomOwner());
	}
	
	public void onLeaveLobbyDone (LobbyEvent eventObj){
		notifications+="onLeaveLobbyDone : " + eventObj.getResult()  + ", Name : " + eventObj.getInfo().getName() + ", Id : " + eventObj.getInfo().getId()+" owner "+eventObj.getInfo().getRoomOwner()+"\n";

		Debug.Log ("onLeaveLobbyDone : " + eventObj.getResult()  + ", RoomName : " + eventObj.getInfo().getName() + ", RoomID : " + eventObj.getInfo().getId());
	}
	
	public void onSubscribeLobbyDone (LobbyEvent eventObj){
		appWarpResponse+="onSubscribeLobbyDone : " + eventObj.getResult()  + ", Name : " + eventObj.getInfo().getName() + ", Id : " + eventObj.getInfo().getId()+" owner "+eventObj.getInfo().getRoomOwner()+"\n";

	}
	
	public void onUnSubscribeLobbyDone (LobbyEvent eventObj){
		notifications+="onSubscribeLobbyDone : " + eventObj.getResult()  + ", Name : " + eventObj.getInfo().getName() + ", Id : " + eventObj.getInfo().getId()+" owner "+eventObj.getInfo().getRoomOwner()+"\n";

	}
	
	public void onCreateRoomDone (RoomEvent eventObj){
		appWarpResponse+="onCreateRoomDone : " + eventObj.getResult() + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId()+"\n";
		Debug.Log ("onCreateRoomDone : " + eventObj.getResult() + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId());
	}	
	
	public void onDeleteRoomDone (RoomEvent eventObj){
		appWarpResponse+="onDeleteRoomDone : " + eventObj.getResult() + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId()+"\n";
		Debug.Log ("onDeleteRoomDone : " + eventObj.getResult() + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId());
	}
	
	public void onGetAllRoomsDone (AllRoomsEvent eventObj){
		Debug.Log ("onGetAllRoomsDone : " + eventObj.getResult());
		string[] rooms = eventObj.getRoomIds ();
		string strRooms="";
		if (rooms != null && rooms.Length != 0) {
			foreach (string room in rooms) {
				Debug.Log ("room " + room);
				strRooms+=room+",";
			}
		}
		appWarpResponse+="onGetAllRoomsDone Rooms "+strRooms+"\n";
	}
	
	public void onGetLiveUserInfoDone (LiveUserInfoEvent eventObj){
		Debug.Log ("onGetLiveUserInfoDone : " + eventObj.getResult() + ", Name : " + eventObj.getName() + ", IsInLobby : " + eventObj.isLocationLobby());
		appWarpResponse+="onGetLiveUserInfoDone : " + eventObj.getResult() + ", Name : " + eventObj.getName() + ", IsInLobby : " + eventObj.isLocationLobby()+"\n";
	}
	
	public void onGetMatchedRoomsDone (MatchedRoomsEvent matchedRoomsEvent){
		Debug.Log ("onGetMatchedRoomsDone : " + matchedRoomsEvent.getResult());
		appWarpResponse+="onGetMatchedRoomsDone : " + matchedRoomsEvent.getResult()+"\n";
	}
	
	public void onGetOnlineUsersDone (AllUsersEvent eventObj){
		Debug.Log ("onGetOnlineUsersDone : " + eventObj.getResult());
		string users = "";

		string[] usernames = eventObj.getUserNames ();
		if (usernames != null && usernames.Length != 0) {
			foreach (string username in usernames) {
				Debug.Log ("username " + username);
				users+=username+",";
			}
		}
		appWarpResponse+="onGetOnlineUsersDone Users "+users+"\n";
	}
	
	public void onSetCustomUserDataDone (LiveUserInfoEvent eventObj){
		Debug.Log ("onSetCustomUserDataDone : " + eventObj.getResult());
	}
	
	//NotifyListener
	public void onRoomCreated (RoomData eventObj)
	{
		notifications+="onRoomCreated "+eventObj.getName()+" destroyed with id "+eventObj.getId()+"\n";
		Debug.Log ("onRoomCreated");
	}
	
	public void onRoomDestroyed (RoomData eventObj)
	{
		Debug.Log ("onRoomDestroyed");
	}
	
	public void onUserLeftRoom (RoomData eventObj, string username)
	{
		notifications+="onUserLeftRoom  username" + username+" roomName "+eventObj.getName()+"\n";
		Debug.Log ("onUserLeftRoom : " + username);
	}
	
	public void onUserJoinedRoom (RoomData eventObj, string username)
	{
		notifications+="onUserJoinedRoom  username" + username+" roomName "+eventObj.getName()+"\n";
		Debug.Log ("onUserJoinedRoom : " + username);
	}
	
	public void onUserLeftLobby (LobbyData eventObj, string username)
	{
		notifications+="onUserLeftLobby  username" + username+"\n";
		Debug.Log ("onUserLeftLobby : " + username);
	}
	
	public void onUserJoinedLobby (LobbyData eventObj, string username)
	{
		notifications+="onUserJoinedLobby  username" + username+"\n";
		Debug.Log ("onUserJoinedLobby : " + username);
	}
	
	public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable)
	{
		Debug.Log ("onUserChangeRoomProperty : " + sender);
	}
	
	public void onPrivateChatReceived(string sender, string message)
	{
		notifications+="onMoveCompleted  sender" + sender+ " message  "+message+"\n";
		Debug.Log ("onPrivateChatReceived Successfull Sender : " + sender + ", Message : " + message);
	}
	
	public void onMoveCompleted(MoveEvent move)
	{
		notifications+="onMoveCompleted  username" + move.getSender()+ " move  "+move.getMoveData()+"\n";
		Debug.Log ("onMoveCompleted by : " + move.getSender());
	}
	
	public void onUserPaused(string locid, Boolean isLobby, string username)
	{
		notifications+="onGameStopped  username" + username+ " locid  "+locid+"\n";
	}
	
	public void onUserResumed(string locid, Boolean isLobby, string username)
	{
		notifications+="onGameStopped  username" + username+ " locid  "+locid+"\n";
	}
	
	public void onGameStarted(string sender, string roomId, string nextTurn)
	{
		notifications+="onGameStopped  sender" + sender+ " roomId  "+roomId+" nextTurn "+nextTurn+"\n";
	}
	
	public void onGameStopped(string sender, string roomId)
	{
		notifications+="onGameStopped  sender" + sender+ " roomId  "+roomId+"\n";
	}
	
	public void onPrivateUpdateReceived(string sender, byte[] update, bool fromUdp)
	{
		notifications+="onPrivateUpdateReceived  sender" + sender+ " updates "+Encoding.UTF8.GetString(update)+"\n";
	}
	
	public void onNextTurnRequest(string lastTurn)
	{
		notifications+="onNextTurnRequest  lastTurn" + lastTurn+"\n";
	}
	
	public void onChatReceived (ChatEvent eventObj){
		
		Debug.Log ("onChatReceived Sender : " + eventObj.getSender() + ", Message : " + eventObj.getMessage());
		notifications+="onChatReceived  Sender : " + eventObj.getSender() + ", Message : " + eventObj.getMessage()+"\n";
	}
	
	public void onUpdatePeersReceived (UpdateEvent eventObj)
	{
	}
	
	public void onLockPropertiesDone(byte result)
	{
		Debug.Log ("onLockPropertiesDone : " + result);
	}
	
	public void onUnlockPropertiesDone(byte result)
	{
		Debug.Log ("onUnlockPropertiesDone : " + result);
	}
	
	public void onLeaveRoomDone (RoomEvent eventObj)
	{
		if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {
			string message = "onLeaveRoomDone : " + eventObj.getResult () + ", RoomName : " + eventObj.getData ().getName () + ", RoomID : " + eventObj.getData ().getId ();
			appWarpResponse+=message+"\n";
			Debug.Log (message);

		} else {
			Debug.Log ("onLeaveRoomDone : " + eventObj.getResult()) ;
		}
	}
	
	public void onSetCustomRoomDataDone (LiveRoomInfoEvent eventObj)
	{
		Debug.Log ("onSetCustomRoomDataDone : " + eventObj.getResult());
	}
	
	//RoomRequestListener
	public void onSubscribeRoomDone (RoomEvent eventObj)
	{			
		if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {
			string message = "onSubscribeRoomDone : " + eventObj.getResult () + ", RoomName : " + eventObj.getData ().getName () + ", RoomID : " + eventObj.getData ().getId ();
			appWarpResponse+=message+"\n";
			Debug.Log (message);

		} else {
			Debug.Log ("onSubscribeRoomDone : " + eventObj.getResult()) ;
		}
//		Debug.Log ("onSubscribeRoomDone : " + eventObj.getResult() + ", RoomName : " + eventObj.getData().getName() + ", RoomID : " + eventObj.getData().getId());
		//SendChat ();
	}
	
	public void onUnSubscribeRoomDone (RoomEvent eventObj)
	{
		if (eventObj.getResult () == WarpResponseResultCode.SUCCESS) {
			string message = "onUnSubscribeRoomDone : " + eventObj.getResult () + ", RoomName : " + eventObj.getData ().getName () + ", RoomID : " + eventObj.getData ().getId ();
			appWarpResponse+=message+"\n";
			Debug.Log (message);

		} else {
			Debug.Log ("onUnSubscribeRoomDone : " + eventObj.getResult()) ;
		}
		}
	
	public void onUpdatePropertyDone(LiveRoomInfoEvent eventObj){
		appWarpResponse+=" onUpdatePropertyDone "+eventObj.getResult()+"\n";
	}
	
	//UpdateRequestListener
	public void onSendUpdateDone (byte result)
	{
		appWarpResponse+=" onSendUpdateDone "+"\n";
		Debug.Log ("Yo, Got the result onSendUpdateDone " + result);
	}
	
	public void onSendPrivateUpdateDone(byte result)
	{
		notifications+=" onSendPrivateUpdateDone "+result+"\n";
		
	}
	

}