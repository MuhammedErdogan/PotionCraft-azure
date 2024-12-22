namespace DomainTest;

using PlayFab;
using PlayFab.AdminModels;
using Shared;
using Newtonsoft.Json;

public class ItemStore
{
    public async Task<PurchaseItemResult> PurchaseItem(string playFabId, PurchaseItemRequest contextRequest)
    {
        var settings = PlayFabConst.Settings;

        var adminInstanceApi = new PlayFabAdminInstanceAPI(settings);

        var titleDataRequest = new GetTitleDataRequest();
        var userDataRequest = new GetUserDataRequest()
        {
            PlayFabId = playFabId
        };

        var userInventoryRequest = new GetUserInventoryRequest()
        {
            PlayFabId = playFabId
        };

        var getTitleDataAsyncTask = adminInstanceApi.GetTitleDataAsync(titleDataRequest);
        var getUserInventoryAsync = adminInstanceApi.GetUserInventoryAsync(userInventoryRequest);

        await Task.WhenAll(getTitleDataAsyncTask, getUserInventoryAsync);

        var titleData = await getTitleDataAsyncTask;
        var userInventory = await getUserInventoryAsync;

        var titleItemStore = string.Empty;

        if (titleData.Result.Data.TryGetValue(PlayFabConst.TITLE_STORE, out var titleItemStoreData))
        {
            titleItemStore = titleItemStoreData;
        }

        var itemList = JsonConvert.DeserializeObject<List<Shared.StoreItem>>(titleItemStore);
        var item = itemList?.FirstOrDefault(x => x.ItemId == contextRequest.ItemId);

        if (item == null)
        {
            //todo if check
        }

        var request = new PurchaseItemRequest
        {
            ItemId = contextRequest.ItemId,
        };

        foreach (var price in item.Prices)
        {
            var currencyId = price.Key;
            var requiredAmount = price.Value;

            var userCurrency = userInventory.Result.VirtualCurrency
                .FirstOrDefault(c => c.Key == currencyId).Value;

            if (userCurrency < requiredAmount)
            {
                throw new Exception($"Yetersiz {currencyId}. Gerekli: {requiredAmount}, Sahip olunan: {userCurrency}");
            }
        }

        // Kullanıcının parasını düş
        foreach (var price in item.Prices)
        {
            await adminInstanceApi.SubtractUserVirtualCurrencyAsync(new SubtractUserVirtualCurrencyRequest
            {
                PlayFabId = playFabId,
                VirtualCurrency = price.Key,
                Amount = price.Value
            });
        }

        // Envantere ekle
        var grantItemsRequest = new PlayFab.AdminModels.GrantItemsToUsersRequest()
        {
            //PlayFabId = playFabId,
            //ItemIds = new List<string> { contextRequest.ItemId },
            //CatalogVersion = PlayFabConst.CATALOG_VERSION
        };

        var grantResult = await adminInstanceApi.GrantItemsToUsersAsync(grantItemsRequest);

        if (grantResult.Result.ItemGrantResults == null || !grantResult.Result.ItemGrantResults.Any())
        {
            throw new Exception("Ürün eklenirken hata oluştu.");
        }

        // Başarılı işlem sonucu döndür
        return new PurchaseItemResult()
        {
            Message = $"{contextRequest.ItemId} satın alındı.",
            UserReadOnlyData = "Satın alma tamamlandı.",
        };
    }
}