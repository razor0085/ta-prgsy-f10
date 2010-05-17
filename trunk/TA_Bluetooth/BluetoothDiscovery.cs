// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothDiscovery
//

using System;
using System.Collections;
using System.Text;
using InTheHand.Net.Sockets;

namespace TA.Bluetooth
{
    /// <summary>
    /// Provides features to discover other Bluetooth devices
    /// </summary>
    public class BluetoothDiscovery
    {
        BluetoothDeviceCollection collection;
        InTheHand.Net.Sockets.BluetoothClient cli;

        #region Constructor
        /// <summary>
        /// Creates a new instance of <see cref="BluetoothDiscovery"/>
        /// </summary>
        public BluetoothDiscovery()
        {
            collection = new BluetoothDeviceCollection();
            cli = new InTheHand.Net.Sockets.BluetoothClient();
        }
        #endregion

        #region DiscoverDevices
        /// <summary>
        /// Discovers accessible Bluetooth devices and returns their names and addresses.
        /// </summary>
        /// <returns>A collection of BluetoothDevice objects describing the devices discovered.</returns>
		///	<example> This sample shows how to call the DiscoverDevices method.			
		///	<code>			
		///	class FindDevices			
		///	{			
		///		static AutoResetEvent wh = new AutoResetEvent(false);		
		///				
		///		public static void DiscoverDev()		
		///		{		
		///			// search for reachable devices	
		///			BluetoothDiscovery discovery = new BluetoothDiscovery();	
		///				
		///			// Search for maximum 255 devices	
		///			// Include paired devices	
		///			// Include remebered devices	
		///			// Include unknown devices	
		///			BluetoothDeviceCollection bdc =	discovery.DiscoverDevices();
		///				
		///			foreach (BluetoothDevice device in bdc)	
		///			{	
		///				Console.WriteLine(device.Name + ";" + device.DeviceAddress);
		///			}	
		///			wh.Set();	
		///		}		
		///				
		///		public static void Main()		
		///		{		
		///			Console.WriteLine("Looking for devices...");	
		///			DiscoverDev();	
		///			wh.WaitOne();	
		///		}		
		///	}			
		///	</code>			
		///	</example>			
		public BluetoothDeviceCollection DiscoverDevices()
        {
            BluetoothDeviceInfo[] deviceInfo = cli.DiscoverDevices(255, true, true, true);

            collection.Clear();
            foreach (BluetoothDeviceInfo info in deviceInfo)
            {
                BluetoothDevice device = new BluetoothDevice(info);
                collection.Add(device);
            }

            return collection;

        }

        /// <summary>
        /// Discovers accessible Bluetooth devices and returns their names and addresses.
        /// </summary>
        /// <param name="service">target service identification as <see cref="Guid"/></param>
        /// <returns>A collection of BluetoothDevie objects describing the devices discovered</returns>
        public BluetoothDeviceCollection DiscoverDevices(Guid service)
        {
            BluetoothDeviceInfo[] deviceInfo = cli.DiscoverDevices(255, true, true, true);

            collection.Clear();
            foreach (BluetoothDeviceInfo info in deviceInfo)
            {
                if (!info.GetServiceRecords(service).Length.Equals(0))
                {
                    BluetoothDevice device = new BluetoothDevice(info);
                    collection.Add(device);
                }
            }

            return collection;

        }

        /// <summary>
        /// Discovers accessible Bluetooth devices and returns their names and addresses.
        /// </summary>
        /// <param name="deviceName">Target device name</param>
        /// <returns>A collection of BluetoothDevice objects describing the devices discovered</returns>
        public BluetoothDeviceCollection DiscoverDevices(String deviceName)
        {
            if (deviceName == null) return null;

            BluetoothDeviceInfo[] deviceInfo = cli.DiscoverDevices(255, true, true, true);

            collection.Clear();
            foreach (BluetoothDeviceInfo info in deviceInfo)
            {
                if (!info.DeviceName.Equals(deviceName))
                {
                    BluetoothDevice device = new BluetoothDevice(info);
                    collection.Add(device);
                }
            }

            return collection;

        }

        /// <summary>
        /// Discovers accessible Bluetooth devices and returns their names and addresses.
        /// </summary>
        /// <param name="serviceList">Array of target services</param>
        /// <returns>A collection of BluetoothDevice objects describing the devices discovered</returns>
        public BluetoothDeviceCollection DiscoverDevices(ArrayList serviceList)
        {
            if (serviceList == null) return null;

            BluetoothDeviceInfo[] deviceInfo = cli.DiscoverDevices(255, true, true, true);

            collection.Clear();
            foreach (BluetoothDeviceInfo info in deviceInfo)
            {
                foreach (Guid service in serviceList)
                {
                    if (!info.GetServiceRecords(service).Length.Equals(0))
                    {
                        BluetoothDevice device = new BluetoothDevice(info);
                        collection.Add(device);

                        // exit inner loop
                        break;
                    }
                }
            }

            return collection;

        }

        /// <summary>
        /// Discovers accessible Bluetooth devices and returns their names and addresses.
        /// </summary>
        /// <param name="count">Max number of discovered devices</param>
        /// <param name="authenticated">discover authenticated devices</param>
        /// <param name="remembered">discover remembered devices</param>
        /// <param name="unkown">discover unkown devices</param>
        /// <returns>A collection of BluetoothDevice objects describing the devices discovered</returns>
		///	<example> This sample shows how to call the DiscoverDevices method.			
		///	<code>			
		///	class FindDevices			
		///	{			
		///		static AutoResetEvent wh = new AutoResetEvent(false);		
		///				
		///		public static void DiscoverDev()		
		///		{		
		///			// search for reachable devices	
		///			BluetoothDiscovery discovery = new BluetoothDiscovery();	
		///				
		///			// Search for maximum 12 devices	
		///			// Exclude paired devices	
		///			// Exclude remebered devices	
		///			// Include unknown devices	
		///			BluetoothDeviceCollection bdc =	
		///				discovery.DiscoverDevices(12, false, false, true);
		///				
		///			foreach (BluetoothDevice device in bdc)	
		///			{	
		///				Console.WriteLine(device.Name + ";" + device.DeviceAddress);
		///			}	
		///			wh.Set();	
		///		}		
		///				
		///		public static void Main()		
		///		{		
		///			Console.WriteLine("Looking for devices...");	
		///			DiscoverDev();	
		///			wh.WaitOne();	
		///		}		
		///	}			
		///	</code>			
		///	</example>			
		public BluetoothDeviceCollection DiscoverDevices(int count, bool authenticated, bool remembered, bool unkown)
        {
            BluetoothDeviceInfo[] deviceInfo = cli.DiscoverDevices(255, authenticated, remembered, unkown);

            collection.Clear();
            foreach (BluetoothDeviceInfo info in deviceInfo)
            {
                BluetoothDevice device = new BluetoothDevice(info);
                collection.Add(device);
            }

            return collection;

        }

        /// <summary>
        /// Gets the previously discovered devices as a <see cref="BluetoothDeviceCollection"/>
        /// </summary>
        /// <returns>list of discovered devices as <see cref="BluetoothDeviceCollection"/></returns>
        public BluetoothDeviceCollection GetDiscoveredDevices()
        {
            return collection;
		}
		#endregion

	}
}
