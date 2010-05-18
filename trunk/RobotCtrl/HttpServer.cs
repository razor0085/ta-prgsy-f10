using System;
using System.Collections.Generic;
using System.Text;
using ServerPattern;
using Executor;
using System.Net.Sockets;

namespace Http
{
	/**
	 * Ein ganz einfacher Web-Server auf TCP und einem beliebigen Port. 
	 * Der Server ist in der Lage, Seitenanforderungen zu dem Luafwerk 
	 * und Verzeichnis zu bearbeiten, von dem er gestartet wurde. 
	 * Wurde der Server z.B. im Laufwerk c: gestartet, so würde eine 
	 * Seitenanforderung "http://localhost/test/index.html" die Datei 
	 * c:\test\index.html laden.
	*/
	public class HttpServer : AbstractServer
	{
		private int call = 0;

		override protected AbstractHandler CreateHandler(Socket client) {
			return new HttpHandler(client, ++call);
		}

		override protected IExecutor CreateExecutor() {
			return new WorkerPool(20);
		}

		public static void Main(String[] args) {
			int port = 8080;
			if (args.Length > 0) {
				port = Int32.Parse(args[0]);
			}
			new HttpServer().Start(port);
			Console.WriteLine("HTTP server running on port: " + port);
		}
	}
}
