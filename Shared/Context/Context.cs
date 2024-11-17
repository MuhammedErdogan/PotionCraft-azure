using Newtonsoft.Json;
using PlayFab.ProfilesModels;

public class Context<T>(FunctionExecutionContext<T> functionExecutionContext) : IContext<T>
{
    public T Request => functionExecutionContext.FunctionArgument;
    
    public string PlayFabId  =>  functionExecutionContext.CallerEntityProfile.Lineage.MasterPlayerAccountId;

    
    public List<string> ExperimentVariants => functionExecutionContext.CallerEntityProfile.ExperimentVariants;

    public Dictionary<string, EntityStatisticValue> Statistics => functionExecutionContext.CallerEntityProfile.Statistics ?? new Dictionary<string, EntityStatisticValue>();
    public Dictionary<string, EntityDataObject> Objects => functionExecutionContext.CallerEntityProfile.Objects ?? new Dictionary<string, EntityDataObject>();
    public Dictionary<string, EntityProfileFileMetadata> Files => functionExecutionContext.CallerEntityProfile.Files ?? new Dictionary<string, EntityProfileFileMetadata>();


    public TitleAuthenticationContext TitleAuthenticationContext => functionExecutionContext.TitleAuthenticationContext;


    public override string ToString()
    {
        return JsonConvert.SerializeObject(functionExecutionContext);
    }
}