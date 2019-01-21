using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Templates
{
    public enum MailEnumTypes
    {
       Password = 1,
       InActiveWorkDefination = 2,       
    }    
    public class MailTemplates
    {
        private Dictionary<MailEnumTypes, string> _dictionary;
        public MailTemplates()
        {
            _dictionary= new Dictionary<MailEnumTypes, string>();
            //{0} - Kullanıcı E-Posta Adresi
            //{1} - Şifre
            _dictionary.Add(MailEnumTypes.Password, "</h1>Proje Yönetim Sistemi Şifre Bildirisi!</h1><p>Merhabalar ve İyi Çalışmalar,</p><p>Bu elektronik posta şifrenizi yenileme veya yeni kayıt durumunda tarafınıza gönderilir.</p><table border='1' cellpadding='8'><tr><td><b>Kullanıcı Adınız</b></td><td><b>Şifreniz</b></td></tr><tr><td>{0}</td><td>{1}</td></tr></table>");
            //{0} - Tamamlanmamış İş Adeti
            //{1} - İçerik
            _dictionary.Add(MailEnumTypes.InActiveWorkDefination, "<h1>Merhaba ve İyi Çalışmalar</h1><p>20-01-2019 Tarihinde Yaptığımız Analiz Sonucunda {0} Adet İşinizi Zamanında Tamamlamadığınızı Görmüş Bulunmaktayız.</p><p>Bu Aşağıdaki İşleri Tamamladıysanız Lütfen Sistem Üzerinde Tamamladığınızı Belirtiniz.</p><table border='1' cellpadding='8'><tr><td><b>Başlık</b></td><td><b>Bitiş Tarihi</b></td></tr>{1}</table>");
        }
        public string Get(MailEnumTypes type)
        {
            string _html = "";
            if (!_dictionary.TryGetValue(type, out _html))
                return _html;
            return _html;
        }        
    }
}
