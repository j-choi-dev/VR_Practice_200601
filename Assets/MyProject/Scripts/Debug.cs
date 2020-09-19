using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Choi.MyProj
{
	public static class Debug
	{
		//TODO "DUMMY"定義禁止
#if !UNITY_EDITOR && USE_RELEASE
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Assert(bool condition, string message, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Assert(bool condition, object message, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Assert(bool condition, string message) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Assert(bool condition, object message) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Assert(bool condition, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Assert(bool condition) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void AssertFormat(bool condition, string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void AssertFormat(bool condition, Object context, string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Break() { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void ClearDeveloperConsole() { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DebugBreak() { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawLine(Vector3 start, Vector3 end) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawLine(Vector3 start, Vector3 end, Color color) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawRay(Vector3 start, Vector3 dir) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void DrawRay(Vector3 start, Vector3 dir, Color color) { }

            
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Log(object message) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void Log(object message, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogAssertion(object message, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogAssertion(object message) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogAssertionFormat(Object context, string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogAssertionFormat(string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogError(object message, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogError(object message) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogErrorFormat(string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogErrorFormat(Object context, string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogException(System.Exception exception, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogException(System.Exception exception) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogFormat(Object context, string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogFormat(string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogWarning(object message) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogWarning(object message, Object context) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogWarningFormat(string format, params object[] args) { }
		[System.Diagnostics.Conditional("DUMMY")]
		public static void LogWarningFormat(Object context, string format, params object[] args) { }
#else
		public static void Assert(bool condition, string message, Object context) { UnityEngine.Debug.Assert(condition, message, context); }
		public static void Assert(bool condition, object message, Object context) { UnityEngine.Debug.Assert(condition, message, context); }
		public static void Assert(bool condition, string message) { UnityEngine.Debug.Assert(condition, message); }
		public static void Assert(bool condition, object message) { UnityEngine.Debug.Assert(condition, message); }
		public static void Assert(bool condition, Object context) { UnityEngine.Debug.Assert(condition, context); }
		public static void Assert(bool condition) { UnityEngine.Debug.Assert(condition); }
		public static void AssertFormat(bool condition, string format, params object[] args) { UnityEngine.Debug.AssertFormat(condition, format, args); }
		public static void AssertFormat(bool condition, Object context, string format, params object[] args) { UnityEngine.Debug.AssertFormat(condition, context, format, args); }
		public static void Break() { UnityEngine.Debug.Break(); }
		public static void ClearDeveloperConsole() { UnityEngine.Debug.ClearDeveloperConsole(); }
		public static void DebugBreak() { UnityEngine.Debug.DebugBreak(); }
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest) { UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest); }
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration) { UnityEngine.Debug.DrawLine(start, end, color, duration); }
		public static void DrawLine(Vector3 start, Vector3 end) { UnityEngine.Debug.DrawLine(start, end); }
		public static void DrawLine(Vector3 start, Vector3 end, Color color) { UnityEngine.Debug.DrawLine(start, end, color); }
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration) { UnityEngine.Debug.DrawRay(start, dir, color, duration); }
		public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest) { UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest); }
		public static void DrawRay(Vector3 start, Vector3 dir) { UnityEngine.Debug.DrawRay(start, dir); }
		public static void DrawRay(Vector3 start, Vector3 dir, Color color) { UnityEngine.Debug.DrawRay(start, dir, color); }


		public static void Log(object message) { UnityEngine.Debug.Log(message); }
		public static void Log(object message, Object context) { UnityEngine.Debug.Log(message, context); }
		public static void LogAssertion(object message, Object context) { UnityEngine.Debug.LogAssertion(message, context); }
		public static void LogAssertion(object message) { UnityEngine.Debug.LogAssertion(message); }
		public static void LogAssertionFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogAssertionFormat(context, format, args); }
		public static void LogAssertionFormat(string format, params object[] args) { UnityEngine.Debug.LogAssertionFormat(format, args); }
		public static void LogError(object message, Object context) { UnityEngine.Debug.LogError(message, context); }
		public static void LogError(object message) { UnityEngine.Debug.LogError(message); }
		public static void LogErrorFormat(string format, params object[] args) { UnityEngine.Debug.LogErrorFormat(format, args); }
		public static void LogErrorFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogErrorFormat(context, format, args); }
		public static void LogException(System.Exception exception, Object context) { UnityEngine.Debug.LogException(exception, context); }
		public static void LogException(System.Exception exception) { UnityEngine.Debug.LogException(exception); }
		public static void LogFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogFormat(context, format, args); }
		public static void LogFormat(string format, params object[] args) { UnityEngine.Debug.LogFormat(format, args); }
		public static void LogWarning(object message) { UnityEngine.Debug.LogWarning(message); }
		public static void LogWarning(object message, Object context) { UnityEngine.Debug.LogWarning(message, context); }
		public static void LogWarningFormat(string format, params object[] args) { UnityEngine.Debug.LogWarningFormat(format, args); }
		public static void LogWarningFormat(Object context, string format, params object[] args) { UnityEngine.Debug.LogWarningFormat(context, format, args); }
#endif
	}
}