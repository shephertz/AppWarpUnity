using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
    public class NotificationListener:NotifyListener
    {
		string debug = "";
        public NotificationListener()
        {
            
        }

        public void onRoomCreated(com.shephertz.app42.gaming.multiplayer.client.events.RoomData eventObj)
        {
			Log("onRoomCreated Name "+eventObj.getName()+" Id "+eventObj.getId() + " MaxUsers " + eventObj.getMaxUsers());
        }

        public void onRoomDestroyed(com.shephertz.app42.gaming.multiplayer.client.events.RoomData eventObj)
        {
			Log("onRoomDestroyed Name " + eventObj.getName() + " Id " + eventObj.getId() + " MaxUsers " + eventObj.getMaxUsers());
        }

        public void onUserLeftRoom(com.shephertz.app42.gaming.multiplayer.client.events.RoomData eventObj, string username)
        {
			Log("onUserLeftRoom Name " + eventObj.getName() + " Id " + eventObj.getId() + " MaxUsers " + eventObj.getMaxUsers() + " username " + username);
        }

        public void onUserJoinedRoom(com.shephertz.app42.gaming.multiplayer.client.events.RoomData eventObj, string username)
        {
			Log("onUserJoinedRoom Name " + eventObj.getName() + " Id " + eventObj.getId() + " MaxUsers " + eventObj.getMaxUsers() + " username " + username);
        }

        public void onUserLeftLobby(com.shephertz.app42.gaming.multiplayer.client.events.LobbyData eventObj, string username)
        {
			Log("onUserLeftLobby Name " + eventObj.getName() + " Id " + eventObj.getId() + " MaxUsers " + eventObj.getMaxUsers() + " username " + username);
        }

        public void onUserJoinedLobby(com.shephertz.app42.gaming.multiplayer.client.events.LobbyData eventObj, string username)
        {
			Log("onUserJoinedLobby Name " + eventObj.getName() + " Id " + eventObj.getId() + " MaxUsers " + eventObj.getMaxUsers() + " username " + username);
        }

        public void onChatReceived(com.shephertz.app42.gaming.multiplayer.client.events.ChatEvent eventObj)
        {
			Log(eventObj.getSender() + " sent " + eventObj.getMessage());
        }

        public void onUpdatePeersReceived(com.shephertz.app42.gaming.multiplayer.client.events.UpdateEvent eventObj)
        {
			Log("onUpdatePeersReceived " + Encoding.UTF8.GetString(eventObj.getUpdate()) + " fromudp " + eventObj.getIsUdp());
        }

        public void onUserChangeRoomProperty(com.shephertz.app42.gaming.multiplayer.client.events.RoomData roomData, string sender, Dictionary<string, object> properties, Dictionary<string, string> lockedPropertiesTable)
        {
			Log("onUserChangeRoomProperty : sender" + roomData.getName() + " sender " + sender);
        }

        public void onPrivateChatReceived(string sender, string message)
        {
			Log("onPrivateChatReceived :"+ sender + " sent " + message);
        }

        public void onMoveCompleted(com.shephertz.app42.gaming.multiplayer.client.events.MoveEvent moveEvent)
        {
			Log("onMoveCompleted : sender" + moveEvent.getSender() + " Next Turn " + moveEvent.getNextTurn());
        }

        public void onUserPaused(string locid, bool isLobby, string username)
        {
			Log("onUserPaused " + locid + " username: " + username+" isLobby "+isLobby);
        }

        public void onUserResumed(string locid, bool isLobby, string username)
        {
			Log("onUserResumed " + locid + " username: " + username + " isLobby " + isLobby);
        }

        public void onGameStarted(string sender, string roomId, string nextTurn)
        {
			Log("onGameStarted Sender" + sender + " Room Id: " + roomId + " Next Turn " + nextTurn);
        }

        public void onGameStopped(string sender, string roomId)
        {
			Log("onGameStopped sender" + sender + " RoomId: " + roomId);
        }

		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}


        public void onPrivateUpdateReceived(string sender, byte[] update, bool fromUdp)
        {
			Log("onPrivateUpdateReceived " + sender + " sent " + Encoding.UTF8.GetString(update)+" fromudp "+fromUdp);
        }


        public void onNextTurnRequest(string lastTurn)
        {
			Log("onNextTurnRequest lastturn" + lastTurn);
        }
    }
}
