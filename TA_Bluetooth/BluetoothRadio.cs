// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothRadio
//

using System;
using System.Collections.Generic;
using System.Text;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;

namespace TA.Bluetooth
{
    /// <summary>
    /// Represents a Bluetooth Radio device.
    /// </summary>
    /// <remarks>Allows you to query properties of the radio hardware and set the mode.</remarks>
    public class BluetoothRadio
    {
        private InTheHand.Net.Bluetooth.BluetoothRadio radio;
		
		#region Constructor
		/// <summary>
        /// Initialize a concrete BluetoothRadio
        /// </summary>
        /// <param name="radio"><see cref="InTheHand.Net.Bluetooth.BluetoothRadio"/> to initialize the concrete BluetoothRadio object</param>
        public BluetoothRadio(InTheHand.Net.Bluetooth.BluetoothRadio radio)
        {
            this.radio = radio;
		}
		#endregion

        #region IsSupported
        /// <summary>
        /// Indicates whether this library can be used with the installed Bluetooth Device
        /// </summary>
		///	<example> This sample shows how to use the IsSupported attribute.
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
		///				
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
		public static Boolean IsSupported
        {
            get
            {
                return InTheHand.Net.Bluetooth.BluetoothRadio.IsSupported;
            }
        }
        #endregion

        #region PrimaryRadio
        /// <summary>
        /// Gets the primary BluetoothRadio
        /// </summary>
        ///	<example> This sample shows how to use the PrimaryRadio attribute.
        ///	<code>			
        /// public static void Main()
        /// {
        ///     // check if Bluetooth-Stick is available
        ///     if (BluetoothRadio.IsSupported)
        ///     {
        ///         // referenc to the primary Bluetooth-Stick 
        ///         BluetoothRadio radio = BluetoothRadio.PrimaryRadio;
        ///         Console.WriteLine("My Bluetooth-Stick:");
        ///         // Bluetooth radio attributes
        ///         Console.WriteLine("Name: " + radio.Name);
        ///         Console.WriteLine("Mac:  " + radio.LocalAddressToString);
        ///         Console.WriteLine("Mode: " + radio.Mode);
        ///     }
        ///     else
        ///     {
        ///         Console.WriteLine("No Bluetooth-Stick is available");
        ///     }
        /// }
        ///	</code>			
        ///	</example>			
        public static BluetoothRadio PrimaryRadio
        {
            get
            {
                return new BluetoothRadio(InTheHand.Net.Bluetooth.BluetoothRadio.PrimaryRadio);
            }
        }
        #endregion

        #region Attributes Mode, Name
        /// <summary>
        /// Gets or Sets the current <see cref="BluetoothRadioMode"/> of operation of the Bluetooth radio. 
        /// </summary>
        public BluetoothRadioMode Mode
        {
            get
            {
                return (BluetoothRadioMode)radio.Mode;
            }
            set
            {
                radio.Mode = (InTheHand.Net.Bluetooth.RadioMode)value;
            }
        }


        /// <summary>
        /// Returns the friendly name of the local Bluetooth radio
        /// </summary>
        /// Remarks:
        ///  Currently read-only on Windows XP. Other devices may cache this device name.
        public String Name
        {
            get
            {
                return radio.Name;
            }
            set
            {
                radio.Name = value;
            }
        }
        #endregion

        #region LocalAddress
        /// <summary>
        /// Returns the address of the local Bluetooth radio device. 
        /// </summary>
        public byte[] LocalAddress
        {
            get
            {
                return radio.LocalAddress.ToByteArray();
            }
		}

        /// <summary>
        /// Returns the address of the local Bluetooth radio device as a String. 
        /// </summary>
        public String LocalAddressToString
        {
            get
            {
                return radio.LocalAddress.ToString();
            }
        }
        #endregion

	}
}
