// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothDevice
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;


namespace TA.Bluetooth
{
    /// <summary>
    /// Provides information about an available device obtained by the client during device discovery.
    /// </summary>
    public class BluetoothDevice
    {
        private BluetoothDeviceInfo deviceInfo;
        private ArrayList openConnections;

        #region Constructor
        /// <summary>
        /// Create new instance of <see cref="BluetoothDevice"/>
        /// </summary>
        /// <param name="deviceInfo"><see cref="BluetoothDeviceInfo"/> object of support framework</param>
        internal BluetoothDevice(BluetoothDeviceInfo deviceInfo)
        {
            this.deviceInfo = deviceInfo;
            this.openConnections = new ArrayList();
        }
        #endregion

        #region Address
        /// <summary>
        /// Device address as <see cref="byte"/>Array
        /// </summary>
        public byte[] Address
        {
            get
            {
                return deviceInfo.DeviceAddress.ToByteArray();
            }
        }

        /// <summary>
        /// Device address as <see cref="string"/>
        /// </summary>
		///	<example> This sample shows how to call the DeviceAddress method.				
		///	<code>				
		///	class BTClient				
		///	{				
		///		public static BluetoothDevice DiscoverDev(Guid service)			
		///		{			
		///			// search for reachable devices		
		///			BluetoothDiscovery discovery = new BluetoothDiscovery();		
		///			BluetoothDeviceCollection bdc = discovery.DiscoverDevices();		
		///					
		///			foreach (BluetoothDevice device in bdc)		
		///			{		
		///				if (device.HasService(service))	
		///				{	
		///					Console.WriteLine(device.Name + ";" + device.DeviceAddress);
		///					return device;
		///				}	
		///				else	
		///				{	
		///					Console.WriteLine(device.Name + "; no Service");
		///				}	
		///			}		
		///			Console.WriteLine("Required service does not exist on target");		
		///			return null;		
		///		}			
		///		
		///		//...use the DiscoverDev to discover devices with a given service
		///		
		///	}				
		///	</code>				
		///	</example>				
		public String DeviceAddress
        {
            get
            {
                return deviceInfo.DeviceAddress.ToString();
            }
        }
        #endregion

        #region Name
        /// <summary>
        /// Device Name as <see cref="string"/>
        /// </summary>
        public String Name
        {
            get
            {
                deviceInfo.Update();
                return deviceInfo.DeviceName;
            }
            set
            {
                deviceInfo.DeviceName = value;
            }
        }
        #endregion

        #region Authenticated
        /// <summary>
        /// Device authenticated
        /// </summary>
        public Boolean Authenticated
        {
            get
            {
                return deviceInfo.Authenticated;
            }
        }
        #endregion

        #region Connect
        /// <summary>
        /// Connect to specified service on this device
        /// </summary>
        /// <param name="service">service identification as <see cref="Guid"/></param>
        /// <returns>connection as <see cref="BluetoothClient"/></returns>
		///	<example> This sample shows how to call the Connect method with a given service.
		///	<code>				
		///	class BTClient				
		///	{				
		///		public static BluetoothDevice DiscoverDev(Guid service)			
		///		{			
		///			// search for reachable devices		
		///			BluetoothDiscovery discovery = new BluetoothDiscovery();		
		///			BluetoothDeviceCollection bdc = discovery.DiscoverDevices();		
		///					
		///			foreach (BluetoothDevice device in bdc)		
		///			{		
		///				if (device.HasService(service))	
		///				{	
		///					Console.WriteLine(device.Name + ";" + device.DeviceAddress);
		///					return device;
		///				}	
		///				else	
		///				{	
		///					Console.WriteLine(device.Name + "; no Service");
		///				}	
		///			}		
		///			Console.WriteLine("Required service does not exist on target");		
		///			return null;		
		///		}			
		///					
		///		public static void Main()			
		///		{			
		///			Console.WriteLine("Search for reachable devices...");		
		///			// desired service		
		///			Guid service = BluetoothServiceList.Robot11;		
		///			BluetoothDevice device = DiscoverDev(service);		
		///			if (device != null)		
		///			{		
		///				// connect to desired service	
		///				BluetoothClient bc = device.Connect(service);	
		///				Console.WriteLine("Outgoing connection " + bc.GetSocket().LocalEndPoint + ".");	
		///				// read transmitted data	
		///				StreamReader sr = new StreamReader(bc.GetStream());	
		///				StreamWriter sw = new StreamWriter(bc.GetStream());	
		///				// request	
		///				sw.WriteLine();	
		///				sw.Flush();	
		///				// print 	
		///				Console.WriteLine("Message received : ");	
		///				Console.WriteLine(sr.ReadLine());	
		///				bc.Close();	
		///			}		
		///		}			
		///	}				
		///	</code>				
		///	</example>				
		public BluetoothClient Connect(Guid service)
        {
            InTheHand.Net.Sockets.BluetoothClient client = new InTheHand.Net.Sockets.BluetoothClient();
            try
            {
                client.Connect(deviceInfo.DeviceAddress, service);
            }
            catch (SocketException sex)
            {
                // target unreachable
                if (sex.ErrorCode == 10049)
                {
                    return null;
                }
            }
            openConnections.Add(client);
            return new BluetoothClient(client, service);
        }

