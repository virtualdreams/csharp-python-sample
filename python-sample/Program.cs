using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IronPython.Hosting;
using Helper;
using Microsoft.Scripting.Hosting;
using System.Reflection;

namespace python_sample
{
	class Program
	{
		static void Main(string[] args)
		{
			BasicScriptHost host = new BasicScriptHost();
			host.Execute();
		}
	}

	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal IId { get; set; }
		static public User operator *(User user1, User user2)
		{
			return new User { Id = user1.Id * user2.Id };
		}
	}

	public class BasicScriptHost
	{
		private ScriptEngine _engine = null;

		public BasicScriptHost()
		{	
			_engine = Python.CreateEngine();

			var searchPaths = _engine.GetSearchPaths();
			searchPaths.Add(IOHelper.ConvertToFullPath("./scripts/"));
			_engine.SetSearchPaths(searchPaths);
		}

		public void Execute()
		{
			//init
			ScriptScope _scope = _engine.CreateScope();
			ScriptSource _source = _engine.CreateScriptSourceFromFile(IOHelper.ConvertToFullPath("./scripts/script.py"));
			
			//export User class to python
			_engine.Runtime.LoadAssembly(typeof(User).Assembly);

			//export member function
			_scope.SetVariable("delegate", new _Delegate(this.Delegate));
			_scope.SetVariable("trigger", new Action<string, string>(this.Trigger));

			//execute the script
			_source.Execute(_scope);

			//get all available names
			var vn = _scope.GetVariableNames();
			
			//get variables from scope if needed
			var v1 = _scope.GetVariable("mul");
			var v2 = _scope.GetVariable("Example");
			var v3 = _scope.GetVariable("var");

			//useful stuff
			//var a1 = _engine.Operations.GetCallSignatures(v3);
			//var a2 = _engine.Operations.GetMemberNames(v3);
			var a1 = _engine.Operations.IsCallable(v1);
			var a2 = _engine.Operations.IsCallable(v2);
			var a3 = _engine.Operations.IsCallable(v3);

			//create instance from variable
			var i = _engine.Operations.CreateInstance(v2);

			//prepare
			User user = new User { Id = 100, Name = ".NET", IId = 0 };

			//call via invoke
			var r1 = _engine.Operations.InvokeMember(i, "Multiply", user) as User;
			var r2 = _engine.Operations.InvokeMember(i, "Create") as User;

			//call via delegate
			Func<User, User> Multiply;
			Multiply = _engine.Operations.GetMember<Func<User, User>>(i, "Multiply");
			User r3 = Multiply(user);

			Func<User, User> Mul;
			Mul = _scope.GetVariable<Func<User, User>>("mul");
			User r4 = Mul(r3);

		}

		//member function to export
		delegate string _Delegate(string str, string expression);
		public string Delegate(string str, string expression)
		{
			return str;
		}

		public void Trigger(string str1, string str2)
		{

		}
	}
}
