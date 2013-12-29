using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI;
using FirstFloor.ModernUI.Windows;

namespace MyDesktop.Services
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	CaliburnContentLoader
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop.Services
     * 文 件 名:	CaliburnContentLoader
     * 创建时间:	2013/12/28 16:40:04
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	b225b79b-76ad-4e87-a2d2-fc68ec99054c  
	 *
	 * 登录用户:	Changweihua
	 * 所 属 域:	Lumia800

	 * 创建年份:	2013
     * 修改时间:
     * 修 改 人:
     * 
     ************************************************************************************/
    #endregion

    public class CaliburnContentLoader : IContentLoader
    {
        public Task<object> LoadContentAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                throw new InvalidOperationException();
            }

            // scheduler ensures LoadContent is executed on the current UI thread
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(uri), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        protected virtual object LoadContent(Uri uri)
        {
            // don't do anything in design mode
            if (ModernUIHelper.IsInDesignMode)
            {
                return null;
            }

            var content = Application.LoadComponent(uri);
            if (content == null)
                return content;

            var vm = Caliburn.Micro.ViewModelLocator.LocateForView(content);
            if (vm == null)
                return content;

            if (content is DependencyObject)
            {
                Caliburn.Micro.ViewModelBinder.Bind(vm, content as DependencyObject, null);
            }
            return content;
        }
    }
}
