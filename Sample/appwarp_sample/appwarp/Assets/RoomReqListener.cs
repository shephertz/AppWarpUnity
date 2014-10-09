using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
    public class RoomReqListener : RoomRequestListener
    {
		string debug = "";
        public RoomReqListener()
        {
        }

        public void onSubscribeRoomDone(com.shephertz.app42.gaming.multiplayer.client.events.RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onSubscribeRoomDone " + eventObj.getData().getId());
            }
            else
            {
				Log("onSubscribeRoomDone Failed " + eventObj.getResult());
            }
        }

        public void onUnSubscribeRoomDone(com.shephertz.app42.gaming.multiplayer.client.events.RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onUnSubscribeRoomDone " + eventObj.getData().getId());
            }
            else
            {
				Log("onUnSubscribeRoomDone Failed " + eventObj.getResult());
            }
        }

        public void onJoinRoomDone(com.shephertz.app42.gaming.multiplayer.client.events.RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onRoomJoined " + eventObj.getData().getId());
            }
            else
            {
				Log("onRoomJoined Failed " + eventObj.getResult());
            }
        }

        public void onLeaveRoomDone(com.shephertz.app42.gaming.multiplayer.client.events.RoomEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onLeaveRoomDone " + eventObj.getData().getId());
            }
            else
            {
				Log("onLeaveRoomDone Failed " + eventObj.getResult());
            }
        }

        public void onGetLiveRoomInfoDone(com.shephertz.app42.gaming.multiplayer.client.events.LiveRoomInfoEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onGetLiveRoomInfoDone " + eventObj.getData().getId());
            }
            else
            {
				Log("onGetLiveRoomInfoDone Failed " + eventObj.getResult());
            }
        }

        public void onSetCustomRoomDataDone(com.shephertz.app42.gaming.multiplayer.client.events.LiveRoomInfoEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onSetCustomRoomDataDone " + eventObj.getData().getId());
            }
            else
            {
				Log("onSetCustomRoomDataDone Failed " + eventObj.getResult());
            }
        }

        public void onUpdatePropertyDone(com.shephertz.app42.gaming.multiplayer.client.events.LiveRoomInfoEvent liveRoomInfoEvent)
        {
            if (liveRoomInfoEvent.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("onUpdatePropertyDone " + liveRoomInfoEvent.getData().getId());
            }
            else
            {
				Log("onUpdatePropertyDone Failed " + liveRoomInfoEvent.getResult());
            }
        }

        public void onLockPropertiesDone(byte result)
        {
            if (result == WarpResponseResultCode.SUCCESS)
            {
				Log("onLockPropertiesDone Success");
            }
            else
            {
				Log("onUnlockPropertiesDone Failed");
            }
        }

        public void onUnlockPropertiesDone(byte result)
        {
            if (result == WarpResponseResultCode.SUCCESS)
            {
				Log("onUnlockPropertiesDone Success");
            }
            else
            {
				Log("onUnlockPropertiesDone Failed");
            }
        }

		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}
    }
}