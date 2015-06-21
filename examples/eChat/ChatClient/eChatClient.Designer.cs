namespace ChatClient
{
    partial class eChatClient
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.groupBox_connection = new System.Windows.Forms.GroupBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.groupBox_chat = new System.Windows.Forms.GroupBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.listBox_messages = new System.Windows.Forms.ListBox();
            this.groupBox_connection.SuspendLayout();
            this.groupBox_chat.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(6, 19);
            this.textBox_ip.MaxLength = 20;
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(125, 20);
            this.textBox_ip.TabIndex = 0;
            this.textBox_ip.Text = "127.0.0.1:2048";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(292, 28);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(100, 23);
            this.button_connect.TabIndex = 1;
            this.button_connect.Text = "Connect !";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // groupBox_connection
            // 
            this.groupBox_connection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_connection.Controls.Add(this.textBox_name);
            this.groupBox_connection.Controls.Add(this.textBox_ip);
            this.groupBox_connection.Location = new System.Drawing.Point(12, 12);
            this.groupBox_connection.Name = "groupBox_connection";
            this.groupBox_connection.Size = new System.Drawing.Size(274, 47);
            this.groupBox_connection.TabIndex = 2;
            this.groupBox_connection.TabStop = false;
            this.groupBox_connection.Text = "Connection";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(137, 19);
            this.textBox_name.MaxLength = 20;
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(125, 20);
            this.textBox_name.TabIndex = 2;
            this.textBox_name.Text = "ValdGeorgio";
            // 
            // groupBox_chat
            // 
            this.groupBox_chat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_chat.Controls.Add(this.textBox_message);
            this.groupBox_chat.Controls.Add(this.listBox_messages);
            this.groupBox_chat.Enabled = false;
            this.groupBox_chat.Location = new System.Drawing.Point(12, 65);
            this.groupBox_chat.Name = "groupBox_chat";
            this.groupBox_chat.Size = new System.Drawing.Size(380, 205);
            this.groupBox_chat.TabIndex = 3;
            this.groupBox_chat.TabStop = false;
            this.groupBox_chat.Text = "Chat";
            // 
            // textBox_message
            // 
            this.textBox_message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_message.Location = new System.Drawing.Point(11, 174);
            this.textBox_message.MaxLength = 256;
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(363, 20);
            this.textBox_message.TabIndex = 1;
            this.textBox_message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_message_KeyDown);
            // 
            // listBox_messages
            // 
            this.listBox_messages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_messages.FormattingEnabled = true;
            this.listBox_messages.Location = new System.Drawing.Point(11, 19);
            this.listBox_messages.Name = "listBox_messages";
            this.listBox_messages.Size = new System.Drawing.Size(363, 134);
            this.listBox_messages.TabIndex = 0;
            // 
            // eChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 282);
            this.Controls.Add(this.groupBox_chat);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.groupBox_connection);
            this.MinimumSize = new System.Drawing.Size(420, 320);
            this.Name = "eChatClient";
            this.ShowIcon = false;
            this.Text = "eChat • Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.eChatClient_FormClosing);
            this.groupBox_connection.ResumeLayout(false);
            this.groupBox_connection.PerformLayout();
            this.groupBox_chat.ResumeLayout(false);
            this.groupBox_chat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.GroupBox groupBox_connection;
        private System.Windows.Forms.GroupBox groupBox_chat;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.ListBox listBox_messages;
        private System.Windows.Forms.TextBox textBox_name;
    }
}

