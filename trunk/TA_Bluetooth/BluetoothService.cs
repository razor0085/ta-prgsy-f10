// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothService
//

using System;
using System.Collections;
using System.Text;
using InTheHand.Net.Sockets;
using System.Threading;
using System.Net.Sockets;

namespace TA.Bluetooth
{
    /// <summary>
    /// Listens for connections from Bluetooth network clients.
    /// </summary>
    /// <remarks>The <see cref="BluetoothListener"/> class provides simple methods that listen for and accept incoming connection requests in blocking synchronous mode.
    /// You can use either a <see cref="BluetoothClient"/> or a <see cref="Socket"/> to connect with a <see cref="BluetoothListener"/></remarks>
    public class BluetoothService
    {
        private BluetoothListener listener;
        private BluetoothServiceListener serviceListener = null;
        private Guid service;
        private Boolean runService = true;

		#region Create service
		/// <summary>
        /// Creates a new Service for the give service <see cref="Guid"/> and start listening for incoming connections
        /// </summary>
        /// <param name="service">service identification as <see cref="Guid"/></param>
		///	<example> This sample shows how to call the CreateService method with a given service.
		///	<code>			
		///	class BTServer			
		///	{			
		///		public static void Main()		
		///		{		
		///			BluetoothService service;	
		///				
		///			// check if Bluetooth-Stick is available	
		///			if (BluetoothRadio.IsSupported)	
		///			{	
		///				// set device Name
		///				BluetoothRadio.PrimaryRadio.Name = "BlaBla";
		///				// desired service
		///				Guid serviceId = new Guid("{FB4B43E4-0328-4056-82A5-7E03BE347082}");
		///				// start new service
		///				service = new BluetoothService();
		///				service.CreateService(serviceId);
		///				Console.WriteLine("Service " + serviceId.ToString() + " started.");
		///			}	
		///			else	
		///			{	
		///				Console.WriteLine("No Bluetooth-Stick is available");
		///				return;
		///			}	
		///				
		///			BluetoothClient client;	
		///			System.Net.Sockets.NetworkStream ns;	
		///			Console.WriteLine("wait for incoming connections...");	
		///			while (true)	
		///			{	
		///				// accept bluetooth connection
		///				client = service.WaitForConnection();
		///				Console.WriteLine("Incoming connection " + client.GetSocket().RemoteEndPoint);
		///				
		///				// transform into network stream
		///				ns = client.GetStream();
		///				
		///				// Output data to stream
		///				StreamWriter sw = new StreamWriter(ns);
		///				sw.WriteLine("Hello from Server!");
		///				
		///				// clear and close stream
		///				sw.Flush();
		///				client.Close();
		///			}	
		///		}		
		///	}			
		///	</code>			
		///	</example>			
		public void CreateService(Guid service)
        {

            listener = new BluetoothListener(service);
            listener.Start();
            this.service = service;
		}
		#endregion

		#region Stop service
		/// <summary>
        /// Stopps the running service
        /// </summary>
        public void StopService()
        {
            runService = false;
            listener.Stop();
		}
		#endregion

		#region Wait for connection
		/// <summary>
        /// Wait for incoming connection.
        /// relays connection to serviceListener if available.
        ///
        /// Blocking code
        /// </summary>
        /// <returns>established connection as<see cref="BluetoothClient"/> or null if service was stoped</returns>
		///	<example> This sample shows how to use the WaitForConnection method.
		///	<code>			
		///	class BTServer			
		///	{			
		///		public static void Main()		
		///		{		
		///			BluetoothService service;	
		///				
		///			// check if Bluetooth-Stick is available	
		///			if (BluetoothRadio.IsSupported)	
		///			{	
		///				// set device Name
		///				BluetoothRadio.PrimaryRadio.Name = "BlaBla";
		///				// desired service
		///				Guid serviceId = new Guid("{FB4B43E4-0328-4056-82A5-7E03BE347082}");
		///				// start new service
		///				service = new BluetoothService();
		///				service.CreateService(serviceId);
		///				Console.WriteLine("Service " + serviceId.ToString() + " started.");
		///			}	
		///			else	
		///			{	
		///				Console.WriteLine("No Bluetooth-Stick is available");
		///				return;
		///			}	
		///				
		///			BluetoothClient client;	
		///			System.Net.Sockets.NetworkStream ns;	
		///			Console.WriteLine("wait for incoming connections...");	
		///			while (true)	
		///			{	
		///				// accept bluetooth connection
		///				client = service.WaitForConnection();
		///				Console.WriteLine("Incoming connection " + client.GetSocket().RemoteEndPoint);
		///				
		///				// transform into network stream
		///				ns = client.GetStream();
		///				
		///				// Output data to stream
		///				StreamWriter sw = new StreamWriter(ns);
		///				sw.WriteLine("Hello from Server!");
		///				
		///				// clear and close stream
		///				sw.Flush();
		///				client.Close();
		///			}	
		///		}		
		///	}			
		///	</code>			
		///	</example>			
		public BluetoothClient WaitForConnection()
        {
            // wait for connection
            while (runService && !listener.Pending())
            {
                Thread.Sleep(200);
            }

            // if service was terminated abort the wait for connection
            if (!runService)
            {
                // notify listener if available
                if (serviceListener != null)
                {
                    serviceListener.ServiceTerminated();
                }
                return null;
            }

            // blocking code but should have waiting client
            InTheHand.Net.Sockets.BluetoothClient bc = listener.AcceptBluetoothClient();
            BluetoothClient client = new BluetoothClient(bc, service);

            // notify listener if available
            if (serviceListener != null)
            {
                serviceListener.HandleConnection(client);
            }

            return client;
        }


        /// <summary>
        /// Wait for incoming connection
        /// Relays connection to serviceListener
        /// Blocking code
        /// </summary>
        internal void WaitForAsynchConnection()
        {
            if (serviceListener == null)
            {
                throw (new Exception("No connection listener supplied!"));
            }

            while (runService)
            {
                if (listener.Pending())
                {
                    // blocking code
                    InTheHand.Net.Sockets.BluetoothClient bc = listener.AcceptBluetoothClient();
                    BluetoothClient client = new BluetoothClient(bc, service);

                    serviceListener.HandleConnection(client);

                }
                else
                {
                    Thread.Sleep(100);
                }
            }
             // notify of disconnection
            serviceListener.ServiceTerminated();

		}
		#endregion

		#region AddListener
		/// <summary>
        /// Add a listener to handle incoming connections for this service
        /// Only one is allowed
        /// </summary>
        /// <param name="serviceListener">listener to take over the connection</param>
        public void AddListener(BluetoothServiceListener serviceListener)
        {
            if (this.serviceListener == null)
            {
                this.serviceListener = serviceListener;
            }
            else
            {
                throw (new Exception("Only one service listener allowed!"));
            }
		}
		#endregion

		#region CreateAsynchService
		/// <summary>
        /// Creates a new Service for the give service <see cref="Guid"/> and starts listening for incoming connections in asynch mode
        /// </summary>
        /// <param name="service">service identification as <see cref="Guid"/></param>
        /// <param name="serviceListener">listener to take over the connection</param>
        public void CreateAsynchService(Guid service, BluetoothServiceListener serviceListener)
        {
            if (service == null || serviceListener == null)
            {
                throw (new Exception("Both serviceId and listener are required to create new service"));
            }

            this.listener = new BluetoothListener(service);
            this.listener.Start();

            this.service = service;
            this.AddListener(serviceListener);
            Thread thread = new Thread(this.WaitForAsynchConnection);
            thread.Start();
        }
		#endregion
	}
}
