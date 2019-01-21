using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    /// <summary>
    /// ServerInfo Tablosu
    /// </summary>
    public class DB_ServerInfo : Base
    {
        /// <summary>
        /// Parametre Listeleme
        /// </summary>
        private ResultModelList<ServerInfo> List(string count)
        {
            var result = new ResultModelList<ServerInfo>();
            try
            {
                if (count == "Hepsi")
                {
                    result.resultItem = _dbContext.ServerInfo
                    .OrderBy(x => x.key_str).ToList();
                }
                else
                {
                    result.resultItem = _dbContext.ServerInfo
                    .OrderBy(x => x.key_str).Take(Convert.ToInt32(count))
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Parametre Listeleme Sync
        /// </summary>        
        public ResultModelList<ServerInfo> ListSync(string count)
        {
            return this.List(count);
        }
        /// <summary>
        /// Parametre Listeleme Async
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModelList<ServerInfo>> ListAsync(string count)
        {
            return await Task.Run(() => this.List(count));
        }
        /// <summary>
        /// Yeni Parametre Ekleme
        /// </summary>
        /// <param name="model">Veri Modeli</param>        
        public async Task<ResultModel> Add(ServerInfo model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.ServerInfo.Add(model);
                await _dbContext.SaveChangesAsync();
                result.success = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Parametre Bulma
        /// </summary>
        /// <param name="id">Parametre Id</param>
        /// <returns></returns>
        private ResultModelSingle<ServerInfo> FindById(string id)
        {
            var result = new ResultModelSingle<ServerInfo>();
            try
            {
                result.resultItem = _dbContext.ServerInfo.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Parametre Bulma
        /// </summary>
        /// <param name="id">Parametre Id</param>
        /// <returns></returns>
        public ResultModelSingle<ServerInfo> FindByIdSync(string id)
        {
            return this.FindById(id);
        }
        /// <summary>
        /// Parametre Bulma
        /// </summary>
        /// <param name="id">Parametre Id</param>
        /// <returns></returns>
        public async Task<ResultModelSingle<ServerInfo>> FindByIdAsync(string id)
        {
            return await Task.Run(() => this.FindById(id));
        }
        /// <summary>
        /// Parametre Id
        /// </summary>
        /// <param name="id">Parametre Id</param>
        /// <returns></returns>
        public async Task<ResultModel> Delete(string id)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.ServerInfo.FirstOrDefault(x => x.Id == id);
                if (value != null)
                {
                    _dbContext.ServerInfo.Remove(value);
                    await _dbContext.SaveChangesAsync();
                    result.success = true;
                }
                else
                    result.success = false;
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Parametre Düzenleme
        /// </summary>
        /// <param name="id">Parametre Id</param>
        /// <param name="model">Yeni Model</param>
        /// <returns></returns>
        public async Task<ResultModel> Edit(string id, ServerInfo model)
        {
            var result = new ResultModel();
            try
            {
                var oldValue = _dbContext.ServerInfo.FirstOrDefault(x => x.Id == id);
                if (oldValue != null)
                {
                    oldValue.key_str = model.key_str;
                    oldValue.value_str = model.value_str;
                    await _dbContext.SaveChangesAsync();
                    result.success = true;
                }
                else
                    result.success = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Version Güncelleme
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel> UpdateVersion()
        {
            var result = new ResultModel();
            try
            {
                var version = _dbContext.ServerInfo.FirstOrDefault(x => x.key_str == "version");
                if (version != null)
                {
                    version.value_str = ((Convert.ToInt32(version.value_str) + 1)).ToString();
                    await _dbContext.SaveChangesAsync();
                    result.success = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        //public async Task<ResultModel> SetServiceStatus()
        //{
        //    var result = new ResultModel();
        //    try
        //    {
        //        var value = _dbContext.ServerInfo.FirstOrDefault(x => x.key_str == "service_status");
        //        if (value != null)
        //        {
        //            value.value_str = value.value_str == "not_running" ? "runnig" : "not_running";
        //            await _dbContext.SaveChangesAsync();
        //            result.success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex);
        //        result.exceptionMessage = ex.Message;                
        //    }
        //    return result;
        //}
    }
}
