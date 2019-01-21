using DataAccessLayer.AdoModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.DLL
{
    public class DB_Menu : Base
    {
        /// <summary>
        /// Yetkiye Göre Kullanıcının Menülerini Getirme
        /// </summary>
        /// <param name="roleId">Yetki Id</param>
        /// <returns></returns>
        private ResultModelList<Menu> ListByRoleId(string roleId)
        {
            var result = new ResultModelList<Menu>();
            try
            {
                result.resultItem = _dbContext.RoleMenu.Include("Menu")
                    .Where(x => x.Role_Id == roleId).Select(x => x.Menu).OrderBy(x => x.Rank)
                    .ToList();
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Yetkiye Göre Kullanıcının Menülerini Getirme Sync
        /// </summary>
        /// <param name="roleId">Yetki Id</param>
        /// <returns></returns>
        public ResultModelList<Menu> ListSync(string roleId)
        {
            return this.ListByRoleId(roleId);
        }
        /// <summary>
        /// Yetkiye Göre Kullanıcının Menülerini Getirme Async
        /// </summary>
        /// <param name="roleId">Yetki Id</param>
        /// <returns></returns>
        public async Task<ResultModelList<Menu>> ListByAsync(string roleId)
        {
            return await Task.Run(() => this.ListByRoleId(roleId));
        }
        /// <summary>
        /// Menülerin Hepsini Getirme
        /// Menülerin Hepsini Getirip Kaç Adet Alt Menüsü Var Gibi Veriler İşleniyor.
        /// </summary>
        private ResultModelList<Menu> ListMainMenu()
        {
            var result = new ResultModelList<Menu>();
            try
            {
                result.resultItem = _dbContext.Menu.OrderBy(x => x.Rank).ToList();                   
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Menülerin Hepsini Getirme Sync
        /// Menülerin Hepsini Getirip Kaç Adet Alt Menüsü Var Gibi Veriler İşleniyor.
        /// </summary>
        public ResultModelList<Menu> ListMainMenuSync()
        {
            return this.ListMainMenu();
        }
        /// <summary>
        /// Menülerin Hepsini Getirme Async
        /// Menülerin Hepsini Getirip Kaç Adet Alt Menüsü Var Gibi Veriler İşleniyor.
        /// </summary>
        public async Task<ResultModelList<Menu>> ListMainMenuAsync()
        {
            return await Task.Run(() => this.ListMainMenu());
        }
        /// <summary>
        /// Menü Ekleme
        /// </summary>
        /// <param name="model">Menü Modeli</param>
        /// <returns></returns>
        public async Task<ResultModel> Add(Menu model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.Menu.Add(model);
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
        /// Menü Bulma
        /// </summary>
        /// <param name="id">Menü Id</param>
        /// <returns></returns>
        private ResultModelSingle<Menu> FindById(string id)
        {
            var result = new ResultModelSingle<Menu>();
            try
            {
                result.resultItem = _dbContext.Menu.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Menü Bulma Sync
        /// </summary>
        /// <param name="id">Menü Id</param>
        /// <returns></returns>
        public ResultModelSingle<Menu> FindByIdSync(string id)
        {
            return this.FindById(id);
        }
        /// <summary>
        /// Menü Bulma Async
        /// </summary>
        /// <param name="id">Menü Id</param>
        /// <returns></returns>
        public async Task<ResultModelSingle<Menu>> FindByIdAsync(string id)
        {
            return await Task.Run(() => this.FindById(id));
        }
        /// <summary>
        /// Menü Silme
        /// </summary>
        /// <param name="id">Menü Id</param>
        /// <returns></returns>
        public async Task<ResultModel> Delete(string id)
        {
            var result = new ResultModel();
            try
            {                
                var values = _dbContext.Menu.Where(x => x.Id == id || x.Nested == id).ToList();
                if(values.Count!=0)
                {
                    _dbContext.Menu.RemoveRange(values);
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
        /// <summary>
        /// Menü Düzenleme
        /// </summary>
        /// <param name="id">Menü Id</param>
        /// <param name="newValue">Yeni Değer</param>
        /// <returns></returns>
        public async Task<ResultModel> Edit(string id,Menu newValue)
        {
            var result = new ResultModel();
            try
            {
                var oldValue = _dbContext.Menu.FirstOrDefault(x => x.Id == id);
                if (oldValue != null)
                {
                    oldValue.Name = newValue.Name;
                    oldValue.Icon = newValue.Icon;
                    oldValue.Controller = newValue.Controller;
                    oldValue.Action = newValue.Action;
                    oldValue.Rank = newValue.Rank;                    
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
        /// <summary>
        /// Alt Menüleri Getirir.
        /// </summary>
        /// <param name="id">Ana Menü Id</param>
        /// <returns></returns>
        private ResultModelList<Menu> ListSubMenu(string id)
        {
            var result = new ResultModelList<Menu>();
            try
            {
                result.resultItem = _dbContext.Menu.Where(x => x.Nested == id)
                    .OrderBy(x => x.Rank).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                result.exceptionMessage = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// Alt Menüleri Getirir. Sync
        /// </summary>
        /// <param name="id">Ana Menü Id</param>
        /// <returns></returns>
        public ResultModelList<Menu> ListSubMenuSync(string id)
        {
            return this.ListSubMenu(id);
        }
        /// <summary>
        /// Alt Menüleri Getirir. Async
        /// </summary>
        /// <param name="id">Ana Menü Id</param>
        /// <returns></returns>
        public async Task<ResultModelList<Menu>> ListSubMenuAsync(string id)
        {
            return await Task.Run(() => this.ListSubMenu(id));
        }
        /// <summary>
        /// Yetki ile Menü İlişkendirmesi Yapar
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        public async Task<ResultModel> AddPoint(RoleMenu model)
        {
            var result = new ResultModel();
            try
            {
                _dbContext.RoleMenu.Add(model);
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
        /// Yetki Erişim Kapatma
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="menu_id"></param>        
        public async Task<ResultModel> DeletePoint(string role_id,string menu_id)
        {
            var result = new ResultModel();
            try
            {
                var value = _dbContext.RoleMenu.Where(x => x.Role_Id == role_id).ToList();
                if (value.Count !=0)
                {
                    foreach (var item in value)
                    {
                        if(item.Menu_Id == menu_id)
                        {
                            _dbContext.RoleMenu.Remove(item);
                        }
                    }
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
    }
}
