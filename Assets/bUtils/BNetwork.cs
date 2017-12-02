using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
// @class BNetwork
// @desc Component for establishing lowlevel connections between computers and basic msg exchange.
public class BNetwork : MonoBehaviour {
	// @prop #:client
	// @type NetworkClient
	// @desc Client used to connect to a server
	static public NetworkClient client;
	// @prop #:instance
	// @type BNetwork
	// @desc Singleton instance
	static public BNetwork instance;
	// @prop #:port
	// @type int
	// @desc Port to connect or listen
	// @def 7777
	public int port = 7777;
	// @prop #:listening
	// @type bool
	// @desc Is the &b[;BNetwork&b]; listening?
	// @def false
	public bool listening = false;
	public int playerID = -1;
	public int lastPlayerID = -1;
	// @prop #:connected
	// @type bool
	// @desc Is the &b[;BNetwork&b]; connected?
	// @def false
	public bool connected = false;
	[HideInInspector]
	// @prop #:connected
	// @type BasicMsg
	// @desc Last message recieved
	public BasicMsg lastMsg = null;
	[Header("Events")]
	public UnityEvent onConnect;
	public UnityEvent onDisconnect;
	public UnityEvent onMsg;
	public UnityEvent onMsgServer;
	
	void Awake() {
		instance = this;
	}
	
	// @method #:Listen
	// @desc Starts the server listening.
	public void Listen() {
		NetworkServer.Listen(port);
		listening = true;
		NetworkServer.RegisterHandler(MsgType.Highest + 24,ServerMsg);
	}
	
	// @method #:Connect
	// @param ip string The ip to connect to
	// @desc Connects the client to a server on the provided ip address.
	public void Connect(string ip) {
		playerID = -1;
		client = new NetworkClient();
		client.Connect(ip,port);
		client.RegisterHandler(MsgType.Connect,Connected);
		client.RegisterHandler(MsgType.Connect,Disconnected);
		client.RegisterHandler(MsgType.Highest + 24,GotMsg);
		client.RegisterHandler(MsgType.Highest + 25,ServerQueryMsg);
	}
	
	// @method #:StartLocalPlayer
	// @desc Starts a server using Listen and connect to it.
	public void StartLocalPlayer() {
		Listen();
		Connect("127.0.0.1");
	}
	
	// @method #:Broadcast
	// @param message BasicMsg Message to send
	// @desc Sends a message to all connected clients.
	// Broadcast >24> ServerMsg >24> GotMsg
	public void Broadcast(BasicMsg message) {
		if (client != null) {
			client.Send(MsgType.Highest + 24,message);
		}
	}
	
	// @method #:BroadcastFromServer
	// @param message BasicMsg Message to send
	// @desc Sends a message to all connected clients.
	// BroadcastFromServer >24> GotMsg
	public void BroadcastFromServer(BasicMsg message) {
		if (listening)
			NetworkServer.SendToAll(MsgType.Highest + 24,message);
	}
	
	// @method #:BroadcastFromServer
	// @param message BasicMsg Message to send
	// @desc Sends a message to the server.
	// SendToServer >25> ServerQueryMsg
	public void SendToServer(BasicMsg message) {
		if (client != null) {
			client.Send(MsgType.Highest + 25,message);
		}
	}
	
	void GotMsg(NetworkMessage raw) {
		BasicMsg msg = raw.ReadMessage<BasicMsg>();
		if (msg.type == "#INTERNAL") {
			if (msg.target == "newID") {
				playerID = int.Parse(msg.msg);
			}
		} else {
			lastMsg = msg;
			onMsg.Invoke();
		}
	}
	
	void ServerMsg(NetworkMessage raw) {
		NetworkServer.SendToAll(MsgType.Highest + 24,raw.ReadMessage<BasicMsg>());
	}
	
	void ServerQueryMsg(NetworkMessage raw) {
		BasicMsg msg = raw.ReadMessage<BasicMsg>();
		if (msg.type == "#INTERNAL") {
			if (msg.target == "getID") {
				lastPlayerID++;
				BroadcastFromServer(new BasicMsg("#INTERNAL","newID",lastPlayerID.ToString()));
			}
		} else {
			lastMsg = msg;
			onMsgServer.Invoke();
		}
	}
	
	void Connected(NetworkMessage msg) {
		SendToServer(new BasicMsg("#INTERNAL","getID",""));
		onConnect.Invoke();
		connected = true;
	}
	void Disconnected(NetworkMessage msg) {
		onDisconnect.Invoke();
		connected = false;
	}
	
	// @class BNetwork.PlayerData
	public struct PlayerData {
		// @prop ip
		// @type string
		// @desc IP address of the player.
		string ip;
	}
	
	
}
// @class BasicMsg
	public class BasicMsg : MessageBase {
		// @prop #:type
		// @type string
		public string type;
		// @prop #:target
		// @type string
		public string target;
		// @prop #:msg
		// @type string
		public string msg;
		// @prop #:source
		// @type int
		public int source;
		
		// @function #:BasicMsg
		// @constructor
		// @param type string Message type
		// @param target string Message target
		// @param message string Message message
		public BasicMsg(string t,string ta,string m,int id = 0) {
			type = t;
			target = ta;
			msg = m;
			source = id;
		}
		
		public BasicMsg() {
			
		}
	}