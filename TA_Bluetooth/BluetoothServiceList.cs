// TA Bluetooth Framework
//
// TA.Bluetooth.BluetoothServiceList
//

using System;
using System.Collections;
using System.Text;

namespace TA.Bluetooth
{
    /// <summary>
    /// Container to remember Generic service Guids
    /// </summary>
    public class BluetoothServiceList
    {
        /// <summary>
        /// List of all static defined service Guids
        /// </summary>
        protected ArrayList serviceList = new ArrayList();

		#region Guid Robot01..32
		/// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c01-b062-14e1718fa07a</value>
        public static Guid Robot01 = new Guid("{c849031a-df6a-4c01-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c02-b062-14e1718fa07a</value>
        public static Guid Robot02 = new Guid("{c849031a-df6a-4c02-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c03-b062-14e1718fa07a</value>
        public static Guid Robot03 = new Guid("{c849031a-df6a-4c03-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c04-b062-14e1718fa07a</value>
        public static Guid Robot04 = new Guid("{c849031a-df6a-4c04-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c05-b062-14e1718fa07a</value>
        public static Guid Robot05 = new Guid("{c849031a-df6a-4c05-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c06-b062-14e1718fa07a</value>
        public static Guid Robot06 = new Guid("{c849031a-df6a-4c06-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c07-b062-14e1718fa07a</value>
        public static Guid Robot07 = new Guid("{c849031a-df6a-4c07-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c08-b062-14e1718fa07a</value>
        public static Guid Robot08 = new Guid("{c849031a-df6a-4c08-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c09-b062-14e1718fa07a</value>
        public static Guid Robot09 = new Guid("{c849031a-df6a-4c09-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c10-b062-14e1718fa07a</value>
        public static Guid Robot10 = new Guid("{c849031a-df6a-4c10-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c11-b062-14e1718fa07a</value>
        public static Guid Robot11 = new Guid("{c849031a-df6a-4c11-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c12-b062-14e1718fa07a</value>
        public static Guid Robot12 = new Guid("{c849031a-df6a-4c12-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c13-b062-14e1718fa07a</value>
        public static Guid Robot13 = new Guid("{c849031a-df6a-4c13-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c14-b062-14e1718fa07a</value>
        public static Guid Robot14 = new Guid("{c849031a-df6a-4c14-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c15-b062-14e1718fa07a</value>
        public static Guid Robot15 = new Guid("{c849031a-df6a-4c15-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c16-b062-14e1718fa07a</value>
        public static Guid Robot16 = new Guid("{c849031a-df6a-4c16-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c17-b062-14e1718fa07a</value>
        public static Guid Robot17 = new Guid("{c849031a-df6a-4c17-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c18-b062-14e1718fa07a</value>
        public static Guid Robot18 = new Guid("{c849031a-df6a-4c18-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c19-b062-14e1718fa07a</value>
        public static Guid Robot19 = new Guid("{c849031a-df6a-4c19-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c20-b062-14e1718fa07a</value>
        public static Guid Robot20 = new Guid("{c849031a-df6a-4c20-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c21-b062-14e1718fa07a</value>
        public static Guid Robot21 = new Guid("{c849031a-df6a-4c21-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c22-b062-14e1718fa07a</value>
        public static Guid Robot22 = new Guid("{c849031a-df6a-4c22-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c23-b062-14e1718fa07a</value>
        public static Guid Robot23 = new Guid("{c849031a-df6a-4c23-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c24-b062-14e1718fa07a</value>
        public static Guid Robot24 = new Guid("{c849031a-df6a-4c24-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c25-b062-14e1718fa07a</value>
        public static Guid Robot25 = new Guid("{c849031a-df6a-4c25-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c26-b062-14e1718fa07a</value>
        public static Guid Robot26 = new Guid("{c849031a-df6a-4c26-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c27-b062-14e1718fa07a</value>
        public static Guid Robot27 = new Guid("{c849031a-df6a-4c27-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c28-b062-14e1718fa07a</value>
        public static Guid Robot28 = new Guid("{c849031a-df6a-4c28-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c29-b062-14e1718fa07a</value>
        public static Guid Robot29 = new Guid("{c849031a-df6a-4c29-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c30-b062-14e1718fa07a</value>
        public static Guid Robot30 = new Guid("{c849031a-df6a-4c30-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c31-b062-14e1718fa07a</value>
        public static Guid Robot31 = new Guid("{c849031a-df6a-4c31-b062-14e1718fa07a}");
        /// <summary>Generic service for the client/robot and robot/robot communication.</summary>
        /// <value>Value of this generic service guid is c849031a-df6a-4c32-b062-14e1718fa07a</value>
        public static Guid Robot32 = new Guid("{c849031a-df6a-4c32-b062-14e1718fa07a}");
		#endregion

		#region Constructor
		/// <summary>
        /// Create new Instance of <see cref="BluetoothServiceList"/>
        /// Initialises the serviceList with all defined Guids
        /// Overwrite constructor in implementing classes to add your own service IDs, but allways call the constructor of the super-class.
        /// e.g public myBluetoothServiceList():base(){...}
        /// </summary>
        public BluetoothServiceList()
        {
            serviceList.Add(Robot01);
			serviceList.Add(Robot02);
			serviceList.Add(Robot03);
			serviceList.Add(Robot04);
			serviceList.Add(Robot05);
			serviceList.Add(Robot06);
			serviceList.Add(Robot07);
			serviceList.Add(Robot08);
			serviceList.Add(Robot09);
			serviceList.Add(Robot10);
			serviceList.Add(Robot11);
			serviceList.Add(Robot12);
			serviceList.Add(Robot13);
			serviceList.Add(Robot14);
			serviceList.Add(Robot15);
			serviceList.Add(Robot16);
			serviceList.Add(Robot17);
			serviceList.Add(Robot18);
			serviceList.Add(Robot19);
			serviceList.Add(Robot20);
			serviceList.Add(Robot21);
			serviceList.Add(Robot22);
			serviceList.Add(Robot23);
			serviceList.Add(Robot24);
			serviceList.Add(Robot25);
			serviceList.Add(Robot26);
			serviceList.Add(Robot27);
			serviceList.Add(Robot28);
			serviceList.Add(Robot29);
			serviceList.Add(Robot30);
			serviceList.Add(Robot31);
			serviceList.Add(Robot32);
		}
		#endregion

		#region GetServiceList
		/// <summary>
        /// Get the list of all defined Guids
        /// </summary>
        /// <returns>Gets an array of all defined Guids</returns>
        public ArrayList GetServiceList()
        {
            return serviceList;
        }
		#endregion
	}
}
