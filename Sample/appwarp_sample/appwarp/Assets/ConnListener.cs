using com.shephertz.app42.gaming.multiplayer.client;
using com.shephertz.app42.gaming.multiplayer.client.command;
using com.shephertz.app42.gaming.multiplayer.client.listener;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
   public class ConnListener:ConnectionRequestListener
    {
		string debug = "";
        public ConnListener()
        {
          
        }

        public void onConnectDone(com.shephertz.app42.gaming.multiplayer.client.events.ConnectEvent eventObj)
        {   
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("Connection Success ");
            }
            else
            {
                Log("Connection Failed ");
            }
            switch (eventObj.getResult())
            {
                case WarpResponseResultCode.AUTH_ERROR:
                    //if (eventObj.getReasonCode() == WarpReasonCode.WAITING_FOR_PAUSED_USER)
                    //{
                    //    // int sessionID = (int)DBManager.getDBData("SessionID");
                    //    WriteOnUI("Auth Error for paused user ");
                    //    //WarpClient.GetInstance().RecoverConnectionWithSessioId(sessionID, "rahul");
                    //}
                    //else
                    //{
                    //    WriteOnUI("Auth Error with reason code " + eventObj.getReasonCode());
                    //    //WarpClient.GetInstance().Connect("rahul");
                    //}
                    break;
                case WarpResponseResultCode.SUCCESS:
                    //DBManager.saveData("SessionID", WarpClient.GetInstance().GetSessionId());
                    Debug.WriteLine("connection success");
                    //_page.showResult("connection success");
                    break;
                case WarpResponseResultCode.CONNECTION_ERROR_RECOVERABLE:
					Log("connection recoverable " + eventObj.getResult());
                    // Deployment.Current.Dispatcher.BeginInvoke(delegate() {   RecoverConnection(); });
                    break;
                case WarpResponseResultCode.SUCCESS_RECOVERED:
                    Debug.WriteLine("Success Recoverd");
					Log("Connect success recoverd: " + eventObj.getResult());
                    break;
                default:
                    Debug.WriteLine("Connection failed");
					Log("connection failed" + eventObj.getResult());
                    break;
            }
        }

        public void onDisconnectDone(com.shephertz.app42.gaming.multiplayer.client.events.ConnectEvent eventObj)
        {
            if (eventObj.getResult() == WarpResponseResultCode.SUCCESS)
            {
				Log("Disconnected " + eventObj.getResult());
            }
            else
            {
				Log("Failed " + eventObj.getResult());
            }
        }

        public void onInitUDPDone(byte resultCode)
        {
			Log("Init udp done");
        }

		private void Log(string msg)
		{
			debug = msg + "\n" + debug;
		}
        public void onLog(string msg)
        {
           
        }
    }
}
