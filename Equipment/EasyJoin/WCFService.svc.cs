using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Business;
using Common.Entity;

namespace EasyJoin
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WCFService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WCFService.svc 或 WCFService.svc.cs，然后开始调试。
    public class WCFService : IWCFService
    {
        public bool AutoIn(AutoinoutInfoEntity model)
        {
            Parking bll = new Parking();
            return bll.AutoIn(model);
        }

        public bool AutoOut(AutoinoutInfoEntity model)
        {
            Parking bll = new Parking();
            return bll.AutoOut(model);
        }
    }
}
