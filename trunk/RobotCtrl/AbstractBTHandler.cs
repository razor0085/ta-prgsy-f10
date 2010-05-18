using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using TA.Bluetooth;

namespace ServerPattern
{
	public abstract class AbstractBTHandler
	{
		private BluetoothClient client;

		public AbstractBTHandler(BluetoothClient client)
		{
			this.client = client;
		}

		public void Run() {
			try {
				if (ReadRequest()) {
					CreateResponse();
				}
				client.Close();
			}
			catch (Exception e) {
				Console.Error.WriteLine(e);
			}
		}

		abstract protected Boolean ReadRequest();
		abstract protected void CreateResponse();
	}
}
