// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothConnectionListener
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace TA.Bluetooth
{
    /// <summary>
    /// Abstract class to handle connection events
    /// </summary>
    public abstract class BluetoothConnectionListener
    {
		#region handle connection
		/// <summary>
        /// Handle incoming data
        /// Calls HandleConnection(NetworkStream) if not overwritten.
        /// </summary>
        /// <param name="client"><see cref="BluetoothClient"/> as source of the incoming data</param>
        public virtual void HandleConnection(BluetoothClient client)
        {
            HandleConnection(client.GetStream());
        }

        /// <summary>
        /// Handle incoming data
        /// </summary>
        /// <param name="stream"><see cref="NetworkStream"/> as source of the incoming data</param>
        public virtual void HandleConnection(NetworkStream stream)
        {
        }

        /// <summary>
        /// Notification of disconnected client
        /// </summary>
        /// <param name="client"><see cref="BluetoothClient"/> object of disconnected device</param>
        public virtual void Disconnected(BluetoothClient client)
        {
        }
		#endregion
	}
}
