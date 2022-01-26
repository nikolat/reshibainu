namespace recv_net
{
	partial class FormRecv
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.TextBoxLog = new System.Windows.Forms.TextBox();
			this.menuStripMain = new System.Windows.Forms.MenuStrip();
			this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemShowDog = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemConfig = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemPause = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemRecieveCondition = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItemTopMost = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemShowReadme = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// TextBoxLog
			// 
			this.TextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxLog.Location = new System.Drawing.Point(12, 29);
			this.TextBoxLog.Multiline = true;
			this.TextBoxLog.Name = "TextBoxLog";
			this.TextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextBoxLog.Size = new System.Drawing.Size(560, 372);
			this.TextBoxLog.TabIndex = 0;
			// 
			// menuStripMain
			// 
			this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile,
            this.ToolStripMenuItemEdit,
            this.ToolStripMenuItemView,
            this.ToolStripMenuItemConfig,
            this.ToolStripMenuItemHelp});
			this.menuStripMain.Location = new System.Drawing.Point(0, 0);
			this.menuStripMain.Name = "menuStripMain";
			this.menuStripMain.Size = new System.Drawing.Size(584, 26);
			this.menuStripMain.TabIndex = 1;
			this.menuStripMain.Text = "menuStripMain";
			// 
			// ToolStripMenuItemFile
			// 
			this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOpen,
            this.ToolStripMenuItemSep1,
            this.ToolStripMenuItemSave,
            this.ToolStripMenuItemSep2,
            this.ToolStripMenuItemClose});
			this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
			this.ToolStripMenuItemFile.Size = new System.Drawing.Size(85, 22);
			this.ToolStripMenuItemFile.Text = "ファイル(&F)";
			// 
			// ToolStripMenuItemOpen
			// 
			this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
			this.ToolStripMenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.ToolStripMenuItemOpen.Size = new System.Drawing.Size(165, 22);
			this.ToolStripMenuItemOpen.Text = "開く(&O)";
			this.ToolStripMenuItemOpen.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
			// 
			// ToolStripMenuItemSep1
			// 
			this.ToolStripMenuItemSep1.Name = "ToolStripMenuItemSep1";
			this.ToolStripMenuItemSep1.Size = new System.Drawing.Size(162, 6);
			// 
			// ToolStripMenuItemSave
			// 
			this.ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
			this.ToolStripMenuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.ToolStripMenuItemSave.Size = new System.Drawing.Size(165, 22);
			this.ToolStripMenuItemSave.Text = "保存(&T)";
			this.ToolStripMenuItemSave.Click += new System.EventHandler(this.ToolStripMenuItemSave_Click);
			// 
			// ToolStripMenuItemSep2
			// 
			this.ToolStripMenuItemSep2.Name = "ToolStripMenuItemSep2";
			this.ToolStripMenuItemSep2.Size = new System.Drawing.Size(162, 6);
			// 
			// ToolStripMenuItemClose
			// 
			this.ToolStripMenuItemClose.Name = "ToolStripMenuItemClose";
			this.ToolStripMenuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.ToolStripMenuItemClose.Size = new System.Drawing.Size(165, 22);
			this.ToolStripMenuItemClose.Text = "終了(&E)";
			this.ToolStripMenuItemClose.Click += new System.EventHandler(this.ToolStripMenuItemClose_Click);
			// 
			// ToolStripMenuItemEdit
			// 
			this.ToolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemClear,
            this.ToolStripMenuItemUpdate});
			this.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit";
			this.ToolStripMenuItemEdit.Size = new System.Drawing.Size(61, 22);
			this.ToolStripMenuItemEdit.Text = "編集(&E)";
			// 
			// ToolStripMenuItemClear
			// 
			this.ToolStripMenuItemClear.Name = "ToolStripMenuItemClear";
			this.ToolStripMenuItemClear.ShortcutKeyDisplayString = "Esc";
			this.ToolStripMenuItemClear.Size = new System.Drawing.Size(177, 22);
			this.ToolStripMenuItemClear.Text = "クリア(&C)";
			this.ToolStripMenuItemClear.Click += new System.EventHandler(this.ToolStripMenuItemClear_Click);
			// 
			// ToolStripMenuItemUpdate
			// 
			this.ToolStripMenuItemUpdate.Name = "ToolStripMenuItemUpdate";
			this.ToolStripMenuItemUpdate.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.ToolStripMenuItemUpdate.Size = new System.Drawing.Size(177, 22);
			this.ToolStripMenuItemUpdate.Text = "表示を更新(&U)";
			this.ToolStripMenuItemUpdate.Click += new System.EventHandler(this.ToolStripMenuItemUpdate_Click);
			// 
			// ToolStripMenuItemView
			// 
			this.ToolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemShowDog});
			this.ToolStripMenuItemView.Name = "ToolStripMenuItemView";
			this.ToolStripMenuItemView.Size = new System.Drawing.Size(62, 22);
			this.ToolStripMenuItemView.Text = "表示(&V)";
			// 
			// ToolStripMenuItemShowDog
			// 
			this.ToolStripMenuItemShowDog.Name = "ToolStripMenuItemShowDog";
			this.ToolStripMenuItemShowDog.Size = new System.Drawing.Size(152, 22);
			this.ToolStripMenuItemShowDog.Text = "犬を表示(&D)";
			this.ToolStripMenuItemShowDog.Click += new System.EventHandler(this.ToolStripMenuItemShowDog_Click);
			// 
			// ToolStripMenuItemConfig
			// 
			this.ToolStripMenuItemConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemPause,
            this.ToolStripMenuItemRecieveCondition,
            this.ToolStripMenuItemSep3,
            this.ToolStripMenuItemTopMost});
			this.ToolStripMenuItemConfig.Name = "ToolStripMenuItemConfig";
			this.ToolStripMenuItemConfig.Size = new System.Drawing.Size(62, 22);
			this.ToolStripMenuItemConfig.Text = "設定(&C)";
			// 
			// ToolStripMenuItemPause
			// 
			this.ToolStripMenuItemPause.Name = "ToolStripMenuItemPause";
			this.ToolStripMenuItemPause.ShortcutKeyDisplayString = "Pause";
			this.ToolStripMenuItemPause.Size = new System.Drawing.Size(280, 22);
			this.ToolStripMenuItemPause.Text = "リアルタイムで更新しない(&S)";
			this.ToolStripMenuItemPause.Click += new System.EventHandler(this.RToolStripMenuItemPause_Click);
			// 
			// ToolStripMenuItemRecieveCondition
			// 
			this.ToolStripMenuItemRecieveCondition.Enabled = false;
			this.ToolStripMenuItemRecieveCondition.Name = "ToolStripMenuItemRecieveCondition";
			this.ToolStripMenuItemRecieveCondition.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.ToolStripMenuItemRecieveCondition.Size = new System.Drawing.Size(280, 22);
			this.ToolStripMenuItemRecieveCondition.Text = "受付条件(&R)";
			// 
			// ToolStripMenuItemSep3
			// 
			this.ToolStripMenuItemSep3.Name = "ToolStripMenuItemSep3";
			this.ToolStripMenuItemSep3.Size = new System.Drawing.Size(277, 6);
			// 
			// ToolStripMenuItemTopMost
			// 
			this.ToolStripMenuItemTopMost.Name = "ToolStripMenuItemTopMost";
			this.ToolStripMenuItemTopMost.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.ToolStripMenuItemTopMost.Size = new System.Drawing.Size(280, 22);
			this.ToolStripMenuItemTopMost.Text = "常に手前に表示(&T)";
			this.ToolStripMenuItemTopMost.Click += new System.EventHandler(this.ToolStripMenuItemTopMost_Click);
			// 
			// ToolStripMenuItemHelp
			// 
			this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemShowReadme});
			this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
			this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(75, 22);
			this.ToolStripMenuItemHelp.Text = "ヘルプ(&H)";
			// 
			// ToolStripMenuItemShowReadme
			// 
			this.ToolStripMenuItemShowReadme.Name = "ToolStripMenuItemShowReadme";
			this.ToolStripMenuItemShowReadme.Size = new System.Drawing.Size(176, 22);
			this.ToolStripMenuItemShowReadme.Text = "readmeを表示(&G)";
			this.ToolStripMenuItemShowReadme.Click += new System.EventHandler(this.ToolStripMenuItemShowReadme_Click);
			// 
			// FormRecv
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 413);
			this.Controls.Add(this.TextBoxLog);
			this.Controls.Add(this.menuStripMain);
			this.MainMenuStrip = this.menuStripMain;
			this.Name = "FormRecv";
			this.Text = "れしばいぬ";
			this.Load += new System.EventHandler(this.FormRecv_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRecv_FormClosed);
			this.menuStripMain.ResumeLayout(false);
			this.menuStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBoxLog;
		private System.Windows.Forms.MenuStrip menuStripMain;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSave;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEdit;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemConfig;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClear;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemUpdate;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPause;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRecieveCondition;
		private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSep2;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemClose;
		private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSep3;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTopMost;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpen;
		private System.Windows.Forms.ToolStripSeparator ToolStripMenuItemSep1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemView;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemShowDog;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemShowReadme;
	}
}

