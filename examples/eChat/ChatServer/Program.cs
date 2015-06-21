using System;
using eNetwork;

namespace ChatServer
{
    class Program
    {
        private static eServer server;

        static void Main(string[] args)
        {
            server = new eServer(2048);
            server.LogMessage = false;
            server.DebugMessage = false;
            server.ReceiveBufferSize = 1024;
            server.OnDataReceived += server_OnDataReceived;
            server.Start();
            Console.WriteLine("[Server Started]");
        }

        static void server_OnDataReceived(eSClient client, byte[] data)
        {
            if (eUtils.IsPacketCompressed(data))
            {
                ePacket packet = eUtils.Deserialize(eUtils.Decompress(data));
                switch (packet.Name)
                {
                    case "connectPacket":
                        Console.WriteLine("Client connected : " + packet.datas["name"]);
                        server.SendToAllExcept(client.ID, data);
                        break;

                    case "disconnectPacket":
                        Console.WriteLine("Client disconnected : " + packet.datas["name"]);
                        server.SendToAllExcept(client.ID, data);
                        break;

                    case "messagePacket":
                        Console.WriteLine(packet.datas["name"] + " : " + packet.datas["message"]);
                        server.SendToAll(data);
                        break;
                }
            }
        }
    }
}
