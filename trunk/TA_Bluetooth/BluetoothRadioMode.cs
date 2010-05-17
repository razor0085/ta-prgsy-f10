// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothRadioMode
//

using System;
using System.Collections.Generic;
using System.Text;
using InTheHand.Net.Bluetooth;


namespace TA.Bluetooth
{
    /// <summary>
    /// Determine all the possible modes of operation of the Bluetooth radio
	/// </summary>
	public enum BluetoothRadioMode : int
	{
		/// <summary>
		/// Bluetooth hardware is powered off
		/// </summary>
		PowerOff,
		/// <summary>
        /// Bluetooth is connectable but your device cannot be discovered by other devices
		/// </summary>
		Connectable,
		/// <summary>
		/// Bluetooth hardware is powered on and device advertises itself to others
		/// </summary>
		Discoverable
	};

    
}
