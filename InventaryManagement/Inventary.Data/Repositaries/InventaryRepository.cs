using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Inventary.Core.Domain.DB;

namespace Inventary.Data.Repositaries
{
    public class InventaryRepository : IInventaryRepository
    {
        private readonly IDBConnectionRepository _dBConnectionRepository;
        DynamicParameters parameters;
        public InventaryRepository(IDBConnectionRepository dBConnectionRepository)
        {
            _dBConnectionRepository = dBConnectionRepository;
        }

        public async Task<int> DeleteInventary(int id)
        {
            IDbTransaction trans = null;
            IDbConnection conn = _dBConnectionRepository.GetConnection;

            try
            {
                trans = conn.BeginTransaction();
                parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var data = await conn.ExecuteAsync("usp_Delete_Inventary",
                    parameters, trans, null, commandType:
                    CommandType.StoredProcedure);
                if (data == 1)
                {
                    trans.Commit();
                    conn.Close();
                    return data;
                }
                else
                {
                    trans.Rollback();
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        public async Task<IEnumerable<InventaryMaster>> GetAllInventary()
        {
            IDbConnection conn = _dBConnectionRepository.GetConnection;
            try
            {
                return await conn.QueryAsync<InventaryMaster>("usp_Get_Inventaries");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<InventaryMaster> GetInventary(int id)
        {
            IDbConnection conn = _dBConnectionRepository.GetConnection;
            try
            {
                parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await conn.QueryFirstOrDefaultAsync<InventaryMaster>("usp_Get_Inventary", parameters,null,null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveInventary(InventaryMaster inventaryMaster)
        {
            IDbTransaction trans = null;
            IDbConnection conn = _dBConnectionRepository.GetConnection;
            try
            {
                trans = conn.BeginTransaction();
                parameters = new DynamicParameters();
                parameters.Add("@inventaryName", inventaryMaster.Name);
                parameters.Add("@inventaryDescription",
                    inventaryMaster.Description);
                parameters.Add("@inventaryQuantity", inventaryMaster.Quantity);
                parameters.Add("@inventaryPricePerUnit",
                    inventaryMaster.PricePerUnit);
                parameters.Add("@inventaryTotalPrice",
                    inventaryMaster.TotalPrice);
                parameters.Add("@inventaryCreatedOn",
                    inventaryMaster.CreatedOn);
                var data = await conn.ExecuteAsync("usp_Save_Inventary",
                    parameters, trans, null, commandType:
                    CommandType.StoredProcedure);
                if (data == 1)
                {
                    trans.Commit();
                    conn.Close();
                    return data;
                }
                else
                {
                    trans.Rollback();
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }

        }

        public async Task<int> UpdateInventary(InventaryMaster inventaryMaster, int id)
        {
            IDbTransaction trans = null;
            IDbConnection conn = _dBConnectionRepository.GetConnection;
            try
            {
                trans = conn.BeginTransaction();
                parameters = new DynamicParameters();
                parameters.Add("@id", id);
                parameters.Add("@inventaryName", inventaryMaster.Name);
                parameters.Add("@inventaryDescription",
                    inventaryMaster.Description);
                parameters.Add("@inventaryQuantity", inventaryMaster.Quantity);
                parameters.Add("@inventaryPricePerUnit",
                    inventaryMaster.PricePerUnit);
                parameters.Add("@inventaryTotalPrice",
                    inventaryMaster.TotalPrice);
                parameters.Add("@inventaryCreatedOn",
                    inventaryMaster.CreatedOn);
                var data = await conn.ExecuteAsync("usp_Update_Inventary",
                     parameters, trans, null, commandType:
                     CommandType.StoredProcedure);
                if (data == 1)
                {
                    trans.Commit();
                    conn.Close();
                    return data;
                }
                else
                {
                    trans.Rollback();
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }
    }
    public interface IInventaryRepository
    {
        Task<IEnumerable<InventaryMaster>> GetAllInventary();
        Task<int> SaveInventary(InventaryMaster inventaryMaster);
        Task<InventaryMaster> GetInventary(int id);
        Task<int> DeleteInventary(int id);
        Task<int> UpdateInventary(InventaryMaster inventaryMaster, int id);
    }
}
