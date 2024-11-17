// Models via Player PlayStream event, entering or leaving a 
// player segment or as part of a player segment based scheduled task.
public class PlayerPlayStreamFunctionExecutionContext<T>
{
    public required PlayFab.CloudScriptModels.PlayerProfileModel PlayerProfile { get; set; }
    public bool PlayerProfileTruncated { get; set; }
    public required PlayFab.CloudScriptModels.PlayStreamEventEnvelopeModel PlayStreamEventEnvelope { get; set; }
    public required TitleAuthenticationContext TitleAuthenticationContext { get; set; }
    public bool? GeneratePlayStreamEvent { get; set; }
    public required T FunctionArgument { get; set; }
}

public class PlayerPlayStreamFunctionExecutionContext : PlayerPlayStreamFunctionExecutionContext<object>
{
}