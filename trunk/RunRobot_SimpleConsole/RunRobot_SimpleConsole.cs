using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RobotCtrl;
using System.Threading;
using RobotView;
using System.IO;
using TA.Bluetooth;

namespace RunRobot_SimpleConsole
{
    class RunRobot_SimpleConsole
    {
        Robot robot;
        BluetoothService service;
        Guid serviceId;
        ArrayList bfList;

        static void Main(string[] args)
        {
            RunRobot_SimpleConsole runRobot_SimpleConsole = new RunRobot_SimpleConsole();
            System.Console.ReadLine();
        }

        public RunRobot_SimpleConsole()
        {
            bfList = new ArrayList();
            //robot = new Robot(RunMode.REAL);
            //robot.PositionInfo = new PositionInfo(0, 0, 0);
            BT_Server(BluetoothServiceList.Robot17);

        }

        public void BT_Server(Guid serviceId)
        {


            // check if Bluetooth-Stick is available
            if (BluetoothRadio.IsSupported)
            {
                // Device visible for others
                // RadioMode setting not supported as long as the
                // WinCE image doesn't include the BthUtil.dll
                // BluetoothRadio.PrimaryRadio.Mode = 
                //     BluetoothRadioMode.Discoverable;

                // set device Name
                BluetoothRadio.PrimaryRadio.Name = "Robot-ULtimate";
                // desired service
                //Guid serviceId = BluetoothServiceList.Robot11;
                this.serviceId = serviceId;
                //Guid serviceId = new Guid("{FB4B43E4-0328-4056-82A5-7E03BE347082}");

                // start new service
                service = new BluetoothService();
                service.CreateService(serviceId);
                System.Console.WriteLine("Service " + serviceId.ToString() + " started.");
            }
            else
            {
                System.Console.WriteLine("No Bluetooth-Stick is available");
                return;
            }

            BluetoothClient client;
            System.Net.Sockets.NetworkStream ns;
            System.Console.WriteLine("wait for incoming connections...");
            while (true)
            {
                // accept bluetooth connection
                client = service.WaitForConnection();
                System.Console.WriteLine("Incoming connection " + client.GetSocket().RemoteEndPoint);

                // transform into network stream
                ns = client.GetStream();

                // Output data to stream
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine("Hello from " + BluetoothRadio.PrimaryRadio.Name);
                //wartet auf Befehl
                StreamReader sr = new StreamReader(ns);
                String bf = "";
                bf += sr.ReadLine();
                while (!bf.Contains("Go"))
                {
                    bf += sr.ReadLine();
                }
                System.Console.WriteLine(bf);
                if (bf.Contains("Go"))
                {
                    string[] tokens = bf.Split(new char[] { ',' });
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        System.Console.WriteLine(tokens[i]);
                    }

                }
                // clear and close stream
                sw.Flush();
                client.Close();
            }
        }
    }
}
