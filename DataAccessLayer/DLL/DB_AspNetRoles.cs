using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_AspNetRoles : Base
    {
        private ResultModelList<AspNetRoles> List()
        {
            var result = new ResultModelList<AspNetRoles>();
            try
            {
                result.resultItem = _dbContext.AspNetRoles.OrderBy(x => x.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<AspNetRoles> ListSync()
        {
            return this.List();
        }
        public async Task<ResultModelList<AspNetRoles>> ListAsync()
        {
            return await Task.Run(() => this.List());
        }
        private ResultModelList<RoleMenu> ListByRoleWithMenu()
        {
            var result = new ResultModelList<RoleMenu>();
            try
            {
                result.resultItem = _dbContext.RoleMenu.Include("AspNetRoles").Include("Menu").OrderBy(x => x.Menu.Rank).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        public ResultModelList<RoleMenu> ListByRoleWithMenuSync()
        {
            return this.ListByRoleWithMenu();
        }
        public async Task<ResultModelList<RoleMenu>> ListByRoleWithMenuAsync()
        {
            return await Task.Run(() => this.ListByRoleWithMenu());
        }
    }
}
