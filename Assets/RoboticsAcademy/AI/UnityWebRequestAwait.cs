using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequestAwait : INotifyCompletion
{
	private UnityWebRequestAsyncOperation asyncOp;
	private Action continuation;

	public UnityWebRequestAwait(UnityWebRequestAsyncOperation asyncOp)
	{
		this.asyncOp = asyncOp;
		asyncOp.completed += OnRequestCompleted;
	}

	public bool IsCompleted { get { return asyncOp.isDone; } }

	public void GetResult() { }

	public void OnCompleted(Action continuation)
	{
		this.continuation = continuation;
	}

	private void OnRequestCompleted(AsyncOperation obj)
	{
		continuation();
	}
}

public static class ExtensionMethods
{
	public static UnityWebRequestAwait GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
	{
		return new UnityWebRequestAwait(asyncOp);
	}
}