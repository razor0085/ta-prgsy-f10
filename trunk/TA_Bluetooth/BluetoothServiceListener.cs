// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothServiceList
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace TA.Bluetooth
{
    /// <summary>
    /// Abstract class to handle incoming connections to a service
    /// </summary>
    public abstract class BluetoothServiceListener
    {
		#region HandleConnection
		/// <summary>
        /// Handle incoming connection
        /// Calls HandleConnection(NetworkStream) if not overwritten.
        /// </summary>
        /// <param name="client"><see cref="BluetoothClient"/> as source of the incoming connection</param>
        public virtual void HandleConnection(BluetoothClient client){
            HandleConnection(client.GetStream());
        }

        /// <summary>
        /// Handle incoming connection
        /// </summary>
        /// <param name="stream"><see cref="NetworkStream"/> as source of the incoming connection</param>
        public virtual void HandleConnection(NetworkStream stream){
		}
		#endregion

		#region ServiceTerminated
		/// <summary>
        /// Notification of terminated service
        /// </summary>
        public virtual void ServiceTerminated()
        {
        }
		#endregion
	}
}
