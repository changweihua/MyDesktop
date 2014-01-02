using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace MyDesktop.Helpers
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	NetworkSettingHelper
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop.Helpers
     * 文 件 名:	NetworkSettingHelper
     * 创建时间:	2013/12/29 17:12:15
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	ef14755d-a4cd-4a8c-b9f3-eec3b69ce53b  
	 *
	 * 登录用户:	Changweihua
	 * 所 属 域:	Lumia800

	 * 创建年份:	2013
     * 修改时间:
     * 修 改 人:
     * 
     ************************************************************************************/
    #endregion

    /// <summary>
    /// C#设置本地网络（DNS、网关、子网掩码、IP）
    /// </summary>
    public class NetworkSettingHelper
    {
        /// <summary>
        /// 设置DNS
        /// </summary>
        /// <param name="dns"></param>
        public static void SetDNS(string[] dns)
        {
            SetIPAddress(null, null, null, dns);
        }
        /// <summary>
        /// 设置网关
        /// </summary>
        /// <param name="gateWay"></param>
        public static void SetGetWay(string gateWay)
        {
            SetIPAddress(null, null, new string[] { gateWay }, null);
        }
        /// <summary>
        /// 设置网关
        /// </summary>
        /// <param name="gateWay"></param>
        public static void SetGateWay(string[] gateWay)
        {
            SetIPAddress(null, null, gateWay, null);
        }
        /// <summary>
        /// 设置IP地址和掩码
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="subMask"></param>
        public static void SetIPAddress(string ip, string subMask)
        {
            SetIPAddress(new string[] { ip }, new string[] { subMask }, null, null);
        }
        /// <summary>
        /// 设置IP地址，掩码和网关
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="subMask"></param>
        /// <param name="getway"></param>
        public static void SetIPAddress(string ip, string subMask, string gateWay)
        {
            SetIPAddress(new string[] { ip }, new string[] { subMask }, new string[] { gateWay }, null);
        }

        private static void SetIPAddress(string[] ip, string[] subMask, string[] gateWay, string[] dns, bool check = false)
        {
            ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = wmi.GetInstances();
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;

            foreach (ManagementObject mo in moc)
            {
                //如果没有启用IP设置的网络设备则跳过
                if (!(bool)mo["IPEnabled"])
                    continue;

                //设置IP地址和掩码
                if (ip != null && subMask != null)
                {
                    inPar = mo.GetMethodParameters("EnableStatic");
                    inPar["IPAddress"] = ip;
                    inPar["SubnetMask"] = subMask;
                    outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                }

                //设置网关地址
                if (gateWay != null)
                {
                    inPar = mo.GetMethodParameters("SetGateways");
                    inPar["DefaultIPGateway"] = gateWay;
                    outPar = mo.InvokeMethod("SetGateways", inPar, null);
                }

                //设置DNS地址
                if (dns != null)
                {
                    inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    inPar["DNSServerSearchOrder"] = dns;
                    outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                }

            }
        }

        /// <summary>
        /// 启用DHCP服务器
        /// </summary>
        private static void EnableDHCP()
        {
            ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = wmi.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //如果没有启用IP设置的网络设备则跳过
                if (!(bool)mo["IPEnabled"])
                    continue;
                //重置DNS为空
                mo.InvokeMethod("SetDNSServerSearchOrder", null);
                //开启DHCP
                mo.InvokeMethod("EnableDHCP", null);
            }
        } 

        /// <summary>
        /// 判断是否符合IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static bool IsIPAddress(string ip)
        {
            //将完整的IP以“.”为界限分组
            string[] arr = ip.Split('.');


            //判断IP是否为四组数组成
            if (arr.Length != 4)
                return false;


            //正则表达式，1~3位整数
            string pattern = @"\d{1,3}";
            for (int i = 0; i < arr.Length; i++)
            {
                string d = arr[i];


                //判断IP开头是否为0
                if (i == 0 && d == "0")
                    return false;


                //判断IP是否是由1~3位数组成
                if (!System.Text.RegularExpressions.Regex.IsMatch(d, pattern))
                    return false;

                if (d != "0")
                {
                    //判断IP的每组数是否全为0
                    d = d.TrimStart('0');
                    if (d == "")
                        return false;

                    //判断IP每组数是否大于255
                    if (int.Parse(d) > 255)
                        return false;
                }
            } 
            
            return true;
        }

    }
}
