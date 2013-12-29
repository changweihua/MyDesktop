using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using FirstFloor.ModernUI.Windows;

namespace MyDesktop
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	ModernContentLoader
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop
     * 文 件 名:	ModernContentLoader
     * 创建时间:	2013/12/28 16:25:14
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	18184d31-c5c2-4232-a588-5e7614ad6ded  
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
    /// 摘要
    /// </summary>
    public class ModernContentLoader : DefaultContentLoader
    {
        protected override object LoadContent(Uri uri)
        {
            var content = base.LoadContent(uri);

            if (content == null)
                return null;

            // Locate the right viewmodel for this view
            var vm = Caliburn.Micro.ViewModelLocator.LocateForView(content);

            if (vm == null)
                return content;

            // Bind it up with CM magic
            if (content is DependencyObject)
            {
                Caliburn.Micro.ViewModelBinder.Bind(vm, content as DependencyObject, null);
            }

            return content;
        }
    }
}
