using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace eNetwork
{
    public class eServer
    {

        // Variables
        #region Variables

        private TcpListener listener;

        private Thread listenThread;

        private List<eSClient> clients;

        public bool Connected { get { return listener.Server.Connected; } }

        public bool DebugMessage { get; set; }

        public bool LogMessage { get; set; }

        public int ReceiveBufferSize { get { return listener.Server.ReceiveBufferSize; } set { listener.Server.ReceiveBufferSize = value; } }

        public delegate void ServerReceiveDataHandler(eSClient client, byte[] data);
        public delegate void ServerConnectedDataHandler(eSClient client);
        public delegate void ServerDisconnectedDataHandler(eSClient client);

        public event ServerReceiveDataHandler OnDataReceived;
        public event ServerConnectedDataHandler OnClientConnected;
        public event ServerDisconnectedDataHandler OnClientDisconnected;

        private int countID = 1;

        #endregion

        // Constructor
        public eServer(int Port)
        {
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Server.ReceiveBufferSize = 512;
            listenThread = new Thread(new ThreadStart(Listen));
            clients = new List<eSClient>();
            DebugMessage = false;
            LogMessage = true;
        }

        // Start the server
        public void Start()
        {
            try
            {
                listener.Start();
                listenThread.Start();
                Log("Server started");
            }
            catch(Exception ex)
            {
                Debug("Error when starting server : " + ex.Message);
            }
        }

        // Stop the server
        public void Stop()
        {
            if(listenThread.ThreadState == ThreadState.Running)
                listenThread.Abort();
            if(listener.Server.Connected)
                listener.Stop();
        }

        // Listen for new connections
        private void Listen()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                eSClient sClient = new eSClient(countID, client);
                clients.Add(sClient);
                Log("New client connected with ID : " + countID + " - " + client.Client.RemoteEndPoint);
                client.GetStream().Write(Encoding.UTF8.GetBytes(countID.ToString()), 0, Encoding.UTF8.GetBytes(countID.ToString()).Length);
                client.GetStream().Flush();
                countID++;
                if (OnClientConnected != null)
                    OnClientConnected.Invoke(sClient);
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(sClient);
            }
        }

        // Handle client
        private void HandleClient(object c)
        {
            eSClient sClient = (eSClient)c;
            TcpClient client = sClient.TcpClient;
            client.ReceiveBufferSize = listener.Server.ReceiveBufferSize;

            byte[] data = new byte[client.ReceiveBufferSize];

            while (true)
            {
                try
                {
                    client.GetStream().Read(data, 0, client.ReceiveBufferSize);
                    if (DebugMessage)
                    {
                        if (eUtils.IsPacketCompressed(data))
                        {
                            ePacket result = eUtils.Deserialize(eUtils.Decompress(data));
                            Debug("[ePacket received]");
                            Debug("Name : " + result.Name);
                            Debug("Size : " + data.Length);
                            Debug("Dic size : " + result.datas.Count);
                        }
                        else
                        {
                            Debug("[Packet received]");
                            Debug("Size : " + data.Length);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.HResult != -2146232800)
                        Debug(ex.HResult);
                    break;    
                }

                if(OnDataReceived != null)
                {
                    try
                    {
                        OnDataReceived.Invoke(sClient, data);
                    }
                    catch (Exception ex)
                    {
                        Debug("Error when invoking OnDataReceived method : " + ex.Message);
                    }
                }
            }

            Log("Client disconnected with ID : " + sClient.ID + " - " + client.Client.RemoteEndPoint);
            if(OnClientDisconnected != null)
                OnClientDisconnected.Invoke(sClient);

            for(int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ID == sClient.ID)
                {
                    clients.RemoveAt(i);
                    break;
                }
            }

            client.Close();
        }

        // Send to all clients
        public void SendToAll(byte[] data)
        {
            foreach (eSClient sc in clients)
            {
                sc.TcpClient.GetStream().Write(data, 0, data.Length);
                sc.TcpClient.GetStream().Flush();
                
            }
        }

        // Send to all clients except with ID
        public void SendToAllExcept(int ID, byte[] data)
        {
            foreach (eSClient sc in clients)
            {
                if (sc.ID != ID)
                {
                    sc.TcpClient.GetStream().Write(data, 0, data.Length);
                    sc.TcpClient.GetStream().Flush();
                }
            }
        }

        // Debug and log messages
        #region LogDebugMessages

        private void Log(string message)
        {
            if (LogMessage)
                Console.WriteLine("[Log] " + message);
        }

        private void Log(object message)
        {
            if(LogMessage)
                Console.WriteLine("[Log] " + message.ToString());
        }

        private void Debug(string message)
        {
            if(DebugMessage)
                Console.WriteLine("[Debug] " + message);
        }

        private void Debug(object message)
        {
            if (DebugMessage)
                Console.WriteLine("[Debug] " + message.ToString());
        }

        #endregion

    }
}
