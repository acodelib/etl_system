using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

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
            user_name = "cocasect@gmail.com";
            password = "3Ricss0n!21";
            smtp = "smtp.gmail.com";
            port = 587;
            msg_outcome = "success";
            
        }        

        //======================================  METHODS =================================================================
        public void sendEmailMessage() { }

        
        public void sendErrorEmailMessage(string to, string job_about, string error_details) {
            MailMessage mail_message = new MailMessage();
            NetworkCredential email_login = new System.Net.NetworkCredential();
            SmtpClient smtp_client = new SmtpClient();

            email_login.UserName = user_name;
            email_login.Password = password;

            mail_message.From = new MailAddress(user_name, "ETL Management System", Encoding.UTF8);
            mail_message.Subject = $"ALERT: job {job_about} has failed!";
            mail_message.Body = $"<p>This is an automated email from the ETL Management System, please do not reply.<br/> <br/> Job {job_about} has failed at {DateTime.Now} with the following error message:<br/> <br/> " + error_details + "</p>"; ;
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
        /*       

            mail_message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtp_client.SendCompleted += new SendCompletedEventHandler(failureListener);
          
        */

        private void failureListener(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {            
            if (e.Cancelled)
                this.msg_outcome = "cancelled"; ;
            if (e.Error != null)
                this.msg_outcome = "failed"; ;
        }

    }
}
