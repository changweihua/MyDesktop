using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;

namespace MyDesktop.Extensions
{
    #region 关于
    /*************************************************************************************
     * CLR 版本:	4.0.30319.18052
     * 类 名 称:	Extensions
     * 机器名称:	LUMIA800
     * 命名空间:	MyDesktop.Extensions
     * 文 件 名:	Extensions
     * 创建时间:	2013/12/28 16:49:16
     * 作    者:	常伟华 Changweihua
	 * 版    权:	本代码版权归常伟华所有 All Rights Reserved (C) 2013 - 2014
     * 签    名:	To be or not, it is not a problem !
     * 网    站:	http://www.cmono.net
     * 邮    箱:	changweihua@outlook.com  
     * 唯一标识:	4e0fbd26-1aa7-4526-8993-32276e098b65  
	 *
	 * 登录用户:	Changweihua
	 * 所 属 域:	Lumia800

	 * 创建年份:	2013
     * 修改时间:
     * 修 改 人:
     * 
     ************************************************************************************/
    #endregion

    public static class CompositionBatchExtension
    {
        public static ComposablePart AddExport<TKey>(this CompositionBatch batch, Func<object> func)
        {
            var typeString = typeof(TKey).ToString();
            return batch.AddExport(
                new Export(
                    new ExportDefinition(
                        typeString,
                        new Dictionary<string, object>() { { "ExportTypeIdentity", typeString } }),
                    func));

        }
    }
}
