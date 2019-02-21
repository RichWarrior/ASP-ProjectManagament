using DataAccessLayer;
using DataAccessLayer.AdoModel;
using DataAccessLayer.Templates;
using FarukSahin.MailService;
using log4net;
using System;
using System.ServiceProcess;
using System.Timers;

namespace TaskScheduler
{
    partial class PasswordNotification : ServiceBase
    {
        /// <summary>
        /// Tanımlama
        /// </summary>
        private BLL bll;
        private Smtp smtp;
        private Timer MailScheduler;
        private Timer CompletedScheduler;
        private static readonly ILog Log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public PasswordNotification()
        {
            InitializeComponent();
            bll = new BLL();
            this.OnStart(null);
        }

        protected async override void OnStart(string[] args)
        {
            /* TODO: Sunucu Mail Bilgileri Çekilecek ve Şifresini Unutan veya Sisteme
             * Yöneticiler Tarafından Eklenen Kullanıcılara Şifreleri Mail Olarak Gönderilecek
             * */
            
            Console.WriteLine(String.Format("[!]{0}-PasswordNotification Çalıştırıldı!", DateTime.Now));
            var smtpInfo = await bll.GetServerParameter<SmtpModel>();
            var delays = await bll.GetServerParameter<DelayModel>();
            Log.Info("Password Notification Çalıştı ve Propertyler Okundu!");
            if (!String.IsNullOrEmpty(smtpInfo.exceptionMessage) || !String.IsNullOrEmpty(delays.exceptionMessage))
            {
                Console.WriteLine("Propertyler Okunamadı");
                Log.Error("Servis Propertyleri Okunurken Bir Sorun Yaşandı!");
            }
            else
            {
                smtp = new Smtp(smtpInfo.resultItem.smtp_sender, smtpInfo.resultItem.smtp_password,
                    smtpInfo.resultItem.smtp_host, smtpInfo.resultItem.smtp_port, smtpInfo.resultItem.version);

                MailScheduler = new Timer();
                MailScheduler.Interval = TimeSpan.FromMinutes(delays.resultItem.smtp_delay).TotalMilliseconds; //Dakika Cinsinden
                MailScheduler.Start();

                /*Şifresi Gönderilen Kullanıcıların Kayıtlarını
                 * Güvenlik Sebebiyle Temzileyecek     
                 * */
                CompletedScheduler = new Timer();
                CompletedScheduler.Interval = TimeSpan.FromMinutes(2).TotalMilliseconds;
                CompletedScheduler.Start();


                MailScheduler.Elapsed += MailScheduler_Elapsed;
                CompletedScheduler.Elapsed += CompletedScheduler_Elapsed;
            }
            //var serviceStatus = await bll.DB_ServerInfo().SetServiceStatus();
            //if (!serviceStatus.success)
            //    Log.Error("Servis Durumu Güncellenemedi!");
            //else
            //    Console.WriteLine(String.Format("[!]{0}-Servis Durumu Güncellendi!",DateTime.Now));
        }

        private async void CompletedScheduler_Elapsed(object sender, ElapsedEventArgs e)
        {
            (sender as Timer).Stop();
            var request = await bll.DB_Action().DeleteCompletedAction();
            if (request.success)
            {
                Console.WriteLine(String.Format("[!]{0}-Action Tablosu Temizlendi!",DateTime.Now));
            }
            else
            {
                if (!String.IsNullOrEmpty(request.exceptionMessage))
                {
                    Log.Error("Action Tablosu Temizlenemedi!");
                }
            }
            (sender as Timer).Start();
        }

        private async void MailScheduler_Elapsed(object sender, ElapsedEventArgs e)
        {
            /*Parametre Versiyonlaması Yapılmalı.
             * Kullanıcılara Belirli Periyotlarla Şifreleri Elektronik Posta Yoluyla
             * Gönderilecek.
             * */
            (sender as Timer).Stop();
            try
            {
                var users = await bll.DB_Action().ListAsync();
                if (!String.IsNullOrEmpty(users.exceptionMessage))
                {
                    Console.WriteLine("MailScheduler Okunamadı");
                    Log.Error("Servis MailScheduler Okunurken Bir Sorun Yaşandı!");
                }
                else
                {
                    Console.WriteLine(String.Format("[!]{0}-{1} Kişiye Mail Gönderilecek!", DateTime.Now, users.resultItem.Count));
                    foreach (var item in users.resultItem)
                    {
                        smtp.AddMail("Proje Yönetim Sistemi", String.Format(bll.LIB_MailTemplates().Get(MailEnumTypes.Password), item.Email,item.Message));
                        var success = await smtp.SendAsync(item.Email);
                        if (success)
                        {
                            Console.WriteLine(String.Format("[!]{0},{1} Kişisine Mail Gönderildi!", DateTime.Now, item.Email));
                        }
                        else
                        {
                            Console.WriteLine(String.Format("[!]{0}-{1} Kişisine Mail Gönderilemedi!", DateTime.Now, item.Email));
                            Log.Error("Mail Gönderilemedi!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("[X]{0}-{1}", DateTime.Now, ex));
                Log.Error(ex);
            }
            (sender as Timer).Start();
        }

        protected async override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Log.Info("Servis Durduruldu!");
            //var serviceStatus = await bll.DB_ServerInfo().SetServiceStatus();
            //if (!serviceStatus.success)
            //    Log.Error("Servis Durumu Güncellenemedi!");
            base.OnStop();
        }
    }
}
