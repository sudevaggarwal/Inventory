using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventary.Core.Domain.DB;
using Inventary.Core.Domain.VM;
using Inventary.Data.Repositaries;
using Omu.ValueInjecter;

namespace Inventary.Services.Servies
{
    public class InventaryService : IInventaryService
    {
        private readonly IInventaryRepository _inventaryRepository;
        public InventaryService(IInventaryRepository inventaryRepository)
        {
            _inventaryRepository = inventaryRepository;
        }

        public async Task<int> DeleteInventrary(int id)
        {
            return await _inventaryRepository.DeleteInventary(id);
        }

        public async Task<IEnumerable<InventaryDetailList>> GetAllInventrary()
        {
            var data = await _inventaryRepository.GetAllInventary();
            if (data != null)
                return data.Select(x => new InventaryDetailList()
                .InjectFrom(x)).Cast<InventaryDetailList>().ToList();
            else
                return null;

        }

        public async Task<SpecificInventary> GetInventrary(int id)
        {
            var data = await _inventaryRepository.GetInventary(id);
            SpecificInventary inventaryDetail = new SpecificInventary();
            if (data != null)
            {
                inventaryDetail.InjectFrom(data);
                return inventaryDetail;
            }
            else
                return null;

        }

        public async Task<int> SaveInventrary(InventaryDetail inventaryDetail)
        {
            InventaryMaster inventaryMaster = new InventaryMaster();
            inventaryMaster.InjectFrom(inventaryDetail);
            inventaryMaster.TotalPrice = inventaryMaster.Quantity * inventaryMaster.PricePerUnit;
            return await _inventaryRepository.SaveInventary(inventaryMaster);
        }

        public async Task<int> UpdateInventrary(InventaryDetail inventaryDetail, int id)
        {
            InventaryMaster inventaryMaster = new InventaryMaster();
            inventaryMaster.InjectFrom(inventaryDetail);
            inventaryMaster.TotalPrice = inventaryMaster.Quantity * inventaryMaster.PricePerUnit;
            return await _inventaryRepository.UpdateInventary(inventaryMaster, id);
        }
    }
    public interface IInventaryService
    {
        Task<IEnumerable<InventaryDetailList>> GetAllInventrary();
        Task<int> SaveInventrary(InventaryDetail emp);
        Task<SpecificInventary> GetInventrary(int id);

        Task<int> DeleteInventrary(int id);

        Task<int> UpdateInventrary(InventaryDetail emp, int id);
    }
}
