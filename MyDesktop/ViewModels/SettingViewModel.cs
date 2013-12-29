using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;

namespace MyDesktop.ViewModels
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	SettingsViewModel
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop.ViewModels
     * 文 件 名:	SettingsViewModel
     * 创建时间:	2013/12/29 12:13:22
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	335f30ab-b363-4e37-b46f-0c35ebf3dfad  
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
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SettingViewModel : Screen
    {
        private string _statusMessage;
        private System.Uri _selectedSource = new System.Uri("/Views/SettingsAppearanceView.xaml", System.UriKind.Relative);

        public SettingViewModel()
        {
            this.TabLinks = new LinkCollection(new Link[] { 
                new Link()
                {
                    DisplayName = "appearance",
                    Source = new System.Uri("/Views/SettingsAppearanceView.xaml", System.UriKind.Relative)
                },
                new Link()
                {
                    DisplayName = "about",
                    Source = new System.Uri("/Views/AboutView.xaml", System.UriKind.Relative)
                }
            });
        }

        public LinkCollection TabLinks
        {
            get;
            set;
        }

        public System.Uri SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                if (this._selectedSource != value)
                {
                    this._selectedSource = value;
                    this.NotifyOfPropertyChange("StatusMessage");
                }
            }
        }
    }
}
