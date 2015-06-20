using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace eNetwork
{
    public class eClient
    {

        // Variables
        #region Variables

        private string IP;
        private int Port;

        public bool Connected { get { return client.Connected; } }

        public bool DebugMessage { get; set; }

        public bool LogMessage { get; set; }

        private TcpClient client;
        private Thread HandleThread;

        public int ID { get { return id; } }
        private int id;

        public int ReceiveBufferSize { get { return client.ReceiveBufferSize; } set { client.ReceiveBufferSize = value; } }

        public delegate void ClientReceiveDataHandler(byte[] data);
        public delegate void ClientConnected();
        public delegate void ClientDisconnected();

        public event ClientReceiveDataHandler OnClientDataReceived;
        public event ClientConnected OnClientConnected;
        public event ClientDisconnected OnClientDisconnected;

        #endregion

        // Constructor
        public eClient(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
            this.id = -1;
            HandleThread = new Thread(new ThreadStart(Handle));
            client = new TcpClient();
            client.ReceiveBufferSize = 512;
            DebugMessage = false;
            LogMessage = false;
        }

        // Connect
        public void Connect()
        {
            try
            {
                client.Connect(IP, Port);
                byte[] id = new byte[client.ReceiveBufferSize];
                client.GetStream().Read(id, 0, client.ReceiveBufferSize);
                this.id = Convert.ToInt16(Encoding.UTF8.GetString(id).TrimEnd());
                Log("Connected with ID : " + this.id);
                if (OnClientConnected != null)
                    OnClientConnected.Invoke();
                HandleThread.Start();
            }
            catch (Exception ex)
            {
                Debug("Error when connecting : " + ex.Message);
            }
        }

        // Disconnect
        public void Disconnect()
        {
            if (HandleThread.ThreadState == ThreadState.Running)
                HandleThread.Abort();
            if (client.Connected)
                client.Close();
        }

        // Handle the client
        private void Handle()
        {
            
            while (true)
            {
                byte[] data = new byte[client.ReceiveBufferSize];

                try
                {
                    client.GetStream().Read(data, 0, client.ReceiveBufferSize);
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2146232800)
                        break;
                    Debug("Error when reading from stream : " + ex.Message);
                }

                if (OnClientDataReceived != null)
                {
                    try
                    {
                        OnClientDataReceived.Invoke(data);
                    }
                    catch (Exception ex)
                    {
                        Debug("Error when invoking OnDataReceived method : " + ex.Message);
                    }
                }
            }

            if (OnClientDisconnected != null)
                OnClientDisconnected.Invoke();
        }

        // Send data to server
        public void Send(byte[] data)
        {
            try
            {
                client.GetStream().Write(data, 0, data.Length);
                client.GetStream().Flush();
            }
            catch (Exception ex)
            {
                Debug("Error when sending message : " + ex.Message);
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
            if (LogMessage)
                Console.WriteLine("[Log] " + message.ToString());
        }

        private void Debug(string message)
        {
            if (DebugMessage)
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
