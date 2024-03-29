﻿/**
 * THIS IS A BAD SAMPLE
 *
 * これはミニマムコードで動くようにしてあるだけなので、意味がないOOPの例です。
 * 本来なら例えば、Baseクラス->GETクラス->OnXXXXXクラス->ユーザークラス
 * という風に継承し、フレームワークとポリモフィズムを活用すべきと思います。
 * ゴーストでは切り分けが難しそうですが、できればMVCらしくしたいところです。
 *
 */

using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;

using D.N;
using Ukagaka.NET.Interfaces;

using recv_net;

namespace Ukagaka.NET
{
	using Header = Protocol.Header;

	class Shiori : MarshalByRefObject, IShiori30
	{
		public const string PROTOCOL = "SHIORI";
		public const string VERSION  = "3.0";
		public const string SENDER   = "D.N.Proxy";
		public const string SENDER_VERSION = "2.0.0";
		public const string CHARSET = "Shift_JIS";

		private bool initialized = false;
		private string basedir = "";
		private IntPtr hwnd;
		private string uniqueID;

		public bool load(IntPtr h, int len)
		{
			this.basedir = Marshal.PtrToStringAnsi(h, len);
			Marshal.FreeHGlobal(h);
			return true;
		}

		public bool unload()
		{
			return true;
		}

		public IntPtr request(IntPtr h, ref int len)
		{
			string request = Marshal.PtrToStringAnsi(h, len);
			Marshal.FreeHGlobal(h);
			string r = this.request(request);
			len = System.Text.Encoding.GetEncoding(Shiori.CHARSET).GetByteCount(r);
			return Marshal.StringToHGlobalAnsi(r);
		}

