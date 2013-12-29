using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace MyDesktop.Helpers
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	LoggerHelper
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop.Helpers
     * 文 件 名:	LoggerHelper
     * 创建时间:	2013/12/29 11:17:24
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	51859fe9-3b16-4cfb-aa65-266cf69bc7eb  
	 *
	 * 登录用户:	Changweihua
	 * 所 属 域:	Lumia800

	 * 创建年份:	2013
     * 修改时间:
     * 修 改 人:
     * 
     ************************************************************************************/
    #endregion


    public class Log4netHelper : ILog
    {
        #region Fields
        private readonly log4net.ILog _innerLogger;
        #endregion

        #region Constructors
        public Log4netHelper(Type type)
        {
            _innerLogger = log4net.LogManager.GetLogger(type);
        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
            _innerLogger.Error(exception.Message, exception);
        }
        public void Info(string format, params object[] args)
        {
            _innerLogger.InfoFormat(format, args);
        }
        public void Warn(string format, params object[] args)
        {
            _innerLogger.WarnFormat(format, args);
        }
        #endregion
    }

    public class DebugHelper : ILog
    {

        #region Fields
        private readonly Type _type;
        #endregion

        #region Constructors
        public DebugHelper(Type type)
        {
            _type = type;
        }
        #endregion

        #region Helper Methods
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}",
                                 DateTime.Now.ToString("o"),
                                 string.Format(format, args));
        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
            Debug.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
        }
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "INFO");
        }
        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "WARN");
        }
        #endregion
    }

    public class NLogHelper : ILog
    {
        #region Fields
        private readonly NLog.Logger _innerLogger;
        #endregion

        #region Constructors
        public NLogHelper(Type type)
        {
            _innerLogger = NLog.LogManager.GetLogger(type.Name);
        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
            _innerLogger.ErrorException(exception.Message, exception);
        }
        public void Info(string format, params object[] args)
        {
            _innerLogger.Info(format, args);
        }
        public void Warn(string format, params object[] args)
        {
            _innerLogger.Warn(format, args);
        }
        #endregion
    }
}
