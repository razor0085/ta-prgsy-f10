// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothConnectionNotifier
//

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace TA.Bluetooth
{
    /// <summary>
    /// Listens on the supplied connection and calls events in case of incoming data
    /// </summary>
    public class BluetoothConnectionNotifier
    {

        private BluetoothClient client;
        private BluetoothConnectionListener listener = null;
        private Boolean listenOnConnection = true;

        #region Constructor

        /// <summary>
        /// Creates new instance of <see cref="BluetoothConnectionNotifier"/>
        /// </summary>
        /// <param name="client">Connection to listen at</param>
        public BluetoothConnectionNotifier(BluetoothClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Creates new instance of <see cref="BluetoothConnectionNotifier"/>
        /// </summary>
        /// <param name="client">Connection to listen at</param>
        /// <param name="listener"><see cref="BluetoothConnectionListener"/> to handle the incomin data</param>
        public BluetoothConnectionNotifier(BluetoothClient client, BluetoothConnectionListener listener)
        {
            this.client = client;
            this.listener = listener;
        }
        #endregion

		#region Listening
		/// <summary>
        /// Listen for incoming data on supplied connection.  Notifies disconnection of client
        /// This is a blocking call!
        /// </summary>
        public void StartListening()
        {

            if (listener == null)
            {
                throw (new Exception("No connection listener supplied!"));
            }

            while (listenOnConnection)
            {
                if (!client.IsConnected())
                {
                    listenOnConnection = false;
                }
                else if (client.GetStream().DataAvailable)
                {

                    listener.HandleConnection(client);
                }
                else
                {
                    Thread.Sleep(100);
                }

            }

            listener.Disconnected(client);
        }

        /// <summary>
        /// Listen for incoming data on supplied connection Notifies disconnection of client.
        /// This is a non-blocking call starting a new thread.
        /// </summary>
        public void StartAsynchListening()
        {
            Thread thread = new Thread(this.StartListening);
            thread.Start();
        }

        /// <summary>
        /// Stop the listening for incoming data
        /// </summary>
        public void StopListening()
        {
            listenOnConnection = false;
        }

        /// <summary>
        /// Add the <see cref="BluetoothConnectionListener"/> intended to handle the incoming data
        /// </summary>
        /// <param name="listener"><see cref="BluetoothConnectionListener"/> to be added to the connection notifier</param>
        public void AddListener(BluetoothConnectionListener listener)
        {
            if (this.listener == null)
            {
                this.listener = listener;
            }
            else
            {
                throw (new Exception("Only one connection listener allowed!"));
            }
        }
		#endregion
	}
}