		public string request(string req)
		{
			string request = req;
			Dictionary<string, string> header = Header.Parse(Header.Unescape(request));
			string value = "";

			Dictionary<string, string> response = new Dictionary<string, string>();
			response["_PROTOCOL_"] = Shiori.PROTOCOL;
			response["_VERSION_"]  = Shiori.VERSION;
			response["Sender"]     = Shiori.SENDER;
			response["Charset"]    = Shiori.CHARSET;
			// SHIORIに対するリクエストでない場合
			if (!header.ContainsKey("_COMMANDLINE_") || !(header["_PROTOCOL_"] == "SHIORI" && header["_VERSION_"] == "3.0")) {
				response["_STATUS_"] = Protocol.STATUS_BAD_REQUEST;
				response["_STRING_"] = Protocol.STRING_BAD_REQUEST;
				return Header.Create(response);
			}
			// 未知のリクエスト
			if (!(header["_METHOD_"] == "NOTIFY" || header["_METHOD_"] == "GET")) {
				response["_STATUS_"] = Protocol.STATUS_BAD_REQUEST;
				response["_STRING_"] = Protocol.STRING_BAD_REQUEST;
				return Header.Create(response);
			}
			// セキュリティレベル="external"なら即返る
			if (header.ContainsKey("SecurityLevel") && header["SecurityLevel"] == "external") {
				response["_STATUS_"] = Protocol.STATUS_NO_CONTENT;
				response["_STRING_"] = Protocol.STRING_NO_CONTENT;
				return Header.Create(response);
			}
			if (header.ContainsKey("ID")) {
				switch (header["ID"]) {
				case "hwnd":
					this.hwnd = (IntPtr) int.Parse(header["Reference0"].Split('\x01')[0]);
					break;
				case "uniqueid":
					this.uniqueID = header["Reference0"];
					break;
				case "OnFirstBoot":
					goto case "OnBoot";
				case "OnWindowStateRestore":
					goto case "OnBoot";
				case "OnGhostChanged":
					goto case "OnBoot";
				case "OnGhostCalled":
					goto case "OnBoot";
				case "OnBoot":
					if (!this.initialized) {
						FormRecv.Start(this.hwnd, this.uniqueID, this.basedir);
						this.initialized = true;
					}
					value = @"\1\s[-1]\0\s[-1]\e";
					break;
				case "OnGhostChanging":
					if (this.initialized) {
						FormRecv.End();
						this.initialized = false;
					}
					value = @"\1\s[-1]\0\s[-1]";
					break;
				case "OnClose":
					if (this.initialized) {
						FormRecv.End();
						this.initialized = false;
					}
					value = @"\1\s[-1]\0\s[-1]\-";
					break;
				case "OnMouseDoubleClick":
					value = @"\1\s[-1]\0\s[0]わん。\e";
					break;
				case "OnUpdateBegin":
					value = @"\1\s[-1]\0\s[0]わんわん。\e";
					break;
				case "OnUpdateReady":
					value = @"\1\s[-1]\0\s[0]ハッハッ。\e";
					break;
				case "OnUpdateComplete":
					value = @"\1\s[-1]\0\s[0]わぉーん。\e";
					break;
				case "OnUpdateFailure":
					value = @"\1\s[-1]\0\s[0]くぅん。\e";
					break;
				case "homeurl":
					value = "https://raw.githubusercontent.com/nikolat/reshibainu/main/";
					break;
				case "useorigin1":
					value = "1";
					break;
				case "version":
					value = Shiori.SENDER + "/" + Shiori.SENDER_VERSION;
					break;
				case "craftman":
					value = "setsuri";
					break;
				case "craftmanw":
					value = "摂理";
					break;
				case "name":
					value = Shiori.SENDER;
					break;
				case "sakura.portalsites":
					List<string[]> sakuraPortalsites = new List<string[]>();
					sakuraPortalsites.Add(new string[] { "うかフィード", "https://feed.ukagaka.net/", "" });
					foreach (string[] site in sakuraPortalsites)
					{
						value += string.Join("\x01", site) + "\x01\x02";
					}
					break;
				case "sakura.recommendsites":
					List<string[]> sakuraRecommendsites = new List<string[]>();
					sakuraRecommendsites.Add(new string[] { "れしばいぬ", "https://github.com/nikolat/reshibainu", "" });
					sakuraRecommendsites.Add(new string[] { "れしば.NET", "https://github.com/nikolat/recv_net", "" });
					foreach (string[] site in sakuraRecommendsites)
					{
						value += string.Join("\x01", site) + "\x01\x02";
					}
					break;
				default:
					break;
				}
			}
			// NOTIFY
			if (header["_METHOD_"] == "NOTIFY") {
				response["_STATUS_"] = Protocol.STATUS_NO_CONTENT;
				response["_STRING_"] = Protocol.STRING_NO_CONTENT;
				return Header.Create(response);
			}
			// GET
			response["_STATUS_"] = (value != "") ? Protocol.STATUS_OK : Protocol.STATUS_NO_CONTENT;
			response["_STRING_"] = (value != "") ? Protocol.STRING_OK : Protocol.STRING_NO_CONTENT;
			if (value != "") {
				response["Value"] = value;
			}
			return Header.Create(response);
		}
	}

	// 伺かプロトコル送受信用クラス
	class Protocol
	{
		public const string OK                    = "200 OK";
		public const string NO_CONTENT            = "204 No Content";
		public const string BREAK                 = "210 Break";
		public const string BAD_REQUEST           = "400 Bad Request";
		public const string REQUEST_TIMEOUT       = "408 Request Timeout";
		public const string CONFLICT              = "409 Conflict";
		public const string REFUSE                = "420 Refuse";
		public const string INTERNAL_SERVER_ERROR = "500 Internal Server Error";
		public const string NOT_IMPLEMENTED       = "501 Not Implemented";
		public const string SERVICE_UNAVAILABLE   = "503 Service Unavailable";
//		public const string NOT_LOCAL_IP          = "510 Not Local IP";
//		public const string IN_BLACK_LIST         = "511 In Black List";
		public const string INVISIBLE             = "512 Invisible";

