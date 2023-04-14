using System.Threading.Tasks;

namespace Codelux.Common.Extensions;

public static class TaskExtensions
{
    /// <summary>
    /// Runs a task as fire-and-forget. Observes the task to prevent the UnobservedTaskException event from raising.
    /// </summary>
    /// <param name="task">The task to be forgotten.</param>
    /// <param name="continueOnCapturedContext">Instructs the task awaiter to attempt continuation on the captured context or not.</param>
    public static void Forget(this Task task, bool continueOnCapturedContext = true)
    {
        if (!task.IsCompleted || task.IsFaulted)
        {
            _ = ForgetAwaited(task, continueOnCapturedContext);
        }

        static async Task ForgetAwaited(Task task, bool continueOnCapturedContext)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            } catch
            {
                // Nothing to do here
            }
        }
    }
}