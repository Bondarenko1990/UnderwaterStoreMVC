using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Domain.Interfaces;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Domain.Core;
using System.Net.Mail;
using System.Net;

namespace WebApplication1.Infrastructure.Data
{
    public class EmailSettings
    {
        public string MailToAddress = "bond-sergey.ua@rambler.ru";
        public string MailFromAddress = "bondsergeyua@gmail.com";
        public bool UseSsl = true;
        public string Username = "bondsergeyua@gmail.com";
        public string Password = "************";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\Programming\Projects\WebApplication1";
    }


        public class EmailOrderProcessor : IOrderProcessor
        {
            private EmailSettings emailSettings;

            public EmailOrderProcessor(EmailSettings settings)
            {
                emailSettings = settings;
            }

            public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = emailSettings.UseSsl;
                    smtpClient.Host = emailSettings.ServerName;
                    smtpClient.Port = emailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = true;

                    smtpClient.Credentials
                        = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                    //if (emailSettings.WriteAsFile)
                    //{
                    //    smtpClient.DeliveryMethod
                    //        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    //    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    //    smtpClient.EnableSsl = false;
                    //}

                    StringBuilder body = new StringBuilder()
                        .AppendLine("Новый заказ обработан")
                        .AppendLine("---")
                        .AppendLine("Товары:");

                    foreach (var line in cart.Lines)
                    {
                        var subtotal = line.Product.Price * line.Quantity;
                        body.AppendFormat("{0} x {1} (итого: {2} грн)",
                            line.Quantity, line.Product.Name, subtotal);
                    }

                    body.AppendFormat("Общая стоимость: {0} грн", cart.ComputeTotalValue())
                        .AppendLine("---")
                        .AppendLine("Доставка:")
                        .AppendLine(shippingInfo.Name)
                        .AppendLine(shippingInfo.Line1)
                        .AppendLine(shippingInfo.Line2 ?? "")
                        .AppendLine(shippingInfo.Line3 ?? "")
                        .AppendLine(shippingInfo.City)
                        .AppendLine(shippingInfo.Country)
                        .AppendLine("---")
                        .AppendFormat("Подарочная упаковка: {0}",
                            shippingInfo.GiftWrap ? "Да" : "Нет");

                    MailMessage mailMessage = new MailMessage(
                                           emailSettings.MailFromAddress,
                                           emailSettings.MailToAddress,
                                           "Новый заказ отправлен!",
                                           body.ToString());

                    if (emailSettings.WriteAsFile)
                    {
                        mailMessage.BodyEncoding = Encoding.UTF8;
                        mailMessage.HeadersEncoding = Encoding.UTF8;
                    }

                    smtpClient.Send(mailMessage);
                }
            }
        }
  }

