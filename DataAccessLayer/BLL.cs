using DataAccessLayer.AdoModel;
using DataAccessLayer.DLL;
using DataAccessLayer.Templates;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Business Login Layer
    /// </summary>
    public class BLL : IDisposable
    {
        /// <summary>
        /// Database Providers
        /// </summary>
        private DB_ServerInfo _serverInfo { get; set; }
        private DB_Menu _menu { get; set; }
        private DB_AspNetUsers _aspNetUsers { get; set; }
        private DB_AspNetRoles _aspNetRoles { get; set; }
        private DB_Action _action { get; set; }
        private DB_Projects _projects { get; set; }
        private DB_WorkDefinition _workDefinition { get; set; }
        private DB_Notes _notes { get; set; }
        private DB_Statistics _statistics { get; set; }

        /// <summary>
        /// Lib Provider
        /// </summary>
        private MailTemplates _mailTemplates { get; set; }

        /// <summary>
        /// ServerInfo Tablosuna Erişim Sağlar
        /// </summary>        
        public DB_ServerInfo DB_ServerInfo()
        {
            if (_serverInfo == null)
                _serverInfo = new DB_ServerInfo();
            return _serverInfo;
        }
        /// <summary>
        /// Menü Tablosuna Erişim Sağlar
        /// </summary>
        /// <returns></returns>
        public DB_Menu DB_Menu()
        {
            if (_menu == null)
                _menu = new DB_Menu();
            return _menu;
        }
        /// <summary>
        /// AspNetUsers Tablosuna Erişim Sağlar.
        /// </summary>
        /// <returns></returns>
        public DB_AspNetUsers DB_AspNetUsers()
        {
            if (_aspNetUsers == null)
                _aspNetUsers = new DB_AspNetUsers();
            return _aspNetUsers;
        }
        /// <summary>
        /// AspNetRoles Tablosuna Erişim Sağlar
        /// </summary>
        /// <returns></returns>
        public DB_AspNetRoles DB_AspNetRoles()
        {
            if (_aspNetRoles == null)
                _aspNetRoles = new DB_AspNetRoles();
            return _aspNetRoles;
        }
        /// <summary>
        /// Action Tablosuna Erişim Sağlar
        /// </summary>        
        public DB_Action DB_Action()
        {
            if (_action == null)
                _action = new DB_Action();
            return _action;
        }
        /// <summary>
        /// Projects Tablosuna Erişim Sağlar
        /// </summary>
        /// <returns></returns>
        public DB_Projects DB_Projects()
        {
            if (_projects == null)
                _projects = new DB_Projects();
            return _projects;
        }
        /// <summary>
        /// WorkDefinition Tablousna Erişim Sağlar.
        /// </summary>
        /// <returns></returns>
        public DB_WorkDefinition DB_WorkDefinition()
        {
            if (_workDefinition == null)
                _workDefinition = new DB_WorkDefinition();
            return _workDefinition;
        }

        public DB_Notes Db_Notes()
        {
            if (_notes == null)
                _notes = new DB_Notes();
            return _notes;
        }

        public DB_Statistics DB_Statistics()
        {
            if (_statistics == null)
                _statistics = new DB_Statistics();
            return _statistics;
        }

        public MailTemplates LIB_MailTemplates()
        {
            if (_mailTemplates == null)
                _mailTemplates = new MailTemplates();
            return _mailTemplates;
        }

        /// <summary>
        /// Sunucu Parametrelerini İşleme
        /// </summary>
        /// <typeparam name="T">İşlenecek Veri Modeli</typeparam>        
        public async Task<ResultModelSingle<T>> GetServerParameter<T>()
        {
            var result = new ResultModelSingle<T>();
            try
            {
                var serverInfo = await this.DB_ServerInfo().ListAsync("Hepsi");
                var _activator = (T)Activator.CreateInstance(typeof(T));
                var _props = _activator.GetType().GetProperties();
                var a = (T)Activator.CreateInstance(typeof(T));
                foreach (var item in serverInfo.resultItem)
                {
                    foreach (var child in _props)
                    {
                        if (child.Name == item.key_str)
                        {
                            if (child.PropertyType == typeof(int))
                            {
                                child.SetValue(a, Convert.ToInt32(item.value_str), null);
                            }
                            else
                            {
                                child.SetValue(a, item.value_str, null);
                            }
                        }
                    }
                }
                result.resultItem = a;
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.Message;
            }
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
