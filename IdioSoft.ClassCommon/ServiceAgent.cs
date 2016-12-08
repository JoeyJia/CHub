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
    /// WebService������
    /// </summary>
    public class ServiceAgent
    {
        private object agent;
        private Type agentType;
        private const string CODE_NAMESPACE = "IdioSoft.WebServiceAgent.Dynamic";
        /// <summary>
        /// �����µĴ���
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
        ///����ָ���ķ���
        ///</summary>
        ///<param name="methodName">����������Сд����</param>
        ///<param name="args">���������ղ���˳��ֵ</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(string methodName, params object[] args)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, args);
        }
        ///<summary>
        ///����ָ������
        ///</summary>
        ///<param name="method">������Ϣ</param>
        ///<param name="args">���������ղ���˳��ֵ</param>

        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(MethodInfo method, params object[] args)
        {
            return method.Invoke(agent, args);
        }
        ///<summary>
        ///����ָ���ķ���
        ///</summary>
        ///<param name="methodName">����������Сд����</param>
        ///<param name="arg0">����</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(string methodName, object arg0)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, new object[] { arg0 });
        }
        ///<summary>
        ///����ָ���ķ���
        ///</summary>
        ///<param name="methodName">����������Сд����</param>
        ///<param name="args0">����</param>
        ///<param name="args1">����</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(string methodName, object arg0, object arg1)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, new object[] { arg0, arg1 });
        }
        ///<summary>
        ///����ָ���ķ���
        ///</summary>
        ///<param name="methodName">����������Сд����</param>
        ///<param name="args0">����</param>
        ///<param name="args1">����</param>
        ///<param name="args2">����</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(string methodName, object arg0, object arg1, object arg2)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, new object[] { arg0, arg1, arg2 });
        }
        ///<summary>
        ///����ָ���ķ���
        ///</summary>
        ///<param name="method">������Ϣ</param>
        ///<param name="args0">����</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(MethodInfo method, object arg0)
        {
            return this.Invoke(method, new object[] { arg0 });
        }
        ///<summary>
        ///����ָ���ķ���
        ///</summary>
        ///<param name="method">������Ϣ</param>
        ///<param name="arg0">����</param>
        ///<param name="arg1">����</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(MethodInfo method, object arg0, object arg1)
        {
            return this.Invoke(method, new object[] { arg0, arg1 });
        }
        ///<summary>
        ///����ָ���ķ���
        ///</summary>
        ///<param name="method">������Ϣ</param>
        ///<param name="arg0">����</param>
        ///<param name="arg1">����</param>
        ///<param name="arg2">����</param>
        ///<returns>Web����ķ���ֵ</returns>
        public object Invoke(MethodInfo method, object arg0, object arg1, object arg2)
        {
            return this.Invoke(method, new object[] { arg0, arg1, arg2 });
        }
        ///<summary>
        ///��ȡ���й�������
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