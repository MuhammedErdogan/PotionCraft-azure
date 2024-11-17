// Models via Entity PlayStream event, entering or leaving an 
// entity segment or as part of an entity segment based scheduled task.

using System;
public class EventFullName
{
    public required string Name { get; set; }
    public required string Namespace { get; set; }
}

public class OriginInfo
{
    public required string Id { get; set; }
    public DateTime? Timestamp { get; set; }
}

public class EntityPlayStreamEvent<T>
{
    public required string SchemaVersion { get; set; }
    public required EventFullName FullName { get; set; }
    public required string Id { get; set; }
    public DateTime Timestamp { get; set; }
    public required PlayFab.CloudScriptModels.EntityKey Entity { get; set; }
    public required PlayFab.CloudScriptModels.EntityKey Originator { get; set; }
    public required OriginInfo OriginInfo { get; set; }
    public required T Payload { get; set; }
    public required PlayFab.ProfilesModels.EntityLineage EntityLineage { get; set; }
}

public class EntityPlayStreamEvent : EntityPlayStreamEvent<object>
{
}

public class EntityPlayStreamFunctionExecutionContext<TPayload, TArg>
{
    public required PlayFab.ProfilesModels.EntityProfileBody CallerEntityProfile { get; set; }
    public required EntityPlayStreamEvent<TPayload> PlayStreamEvent { get; set; }
    public required TitleAuthenticationContext TitleAuthenticationContext { get; set; }
    public bool? GeneratePlayStreamEvent { get; set; }
    public required TArg FunctionArgument { get; set; }
}

public class EntityPlayStreamFunctionExecutionContext : EntityPlayStreamFunctionExecutionContext<object, object>
{
}