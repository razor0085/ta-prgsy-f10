// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothDeviceCollection
//

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace TA.Bluetooth
{
    /// <summary>
    /// Collection of BluetoothDevice objects
    /// </summary>
    public class BluetoothDeviceCollection : System.Collections.ICollection, System.Collections.IList
    {
        private ArrayList devices;

		#region Constructor
		/// <summary>
        /// Create new instance of <see cref="BluetoothDeviceCollection"/>
        /// </summary>
        internal BluetoothDeviceCollection()
		{
			devices = new ArrayList();
		}
		#endregion

		#region CopyTo
		/// <summary>
        /// Copies the entire System.Collections.ArrayList to a compatible one-dimensional
        ///     System.Array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        ///     copied from System.Collections.ArrayList. The System.Array must have zero-based
        ///     indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(System.Array array, System.Int32 index)
		{
			devices.CopyTo(array, index);
		}

        /// <summary>
        /// Copies the entire System.Collections.ArrayList to a compatible one-dimensional
        ///     System.Array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements
        ///     copied from System.Collections.ArrayList. The System.Array must have zero-based
        ///     indexing.</param>
        /// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(BluetoothDeviceCollection array, System.Int32 index)
		{
			devices.CopyTo(array.devices.ToArray(), index);
        }
        #endregion

		#region Count
		/// <summary>
        /// Gets the number of elements actually contained in this list
        /// </summary>
        public int Count
		{
			get
			{
				return devices.Count;
			}
		}
		#endregion

		#region Synch operations
		/// <summary>
        /// Gets a value indication whether access to this list is synchronized (thread safe)
        /// </summary>
		public bool IsSynchronized
		{
			get
			{
				return devices.IsSynchronized;
			}
		}

        /// <summary>
        /// Gets an object that can be used to synchronize access to the list
        /// </summary>
		public object SyncRoot
		{
			get
			{
				return devices.SyncRoot;
			}
		}
		#endregion

		#region Enumerator
		/// <summary>
        /// Returns an enumerator for the entire list
        /// </summary>
        /// <returns>An System.Collections.IEnumerator for the entire list</returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			return devices.GetEnumerator();
		}
		#endregion

		#region Add
		/// <summary>
        /// Add an object to the end of the list
        /// </summary>
        /// <param name="value">object to be added to the list</param>
        /// <returns>index at which the object was added</returns>
		public System.Int32 Add(System.Object value)
		{
			return devices.Add(value);
		}

        /// <summary>
        /// Add an <see cref="BluetoothDevice"/> to the end of the list
        /// </summary>
        /// <param name="value"><see cref="BluetoothDevice"/> to be added to the list</param>
        /// <returns>index at which the object was added</returns>
		public System.Int32 Add(BluetoothDevice value)
		{
			return devices.Add(value);
        }
        #endregion

		#region Clear
		/// <summary>
        /// remove all elements from the list
        /// </summary>
        public void Clear()
		{
			devices.Clear();
		}
		#endregion

		#region IndexOf
		/// <summary>
        /// Searches for the specified System.Object and returns the zero-based index
        ///     of the first occurrence within the entire System.Collections.ArrayList.
        /// </summary>
        /// <param name="value">object to locate in the list</param>
        /// <returns>zero-based index of location for the given object</returns>
		public System.Int32 IndexOf(System.Object value)
		{
			return devices.IndexOf(value);
		}

        /// <summary>
        /// Searches for the specified BluetoothDevice and returns the zero-based index
        ///     of the first occurrence within the entire System.Collections.ArrayList.
        /// </summary>
        /// <param name="value">BluetoothDevice to locate in the list</param>
        /// <returns>zero-based index of location for the given BluetoothDevice</returns>
		public System.Int32 IndexOf(BluetoothDevice value)
		{
			return devices.IndexOf(value);
        }
        #endregion

        #region Contains
        /// <summary>
        /// Determines whether an element is in the list
        /// </summary>
        /// <param name="value">Object to locate</param>
        /// <returns>true if item is found</returns>
        public Boolean Contains(System.Object value)
		{
			return devices.Contains(value);
		}

        /// <summary>
        /// Determines whether an element is in the list
        /// </summary>
        /// <param name="value">BluetoothDevice to locate</param>
        /// <returns>true if item is found</returns>
		public Boolean Contains(BluetoothDevice value)
		{
			return devices.Contains(value);
        }
        #endregion

        #region Insert
        /// <summary>
        /// inserts a element into the list at specified index
        /// </summary>
        /// <param name="index">Index at which to inser</param>
        /// <param name="value">Object to add</param>
        public void Insert(System.Int32 index, System.Object value)
		{
			devices.Insert(index, value);
		}

        /// <summary>
        /// inserts a element into the list at specified index
        /// </summary>
        /// <param name="index">Index at which to inser</param>
        /// <param name="value">BluetoothDevice to add</param>
		public void Insert(System.Int32 index, BluetoothDevice value)
		{
			devices.Insert(index, value);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Remove first occurance of the object from the list
        /// </summary>
        /// <param name="value">Object to remove</param>
        public void Remove(System.Object value)
		{
			devices.Remove(value);
		}

        /// <summary>
        /// Remove first occurance of the BluetoothDevice from the list
        /// </summary>
        /// <param name="value">BluetoothDevice to remove</param>
        public void Remove(BluetoothDevice value)
		{
			devices.Remove(value);
		}

        /// <summary>
        /// Remove element at specified index
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
		public void RemoveAt(System.Int32 index)
		{
			devices.RemoveAt(index);
        }
        #endregion

		#region List operations
		/// <summary>
        /// Gets value indicating whether the list is fixed size
        /// </summary>
        public bool IsFixedSize
		{
			get
			{
				return devices.IsFixedSize;
			}
		}

        /// <summary>
        /// Gets value indicating whether the list is read only
        /// </summary>
		public bool IsReadOnly
		{
			get
			{
				return devices.IsReadOnly;
			}
		}

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
		public object this[int index]
		{
			get
			{
				return devices[index];
			}
			set
			{
				devices[index] = value;
			}
		}
		#endregion

	}
}
