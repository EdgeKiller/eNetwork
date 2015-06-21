# eNetwork
.Net Library for easy networking in CSharp (C#)

## More informations !

- Easy to use
- Only TCP for the moment
- Send byte array or ePacket object
- Compatible with all .net compatible languages
- Each client has an unique ID during the session

## How to use ?

#### • eServer

```csharp
//Declare new eServer with the port
eServer server = new eServer(2048);

//Add events
server.OnDataReceived += server_OnDataReceived;
server.OnClientConnected += server_OnClientConnected;
server.OnClientDisconnected += server_OnClientDisconnected;

static void server_OnClientConnected(eSClient client)
{
}

static void server_OnClientDisconnected(eSClient client)
{
}

static void server_OnDataReceived(eSClient client, byte[] data)
{
}

//Configure the server
server.DebugMessage = true; //Errors messages
server.LogMessage = true; //Logs messages like client connected, data received...

//Start the server
server.Start();

//Close the server
server.Stop();

//Send data
server.SendToAll(data); //Data must be an byte array
server.SendToAllExcept(clientID, data); //Send to all client except the one with clientID

//Check server state
bool state = server.Connected; //Return a boolean

//Set the buffer size
server.ReceiveBufferSize = 2048; //In byte, set to 512 by default
```

#### • eClient

```csharp
//Declare new eClient with the IP and the port
eClient client = new eClient("127.0.0.1", 2048);

//Add events
client.OnDataReceived += client_OnDataReceived;
client.OnClientConnected += client_OnConnected;
client.OnClientDisconnected += client_OnDisconnected;

static void client_OnConnected()
{
}

static void client_OnDisconnected()
{
}

static void client_OnDataReceived(byte[] data)
{
}

//Configure the client
client.DebugMessage = true; //Errors messages
client.LogMessage = true; //Logs messages like connected, data received...

//Connect the client
client.Connect();

//Disconnect the client
client.Disconnect();

//Send data
client.Send(data); //Data must be an byte array

//Check client state
bool state = client.Connected; //Return a boolean

//Set the buffer size
client.ReceiveBufferSize = 2048; //In byte, set to 512 by default
```

#### • eUtils

eUtils is a static class.

```csharp
//Serialize object (like ePacket) to byte array
byte[] data = eUtils.Serialize(obj);

//Deserialize byte array to specific object
obj myObject = eUtils.Deserialize<obj>(data);

//Deserialize byte array to ePacket
ePacket packet = eUtils.Deserialize(data);

//Compress byte array (very useful to send ePacket)
byte[] compressedData = eUtils.Compress(data);

//Decompress byte array
byte[] decompressedData = eUtils.Decompress(compressedData);

//Check if byte array is ePacket
bool isePacket = eUtils.IsPacket(data);

//Check if byte array is compressed ePacket
bool isePacket = eUtils.IsPacketCompressed(compressedData);
```

#### • ePacket

You can send ePacket, you just need to serialize (and maybe compress) it.

#### • eSClient

eSClient contains a TCPClient and the ID (int).
