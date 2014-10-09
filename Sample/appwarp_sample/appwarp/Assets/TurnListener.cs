using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
    public class TurnListener:TurnBasedRoomListener
    {
		string debug = "";
        public TurnListener()
        {

        }

        public void onSendMoveDone(byte result)
        {
            if (result == WarpResponseResultCode.SUCCESS)
            {
				Log("onSendMoveDone Success");
            }
            else
            {
				Log("onSendMoveDone Failed ");
            }
        }

        public void onStartGameDone(byte result)
        {
            if (result == WarpResponseResultCode.SUCCESS)
            {
				Log("onStartGameDone Success" );
            }
            else
            {
				Log("onStartGameDone Failed ");
            }
        }

        public void onStopGameDone(byte result)
        {
            if (result == WarpResponseResultCode.SUCCESS)
            {
				Log("onStopGameDone Success");
            }
            else
            {
				Log("onStopGameDone Failed ");
            }
        }

        public void onSetNextTurnDone(byte result)
        {
            if (result == WarpResponseResultCode.SUCCESS)
            {
				Log("onSetNextTurnDone Success");
            }
            else
            {
				Log("onSetNextTurnDone Failed ");
            }
        }

        public void onGetMoveHistoryDone(byte result, com.shephertz.app42.gaming.multiplayer.client.events.MoveEvent[] moves)
        {
			Log("onGetMoveHistoryDone " + result);
        }

		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}

    }
}
