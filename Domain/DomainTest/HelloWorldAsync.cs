using PlayFab;
using PlayFab.AdminModels;
using Shared;

namespace DomainTest;

public class HelloWorldAsync : IHelloWorldAsync
{
    public async Task<HelloWorldResult> HelloWorld(string playFabId, HelloWorldRequest contextRequest)
    {
        var settings = PlayFabConst.Settings;

        var adminInstanceApi = new PlayFabAdminInstanceAPI(settings);

        var titleDataRequest = new GetTitleDataRequest();
        var userDataRequest = new GetUserDataRequest()
        {
            PlayFabId = playFabId
        };

        var getTitleDataAsyncTask = adminInstanceApi.GetTitleDataAsync(titleDataRequest);
        var getUserReadOnlyDataAsyncTask = adminInstanceApi.GetUserReadOnlyDataAsync(userDataRequest);

        await Task.WhenAll(getTitleDataAsyncTask, getUserReadOnlyDataAsyncTask);

        var getTitleDataAsync = await getTitleDataAsyncTask;
        var getUserReadOnlyDataAsync = await getUserReadOnlyDataAsyncTask;

        var testTitleData = string.Empty;
        var userReadOnlyData = string.Empty;
        if (getTitleDataAsync.Result.Data.TryGetValue("Test", out var testTitle))
        {
            testTitleData = testTitle;
        }

        if (getUserReadOnlyDataAsync.Result.Data.TryGetValue("Test", out var userDataRecord))
        {
            userReadOnlyData = userDataRecord.Value;
        }


        userReadOnlyData += "-" + testTitleData + " " + Guid.NewGuid();

        var updateUserDataRequest = new UpdateUserDataRequest()
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