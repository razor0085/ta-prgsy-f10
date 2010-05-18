using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Executor;
using TA.Bluetooth;

namespace ServerPattern
{
	public abstract class AbstractBTServer
	{
		private Boolean running = true;
		private Guid serviceId;
		private string deviceName;
		public static Guid DEFAULTSERVICE = new Guid("{FB4B43E4-0328-4056-82A5-7E03BE347082}");
		public const string DEFAULTNAME = "devicename";

		protected void Run() {
			IExecutor executor = CreateExecutor();
			BluetoothService service = CreateServerSocket();
			while (running) {
				AbstractBTHandler handler = CreateHandler(service.WaitForConnection());
				executor.Execute(handler.Run);
			}
		}

		virtual protected IExecutor CreateExecutor() {
			return new PlainThreadExecutor();
		}

		protected BluetoothService CreateServerSocket()
		{
			BluetoothService service = new BluetoothService();
			service.CreateService(serviceId);
			return service;
		}

		public void Start() {
			this.Start(DEFAULTSERVICE,DEFAULTNAME);
		}

		public void Start(Guid serviceID) {
			this.Start(serviceID,DEFAULTNAME);
		}

		public void Start(Guid serviceID, string device) {
			Thread server = new Thread(Run);
			this.serviceId = serviceID;
			this.deviceName = device;
			BluetoothRadio.PrimaryRadio.Name = deviceName;
			server.Start();
		}

		public Guid GetServiceID() {
			return serviceId;
		}

		public string GetDeviceName() {
			return deviceName;
		}

		abstract protected AbstractBTHandler CreateHandler(BluetoothClient client);
	}
}
