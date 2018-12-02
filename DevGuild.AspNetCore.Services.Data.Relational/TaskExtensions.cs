using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data.Relational
{
    /// <summary>
    /// Contains extensions for <see cref="Task"/> class.
    /// </summary>
    internal static class TaskExtensions
    {
        /// <summary>
        /// Continues the specified task with specified continuation and returns the resulting task.
        /// </summary>
        /// <typeparam name="T1">The type of the result of the original task.</typeparam>
        /// <typeparam name="T2">The type of the result of the continuation.</typeparam>
        /// <param name="task">The original task.</param>
        /// <param name="continuation">The continuation.</param>
        /// <returns>A task representing result of the continuation applied to the provided task.</returns>
        public static Task<T2> Then<T1, T2>(this Task<T1> task, Func<T1, T2> continuation)
        {
            var tcs = new TaskCompletionSource<T2>();
            task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.TrySetException(t.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[] { });
                    }
                    else if (t.IsCanceled)
                    {
                        tcs.TrySetCanceled();
                    }
                    else
                    {
                        tcs.TrySetResult(continuation(t.Result));
                    }
                }, TaskContinuationOptions.ExecuteSynchronously);

            return tcs.Task;
        }

        /// <summary>
        /// Continues the specified task with specified continuation and returns the resulting task.
        /// </summary>
        /// <typeparam name="T">The type of the result of the continuation.</typeparam>
        /// <param name="task">The original task.</param>
        /// <param name="continuation">The continuation.</param>
        /// <returns>A task representing result of the continuation applied to the provided task.</returns>
        public static Task<T> Then<T>(this Task task, Func<T> continuation)
        {
            var tcs = new TaskCompletionSource<T>();
            task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.TrySetException(t.Exception?.InnerExceptions ?? (IEnumerable<Exception>)new Exception[] { });
                    }
                    else if (t.IsCanceled)
                    {
                        tcs.TrySetCanceled();
                    }
                    else
                    {
                        tcs.TrySetResult(continuation());
                    }
                }, TaskContinuationOptions.ExecuteSynchronously);

            return tcs.Task;
        }
    }
}
