using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Executor;

namespace ServerPattern
{
	public abstract class AbstractServer
	{
		private Boolean running = true;
		private int port;
		private string host;
		public const int DEFAULTPORT = 8080;
		public const string DEFAULTHOST = "192.168.1.111";

		protected void Run() {
            System.Console.WriteLine("server erzeugt");
			IExecutor executor = CreateExecutor();
			Socket listen = CreateServerSocket();
			while (running) {
				AbstractHandler handler = CreateHandler(listen.Accept());
                System.Console.WriteLine("Verbindung aufgebaut " + handler.ToString());
				executor.Execute(handler.Run);
			}
		}

		virtual protected IExecutor CreateExecutor() {
			return new PlainThreadExecutor();
		}

		protected Socket CreateServerSocket() {
            IPAddress ipAddress = IPAddress.Parse(DEFAULTHOST);
            System.Console.WriteLine(ipAddress.ToString());
			//IPAddress ipAddress = Dns.GetHostEntry(host).AddressList[0];
			TcpListener listener = new TcpListener(ipAddress, port);
			listener.Start();
			return listener.Server;
		}

		public void Start() {
			this.Start(0,null);
		}

		public void Start(int port) {
			this.Start(port,null);
		}

		public void Start(int port, string host) {
			Thread server = new Thread(Run);
			if (port <= 0)
				this.port = DEFAULTPORT;
			else
				this.port = port;
			if (host == null)
				this.host = DEFAULTHOST;
			else
				this.host = host;
			server.Start();
		}

		public int GetPort() {
			return port;
		}

		public string GetHost() {
			return host;
		}

		abstract protected AbstractHandler CreateHandler(Socket client);
	}
}
