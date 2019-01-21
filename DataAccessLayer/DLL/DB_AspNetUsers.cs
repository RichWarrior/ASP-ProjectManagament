using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_AspNetUsers : Base
    {
        /// <summary>
        /// Kullanıcıları Listeler
        /// </summary>
        /// <param name="args">Kullanıcı Sayısı</param>
        /// <returns></returns>
        private ResultModelList<AspNetUsers> List(string args)
        {
            var result = new ResultModelList<AspNetUsers>();
            try
            {
                if (args == "Hepsi")
                {
                    result.resultItem = _dbContext.AspNetUsers.Include("AspNetRoles").ToList();
                }
                else
                {
                    result.resultItem = _dbContext.AspNetUsers.Include("AspNetRoles").Take(Convert.ToInt32(args))
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
        /// Kullanıcıları Listeler Sync
        /// </summary>
        /// <param name="args">Kullanıcı Sayısı</param>
        /// <returns></returns>
        public ResultModelList<AspNetUsers> ListSync(string args)
        {
            return this.List(args);
        }
        /// <summary>
        /// Kullanıcıları Listeler Async
        /// </summary>
        /// <param name="args">Kullanıcı Sayısı</param>
        /// <returns></returns>
        public async Task<ResultModelList<AspNetUsers>> ListAsync(string args)
        {
            return await Task.Run(() => this.List(args));
        }
        private ResultModelSingle<AspNetUsers> FindById(string id)
        {
            var result = new ResultModelSingle<AspNetUsers>();
            try
            {
                result.resultItem = result.resultItem = _dbContext.AspNetUsers.Include("AspNetRoles").FirstOrDefault(x=>x.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelSingle<AspNetUsers>FindByIdSync(string id)
        {
            return this.FindById(id);
        }
        public async Task<ResultModelSingle<AspNetUsers>> FindByIdAsync(string id)
        {
            return await Task.Run(() => this.FindById(id));
        }
    }
}
