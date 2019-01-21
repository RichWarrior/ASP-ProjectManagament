using System.Collections.Generic;

namespace DataAccessLayer.AdoModel
{
    /// <summary>
    /// Veritabanından Dizi Şeklinde Model Alma
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModelList<T>
    {
        /// <summary>
        /// Hata Oluşursa Hatayı Handle Etme
        /// </summary>
        public string exceptionMessage { get; set; }
        public List<T> resultItem { get; set; }

        public ResultModelList()
        {
            resultItem = new List<T>();
        }
    }    
    /// <summary>
    /// Veritabanından Tekli Veri Alma
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModelSingle<T>
    {
        /// <summary>
        /// Hata Oluşursa Hatayı Handle Etme
        /// </summary>
        public string exceptionMessage { get; set; }
        public T resultItem { get; set; }
    }
    /// <summary>
    /// Ekleme,Silme,Güncelleme İşlemlerinin Durumunu Handle Etme
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// Hata Oluşursa Hatayı Handle Etme
        /// </summary>
        public string exceptionMessage { get; set; }
        public bool success { get; set; }
    }
}
