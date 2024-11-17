using PlayFab;
using PlayFab.AdminModels;
using SharedTest;

namespace DomainTest;

public class HelloWorldAsync : IHelloWorldAsync
{
    public async Task<HelloWorldResult> HelloWorld(string playFabId, HelloWorldRequest contextRequest)
    {
        //
        var settings = new PlayFabApiSettings
        {
            ProductionEnvironmentUrl = PlayFabSettings.staticSettings.ProductionEnvironmentUrl,
            DeveloperSecretKey = "1D8OFW49CN9T7JXIH6Y8469GZ1YFDH1PR4NRGMCUEQ5UGNKFAM",
            TitleId = "8B362",
        };

        PlayFabAdminInstanceAPI adminInstanceApi = new PlayFabAdminInstanceAPI(settings);

        GetTitleDataRequest titleDataRequest = new GetTitleDataRequest();
        GetUserDataRequest userDataRequest = new GetUserDataRequest()
        {
            PlayFabId = playFabId
        };

        var getTitleDataAsyncTask = adminInstanceApi.GetTitleDataAsync(titleDataRequest);
        var getUserReadOnlyDataAsyncTask = adminInstanceApi.GetUserReadOnlyDataAsync(userDataRequest);

        await Task.WhenAll(getTitleDataAsyncTask, getUserReadOnlyDataAsyncTask);

        var getTitleDataAsync = await getTitleDataAsyncTask;
        var getUserReadOnlyDataAsync = await getUserReadOnlyDataAsyncTask;

        string testTitleData = string.Empty;
        string userReadOnlyData = string.Empty;
        if (getTitleDataAsync.Result.Data.TryGetValue("Test", out var testTitle))
        {
            testTitleData = testTitle;
        }

        if (getUserReadOnlyDataAsync.Result.Data.TryGetValue("Test", out var userDataRecord))
        {
            userReadOnlyData = userDataRecord.Value;
        }


        userReadOnlyData += "-" + testTitleData + " " + Guid.NewGuid();

        UpdateUserDataRequest updateUserDataRequest = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { "Test", userReadOnlyData }
            },
            PlayFabId = playFabId
        };

        var updateUserReadOnlyDataAsync = await adminInstanceApi.UpdateUserReadOnlyDataAsync(updateUserDataRequest);
        if (updateUserReadOnlyDataAsync.Error != null)
        {
            //todo if check
        }

        return new HelloWorldResult()
        {
            Message = contextRequest.Name + " " + Guid.NewGuid(),
            DataVersion = updateUserReadOnlyDataAsync.Result.DataVersion,
            UserReadOnlyData = userReadOnlyData,
        };
    }
}