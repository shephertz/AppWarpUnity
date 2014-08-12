using UnityEngine;

using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.events;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.message;
using com.shephertz.app42.gaming.multiplayer.client.transformer;

using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Listener : ConnectionRequestListener, LobbyRequestListener, ZoneRequestListener, RoomRequestListener, ChatRequestListener, UpdateRequestListener, NotifyListener
	{
		int state = 0;
		string debug = "";
		
		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}

		public string getDebug()
		{
			return debug;
		}
		
		//ConnectionRequestListener
		public void onConnectDone(ConnectEvent eventObj)
		{
			if(eventObj.getResult() == 0)
			{
				WarpClient.GetInstance().SubscribeRoom(appwarp.roomid);
			}
			Log ("onConnectDone : " + eventObj.getResult());
		}

		public void onInitUDPDone(byte res)
		{
		}
		
		public void onLog(String message){
			Log (message);
		}
		
		public void onDisconnectDone(ConnectEvent eventObj)
		{
			Log("onDisconnectDone : " + eventObj.getResult());
		}
		
		//LobbyRequestListener
		public void onJoinLobbyDone (LobbyEvent eventObj)
		{
			Log ("onJoinLobbyDone : " + eventObj.getResult());
			if(eventObj.getResult() == 0)
			{
				state = 1;
			}
		}
		
		public void onLeaveLobbyDone (LobbyEvent eventObj)
		{
			Log ("onLeaveLobbyDone : " + eventObj.getResult());
		}
		
		public void onSubscribeLobbyDone (LobbyEvent eventObj)
		{
			Log ("onSubscribeLobbyDone : " + eventObj.getResult());
			if(eventObj.getResult() == 0)
			{
				WarpClient.GetInstance().JoinLobby();
			}
		}
		
		public void onUnSubscribeLobbyDone (LobbyEvent eventObj)
		{
			Log ("onUnSubscribeLobbyDone : " + eventObj.getResult());
		}
		
		public void onGetLiveLobbyInfoDone (LiveRoomInfoEvent eventObj)
		{
			Log ("onGetLiveLobbyInfoDone : " + eventObj.getResult());
		}
		
		//ZoneRequestListener
		public void onDeleteRoomDone (RoomEvent eventObj)
		{
			Log ("onDeleteRoomDone : " + eventObj.getResult());
		}
		
		public void onGetAllRoomsDone (AllRoomsEvent eventObj)
		{
			Log ("onGetAllRoomsDone : " + eventObj.getResult());
		}
		
		public void onCreateRoomDone (RoomEvent eventObj)
		{
			Log ("onCreateRoomDone : " + eventObj.getResult());
		}
		
		public void onGetOnlineUsersDone (AllUsersEvent eventObj)
		{
			Log ("onGetOnlineUsersDone : " + eventObj.getResult());
		}
		
		public void onGetLiveUserInfoDone (LiveUserInfoEvent eventObj)
		{
			Log ("onGetLiveUserInfoDone : " + eventObj.getResult());
		}
		
		public void onSetCustomUserDataDone (LiveUserInfoEvent eventObj)
		{
			Log ("onSetCustomUserDataDone : " + eventObj.getResult());
		}
		
        public void onGetMatchedRoomsDone(MatchedRoomsEvent eventObj)
		{
			if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
                Log ("GetMatchedRooms event received with success status");
                foreach (var roomData in eventObj.getRoomsData())
                {
                    Log("Room ID:" + roomData.getId());
                }
            }
		}		
		//RoomRequestListener
		public void onSubscribeRoomDone (RoomEvent eventObj)
		{
			if(eventObj.getResult() == 0)
			{
				/*string json = "{\"start\":\""+id+"\"}";
				WarpClient.GetInstance().SendChat(json);
				state = 1;*/
				WarpClient.GetInstance().JoinRoom(appwarp.roomid);
			}
			
			Log ("onSubscribeRoomDone : " + eventObj.getResult());
		}
		
		public void onUnSubscribeRoomDone (RoomEvent eventObj)
		{
			Log ("onUnSubscribeRoomDone : " + eventObj.getResult());
		}
		
		public void onJoinRoomDone (RoomEvent eventObj)
		{
			if(eventObj.getResult() == 0)
			{
				state = 1;
			}
			Log ("onJoinRoomDone : " + eventObj.getResult());
			
		}
		
		public void onLockPropertiesDone(byte result)
		{
			Log ("onLockPropertiesDone : " + result);
		}
		
		public void onUnlockPropertiesDone(byte result)
		{
			Log ("onUnlockPropertiesDone : " + result);
		}
		
		public void onLeaveRoomDone (RoomEvent eventObj)
		{
			Log ("onLeaveRoomDone : " + eventObj.getResult());
		}
		
		public void onGetLiveRoomInfoDone (LiveRoomInfoEvent eventObj)
		{
			Log ("onGetLiveRoomInfoDone : " + eventObj.getResult());
		}
		
		public void onSetCustomRoomDataDone (LiveRoomInfoEvent eventObj)
		{
			Log ("onSetCustomRoomDataDone : " + eventObj.getResult());
		}
		
		public void onUpdatePropertyDone(LiveRoomInfoEvent eventObj)
        {
            if (WarpResponseResultCode.SUCCESS == eventObj.getResult())
            {
                Log ("UpdateProperty event received with success status");
            }
            else
            {
                Log ("Update Propert event received with fail status. Status is :" + eventObj.getResult().ToString());
            }
        }
		
		//ChatRequestListener
		public void onSendChatDone (byte result)
		{
			Log ("onSendChatDone result : " + result);
			
		}
		
		public void onSendPrivateChatDone(byte result)
		{
			Log ("onSendPrivateChatDone : " + result);
		}
		
		//UpdateRequestListener
		public void onSendUpdateDone (byte result)
		{
		}
		public void onSendPrivateUpdateDone (byte result)
		{
			Log ("onSendPrivateUpdateDone : " + result);
		}
		//NotifyListener
		public void onRoomCreated (RoomData eventObj)
		{
			Log ("onRoomCreated");
		}
		public void onPrivateUpdateReceived (string sender, byte[] update, bool fromUdp)
		{
			Log ("onPrivateUpdate");
		}
		public void onRoomDestroyed (RoomData eventObj)
		{
			Log ("onRoomDestroyed");
		}
		
		public void onUserLeftRoom (RoomData eventObj, string username)
		{
			Log ("onUserLeftRoom : " + username);
		}
		
		public void onUserJoinedRoom (RoomData eventObj, string username)
		{
			Log ("onUserJoinedRoom : " + username);
		}
		
		public void onUserLeftLobby (LobbyData eventObj, string username)
		{
			Log ("onUserLeftLobby : " + username);
		}
		
		public void onUserJoinedLobby (LobbyData eventObj, string username)
		{
			Log ("onUserJoinedLobby : " + username);
		}
		
		public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable)
		{
			Log ("onUserChangeRoomProperty : " + sender);
		}
			
		public void onPrivateChatReceived(string sender, string message)
		{
			Log ("onPrivateChatReceived : " + sender);
		}
		
		public void onMoveCompleted(MoveEvent move)
		{
			Log ("onMoveCompleted by : " + move.getSender());
		}
		
		public void onChatReceived (ChatEvent eventObj)
		{
			Log(eventObj.getSender() + " sended " + eventObj.getMessage());
			SimpleJSON.JSONNode msg =  SimpleJSON.JSON.Parse(eventObj.getMessage());
			//msg[0] 
			if(eventObj.getSender() != appwarp.username)
			{
				appwarp.movePlayer(msg["x"].AsFloat,msg["y"].AsFloat,msg["z"].AsFloat);
				//Log(msg["x"].ToString()+" "+msg["y"].ToString()+" "+msg["z"].ToString());
			}
		}
		
		public void onUpdatePeersReceived (UpdateEvent eventObj)
		{
			Log ("onUpdatePeersReceived");
		}
		
		public void onUserChangeRoomProperty(RoomData roomData, string sender, Dictionary<String, System.Object> properties)
        {
            Log("Notification for User Changed Room Propert received");
            Log(roomData.getId());
            Log(sender);
            foreach (KeyValuePair<String, System.Object> entry in properties)
            {
                Log("KEY:" + entry.Key);
                Log("VALUE:" + entry.Value.ToString());
            }
        }
	
		public void sendMsg(string msg)
		{
			if(state == 1)
			{
				WarpClient.GetInstance().SendChat(msg);
			}
		}
		
		public void onUserPaused(string a, bool b, string c)
		{
		}
		
		public void onUserResumed(string a, bool b, string c)
		{
		}
		
		public void onGameStarted(string a, string b, string c)
		{
		}
		
		public void onGameStopped(string a, string b)
		{
		}
	}
}