        /// <summary>
        /// Connect to specified service on this device
        /// </summary>
        /// <param name="mac">bluetooth mac address as "001122334455"</param>
        /// <param name="service">service identification as <see cref="Guid"/></param>
        /// <returns>connection as <see cref="BluetoothClient"/></returns>
		///	<example> This sample shows how to call the Connect method with a given mac address and s service.			
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
		public static BluetoothClient Connect(String mac, Guid service)
        {
            InTheHand.Net.Sockets.BluetoothClient client = new InTheHand.Net.Sockets.BluetoothClient();
            try
            {
                BluetoothAddress addr = BluetoothAddress.Parse(mac);
                BluetoothEndPoint ep = new BluetoothEndPoint(addr, service);
                client.Connect(ep);
            }
            catch (Exception)
            {
                // target unreachable
                return null;
            }
            return new BluetoothClient(client, service);
        }
        #endregion

        #region HasService
        /// <summary>
        /// Check if desired service is available on this device
        /// </summary>
        /// <param name="service">service identification as <see cref="Guid"/></param>
        /// <returns>true if service exists</returns>
		///	<example> This sample shows how to call the HasService method.				
		///	<code>				
		///	class BTClient				
		///	{				
		///		public static BluetoothDevice DiscoverDev(Guid service)			
		///		{			
		///			// search for reachable devices		
		///			BluetoothDiscovery discovery = new BluetoothDiscovery();		
		///			BluetoothDeviceCollection bdc = discovery.DiscoverDevices();		
		///					
		///			foreach (BluetoothDevice device in bdc)		
		///			{		
		///				if (device.HasService(service))	
		///				{	
		///					Console.WriteLine(device.Name + ";" + device.DeviceAddress);
		///					return device;
		///				}	
		///				else	
		///				{	
		///					Console.WriteLine(device.Name + "; no Service");
		///				}	
		///			}		
		///			Console.WriteLine("Required service does not exist on target");		
		///			return null;		
		///		}			
		///		
		///		//...use the DiscoverDev to discover devices whit the given service
		///		
		///	}				
		///	</code>				
		///	</example>				
		public Boolean HasService(Guid service)
        {
            try
            {
                ServiceRecord[] records = deviceInfo.GetServiceRecords(service);

                if (records.Length.Equals(0))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10108)
                {
                    return false;
                }
                else
                {
                    throw se;
                }
            }

        }
        #endregion

        #region Close
        /// <summary>
        /// terminates all open connections to this device
        /// </summary>
        public void Close()
        {
            foreach (InTheHand.Net.Sockets.BluetoothClient client in openConnections)
            {
                client.Close();
            }
            openConnections.Clear();
        }
        #endregion

        #region GetClient
        /// <summary>
        /// Tries to find already opened connection to specified service
        /// </summary>
        /// <param name="service">service identification as <see cref="Guid"/></param>
        /// <returns>connection as <see cref="BluetoothClient"/>, null if not available</returns>
        public BluetoothClient GetClient(Guid service)
        {
            foreach (BluetoothClient client in openConnections)
            {
                if (client.Service.Equals(service))
                {
                    return client;
                }
            }
            return null;
        }
        #endregion

        #region Refresh

        /// <summary>
        /// Forces the system to refresh the device information
        /// </summary>
        public void Refresh()
        {
            deviceInfo.Refresh();
        }
        #endregion
    }
}
