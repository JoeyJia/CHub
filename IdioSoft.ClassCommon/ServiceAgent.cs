using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Xml;
using System.IO;

namespace IdioSoft.ClassCommon
{
    /// <summary>
    /// WebService代理类
    /// </summary>
    public class ServiceAgent
    {
        private object agent;
        private Type agentType;
        private const string CODE_NAMESPACE = "IdioSoft.WebServiceAgent.Dynamic";
        /// <summary>
        /// 创建新的代理
        /// </summary>
        /// <param name="serviceUri">webservice Url</param>
        public ServiceAgent(string serviceUri)
        {
            XmlTextReader reader = new XmlTextReader(serviceUri + "?wsdl");
            ServiceDescription sd = ServiceDescription.Read(reader);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);
            CodeNamespace cn = new CodeNamespace(CODE_NAMESPACE);
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            Microsoft.CSharp.CSharpCodeProvider icc = new Microsoft.CSharp.CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            CompilerResults cr = icc.CompileAssemblyFromDom(cp, ccu);
            agentType = cr.CompiledAssembly.GetTypes()[0];
            agent = Activator.CreateInstance(agentType);
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="methodName">方法名，大小写敏感</param>
        ///<param name="args">参数，按照参数顺序赋值</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(string methodName, params object[] args)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, args);
        }
        ///<summary>
        ///调用指定方法
        ///</summary>
        ///<param name="method">方法信息</param>
        ///<param name="args">参数，按照参数顺序赋值</param>

        ///<returns>Web服务的返回值</returns>
        public object Invoke(MethodInfo method, params object[] args)
        {
            return method.Invoke(agent, args);
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="methodName">方法名，大小写敏感</param>
        ///<param name="arg0">参数</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(string methodName, object arg0)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, new object[] { arg0 });
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="methodName">方法名，大小写敏感</param>
        ///<param name="args0">参数</param>
        ///<param name="args1">参数</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(string methodName, object arg0, object arg1)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, new object[] { arg0, arg1 });
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="methodName">方法名，大小写敏感</param>
        ///<param name="args0">参数</param>
        ///<param name="args1">参数</param>
        ///<param name="args2">参数</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(string methodName, object arg0, object arg1, object arg2)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, new object[] { arg0, arg1, arg2 });
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="method">方法信息</param>
        ///<param name="args0">参数</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(MethodInfo method, object arg0)
        {
            return this.Invoke(method, new object[] { arg0 });
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="method">方法信息</param>
        ///<param name="arg0">参数</param>
        ///<param name="arg1">参数</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(MethodInfo method, object arg0, object arg1)
        {
            return this.Invoke(method, new object[] { arg0, arg1 });
        }
        ///<summary>
        ///调用指定的方法
        ///</summary>
        ///<param name="method">方法信息</param>
        ///<param name="arg0">参数</param>
        ///<param name="arg1">参数</param>
        ///<param name="arg2">参数</param>
        ///<returns>Web服务的返回值</returns>
        public object Invoke(MethodInfo method, object arg0, object arg1, object arg2)
        {
            return this.Invoke(method, new object[] { arg0, arg1, arg2 });
        }
        ///<summary>
        ///获取所有公共方法
        ///</summary>
        public MethodInfo[] Methods
        {
            get
            {
                return agentType.GetMethods();
            }
        }
    }
}
