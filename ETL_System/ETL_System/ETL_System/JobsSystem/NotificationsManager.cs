using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ETL_System
{
    public class NotificationsManager  {

        //======================================  FIELDS ==================================================================
        string user_name;
        string password;
        string smtp;
        int port;
        string msg_outcome;

        //======================================  CONSTRUCTOR =============================================================
        public NotificationsManager() {
            user_name = "andrei.gurguta.system@gmail.com";
            password = "Dublin22";
            smtp = "smtp.gmail.com";
            port = 587;
            msg_outcome = "success";
            
        }        

        //======================================  METHODS =================================================================
        public void sendEmailMessage() { }

        public void sendRecoveryEmailMessage(string to, string job_about) {
            NetworkCredential email_login = new System.Net.NetworkCredential();
            MailMessage mail_message = new MailMessage();
            SmtpClient smtp_client = new SmtpClient();

            email_login.UserName = user_name;
            email_login.Password = password;

            mail_message.From = new MailAddress(user_name, "ETL Management System", Encoding.UTF8);
            mail_message.Subject = $"Job {job_about} has recovered.";
            mail_message.Body = $"<p>This is an automated email from the ETL Management System, please do not reply.<br/> <br/> Job {job_about} has has recovered after a previous failure.<br/> <br/> </p>"; 
            mail_message.IsBodyHtml = true;
            mail_message.To.Add(new MailAddress(to));
            mail_message.Priority = MailPriority.High;
            mail_message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            smtp_client.Host = this.smtp;
            smtp_client.EnableSsl = true;
            smtp_client.UseDefaultCredentials = true;
            smtp_client.Credentials = email_login;
            smtp_client.Port = this.port;
            smtp_client.SendCompleted += new SendCompletedEventHandler(failureListener);
            smtp_client.SendAsync(mail_message, new object());
        }


        public void sendErrorEmailMessage(string to, string job_about, string error) {
            NetworkCredential email_login   = new System.Net.NetworkCredential();
            MailMessage mail_message        = new MailMessage();
            SmtpClient smtp_client          = new SmtpClient();            

            email_login.UserName = user_name;
            email_login.Password = password;

            mail_message.From = new MailAddress(user_name, "ETL Management System", Encoding.UTF8);
            mail_message.Subject = $"ALERT: job {job_about} has failed!";
            mail_message.IsBodyHtml = true;
            mail_message.Body = $"<p>This is an automated email from the ETL Management System, please do not reply.<br/> <br/> Job {job_about} has failed at {DateTime.Now} with the following error message:<br/> <br/> {error} </p>";                     
            mail_message.To.Add(new MailAddress(to));
            mail_message.Priority = MailPriority.High;
            mail_message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            smtp_client.Host = this.smtp;
            smtp_client.EnableSsl = true;
            smtp_client.UseDefaultCredentials = true;
            smtp_client.Credentials = email_login;
            smtp_client.Port = this.port;
            smtp_client.SendCompleted += new SendCompletedEventHandler(failureListener);
            smtp_client.SendAsync(mail_message, new object());         
        }
        /*       

            mail_message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtp_client.SendCompleted += new SendCompletedEventHandler(failureListener);
          
        */

        private void failureListener(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
            if (e.Cancelled)            {
                this.msg_outcome = "cancelled";
                Console.WriteLine(e.Cancelled.ToString());
            }
            if (e.Error != null) {
                this.msg_outcome = "failed";
                Console.WriteLine(e.Error.ToString());
            }
        }

    }
}
