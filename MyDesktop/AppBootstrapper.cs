using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using MyDesktop.ViewModels;
using MyDesktop.Extensions;
using MyDesktop.Helpers;

namespace MyDesktop
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	AppBootstrapper
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop
     * 文 件 名:	AppBootstrapper
     * 创建时间:	2013/12/28 16:42:00
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	cd713177-04ac-40d0-ad6d-862330c93efc  
	 *
	 * 登录用户:	Changweihua
	 * 所 属 域:	Lumia800

	 * 创建年份:	2013
     * 修改时间:
     * 修 改 人:
     * 
     ************************************************************************************/
    #endregion

    public class AppBootstrapper : Bootstrapper<IShellViewModel>
    {
        private static CompositionContainer _container;

        static AppBootstrapper()
        {
            LogManager.GetLog = type => new Log4netHelper(type);
        }

        public static T GetInstance<T>()
        {
            string contract = AttributedModelServices.GetContractName(typeof(T));

            var sexports = _container.GetExportedValues<object>(contract);
            if (sexports.Count() > 0)
                return sexports.OfType<T>().First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override void Configure()
        {
            // Add New ViewLocator Rule
            ViewLocator.NameTransformer.AddRule(
                @"(?<nsbefore>([A-Za-z_]\w*\.)*)?(?<nsvm>ViewModels\.)(?<nsafter>([A-Za-z_]\w*\.)*)(?<basename>[A-Za-z_]\w*)(?<suffix>ViewModel$)",
                @"${nsbefore}Views.${nsafter}${basename}View",
                @"(([A-Za-z_]\w*\.)*)?ViewModels\.([A-Za-z_]\w*\.)*[A-Za-z_]\w*ViewModel$"
            );

            _container = new CompositionContainer(
                    new AggregateCatalog(
                    new AssemblyCatalog(typeof(IShellViewModel).Assembly),
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>().FirstOrDefault()
                )
            );

            var batch = new CompositionBatch();
            batch.AddExport<IWindowManager>(() => new WindowManager());
            batch.AddExport<IEventAggregator>(() => new EventAggregator());
            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;

            var exports = _container.GetExportedValues<object>(contract);
            return exports.FirstOrDefault();
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            var ret = Enumerable.Empty<object>();

            string contract = AttributedModelServices.GetContractName(serviceType);
            return _container.GetExportedValues<object>(contract);
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
    }
}
