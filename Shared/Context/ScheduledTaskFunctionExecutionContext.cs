// Models via Scheduled task
public class PlayStreamEventHistory
{
    public required string ParentTriggerId { get; set; }
    public required string ParentEventId { get; set; }
    public bool TriggeredEvents { get; set; }
}

public class ScheduledTaskFunctionExecutionContext<T>
{
    public required PlayFab.CloudScriptModels.NameIdentifier ScheduledTaskNameId { get; set; }
    public required Stack<PlayStreamEventHistory> EventHistory { get; set; }
    public required TitleAuthenticationContext TitleAuthenticationContext { get; set; }
    public bool? GeneratePlayStreamEvent { get; set; }
    public required T FunctionArgument { get; set; }
}

public class ScheduledTaskFunctionExecutionContext : ScheduledTaskFunctionExecutionContext<object>
{
}