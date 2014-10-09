using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
    public class ZoneReqListener : ZoneRequestListener
    {
		string debug = "";
        public ZoneReqListener()
        {
           
        }

        public void onDeleteRoomDone(com.shephertz.app42.gaming.multiplayer.client.events.RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onDeleteRoomDone " + eventObj.getData().getId());
            }
            else
            {
				Log("onDeleteRoomDone Failed ");
            }
        }

        public void onGetAllRoomsDone(com.shephertz.app42.gaming.multiplayer.client.events.AllRoomsEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
                string[] roomids = eventObj.getRoomIds();
                string roomidlist = "";
                for (int i = 0; i < roomids.Length; i++)
                {
                    roomidlist = roomidlist + "\n" + roomids[i];
                }
				Log("Get All Rooms Done room ids:\n" + roomidlist);
            }
            else
            {
				Log("Get All Rooms Done Failed ");
            }
        }

        public void onCreateRoomDone(com.shephertz.app42.gaming.multiplayer.client.events.RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("Room Created " + eventObj.getData().getId());
            }
            else
            {
				Log("Room Creation Failed " + eventObj.getResult());
            }
        }

        public void onGetOnlineUsersDone(com.shephertz.app42.gaming.multiplayer.client.events.AllUsersEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
                string[] usernames = eventObj.getUserNames();
                string users = "";
                for (int i = 0; i < usernames.Length; i++)
                {
                    users = users + "\n" + usernames[i];
                }
				Log("GetOnlineUsers Done " + eventObj.getUserNames());
            }
            else
            {
				Log("GetOnlineUsers Failed ");
            }
        }

        public void onGetLiveUserInfoDone(com.shephertz.app42.gaming.multiplayer.client.events.LiveUserInfoEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("GetLiveUserInfo Done room Id: " + eventObj.getLocationId());
            }
            else
            {
				Log("GetLiveUserInfo Failed ");
            }
        }

        public void onSetCustomUserDataDone(com.shephertz.app42.gaming.multiplayer.client.events.LiveUserInfoEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("GetLiveUserInfo Done room Id: " + eventObj.getLocationId());
            }
            else
            {
				Log("GetLiveUserInfo Failed ");
            }
        }

        public void onGetMatchedRoomsDone(com.shephertz.app42.gaming.multiplayer.client.events.MatchedRoomsEvent matchedRoomsEvent)
        {
            if (matchedRoomsEvent.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onGetMatchedRoomsDone Done room Id: " + matchedRoomsEvent.getRoomsData());
            }
            else
            {
				Log("onGetMatchedRoomsDone Failed ");
            }
        }

        //public void onInvokeZoneRPCDone(com.shephertz.app42.gaming.multiplayer.client.events.RPCEvent rpcEvent)
        //{
            
        //}
		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}
    }
}
