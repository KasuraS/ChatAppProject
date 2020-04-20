namespace ClientGUI
{
    partial class MainHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainHome));
            this.CreateTopicButton = new System.Windows.Forms.Button();
            this.TopicContainer = new System.Windows.Forms.GroupBox();
            this.Panel4UsersInList = new System.Windows.Forms.FlowLayoutPanel();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TopicLabel = new System.Windows.Forms.Label();
            this.InputMessage = new System.Windows.Forms.RichTextBox();
            this.MsgTextBox = new System.Windows.Forms.RichTextBox();
            this.WelcomeMsg = new System.Windows.Forms.TextBox();
            this.Container1 = new System.Windows.Forms.GroupBox();
            this.Panel4TopicsList1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Container2 = new System.Windows.Forms.GroupBox();
            this.Panel4TopicsList2 = new System.Windows.Forms.FlowLayoutPanel();
            this.PrivateChatButton = new System.Windows.Forms.Button();
            this.TopicContainer.SuspendLayout();
            this.Container1.SuspendLayout();
            this.Container2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CreateTopicButton
            // 
            this.CreateTopicButton.Location = new System.Drawing.Point(12, 12);
            this.CreateTopicButton.Name = "CreateTopicButton";
            this.CreateTopicButton.Size = new System.Drawing.Size(86, 23);
            this.CreateTopicButton.TabIndex = 2;
            this.CreateTopicButton.Text = "Create Topic";
            this.CreateTopicButton.UseVisualStyleBackColor = true;
            this.CreateTopicButton.Click += new System.EventHandler(this.CreateTopicButton_Click);
            // 
            // TopicContainer
            // 
            this.TopicContainer.Controls.Add(this.Panel4UsersInList);
            this.TopicContainer.Controls.Add(this.SendMessageButton);
            this.TopicContainer.Controls.Add(this.label2);
            this.TopicContainer.Controls.Add(this.TopicLabel);
            this.TopicContainer.Controls.Add(this.InputMessage);
            this.TopicContainer.Controls.Add(this.MsgTextBox);
            this.TopicContainer.Location = new System.Drawing.Point(241, 6);
            this.TopicContainer.Name = "TopicContainer";
            this.TopicContainer.Size = new System.Drawing.Size(591, 453);
            this.TopicContainer.TabIndex = 4;
            this.TopicContainer.TabStop = false;
            this.TopicContainer.Visible = false;
            // 
            // Panel4UsersInList
            // 
            this.Panel4UsersInList.BackColor = System.Drawing.SystemColors.Window;
            this.Panel4UsersInList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel4UsersInList.Location = new System.Drawing.Point(448, 19);
            this.Panel4UsersInList.Margin = new System.Windows.Forms.Padding(5);
            this.Panel4UsersInList.Name = "Panel4UsersInList";
            this.Panel4UsersInList.Size = new System.Drawing.Size(142, 382);
            this.Panel4UsersInList.TabIndex = 6;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Enabled = false;
            this.SendMessageButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendMessageButton.Location = new System.Drawing.Point(448, 407);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(143, 46);
            this.SendMessageButton.TabIndex = 5;
            this.SendMessageButton.Text = "SEND MESSAGE";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(15, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Your message :";
            // 
            // TopicLabel
            // 
            this.TopicLabel.AutoSize = true;
            this.TopicLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TopicLabel.Location = new System.Drawing.Point(14, 0);
            this.TopicLabel.Name = "TopicLabel";
            this.TopicLabel.Size = new System.Drawing.Size(67, 20);
            this.TopicLabel.TabIndex = 3;
            this.TopicLabel.Text = "MyTopic";
            // 
            // InputMessage
            // 
            this.InputMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputMessage.BackColor = System.Drawing.Color.White;
            this.InputMessage.Location = new System.Drawing.Point(8, 396);
            this.InputMessage.Name = "InputMessage";
            this.InputMessage.Size = new System.Drawing.Size(428, 57);
            this.InputMessage.TabIndex = 2;
            this.InputMessage.Text = "";
            this.InputMessage.TextChanged += new System.EventHandler(this.InputMessage_TextChanged);
            // 
            // MsgTextBox
            // 
            this.MsgTextBox.Location = new System.Drawing.Point(8, 27);
            this.MsgTextBox.Name = "MsgTextBox";
            this.MsgTextBox.ReadOnly = true;
            this.MsgTextBox.Size = new System.Drawing.Size(428, 346);
            this.MsgTextBox.TabIndex = 1;
            this.MsgTextBox.Text = "";
            // 
            // WelcomeMsg
            // 
            this.WelcomeMsg.BackColor = System.Drawing.SystemColors.Control;
            this.WelcomeMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.WelcomeMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.WelcomeMsg.Location = new System.Drawing.Point(13, 413);
            this.WelcomeMsg.Name = "WelcomeMsg";
            this.WelcomeMsg.Size = new System.Drawing.Size(222, 19);
            this.WelcomeMsg.TabIndex = 6;
            this.WelcomeMsg.Text = "Welcome, user!";
            this.WelcomeMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Container1
            // 
            this.Container1.Controls.Add(this.Panel4TopicsList1);
            this.Container1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Container1.Location = new System.Drawing.Point(12, 54);
            this.Container1.Name = "Container1";
            this.Container1.Size = new System.Drawing.Size(223, 160);
            this.Container1.TabIndex = 7;
            this.Container1.TabStop = false;
            this.Container1.Text = "My Topics List";
            // 
            // Panel4TopicsList1
            // 
            this.Panel4TopicsList1.AutoScroll = true;
            this.Panel4TopicsList1.Location = new System.Drawing.Point(3, 20);
            this.Panel4TopicsList1.Name = "Panel4TopicsList1";
            this.Panel4TopicsList1.Size = new System.Drawing.Size(214, 140);
            this.Panel4TopicsList1.TabIndex = 6;
            // 
            // Container2
            // 
            this.Container2.Controls.Add(this.Panel4TopicsList2);
            this.Container2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Container2.Location = new System.Drawing.Point(12, 218);
            this.Container2.Name = "Container2";
            this.Container2.Size = new System.Drawing.Size(223, 160);
            this.Container2.TabIndex = 8;
            this.Container2.TabStop = false;
            this.Container2.Text = "Other Topics";
            // 
            // Panel4TopicsList2
            // 
            this.Panel4TopicsList2.AutoScroll = true;
            this.Panel4TopicsList2.Location = new System.Drawing.Point(3, 20);
            this.Panel4TopicsList2.Name = "Panel4TopicsList2";
            this.Panel4TopicsList2.Size = new System.Drawing.Size(214, 140);
            this.Panel4TopicsList2.TabIndex = 6;
            // 
            // PrivateChatButton
            // 
            this.PrivateChatButton.Location = new System.Drawing.Point(144, 12);
            this.PrivateChatButton.Name = "PrivateChatButton";
            this.PrivateChatButton.Size = new System.Drawing.Size(85, 23);
            this.PrivateChatButton.TabIndex = 9;
            this.PrivateChatButton.Text = "Private Chat";
            this.PrivateChatButton.UseVisualStyleBackColor = true;
            // 
            // MainHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 471);
            this.Controls.Add(this.PrivateChatButton);
            this.Controls.Add(this.Container2);
            this.Controls.Add(this.Container1);
            this.Controls.Add(this.WelcomeMsg);
            this.Controls.Add(this.TopicContainer);
            this.Controls.Add(this.CreateTopicButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chatbox - Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainHome_FormClosing);
            this.Load += new System.EventHandler(this.MainHome_Load);
            this.TopicContainer.ResumeLayout(false);
            this.TopicContainer.PerformLayout();
            this.Container1.ResumeLayout(false);
            this.Container2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CreateTopicButton;
        private System.Windows.Forms.GroupBox TopicContainer;
        private System.Windows.Forms.RichTextBox MsgTextBox;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TopicLabel;
        private System.Windows.Forms.RichTextBox InputMessage;
        private System.Windows.Forms.TextBox WelcomeMsg;
        private System.Windows.Forms.GroupBox Container1;
        private System.Windows.Forms.GroupBox Container2;
        private System.Windows.Forms.FlowLayoutPanel Panel4TopicsList1;
        private System.Windows.Forms.FlowLayoutPanel Panel4TopicsList2;
        private System.Windows.Forms.FlowLayoutPanel Panel4UsersInList;
        private System.Windows.Forms.Button PrivateChatButton;
    }
}