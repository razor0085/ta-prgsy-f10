using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TA.Bluetooth;

namespace RemoteController_XP
{
    class RemoteController
    {
        public static BluetoothDevice DiscoverDev(Guid service)
		{
			// search for reachable devices
			BluetoothDiscovery discovery = new BluetoothDiscovery();

			// Search for maximum 12 devices
			// Exclude paired devices
			// Exclude remebered devices
			// Include unknown devices
			BluetoothDeviceCollection bdc =
				discovery.DiscoverDevices(12, false, false, true);

			foreach (BluetoothDevice device in bdc)
			{
				if (device.HasService(service))
				{
					Console.WriteLine(device.Name + ";" + device.DeviceAddress);
					return device;
				}
				else
				{
					Console.WriteLine(device.Name + "; no Service");
				}
			}
			Console.WriteLine("Required service does not exist on target");
			return null;
		}

		public static void Main()
		{
			Console.WriteLine("Search for reachable devices...");
			// desired service
			Guid service = BluetoothServiceList.Robot17;
			//Guid service = new Guid("{FB4B43E4-0328-4056-82A5-7E03BE347082}");
			BluetoothDevice device = DiscoverDev(service);
            //BluetoothDevice device = new BluetoothDevice();
			if (device != null)
			{
				// connect to desired service
				BluetoothClient bc = device.Connect(service);
				Console.WriteLine("Outgoing connection " + bc.GetSocket().LocalEndPoint + ".");
				// read transmitted data
                StreamReader sr = new StreamReader("Fahrbefehl.txt");
				StreamWriter sw = new StreamWriter(bc.GetStream());
				// request
                String line="";
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    sw.WriteLine(line);
                    sw.Flush();
                }
				// print
                Console.ReadLine();  
                    bc.Close();
			}
		}
    }
}
