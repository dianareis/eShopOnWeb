using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.ApplicationCore.Constants;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class TypeController : BaseApiController
    {
        private readonly IAsyncRepository<CatalogType> _typeRepository;

        public TypeController(IAsyncRepository<CatalogType> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogType>> ListCatalogType()
        {
            var types = await _typeRepository.ListAllAsync();
            return Ok(types);
        }
        

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogType>> GetTypeById(int id)
        {
            try
            {
                var typeId = await _typeRepository.GetByIdAsync(id);
                return Ok(typeId);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        [HttpPost("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CatalogType>> UpdateCatalogType(int id, string newType)
        {
            var types = _typeRepository.ListAllAsync();
            //Get existing CatalogType
            try {
                var existingCatalogType = await _typeRepository.GetByIdAsync(id);
                //Build updated CatalogItem
                var updatedCatalogType = existingCatalogType;
                updatedCatalogType.Type = newType;
                await _typeRepository.UpdateAsync(updatedCatalogType);
                return Ok();
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        [HttpDelete("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<ActionResult<CatalogType>> DeleteCatalogType(int id)
        {
            try {
                var existingCatalogType = await _typeRepository.GetByIdAsync(id);

                await _typeRepository.DeleteAsync(existingCatalogType);
                return Ok();
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CatalogType>> AddCatalogType(string type)
        {
            var newCatalogType = new CatalogType();

            var catalogTypes = await _typeRepository.ListAllAsync();
            var lastId = catalogTypes.OrderBy(x => x.Id).LastOrDefault();

            newCatalogType.Id = lastId.Id + 1;
            newCatalogType.Type = type;

            await _typeRepository.AddAsync(newCatalogType);
            return Ok();
        }
    }
}
