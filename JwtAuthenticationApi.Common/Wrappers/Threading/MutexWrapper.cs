﻿namespace JwtAuthenticationApi.Common.Wrappers.Threading
{
    using System.Diagnostics.CodeAnalysis;
    using JwtAuthenticationApi.Common.Abstraction.Wrappers.Threading;

    /// <summary>
    /// <see cref="Mutex"/> wrapper.
    /// </summary>
    [ExcludeFromCodeCoverage]
	internal class MutexWrapper: IMutexWrapper
	{
		private readonly Mutex _mutex;

		/// <summary>
		/// Initializes new instance of <see cref="MutexWrapper"/> with basic mutex options like initially owned mutex and name.
		/// </summary>
		public MutexWrapper(bool initiallyOwned, string name)
		{
			_mutex = new Mutex(initiallyOwned, name);
		}

		public void WaitOne()
		{
			_mutex.WaitOne();
		}

		public void ReleaseMutex()
		{
			_mutex.ReleaseMutex();
		}

		public void Dispose()
		{
			_mutex?.Dispose();
		}
	}
}