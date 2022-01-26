using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using Ukagaka.NET;

namespace recv_net
{
	public partial class FormRecv : Form
	{
		#region スレッド生成処理

		private System.Windows.Forms.Timer timer1;

		public const int FRAMERATE = 20;	//フレームレート

		private static string basedir;
		private static Thread thread;

		private static IntPtr hWndDSSTPServer;
		private static string uniqueID;

		[STAThread]
		public static void Start(IntPtr hwnd, string uniqueID, string basedir)
		{
			FormRecv.hWndDSSTPServer = hwnd;
			FormRecv.uniqueID = uniqueID;
			FormRecv.threadend = false;
			FormRecv.basedir = basedir;
			FormRecv.thread = new Thread(new ThreadStart(FormRecv.Thread));
			FormRecv.thread.Start();
		}

		private static bool threadend;
		public static void End()
		{
			FormRecv.threadend = true;
			System.Threading.Thread.Sleep(1);
			FormRecv.thread.Join();
		}

		private static void Thread()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormRecv());
		}

		private bool finalized = false;
		private void OnTick(object sender, EventArgs e)
		{
			if (!FormRecv.threadend)
			{
			}
			else
			{
				if (!finalized)
				{
					this.Close();
					this.Dispose();
					finalized = true;
				}
			}
		}

		#endregion

		#region フォーム内部処理

		// 里々からのメッセージ受信処理オブジェクト
		private Recv RecvObj = new Recv();
		// 内部保持用ログテキスト
		private string logMsg = "";

		public FormRecv()
		{
			InitializeComponent();

			this.timer1 = new System.Windows.Forms.Timer();
			this.timer1.Enabled = true;
			this.timer1.Interval = FormRecv.FRAMERATE;
			this.timer1.Tick += new EventHandler(OnTick);
		}

		private void FormRecv_Load(object sender, EventArgs e)
		{
			// 里々からのメッセージ受信処理開始
			Recv.RecvEvt += RecvObj_RecvInfoEvt;
			RecvObj.Start_GetMsgFromSATORI();
			try
			{
				this.Icon = new System.Drawing.Icon(FormRecv.basedir + "icon.ico");
			}
			catch (Exception)
			{
			}
		}

		private void FormRecv_FormClosed(object sender, FormClosedEventArgs e)
		{
			// メインスレッドを終了
			if (FormRecv.hWndDSSTPServer != IntPtr.Zero)
			{
				Dictionary<string, string> sstp = new Dictionary<string, string>();
				sstp["_METHOD_"] = "SEND";
				sstp["_PROTOCOL_"] = "SSTP";
				sstp["_VERSION_"] = "1.4";
				sstp["Charset"] = "Shift_JIS";
				sstp["Sender"] = "FormRecv";
				sstp["ID"] = FormRecv.uniqueID;
				sstp["HWnd"] = FormRecv.hWndDSSTPServer.ToString();
				sstp["Script"] = @"\![raise,OnClose]\e";

				DSSTP dsstp = new DSSTP(FormRecv.hWndDSSTPServer);
				dsstp.Send(Protocol.Header.Create(sstp));
			}
			// 里々からのメッセージ受信処理終了
			Recv.RecvEvt -= RecvObj_RecvInfoEvt;
			RecvObj.End_GetMsgFromSATORI();
		}

		// デリゲートの宣言(有効で無いスレッド間の操作用)
		private delegate void D_DspMyTextBox(string inf);

		private void RecvObj_RecvInfoEvt(object sender, Recv.RecvInfEventArgs e)
		{
			// TextBoxLogに里々のログを表示(有効で無いスレッド間の操作用)
			TextBoxLog.Invoke(new D_DspMyTextBox(DspMyTextBox), e.RecvInf);
		}

		public void DspMyTextBox(string inf)
		{
			if (this.ToolStripMenuItemPause.Checked)
			{
				// ログを内部に保持し、表示は反映しない
				logMsg += inf;
			}
			else
			{
				// ログを内部に保持し、TextBoxLogに里々のログを表示
				logMsg += inf;
				TextBoxLog.AppendText(inf);
			}
		}

		#endregion

		#region 特殊ショートカットキー

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Escape:
					ClearText();
					return false;
				case Keys.Pause:
					PauseUpdate();
					return false;
				default:
					return base.ProcessCmdKey(ref msg, keyData);
			}
		}

		#endregion

		#region メニュー

		// [ファイル]
		// 開く
		private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			// ゴーストエクスプローラ起動
			if (FormRecv.hWndDSSTPServer != IntPtr.Zero)
			{
				Dictionary<string, string> sstp = new Dictionary<string, string>();
				sstp["_METHOD_"] = "SEND";
				sstp["_PROTOCOL_"] = "SSTP";
				sstp["_VERSION_"] = "1.4";
				sstp["Charset"] = "Shift_JIS";
				sstp["Sender"] = "FormRecv";
				sstp["ID"] = FormRecv.uniqueID;
				sstp["HWnd"] = FormRecv.hWndDSSTPServer.ToString();
				sstp["Script"] = @"\![open,ghostexplorer]\e";

				DSSTP dsstp = new DSSTP(FormRecv.hWndDSSTPServer);
				dsstp.Send(Protocol.Header.Create(sstp));
			}
		}
		// 保存
		private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
		{
			string path = FormRecv.basedir + "recv.txt";
			StreamWriter sw = new StreamWriter(
				path, false, Encoding.GetEncoding("shift_jis"));
			sw.Write(TextBoxLog.Text);
			sw.Close();
			System.Diagnostics.Process.Start(path);
		}
		// 終了
		private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		// [編集]
		// クリア
		private void ToolStripMenuItemClear_Click(object sender, EventArgs e)
		{
			ClearText();
		}
		private void ClearText()
		{
			TextBoxLog.Text = "";
			logMsg = "";
		}
		// 表示を更新
		private void ToolStripMenuItemUpdate_Click(object sender, EventArgs e)
		{
			updateTextBox();
		}
		private void updateTextBox()
		{
			TextBoxLog.Text = logMsg;
			TextBoxLog.SelectionStart = TextBoxLog.Text.Length;
			TextBoxLog.Focus();
			TextBoxLog.ScrollToCaret();
		}
		// [表示]
		// 犬を表示
		private void ToolStripMenuItemShowDog_Click(object sender, EventArgs e)
		{
			this.ToolStripMenuItemShowDog.Checked =
				!this.ToolStripMenuItemShowDog.Checked;
			int surface;
			if (this.ToolStripMenuItemShowDog.Checked)
			{
				surface = 0;
			}
			else
			{
				surface = -1;
			}
			if (FormRecv.hWndDSSTPServer != IntPtr.Zero)
			{
				Dictionary<string, string> sstp = new Dictionary<string, string>();
				sstp["_METHOD_"] = "SEND";
				sstp["_PROTOCOL_"] = "SSTP";
				sstp["_VERSION_"] = "1.4";
				sstp["Charset"] = "Shift_JIS";
				sstp["Sender"] = "FormRecv";
				sstp["ID"] = FormRecv.uniqueID;
				sstp["HWnd"] = FormRecv.hWndDSSTPServer.ToString();
				sstp["Script"] = @"\0\s[" + surface + @"]\e";

				DSSTP dsstp = new DSSTP(FormRecv.hWndDSSTPServer);
				dsstp.Send(Protocol.Header.Create(sstp));
			}
		}
		// [設定]
		// リアルタイムで更新しない
		private void RToolStripMenuItemPause_Click(object sender, EventArgs e)
		{
			PauseUpdate();
		}
		private void PauseUpdate()
		{
			if (this.ToolStripMenuItemPause.Checked)
			{
				updateTextBox();
			}
			this.ToolStripMenuItemPause.Checked =
				!this.ToolStripMenuItemPause.Checked;
		}
		// 常に手前に表示
		private void ToolStripMenuItemTopMost_Click(object sender, EventArgs e)
		{
			this.ToolStripMenuItemTopMost.Checked =
				!this.ToolStripMenuItemTopMost.Checked;
			this.TopMost = !this.TopMost;
		}
		// [ヘルプ]
		// readmeを表示
		private void ToolStripMenuItemShowReadme_Click(object sender, EventArgs e)
		{
			if (FormRecv.hWndDSSTPServer != IntPtr.Zero)
			{
				Dictionary<string, string> sstp = new Dictionary<string, string>();
				sstp["_METHOD_"] = "SEND";
				sstp["_PROTOCOL_"] = "SSTP";
				sstp["_VERSION_"] = "1.4";
				sstp["Charset"] = "Shift_JIS";
				sstp["Sender"] = "FormRecv";
				sstp["ID"] = FormRecv.uniqueID;
				sstp["HWnd"] = FormRecv.hWndDSSTPServer.ToString();
				sstp["Script"] = @"\![open,readme]\e";

				DSSTP dsstp = new DSSTP(FormRecv.hWndDSSTPServer);
				dsstp.Send(Protocol.Header.Create(sstp));
			}
		}

		#endregion
	}
}
