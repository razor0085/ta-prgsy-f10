using System;
using System.Collections.Generic;
using System.Text;
using ServerPattern;
using Executor;
using TA.Bluetooth;

namespace Http
{
	/**
	 * Ein ganz einfacher Web-Server auf Bluetooth. 
	 * Der Server ist in der Lage, Seitenanforderungen zu dem Luafwerk 
	 * und Verzeichnis zu bearbeiten, von dem er gestartet wurde. 
	 * Wurde der Server z.B. im Laufwerk c: gestartet, so würde eine 
	 * Seitenanforderung "http://localhost:8080/test/index.html" die Datei 
	 * c:\test\index.html laden.
	*/
	public class HttpServer : AbstractBTServer
	{
		private int call = 0;

		/**
		 * html document base - Arbeitsverzeichnis für die html-Dokumente.<br>
		 */
		public static String htdocs = "";
		public static String startdoc = "log.txt";

		override protected AbstractBTHandler CreateHandler(BluetoothClient client)
		{
			return new HttpHandler(client, ++call);
		}

		override protected IExecutor CreateExecutor() {
			return new WorkerPool(20);
		}

		public static void Main(String[] args) {
			AbstractBTServer server = new HttpServer();
			if (args.Length > 0)
			{
				Guid serviceId = new Guid(args[0]);
				server.Start(serviceId);
			}
			else
			{
				server.Start();
			}
			Console.WriteLine("HTTP BTServer running on service "+server.GetServiceID().ToString());
		}
	}
}
