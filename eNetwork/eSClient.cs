using System.Net.Sockets;

namespace eNetwork
{
    public class eSClient
    {

        // Variables
        #region Variables

        public int ID { get { return id; } }
        private int id;

        public TcpClient TcpClient { get { return client; } }
        private TcpClient client;

        #endregion

        // Constructor
        public eSClient(int ID, TcpClient client)
        {
            id = ID;
            this.client = client;
        }

    }
}
