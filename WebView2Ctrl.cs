using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Produire.EdgeControl
{
	[種類(DocUrl = "https://rdr.utopiat.net/docs/plugins/"), メインスレッド]
	public class Edgeウェブビュー : UserControl, IProduireClass
	{
		WebView2 view = new WebView2();

		public Edgeウェブビュー()
		{
			view.Dock = DockStyle.Fill;
			Controls.Add(view);
			view.CoreWebView2InitializationCompleted += View_CoreWebView2InitializationCompleted;
		}
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			初期化();
		}

		bool initialized;
		[自分を]
		public void 初期化()
		{
			view.EnsureCoreWebView2Async();
			view.NavigationStarting += View_NavigationStarting;
			view.WebMessageReceived += View_WebMessageReceived;
		}

		public event EventHandler 初期化が完了した;
		private void View_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			if (初期化が完了した != null) 初期化が完了した(sender, new 初期化が完了した情報(e));
		}
		public class 初期化が完了した情報 : ProduireEventArgs<CoreWebView2InitializationCompletedEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public 初期化が完了した情報(CoreWebView2InitializationCompletedEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public bool 成功
			{
				get { return e.IsSuccess; }
			}
		}

		public event EventHandler ウェブメッセージを受信した;
		private void View_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			if (ウェブメッセージを受信した != null) ウェブメッセージを受信した(sender, EventArgs.Empty);
		}

		public class ウェブメッセージを受信した情報 : ProduireEventArgs<CoreWebView2WebMessageReceivedEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public ウェブメッセージを受信した情報(CoreWebView2WebMessageReceivedEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public string ソース
			{
				get { return e.Source; }
			}

			/// <summary></summary>
			public string JSONメッセージ
			{
				get { return e.WebMessageAsJson; }
			}
		}

		private void View_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
		{
			if (initialized) return;

			view.CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;
			view.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
			view.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
			view.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
			view.CoreWebView2.DOMContentLoaded += CoreWebView2_DOMContentLoaded;
			view.CoreWebView2.ContentLoading += CoreWebView2_ContentLoading;
			view.CoreWebView2.ContainsFullScreenElementChanged += view_ContainsFullScreenElementChanged;
			initialized = true;
		}

		public event EventHandler タイトルが変化した;
		private void CoreWebView2_DocumentTitleChanged(object sender, object e)
		{
			if (タイトルが変化した != null) タイトルが変化した(sender, EventArgs.Empty);
		}

		public event EventHandler フルスクリーンに切り替わった;
		void view_ContainsFullScreenElementChanged(object sender, object e)
		{
			if (フルスクリーンに切り替わった != null) フルスクリーンに切り替わった(sender, EventArgs.Empty);
		}

		public event EventHandler ページが読み込まれている;
		private void CoreWebView2_ContentLoading(object sender, CoreWebView2ContentLoadingEventArgs e)
		{
			if (ページが読み込まれている != null) ページが読み込まれている(sender, new ページが読み込まれている情報(e));
		}

		public class ページが読み込まれている情報 : ProduireEventArgs<CoreWebView2ContentLoadingEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public ページが読み込まれている情報(CoreWebView2ContentLoadingEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public ulong ナビゲーションID
			{
				get { return e.NavigationId; }
			}
		}

		public event EventHandler ページが読み込まれた;

		private void CoreWebView2_DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
		{
			if (ページが読み込まれた != null) ページが読み込まれた(sender, e);
		}

		public class ページが読み込まれた情報 : ProduireEventArgs<CoreWebView2DOMContentLoadedEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public ページが読み込まれた情報(CoreWebView2DOMContentLoadedEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public ulong ナビゲーションID
			{
				get { return e.NavigationId; }
			}
		}

		public event ProduireEventHandler 表示が開始される;
		private void CoreWebView2_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
		{
			if (表示が開始される != null) 表示が開始される(sender, new 表示が開始される情報(e));
		}


		public class 表示が開始される情報 : ProduireEventArgs<CoreWebView2NavigationStartingEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public 表示が開始される情報(CoreWebView2NavigationStartingEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public bool キャンセル
			{
				get { return e.Cancel; }
				set { e.Cancel = value; }
			}

			/// <summary></summary>
			public string アドレス
			{
				get { return e.Uri.ToString(); }
			}
		}

		public event ProduireEventHandler 表示が開始された;
		private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			if (表示が開始された != null) 表示が開始された(sender, new 表示が開始された情報(e));
		}

		public class 表示が開始された情報 : ProduireEventArgs<CoreWebView2NavigationCompletedEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public 表示が開始された情報(CoreWebView2NavigationCompletedEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public CoreWebView2WebErrorStatus エラー状態
			{
				get { return e.WebErrorStatus; }
			}

			/// <summary></summary>
			public ulong ナビゲーションID
			{
				get { return e.NavigationId; }
			}
		}

		public event ProduireEventHandler 別ウィンドウが表示される;
		private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
		{
			if (別ウィンドウが表示される != null) 別ウィンドウが表示される(sender, new 別ウィンドウが表示される情報(e));
		}

		public class 別ウィンドウが表示される情報 : ProduireEventArgs<CoreWebView2NewWindowRequestedEventArgs>
		{
			/// <summary>EventArgsを生成します</summary>
			public 別ウィンドウが表示される情報(CoreWebView2NewWindowRequestedEventArgs e)
				: base(e)
			{ }

			/// <summary></summary>
			public bool 処理済み
			{
				get { return e.Handled; }
				set { e.Handled = value; }
			}

			/// <summary></summary>
			public string アドレス
			{
				get { return e.Uri.ToString(); }
			}
		}


		#region 手順
		/// <summary>表示しているページを更新(再表示)します</summary>
		[自分を]
		public void 更新()
		{
			if (!initialized) 初期化();
			view.Reload();
		}
		/// <summary>ページの読み込みを中止します</summary>
		[自分を]
		public void 中止()
		{
			if (!initialized) 初期化();
			view.Stop();
		}
		/// <summary>前に表示したページへ戻ります</summary>
		[自分を]
		public void 戻る()
		{
			if (!initialized) 初期化();
			view.GoBack();
		}
		/// <summary>次に表示したページへ進みます</summary>
		[自分を]
		public void 進む()
		{
			if (!initialized) 初期化();
			view.GoForward();
		}

		/// <summary>指定したページへ移動します。
		///【対象】にはインターネットアドレスを指定します。
		///</summary>
		///<remarks>【アドレス】へ</remarks>
		[自分を, 動詞("移動")]
		public void 移動([へ] string アドレス)
		{
			if (view.CoreWebView2 == null) 初期化();
			view.CoreWebView2.Navigate(アドレス);
		}
		[自分で]
		public void ググる([を] string キーワード)
		{
			if (view.CoreWebView2 == null) 初期化();
			try
			{
				移動("http://www.google.co.jp/search?q=" + キーワード);
			}
			catch (Exception)
			{ }
		}
		[自分で]
		public void ヤフーする([を] string キーワード)
		{
			if (view.CoreWebView2 == null) 初期化();
			try
			{
				byte[] data = Encoding.GetEncoding("euc-jp").GetBytes(キーワード);
				string encoding = System.Web.HttpUtility.UrlEncode(data);
				移動("http://search.yahoo.co.jp/search?p=" + encoding);
			}
			catch (Exception)
			{ }
		}
		[自分で]
		public void Bingする([を] string キーワード)
		{
			if (view.CoreWebView2 == null) 初期化();
			try
			{
				byte[] data = Encoding.UTF8.GetBytes(キーワード);
				string encoding = System.Web.HttpUtility.UrlEncode(data);
				移動("http://www.bing.com/search?q=" + encoding);
			}
			catch (Exception)
			{ }
		}


		[自分で]
		public string 実行する([を] string スクリプト)
		{
			if (view.CoreWebView2 == null) 初期化();
			var task = view.ExecuteScriptAsync(スクリプト);
			task.ConfigureAwait(false);
			string result = task.Result;
			return result;
		}

		#endregion

		#region 設定項目
		/// <summary>前に表示したページがあるかどうか</summary>
		/// <returns>□</returns>
		public bool 戻せる
		{
			get { return view.CanGoBack; }
		}
		/// <summary>次に表示したページがあるかどうか</summary>
		/// <returns>□</returns>
		public bool 進める
		{
			get { return view.CanGoForward; }
		}
		/// <summary>現在表示しているページのHTMLソース</summary>
		/// <returns>◎</returns>
		[非生成]
		public string ソース
		{
			get
			{
				return "";
			}
			set
			{
				if (view.CoreWebView2 == null) 初期化();
				view.NavigateToString(value);
			}
		}
		/// <summary>現在表示しているページのタイトルを取得します</summary>
		/// <returns>□</returns>
		public string タイトル
		{
			get { return view.CoreWebView2.DocumentTitle; }
		}
		/// <summary>インターネットアドレス</summary>
		/// <returns>◎</returns>
		public string アドレス
		{
			get
			{
				if (view.Source == null) return "";
				return view.Source.ToString();
			}
			set
			{
				if (view.CoreWebView2 == null) 初期化();
				if (value == null || value.Length == 0)
					view.Source = new Uri("about:");
				else
					view.Source = new Uri(value);
			}
		}
		/// <summary></summary>
		/// <returns>□</returns>
		public string バージョン
		{
			get { return view.ProductVersion.ToString(); }
		}

		public WebView2 元実体
		{
			get { return view; }
		}
		#endregion
	}
}
