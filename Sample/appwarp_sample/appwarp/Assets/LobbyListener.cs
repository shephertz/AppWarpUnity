using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
    public class LobbyListener : LobbyRequestListener
    {
		string debug = "";
        public LobbyListener()
        {
           
        }

        public void onJoinLobbyDone(com.shephertz.app42.gaming.multiplayer.client.events.LobbyEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onJoinLobbyDone " + eventObj.getInfo().getName());
            }
            else
            {
				Log("onJoinLobbyDone Failed ");
            }
        }

        public void onLeaveLobbyDone(com.shephertz.app42.gaming.multiplayer.client.events.LobbyEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onLeaveLobbyDone " + eventObj.getInfo().getName());
            }
            else
            {
				Log("onLeaveLobbyDone Failed ");
            }
        }

        public void onSubscribeLobbyDone(com.shephertz.app42.gaming.multiplayer.client.events.LobbyEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onSubscribeLobbyDone " + eventObj.getInfo().getName());
            }
            else
            {
				Log("onSubscribeLobbyDone Failed ");
            }
        }

        public void onUnSubscribeLobbyDone(com.shephertz.app42.gaming.multiplayer.client.events.LobbyEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onUnSubscribeLobbyDone " + eventObj.getInfo().getName());
            }
            else
            {
				Log("onUnSubscribeLobbyDone Failed ");
            }
        }

        public void onGetLiveLobbyInfoDone(com.shephertz.app42.gaming.multiplayer.client.events.LiveRoomInfoEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onGetLiveLobbyInfoDone " + eventObj.getData().getName());
            }
            else
            {
				Log("onGetLiveLobbyInfoDone Failed ");
            }
        }

		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}
    }
}
