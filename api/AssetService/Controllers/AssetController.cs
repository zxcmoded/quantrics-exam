using AssetService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/{Controller}")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetRepository iAssetRepository;
        public AssetController(IAssetRepository iAssetRepository)
        {
            this.iAssetRepository = iAssetRepository;
        }

        [HttpPost]
        [Route("search")]
        public IActionResult GetAssets([FromBody] AssetFilter filter)
        {
            try
            {
                return Ok(iAssetRepository.FetchAssets(filter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddAsset([FromBody] Asset asset)
        {
            try
            {
                var result = iAssetRepository.Add(asset);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateAsset([FromBody] Asset asset)
        {
            try
            {
                var result = iAssetRepository.Update(asset);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = iAssetRepository.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}