using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using NetPlan.Model;

namespace NetPlan.IContract
{
    [ServiceContract( SessionMode = SessionMode.NotAllowed)]
    public  interface IAircomServcie
     {
        /// <summary>
        /// 创建仿真任务
        /// </summary>
        /// <param name="Data">仿真数据信息</param>
        /// <returns>接收数据成功或失败，成功返回True</returns>
        [OperationContract()]
         bool CreateTask(PLAData Data);
        [OperationContract]
        bool Hello();
     }
}
