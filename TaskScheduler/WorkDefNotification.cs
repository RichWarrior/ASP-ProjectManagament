using DataAccessLayer;
using DataAccessLayer.AdoModel;
using FarukSahin.MailService;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TaskScheduler
{
    partial class WorkDefNotification : ServiceBase
    {
        private BLL bll;
        private Smtp smtp;
        private Timer TaskSchedulerTimer;
        private static readonly ILog Log =
             LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public WorkDefNotification()
        {
            InitializeComponent();
            bll = new BLL();
            this.OnStart(null);
        }

        protected async override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            Console.WriteLine(String.Format("[!]{0}-WorkDefNotification Çalıştırıldı!", DateTime.Now));
            var keys = await bll.GetServerParameter<SmtpModel>();
            var delays = await bll.GetServerParameter<DelayModel>();
            Log.Info("WorkDef Notification Çalıştı ve Propertyler Okundu!");
            if (!String.IsNullOrEmpty(keys.exceptionMessage) || !String.IsNullOrEmpty(delays.exceptionMessage))
            {
                Console.WriteLine("Propertyler Okunumadı!");
                Log.Error("Servis Propertyleri Okunurken Bir Sorun Yaşandı!");
            }
            else
            {
                smtp = new Smtp(keys.resultItem.smtp_sender, keys.resultItem.smtp_password, keys.resultItem.smtp_host, keys.resultItem.smtp_port, keys.resultItem.version);

                TaskSchedulerTimer = new Timer();
                TaskSchedulerTimer.Interval = TimeSpan.FromDays(delays.resultItem.workdef_delay).TotalMilliseconds; //Gün Cinsinden                
                TaskSchedulerTimer.Start();
                TaskSchedulerTimer.Elapsed += TaskSchedulerTimer_Elapsed;
            }
        }

        private async void TaskSchedulerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            (sender as Timer).Stop();
            var workDefs = await bll.DB_WorkDefinition().ListInCompletedAllAsync(DateTime.Now);
            if (!String.IsNullOrEmpty(workDefs.exceptionMessage))
            {
                Console.WriteLine("Propertyler Okunumadı!");
                Log.Error("Servis Propertyleri Okunurken Bir Sorun Yaşandı!");
            }
            else
            {
                var users = workDefs.resultItem.GroupBy(x => x.User_Id);
                if(users.Count()==0)
                    Console.WriteLine("Herhangi Bir Work Definition Bulunamadı!");
                else
                {
                    try
                    {
                        ///Mail İçeriği Hazırlanacak
                        var InCompletedWorkDefCount = 0;
                        var content = "";
                        var counter = 0;
                        foreach (var item in users)
                        {
                            var _props = workDefs.resultItem.Where(x => x.User_Id == item.Key);
                            InCompletedWorkDefCount = _props.Count();
                            foreach (var _child in _props)
                            {
                                content += String.Format("<tr><td>{0}</td><td>{1}</td></tr>", _child.Title, _child.ExpireDate.ToShortDateString());
                                if (counter + 1 == InCompletedWorkDefCount)
                                {
                                    smtp.AddMail("Proje Yönetim Sistemi", String.Format(bll.LIB_MailTemplates().Get(DataAccessLayer.Templates.MailEnumTypes.InActiveWorkDefination), InCompletedWorkDefCount, content));
                                    var smtpReq = await smtp.SendAsync(_child.AspNetUsers.Email);
                                    if (!smtpReq)
                                    {
                                        Log.Error("WorkDef Hesaplamaları Gönderilemiyor.");
                                    }                                    
                                    counter = 0;
                                    content = "";
                                }
                                else
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Log.Error(ex);                        
                    }
                }
                
            }
            (sender as Timer).Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
