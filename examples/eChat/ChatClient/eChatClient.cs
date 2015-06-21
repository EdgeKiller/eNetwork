using System;
using System.Windows.Forms;
using eNetwork;

namespace ChatClient
{
    public partial class eChatClient : Form
    {
        private eClient client;

        public eChatClient()
        {
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (!groupBox_chat.Enabled)
            {
                if (textBox_ip.Text.Trim() != "" && textBox_name.Text.Trim() != "" && textBox_ip.Text.Contains(":"))
                {
                    groupBox_connection.Enabled = false;
                    client = new eClient(textBox_ip.Text.Split(':')[0], Convert.ToInt32(textBox_ip.Text.Split(':')[1]));
                    client.LogMessage = false;
                    client.DebugMessage = false;
                    client.ReceiveBufferSize = 1024;
                    client.OnConnected += client_OnConnected;
                    client.OnDisconnected += client_OnDisconnected;
                    client.OnDataReceived += client_OnDataReceived;
                    client.Connect();
                    groupBox_chat.Enabled = true;
                    button_connect.Text = "Disconnect !";
                }
            }
            else
            {
                client.Disconnect();
                groupBox_chat.Enabled = false;
                groupBox_connection.Enabled = true;
                button_connect.Text = "Connect !";
            }
        }

        void client_OnDataReceived(byte[] data)
        {
            if (eUtils.IsPacketCompressed(data))
            {
                ePacket packet = eUtils.Deserialize(eUtils.Decompress(data));
                switch (packet.Name)
                {
                    case "connectPacket":
                        listBox_messages.Items.Add("Client connected : " + packet.datas["name"]);
                        break;

                    case "disconnectPacket":
                        listBox_messages.Items.Add("Client disconnected : " + packet.datas["name"]);
                        break;

                    case "messagePacket":
                        listBox_messages.Items.Add(packet.datas["name"] + " : " + packet.datas["message"]);
                        break;
                }
            }
        }

        void client_OnDisconnected()
        {
            ePacket disconnectPacket = new ePacket("disconnectPacket");
            disconnectPacket.datas.Add("name", textBox_name.Text.Trim());
            client.Send(eUtils.Compress(eUtils.Serialize(disconnectPacket)));
        }

        void client_OnConnected()
        {
            ePacket connectPacket = new ePacket("connectPacket");
            connectPacket.datas.Add("name", textBox_name.Text.Trim());
            client.Send(eUtils.Compress(eUtils.Serialize(connectPacket)));
        }

        private void textBox_message_KeyDown(object sender, KeyEventArgs e)
        {
            if(textBox_message.Text.Trim() != "" && e.KeyCode == Keys.Enter)
            {
                ePacket messagePacket = new ePacket("messagePacket");
                messagePacket.datas.Add("name", textBox_name.Text.Trim());
                messagePacket.datas.Add("message", textBox_message.Text.Trim());
                client.Send(eUtils.Compress(eUtils.Serialize(messagePacket)));
                textBox_message.Text = "";
            }
        }

        private void eChatClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            ePacket disconnectPacket = new ePacket("disconnectPacket");
            disconnectPacket.datas.Add("name", textBox_name.Text.Trim());
            client.Send(eUtils.Compress(eUtils.Serialize(disconnectPacket)));
        }
    }
}