		public const string STATUS_OK                    = "200";
		public const string STATUS_NO_CONTENT            = "204";
		public const string STATUS_BREAK                 = "210";
		public const string STATUS_BAD_REQUEST           = "400";
		public const string STATUS_REQUEST_TIMEOUT       = "408";
		public const string STATUS_CONFLICT              = "409";
		public const string STATUS_REFUSE                = "420";
		public const string STATUS_INTERNAL_SERVER_ERROR = "500";
		public const string STATUS_NOT_IMPLEMENTED       = "501";
		public const string STATUS_SERVICE_UNAVAILABLE   = "503";
//		public const string STATUS_NOT_LOCAL_IP          = "510";
//		public const string STATUS_IN_BLACK_LIST         = "511";
		public const string STATUS_INVISIBLE             = "512";

		public const string STRING_OK                    = "OK";
		public const string STRING_NO_CONTENT            = "No Content";
		public const string STRING_BREAK                 = "Break";
		public const string STRING_BAD_REQUEST           = "Bad Request";
		public const string STRING_REQUEST_TIMEOUT       = "Request Timeout";
		public const string STRING_CONFLICT              = "Conflict";
		public const string STRING_REFUSE                = "Refuse";
		public const string STRING_INTERNAL_SERVER_ERROR = "Internal Server Error";
		public const string STRING_NOT_IMPLEMENTED       = "Not Implemented";
		public const string STRING_SERVICE_UNAVAILABLE   = "Service Unavailable";
//		public const string STRING_NOT_LOCAL_IP          = "Not Local IP";
//		public const string STRING_IN_BLACK_LIST         = "In Black List";
		public const string STRING_INVISIBLE             = "Invisible";

		// 改行
		public const string CRLF = "\r\n";

		private static Regex rGeneralEntry = new Regex(@"^(.+?)\s*:\s*(.+)\s*$", RegexOptions.Compiled);

		// 伺かプロトコルヘッダ解析クラス
		public class Header
		{
			// Dictionary<string, string> からヘッダ文字列生成
			public static string Create(Dictionary<string, string> sd)
			{
				string header = "";
				if (!sd.ContainsKey("_STATUS_")) {			// REQUEST  ヘッダ
					header +=
						sd["_METHOD_"] + " " + sd["_PROTOCOL_"] + "/" + sd["_VERSION_"] + Protocol.CRLF;
					for (int i = 0; sd.ContainsKey("Entry" + i); ++i) { 	// SSTP Entry 例外
						header += "Entry: " + sd["Entry" + i] + Protocol.CRLF;
						sd.Remove("Entry" + i);
					}
					for (int i = 0; sd.ContainsKey("IfGhost" + i); ++i) {	// SSTP IfGhost-Script 例外
						header  += "IfGhost: " + sd["IfGhost" + i] + Protocol.CRLF;
						sd.Remove("IfGhost" + i);
						if (sd.ContainsKey("Script" + i)) {
							header += "Script: "  + sd["Script"  + i] + Protocol.CRLF;	// IfGhostがあれば、Scriptがあるはず……
							sd.Remove("Script"  + i);
						} else break;
					}
					for (int i = 0; sd.ContainsKey("GhostEx" + i); ++i) {	// SHIORI GhostEx 例外
						header += "GhostEx: " + sd["GhostEx"] + Protocol.CRLF;
						sd.Remove("GhostEx");
					}
				} else {									// RESPONSE ヘッダ
					header  +=  sd["_PROTOCOL_"] + "/" + sd["_VERSION_"] + " "
							+	sd["_STATUS_"]   + " " + sd["_STRING_"]  + Protocol.CRLF;
				}
				// もし keys に_.+_のキーがあれば全て削除
				Regex r = new Regex(@"^_.+_$", RegexOptions.Compiled);

				string[] keys = new string[sd.Keys.Count];
				sd.Keys.CopyTo(keys, 0);
				foreach (string key in keys) {
					if (r.Match(key).Success)
						sd.Remove(key);
				}
				// その他のキーを追加
				foreach (string key in sd.Keys) {
					header += key + ": " + sd[key] + Protocol.CRLF;	// キー先頭を大文字に変換
				}
				header += Protocol.CRLF;
				return header;
			}

