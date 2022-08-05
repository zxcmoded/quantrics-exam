using AssetService.Models;

public interface IAssetRepository
{
    ResponsePayload<Asset> Add(Asset Req);
    ResponsePayload<Asset> Update(Asset Req);
    bool Delete(int Id);
    List<Asset> FetchAssets(AssetFilter filter);
}