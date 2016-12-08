using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace CHub.ClassLibrary
{
    /// <summary>
    /// 计算表达式的类
    /// </summary>
    public static class CalculateExpression
    {
        /// <summary>
        /// 接受一个string类型的表达式并计算结果,返回一个object对象,静态方法 
        /// </summary>
        /// <param name="expression">传递过来的公式字符串</param>
        /// <returns></returns>
        public static object Calculate(string expression)
        {
            #region "错误代码，取消"
            //string className = "Calc ";
            //string methodName = "Run ";
            //expression = expression.Replace("/ ", "*1.0/ ");

            ////创建编译器实例 
            //CodeDomProvider complier = (new Microsoft.CSharp.CSharpCodeProvider());
            ////设置编译参数
            //CompilerParameters paras = new CompilerParameters();
            //paras.GenerateExecutable = false;
            //paras.GenerateInMemory = true;

            ////创建动态代码
            //StringBuilder classSource = new StringBuilder();
            //classSource.Append("public   class   " + className + "\n ");
            //classSource.Append("{\n ");
            //classSource.Append("         public   object   " + methodName + "()\n ");
            //classSource.Append("         {\n ");
            //classSource.Append("                 return   " + expression + ";\n ");
            //classSource.Append("         }\n ");
            //classSource.Append("} ");

            ////编译代码
            //CompilerResults result = complier.CompileAssemblyFromSource(paras, classSource.ToString());

            ////获取编译后的程序集
            //Assembly assembly = result.CompiledAssembly;

            ////动态调用方法
            //object eval = assembly.CreateInstance(className);
            //MethodInfo method = eval.GetType().GetMethod(methodName);
            //object reobj = method.Invoke(eval, null);
            //GC.Collect();
            //return reobj;
            #endregion

            string strPre = "using System;" + "public static class driver" + "{" + "public static object Run()" + "{";//声明返回过程头
            string strFix =
                "}" +
                "}";//声明返回过程尾
            CompilerResults results = null;
            CSharpCodeProvider provider = new CSharpCodeProvider();//动态引用C#创建程序集
            CompilerParameters options = new CompilerParameters();
            options.GenerateInMemory = true;
            StringBuilder sb = new StringBuilder();
            sb.Append(strPre);
            sb.Append("return "+expression+";");
            sb.Append(strFix);
            results = provider.CompileAssemblyFromSource(options, sb.ToString());
            Type driverType = results.CompiledAssembly.GetType("driver");
            object o = driverType.InvokeMember("Run", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, null);
            return o;

        } 

    }
}
