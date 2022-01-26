using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;

//namespace recv_net
//{
	class Recv
	{
		// 里々からの情報を送信するイベントの定義
		public class RecvInfEventArgs : EventArgs
		{
			public string RecvInf;
		}
		public delegate void RecvEventHandler(object sender, RecvInfEventArgs e);
		public static event RecvEventHandler RecvEvt;
		protected virtual void OnRecvEvt(RecvInfEventArgs e)
		{
			if (RecvEvt != null)
			{
				RecvEvt(this, e);
			}
		}

		// ウィンドウクラスの名前
		private const string ClassName = "れしば";

		// ウィンドウクラス登録API
		private delegate IntPtr D_WNDPROC(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct WNDCLASSEX
		{
			public int cbSize;              // 構造体サイズ
			public uint style;               // スタイル
			public IntPtr lpfnWndProc;   // ウィンドウ処理関数
			public int cbClsExtra;          // 拡張情報1
			public int cbWndExtra;          // 拡張情報2
			public IntPtr hInstance;           // インスタンのスハンドル
			public IntPtr hIcon;               // アイコンのハンドル
			public IntPtr hCursor;             // カーソルのハンドル
			public IntPtr hbrBackground;       // ウィンドウ背景のハンドル
			public string lpszMenuName;        // メニューの名前
			public string lpszClassName;    // ウィンドウクラスの名前
			public IntPtr hIconSm;             // 小さいアイコンのハンドル
		};
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterClassExW")]
		private static extern ushort RegisterClassEx(ref WNDCLASSEX wcex);

		// ウィンドウ作成API
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateWindowEx")]
		private static extern IntPtr CreateWindowEx(
			int dwExStyle,
			string lpClassName,
			string lpWindowName,
			uint dwStyle,
			int X,
			int Y,
			int nWidth,
			int nHeight,
			IntPtr hWndParent,
			IntPtr hMenu,
			IntPtr hInstance,
			IntPtr lpParam);

		// ウィンドウ破棄API
		[DllImport("user32.dll")]
		private static extern IntPtr DestroyWindow(IntPtr hWnd);

		// メッセージ取得API
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetMessageW")]
		private static extern IntPtr GetMessage(ref Message lpMsg, IntPtr hWnd, int wMsgFilterMin, int wMsgFilterMax);

		// ウィンドウプロシージャへのメッセージ送信API
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr TranslateMessage(ref Message lpMsg);
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "DispatchMessageW")]
		private static extern IntPtr DispatchMessage(ref Message lpMsg);

		// デフォルトのメッセージ処理API
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "DefWindowProcW")]
		private static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		// メッセージ送信API
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SendMessageW")]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "PostMessageW")]
		private static extern IntPtr PostMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		// ウィンドウ・メッセージの定義
		private const int WM_NULL = 0x0;
		private const int WM_DESTROY = 0x2;
		private const int WM_CLOSE = 0x10;
		private const int WM_QUIT = 0x12;

		// 里々からのメッセージ定義
		private const int WM_COPYDATA = 0x4A;
		[StructLayout(LayoutKind.Sequential)]
		private struct COPYDATASTRUCT
		{
			public IntPtr dwData;
			public int cbData;
			public IntPtr lpData;
		};

		// ウィンドウ処理関数をデリゲート変数に保存
		// (ガベージ コレクションされたデリゲートのエラー例外の対応)
		private D_WNDPROC MsgFromSATORI_WndProc;

		// ウィンドウクラスのスレッド
		private Thread MsgFromSATORI_Thread = null;

		// ウィンドウクラスのハンドル
		private IntPtr MsgFromSATORI_hWnd = IntPtr.Zero;

		// 里々からのメッセージ受信処理開始
		public void Start_GetMsgFromSATORI()
		{
			try
			{
				MsgFromSATORI_WndProc = MyWndProc;
				MsgFromSATORI_Thread = new Thread(new ThreadStart(GetMsgFromSATORI));
				if (MsgFromSATORI_Thread != null)
				{
					MsgFromSATORI_Thread.Start();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// 里々からのメッセージ受信処理終了
		public void End_GetMsgFromSATORI()
		{
			try
			{
				if (MsgFromSATORI_hWnd != IntPtr.Zero)
				{
					SendMessage(MsgFromSATORI_hWnd, WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			//MsgFromSATORI_Thread.Join();
		}

		// 里々からのメッセージ受信処理
		private void GetMsgFromSATORI()
		{
			try
			{
				const uint CS_GLOBALCLASS = 0x4000;
				// ウィンドウクラス登録
				WNDCLASSEX wcex = new WNDCLASSEX();
				wcex.cbSize = Marshal.SizeOf(wcex);
				wcex.hInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);
				wcex.lpszClassName = ClassName;
				wcex.lpfnWndProc = Marshal.GetFunctionPointerForDelegate(MsgFromSATORI_WndProc);
				wcex.style = CS_GLOBALCLASS;
				wcex.hIcon = IntPtr.Zero;
				wcex.hIconSm = IntPtr.Zero;
				wcex.hCursor = IntPtr.Zero;
				wcex.lpszMenuName = "";
				wcex.cbClsExtra = 0;
				wcex.cbWndExtra = 0;
				wcex.hbrBackground = IntPtr.Zero;
				ushort a = RegisterClassEx(ref wcex);

				// ウィンドウ作成
				MsgFromSATORI_hWnd =
					CreateWindowEx(0, wcex.lpszClassName, wcex.lpszClassName,
					0, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, wcex.hInstance, IntPtr.Zero);

				if (MsgFromSATORI_hWnd != IntPtr.Zero)
				{
					// メッセージ取得
					int rtn;
					Message mmm = new Message();
					while (true)
					{
						rtn = (int)GetMessage(ref mmm, MsgFromSATORI_hWnd, 0, 0);
						if (rtn <= 0) break;
						// ウィンドウプロシージャへのメッセージ送信
						try { TranslateMessage(ref mmm); }
						catch (Exception) { }
						try { DispatchMessage(ref mmm); }
						catch (Exception) { }
					}
				}
			}
			catch (Exception) { }
			finally
			{
				// ウィンドウ破棄
				try { DestroyWindow(MsgFromSATORI_hWnd); }
				catch (Exception) { }
				// ウィンドウクラスのハンドル初期化
				MsgFromSATORI_hWnd = IntPtr.Zero;
			}
		}

		// カスタマイズしたウィンドウ処理関数
		private IntPtr MyWndProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam)
		{
			try
			{
				switch (wMsg)
				{
					case WM_COPYDATA:
						string str = "";
						COPYDATASTRUCT cds = new COPYDATASTRUCT();
						cds = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT));
						if (cds.cbData > 0)
						{
							byte[] data = new byte[cds.cbData];
							Marshal.Copy(cds.lpData, data, 0, cds.cbData);
							Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
							str = sjisEnc.GetString(data).Replace("\0", "\r\n");
						}
						RecvInfEventArgs e = new RecvInfEventArgs();
						e.RecvInf = str;
						OnRecvEvt(e);
						return IntPtr.Zero;
					case WM_DESTROY:
						PostMessage(hWnd, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
						break;
					case WM_CLOSE:
						break;
					case WM_QUIT:
						break;
					case WM_NULL:
						break;
				}
				// デフォルトのメッセージ処理
				return DefWindowProc(hWnd, wMsg, wParam, lParam);
			}
			catch (Exception) { }
			return IntPtr.Zero;
		}
	}
//}
