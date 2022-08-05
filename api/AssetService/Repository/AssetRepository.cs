using AssetService.Models;

public class AssetRepository : IAssetRepository
{
    private readonly examContext db;
    public AssetRepository(examContext db)
    {
        this.db = db;
    }
    public ResponsePayload<Asset> Add(Asset Req)
    {
        var isAssetNameExist = db.Assets.Any(z => z.Name == Req.Name);
        if (isAssetNameExist)
        {
            return new ResponsePayload<Asset>(false, "Name already Exist", new Asset());
        }
        else
        {
            db.Assets.Add(Req);
            db.SaveChanges();
            return new ResponsePayload<Asset>(true, $"Asset {Req.Name} added successfully", Req);
        }
    }

    public bool Delete(int Id)
    {
        Asset asset = db.Assets.Find(Id);
        db.Assets.Remove(asset);
        var result = db.SaveChanges();

        return result > 0;
    }

    public List<Asset> FetchAssets(AssetFilter filter)
    {
        IQueryable<Asset> baseQuery = db.Assets;

        if (!String.IsNullOrEmpty(filter.Name))
        {
            baseQuery = baseQuery.Where(z => z.Name.Contains(filter.Name));
        }

        if (filter.Price.HasValue)
        {
            baseQuery = baseQuery.Where(z => z.Price == filter.Price.Value);
        }

        // if valid from and valid to filter has value
        if (filter.ValidFrom.HasValue && filter.ValidTo.HasValue)
        {
            baseQuery = baseQuery.Where(z => z.ValidFrom.Value.Date >= filter.ValidFrom.Value.Date && z.ValidTo.Value.Date <= filter.ValidTo.Value.Date);
        }
        else
        {
            if (filter.ValidFrom.HasValue)
            {
                baseQuery = baseQuery.Where(z => z.ValidFrom.Value.Date >= filter.ValidFrom.Value.Date);
            }
            else if (filter.ValidTo.HasValue)
            {
                baseQuery = baseQuery.Where(z => z.ValidTo.Value.Date <= filter.ValidTo.Value.Date);
            }
        }

        return baseQuery.ToList();
    }

    public ResponsePayload<Asset> Update(Asset Req)
    {
        var isAssetNameExist = db.Assets.Any(z => z.Name == Req.Name && z.Id != Req.Id);
        if (isAssetNameExist)
        {
            return new ResponsePayload<Asset>(false, "Name already Exist", new Asset());
        }
        else
        {
            var asset = db.Assets.Find(Req.Id);
            asset.Name = Req.Name;
            asset.Price = Req.Price;
            asset.ValidFrom = Req.ValidFrom;
            asset.ValidTo = Req.ValidTo;
            db.SaveChanges();

            return new ResponsePayload<Asset>(true, $"Asset {Req.Name} updated successfully", asset);
        }
    }
}
