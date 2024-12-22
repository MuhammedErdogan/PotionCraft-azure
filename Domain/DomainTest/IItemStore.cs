using Shared;

namespace DomainTest;

public interface IItemStore
{
    public Task<PurchaseItemResult> PurchaseItem(string playFabId, PurchaseItemRequest contextRequest);
}