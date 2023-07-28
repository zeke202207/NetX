using NetX.Module;

namespace NetX.TaskScheduling;

internal class TaskSchedulingConstEnum
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    [ModuleKey]
    public const string C_TASKSCHEDULING_KEY = "10000000000000000000000000000004";

    /// <summary>
    /// swagger分组名称
    /// </summary>
    public const string C_TASKSCHEDULING_GROUPNAME = "taskscheduling";
}

public enum JobTaskState : int
{
    None =0,
    //1->Started 2->Paused 3->Resumed 4->Deleted 5->Interrupted
    Started = 1,
    Paused = 2,
    Resumed = 3,
    Deleted = 4,
    Interrupted = 5
}
