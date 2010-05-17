// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothClient
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using InTheHand.Net.Sockets;

namespace TA.Bluetooth
{
    /// <summary>
    /// Represents connection to other Bluetooth network devices
    /// </summary>
    public class BluetoothClient
    {
        private volatile InTheHand.Net.Sockets.BluetoothClient cli;
        private Guid service;

        #region Constuctor
        /// <summary>
        /// Creates a new instance of <see cref="BluetoothClient"/>.
        /// </summary>
        /// <param name="client">connected BluetoothClient of the support Framework</param>
        /// <param name="service"> The service identification as <see cref="Guid"/></param>
        internal BluetoothClient(InTheHand.Net.Sockets.BluetoothClient client, Guid service)
        {
            this.cli = client;
            this.service = service;
        }
        #endregion

        #region Connected

        /// <summary>
        /// Gets a value indicating whether the underlying <see cref="Socket"/> for a <see cref="BluetoothClient"/> is connected to a remote host.
        /// </summary>
        public bool Connected
        {
            get
            {
                return cli.Client.Connected;
            }
        }
        #endregion

        #region ServiceId
        /// <summary>
        /// Gets the service <see cref="Guid"/> this client is connected to.
        /// </summary>
        public Guid Service
        {
            get
            {
                return service;
            }
        }
        #endregion

        #region RemoteDeviceName
        /// <summary>
        /// Get the name of the remote device
        /// </summary>
        public String RemoteDeviceName
        {
            get
            {
                return cli.RemoteMachineName;
            }
        }
        #endregion

        #region Socket
        /// <summary>
        /// Gets the underlying <see cref="Socket"/>.
        /// </summary>
        /// <returns>Socket of this connection</returns>
		///	<example> This sample shows how to call the GetSocket method.			
		///	<code>			
		///	class BTClient			
		///	{			
		///		public static void Main(String[] args)		
		///		{	
		///			String mac = "001122334455";	
		///			Console.WriteLine("Search for device "+mac+"...");	
		///			// desired service	
		///			Guid service = BluetoothServiceList.Robot11;	
		///			// connect to desired service	
		///			BluetoothClient bc = BluetoothDevice.Connect(mac, service);	
		///			Console.WriteLine("Outgoing connection " + bc.GetSocket().LocalEndPoint + ".");	
		///			// read transmitted data	
		///			StreamReader sr = new StreamReader(bc.GetStream());	
		///			StreamWriter sw = new StreamWriter(bc.GetStream());	
		///			// request	
		///			sw.WriteLine();	
		///			sw.Flush();	
		///			// print 	
		///			Console.WriteLine("Message received : ");	
		///			Console.WriteLine(sr.ReadLine());	
		///			bc.Close();	
		///		}		
		///	}			
		///	</code>			
		///	</example>			
		public Socket GetSocket()
        {
            return cli.Client;
        }
        #endregion

        #region Stream
        /// <summary>
        /// Gets the underlying <see cref="NetworkStream"/>
        /// </summary>
        /// <returns>Stream of this connection</returns>
		///	<example> This sample shows how to use the network streams.			
		///	<code>			
		///	class BTClient			
		///	{			
		///		public static void Main(String[] args)		
		///		{	
		///			String mac = "001122334455";	
		///			Console.WriteLine("Search for device "+mac+"...");	
		///			// desired service	
		///			Guid service = BluetoothServiceList.Robot11;	
		///			// connect to desired service	
		///			BluetoothClient bc = BluetoothDevice.Connect(mac, service);	
		///			Console.WriteLine("Outgoing connection " + bc.GetSocket().LocalEndPoint + ".");	
		///			// read transmitted data	
		///			StreamReader sr = new StreamReader(bc.GetStream());	
		///			StreamWriter sw = new StreamWriter(bc.GetStream());	
		///			// request	
		///			sw.WriteLine();	
		///			sw.Flush();	
		///			// print 	
		///			Console.WriteLine("Message received : ");	
		///			Console.WriteLine(sr.ReadLine());	
		///			bc.Close();	
		///		}		
		///	}			
		///	</code>			
		///	</example>			
		public NetworkStream GetStream()
        {
            return cli.GetStream();
        }
        #endregion

        #region Available
        /// <summary>
        /// Gets the amount of data that has been received from the network and is available to be read.
        /// </summary>
        /// <value>The number of bytes of data received from the network and available to be read.</value>
        /// <exception cref="ObjectDisposedException">The <see cref="Socket"/> has been closed.</exception>
        public int Available
        {
            get
            {
                return cli.Client.Available;
            }
        }
        #endregion

        #region Close
        /// <summary>
        /// Disposes this <see cref="BluetoothClient"/> instance and closes the underlying connection. 
        /// </summary>
        public void Close()
        {

            // socket
            cli.Client.Close();
            // Object
            cli.Close();

        }
        #endregion

        #region IsConnected
        /// <summary>
        /// Returns true if the socket is connected to the server. The property 
        /// Socket.Connected does not always indicate if the socket is currently 
        /// connected, this polls the socket to determine the latest connection state.
        /// </summary>
        /// <returns>true if the socket is connected</returns>
        public Boolean IsConnected()
        {

            // return right away if no socket is available
            if (cli == null)
            {
                return false;
            }

            // the socket is not connected if the Connected property is false
            if (cli.Client.Connected == false)
            {
                return false;
            }

            //try
            //{
            //    // there is no guarantee that the socket is connected even if the Connected property is true
            //    bool bSelectRead = cli.Client.Poll(1, SelectMode.SelectRead);
            //    bool bSelectWrite = cli.Client.Poll(1, SelectMode.SelectWrite);
            //    if (bSelectWrite && bSelectRead)
            //    {
            //        // poll for error to see if socket is connected
            //        cli.Client.Receive(new byte[0], 0, 0, SocketFlags.Peek);
            //        cli.Client.Send(new byte[0], 0, 0, SocketFlags.None);
            //        return cli.Client.Connected;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (SocketException)
            //{
            //    return false;
            //}
            //catch (ObjectDisposedException)
            //{
            //    return false;
            //}

            try
            {
                Boolean bufferedData = cli.Client.Available > 0;
                // buffered data waiting to be recieved
                if (bufferedData)
                {
                    return true;
                }
                // poll for error to see if socket is connected
                Boolean disconnected = cli.Client.Poll(1, SelectMode.SelectError) || 
                    cli.Client.Poll(1, SelectMode.SelectRead) || 
                    cli.Client.Poll(1, SelectMode.SelectWrite);


                return !disconnected;
            }
            catch (SocketException)
            {
                return false;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }

        }
        #endregion

    }
}