			// ヘッダを分析し、Dictionary<string, string> に格納
			public static Dictionary<string, string> Parse(string header)
			{
				StringCollection line = new StringCollection();
				Dictionary<string, string> sd = new Dictionary<string, string>();

				line.AddRange(Regex.Split(header, "\r\n"));
				Match m = Regex.Match(line[0], @"^(.+)\s+(.+)/(\d\.\d)$");
				Match n = Regex.Match(line[0], @"^(.+)/(\d\.\d)\s+(\d{3})\s+(.+)$");
				if (m.Success) {			// REQUEST  - e.g.: "^(GET String) (SHIORI)/(2.5)$"
					sd["_COMMANDLINE_"] = line[0];
					sd["_METHOD_"]      = m.Groups[1].Value;
					sd["_PROTOCOL_"]    = m.Groups[2].Value;
					sd["_VERSION_"]     = m.Groups[3].Value;
				} else if (n.Success) {		// RESPONSE - e.g.: "^(SHIORI)/(3.0) (204) (No Content)$"
					sd["_STATUSLINE_"]  = line[0];
					sd["_PROTOCOL_"]    = n.Groups[1].Value;
					sd["_VERSION_"]     = n.Groups[2].Value;
					sd["_STATUS_"]      = n.Groups[3].Value;
					sd["_STRING_"]      = n.Groups[4].Value;
				}
				line.RemoveAt(0);		// コマンドライン削除

				// SSTP の Entry, IfGhost - Script は例外にならざるを得ないので、このような処理になった
				// その他の方法論としては、レスポンス格納用の変数をさらにネストするか、
				// 行単位で保持するか、独自の管理クラスを作るか、
				// そもそも帰ってきた値を変数としてキープしておくのをやめるか、等々
//				int e = 0;		// Entryキー用カウンタ
//				int i = 0;		// IfGhost 用カウンタ
//				int s = 0;		// Script 用カウンタ
//				Regex re = new Regex(@"^Entry\s*:\s*(.+)\s*$",            RegexOptions.Compiled);
//				Regex ri = new Regex(@"^IfGhost\s*:\s*(.+)\s*$",          RegexOptions.Compiled);
//				Regex rs = new Regex(@"^Script\s*:\s*(.+)\s*$",           RegexOptions.Compiled);
//				Regex rg = new Regex(@"^GhostEx\s*:\s*(.+)\s*$",          RegexOptions.Compiled);
//				Regex rb = new Regex(@"^X-Bottle-IfGhost\s*:\s*(.+)\s*$", RegexOptions.Compiled); // ボトル拡張
				for (int j = 0; j < line.Count; ++j) {
					m = Protocol.rGeneralEntry.Match(line[j]);
					if (m.Success) {	// e.g.: "^Value: \0\s[10]Hello, World!\e"
						sd[m.Groups[1].Value]  = m.Groups[2].Value.Trim();
//					} else if (re.Match(c).Success) {		// "Entry: SAKURA script"
//						sd["Entry"   + e++]    = re.Match(c).Groups[1].Value;
//					} else if (ri.Match(c).Success) {	// "IfGhost: Ghost name(s)"
//						sd["IfGhost" + i++]    = ri.Match(c).Groups[1].Value;
//					} else if (rs.Match(c).Success) {	// "Script: SAKURA script"
//						sd["GhostEx"  + s++]   = rs.Match(c).Groups[1].Value;
//					} else if (rg.Match(c).Success) {	// "GhostEx: Ghost name"
//						sd["Script"  + s++]    = rg.Match(c).Groups[1].Value;
//					} else if (rb.Match(c).Success) {	// "X-Bottle-IfGhost: Ghost name"
//						sd["X-Bottle-IfGhost"] = rb.Match(c).Groups[1].Value;
					}
				}
				return sd;
			}

			public static string Unescape(string e)
			{
				string r = Regex.Replace(e, @"\\r\\n", Protocol.CRLF);
				return r;
			}

			public static string Escape(string e)
			{
				string r = Regex.Replace(e, Protocol.CRLF, @"\\r\\n");
				return r;
			}
		}
	}
}
