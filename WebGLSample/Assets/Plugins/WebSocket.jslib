
var LibraryWebSockets = {
$webSocketInstances: [],

SocketCreate: function(url)
{
	var str = Pointer_stringify(url);
      console.log("Connecting with :"+str);
	 var socket = {
		socket: new WebSocket(str),
		buffer: new Uint8Array(0),
		error: null,
		messages: [],
		errorCode:0
	}
	socket.socket.onmessage = function (e) {
	
		// Todo: handle other data types?
		
		if (e.data instanceof Blob)
		{
			var reader = new FileReader();
			reader.addEventListener("loadend", function() {
			console.log("Response : "+reader.result);
				var array = new Uint8Array(reader.result);
				socket.messages.push(array);
				//console.log("Response : "+array);
			});
			reader.readAsArrayBuffer(e.data);
		}
	};
	
	socket.socket.onclose = function (e) {
		console.log("IN Socket Close : "+e.code);
		socket.errorCode=e.code;
			console.log("IN Socket Close : "+socket.errorCode);
		if (e.code != 1000)
		{
			if (e.reason != null && e.reason.length > 0){
				//alert("e.reason " + e.reason);
				socket.error = e.reason;
			}
			else
			{
				//alert("error " + e.code);
				switch (e.code)
				{
					case 1001: 
						socket.error = "Endpoint going away.";
						break;
					case 1002: 
						socket.error = "Protocol error.";
						break;
					case 1003: 
						socket.error = "Unsupported message.";
						break;
					case 1005: 
						socket.error = "No status.";
						break;
					case 1006: 
						socket.error = "Abnormal disconnection.";
						break;
					case 1009: 
						socket.error = "Data frame too large.";
						break;
					default:
						socket.error = "Error "+e.code;
				}
			}
		}
		console.log("IN On Close Complete: ");
	}
	var instance = webSocketInstances.push(socket) - 1;
	return instance;
},

SocketState: function (socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	
	return socket.socket.readyState;
},
SocketErrorCode: function (socketInstance)
{
	
	var socket = webSocketInstances[socketInstance];
	if(socket==null)
	return 1006;
	return socket.errorCode;
},
SocketError: function (socketInstance, ptr, bufsize)
{
console.log("IN Socket Error : ");
  	var ptr = HEAPU32[ptr>>2];
 	var socket = webSocketInstances[socketInstance];
 	if (socket.error == null)
 		return 0;
    var str = socket.error.slice(0, Math.max(0, bufsize - 1));
    writeStringToMemory(str, ptr, false);
	return 1;
},

SocketSend: function (socketInstance, ptr, length)
{
	var ptr = HEAPU32[ptr>>2];
	var socket = webSocketInstances[socketInstance];
	socket.socket.send (HEAPU8.buffer.slice(ptr, ptr+length));
},

SocketRecvLength: function(socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	
	if (socket.messages.length == 0){
		return 0;
	}
	return socket.messages[0].length;
},

SocketRecv: function (socketInstance, array, length)
{

	//var ptr = HEAPU32[ptr>>2];
	var socket = webSocketInstances[socketInstance];
	
	if (socket.messages.length == 0)
		return 0;
	if (socket.messages[0].length > length)
		return 0;
	HEAPU8.set(socket.messages[0], array);
	socket.messages = socket.messages.slice(1);
},

SocketClose: function (socketInstance)
{
	var socket = webSocketInstances[socketInstance];
	socket.socket.close();
},

CustomWebSocketEncode : function(_type,_requestType,_getPayloadType,_sessionID,_getPayLoad){
	var newPayload = Pointer_stringify(_getPayLoad);

    var payloadSize = 0;
	if(newPayload =="{}"){
		newPayload = "";
	}
	else{
		payloadSize = newPayload.length;
	}
	
	var bytearray = new Uint8Array(16 + payloadSize);
    bytearray[0] = _type;
    bytearray[1] = _requestType;

    bytearray[2] = _sessionID >>> 24;
    bytearray[3] = _sessionID >>> 16;
    bytearray[4] = _sessionID >>> 8;
    bytearray[5] = _sessionID;

    for (var i = 6; i <= 9; i++) {
        bytearray[i] = 0;
    }
    bytearray[10] = 0;
        bytearray[11] = _getPayloadType;
   
    bytearray[12] = payloadSize >>> 24;
    bytearray[13] = payloadSize >>> 16;
    bytearray[14] = payloadSize >>> 8;
    bytearray[15] = payloadSize;
	
	for (var i = 0; i <payloadSize; i++) {
            bytearray[16 + i] = newPayload.charCodeAt(i);
        }
	
	var socket = webSocketInstances[0];
	socket.socket.send (bytearray.buffer);
},

CustomWebSocketByteEncode : function(_type,_requestType,_getPayloadType,_sessionID,payloadBytes){
	var payloadSize = payloadBytes.length;
	var bytearray = new Uint8Array(16 + payloadSize);
    bytearray[0] = _type;
    bytearray[1] = _requestType;

    bytearray[2] = _sessionID >>> 24;
    bytearray[3] = _sessionID >>> 16;
    bytearray[4] = _sessionID >>> 8;
    bytearray[5] = _sessionID;

    for (var i = 6; i <= 9; i++) {
        bytearray[i] = 0;
    }
    bytearray[10] = 0;
        bytearray[11] = _getPayloadType;
   
    
    bytearray[12] = payloadSize >>> 24;
    bytearray[13] = payloadSize >>> 16;
    bytearray[14] = payloadSize >>> 8;
    bytearray[15] = payloadSize;
	
	for (var i = 0; i <payloadSize; i++) {
            bytearray[16 + i] = payloadBytes[i];
        }
	
	var socket = webSocketInstances[0];
	socket.socket.send (bytearray.buffer);
}
};

autoAddDeps(LibraryWebSockets, '$webSocketInstances');
mergeInto(LibraryManager.library, LibraryWebSockets);
