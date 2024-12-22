using PlayFab;

namespace DomainTest;

public static class PlayFabConst
{
    public static readonly PlayFabApiSettings Settings = new()
    {
        ProductionEnvironmentUrl = PlayFabSettings.staticSettings.ProductionEnvironmentUrl,
        DeveloperSecretKey = "1D8OFW49CN9T7JXIH6Y8469GZ1YFDH1PR4NRGMCUEQ5UGNKFAM",
        TitleId = "8B362",
    };

    public const string TITLE_STORE = "ItemStore";
}