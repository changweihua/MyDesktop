using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace MyDesktop.ViewModels
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	ChildViewModel
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop.ViewModels
     * 文 件 名:	ChildViewModel
     * 创建时间:	2013/12/28 16:29:35
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	a42d5500-93ec-4d41-a121-e53babba3c96  
	 *
	 * 登录用户:	Changweihua
	 * 所 属 域:	Lumia800

	 * 创建年份:	2013
     * 修改时间:
     * 修 改 人:
     * 
     ************************************************************************************/
    #endregion

    [Export]
    public class ChildViewModel : Screen
    {
        public void ClickMe()
        {
            MessageBox.Show("Hello");
        }
    }
}
