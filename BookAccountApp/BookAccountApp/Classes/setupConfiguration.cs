using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookAccountApp.Classes
{
   public  class setupConfiguration
    {
        
       
        public static string GetMotherBoardID()
        {
            string mbInfo = String.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                    mbInfo = Convert.ToString(propData.Value);
            }

            return mbInfo;
        }
        //public static String GetHDDSerialNo()
        //{
        //    //ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
        //    //ManagementObjectCollection mcol = mangnmt.GetInstances();
        //    //string result = "";
        //    //foreach (ManagementObject strt in mcol)
        //    //{
        //    //    result += Convert.ToString(strt["VolumeSerialNumber"]);
        //    //}
        //    //return result;
        //    string systemLogicalDiskDeviceId = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 2);

        //    // Start by enumerating the logical disks
        //    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DeviceID='" + systemLogicalDiskDeviceId + "'"))
        //    {
        //        foreach (ManagementObject logicalDisk in searcher.Get())
        //            foreach (ManagementObject partition in logicalDisk.GetRelated("Win32_DiskPartition"))
        //                foreach (ManagementObject diskDrive in partition.GetRelated("Win32_DiskDrive"))
        //                    return diskDrive["SerialNumber"].ToString();
        //    }

        //    return null;
        //}
        public static String GetHDDSerialNo()
        {

            string systemLogicalDiskDeviceId = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 2);
            if (string.IsNullOrEmpty(systemLogicalDiskDeviceId) || systemLogicalDiskDeviceId == null)
            {
                systemLogicalDiskDeviceId = "C:";
            }
            //Create our ManagementObject, passing it the drive letter to the
            //DevideID using WQL
            ManagementObject disk = new ManagementObject("Win32_LogicalDisk.DeviceID=\"" + systemLogicalDiskDeviceId + "\"");
            //bind our management object
            disk.Get();
            //Return the serial number
            return disk["VolumeSerialNumber"].ToString();

        }
       
    }
}
