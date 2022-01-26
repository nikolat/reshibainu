using System;
using System.Runtime.InteropServices;

namespace Ukagaka.NET {
	class DSSTP
	{
		[DllImport("USER32.dll")]
		public static extern uint SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

		[DllImport("USER32.dll")]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

		[StructLayout(LayoutKind.Sequential)]
		public struct COPYDATASTRUCT
		{
			public IntPtr dwData;
			public uint   cbData;
            [MarshalAs(UnmanagedType.LPStr)]
			public string lpData;
		}

		private IntPtr hWnd;

		private static int  DEFAULT_PORT = 9801;
		private static uint WM_COPYDATA = 0x0000004a;

		public DSSTP(IntPtr hWnd)
		{
			this.hWnd = hWnd;
		}

		public uint Send(string sstp)
		{
			return Send(this.hWnd, DEFAULT_PORT, sstp);
		}

		public uint Send(IntPtr hwnd, int port, string sstp)
		{
			COPYDATASTRUCT cds;
			cds.dwData = (IntPtr) port;
			cds.cbData = (uint)   sstp.Length;
			cds.lpData = sstp;

			uint result = SendMessage(hwnd, WM_COPYDATA, IntPtr.Zero, ref cds);

			return result;
		}
	}
}
