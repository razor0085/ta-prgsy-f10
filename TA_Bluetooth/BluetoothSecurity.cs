// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothSecurity
//

using System;
using System.Collections.Generic;
using System.Text;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;

namespace TA.Bluetooth
{
    /// <summary>
    /// Handles security between bluetooth devices. Current supported on devices only.
    /// </summary>
    class BluetoothSecurity
    {
		#region Pair request
		//TODO PairRequest XmlDocs for XP and CE pre 5.0.
        /// <summary>
        /// Intiates pairing for a remote device.
        /// </summary>
        /// <param name="device">Remote device with which to pair.</param>
        /// <param name="pin">Chosen PIN code, must be between 1 and 16 ASCII characters.</param>
        /// <remarks><para>On Windows CE platforms this calls <c>BthPairRequest</c>,
        /// its MSDN remarks say:
        /// </para>
        /// <para>BthPairRequest passes the parameters to the <c>BthSetPIN</c>
        /// function and creates an ACL connection. Once the connection is established,
        /// it calls the <c>BthAuthenticate</c> function to authenticate the device.;
        /// </para>
        /// <para>See also 
        /// <see cref="M:InTheHand.Net.Bluetooth.BluetoothSecurity.SetPin(InTheHand.Net.BluetoothAddress,System.String)"/>
        /// </para>
        /// </remarks>
        /// <returns></returns>
        public static Boolean PairRequest(BluetoothDevice device, string pin)
        {
            BluetoothAddress address = new BluetoothAddress(device.Address);
            return InTheHand.Net.Bluetooth.BluetoothSecurity.PairRequest(address, pin);
		}
		#endregion

		#region RemoveDevice
		/// <summary>
        /// Remove the pairing with the specified device
        /// </summary>
        /// <param name="device">Remote device with which to remove pairing.</param>
        /// <returns>TRUE if device was successfully removed, else FALSE.</returns>
        public static Boolean RemoveDevice(BluetoothDevice device)
        {
            BluetoothAddress address = new BluetoothAddress(device.Address);
            return InTheHand.Net.Bluetooth.BluetoothSecurity.RemoveDevice(address);
		}
		#endregion

		#region SetPin
		/// <summary>
        /// This function stores the personal identification number (PIN) for the Bluetooth device.
        /// <para><b>Not supported on Windows XP</b></para>
        /// </summary>
        /// <param name="address">Address of remote device.</param>
        /// <param name="pin">Pin, alphanumeric string of between 1 and 16 ASCII characters.</param>
        /// <remarks><para>On Windows CE platforms this calls <c>BthSetPIN</c>,
        /// its MSDN remarks say:
        /// </para>
        /// <para>Stores the pin for the Bluetooth device identified in pba.
        /// The active connection to the device is not necessary, nor is the presence
        /// of the Bluetooth controller. The PIN is persisted in the registry until
        /// BthRevokePIN is called.
        /// </para>
        /// <para>While the PIN is stored, it is supplied automatically
        /// after the PIN request is issued by the authentication mechanism, so the
        /// user will not be prompted for it. Typically, for UI-based devices, you
        /// would set the PIN for the duration of authentication, and then revoke
        /// it after authentication is complete.;
        /// </para>
        /// <para>See also 
        /// <see cref="M:InTheHand.Net.Bluetooth.BluetoothSecurity.RevokePin(InTheHand.Net.BluetoothAddress)"/>
        /// </para>
        /// </remarks>
        /// <returns>True on success, else False.</returns>
        /// <seealso cref="M:InTheHand.Net.Bluetooth.BluetoothSecurity.RevokePin(InTheHand.Net.BluetoothAddress)"/>
        /// <exception cref="PlatformNotSupportedException">Not supported on Windows XP - use PairRequest</exception>
        public static Boolean SetPin(byte[] address, string pin)
        {
#if WinXP
            throw (new PlatformNotSupportedException("Not supported on Windows XP!"));
#else
            BluetoothAddress btAddress = new BluetoothAddress(address);
            return InTheHand.Net.Bluetooth.BluetoothSecurity.SetPin(btAddress, pin);
#endif
		}
		#endregion

	}
}
