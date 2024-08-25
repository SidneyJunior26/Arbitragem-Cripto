using System.Net.Mail;
using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Email;

public class EmailService : IEmailService
{
    public void SendNotificationOpportunity(Opportunity opportunity)
    {
        string assunto = $"Oportunidade Localizada - {opportunity.Crypto.Symbol}";

        string corpo = @$"<!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""UTF-8"">
                        <title>$""Oportunidade Localizada - {opportunity.Crypto.Symbol}""</title>
                        <style>
                        body {{
                            font-family: Arial, sans-serif;
                        }}

                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                        }}

                        .logo {{
                            text-align: center;
                            margin-bottom: 20px;
                        }}

                        .code {{
                            font-size: 20px;
                            text-align: center;
                            margin-bottom: 20px;
                        }}

                        .instructions {{
                            margin-bottom: 20px;
                        }}

                        .button {{
                            display: inline-block;
                            background-color: #F8A824;
                            color: #000000;
                            padding: 10px 20px;
                            text-decoration: none;
                            border-radius: 4px;
                        }}

                        a {{
                            color: #000000;
                        }}
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                          <div class=""code"">
                              Moeda: <strong>{opportunity.Crypto.Symbol} - {opportunity.Crypto.Name}</strong>
                              </br>
                              Spread: <strong>{opportunity.DifferencePercentage}%</strong>
                              </br>
                              Exchange Compra: <strong>{opportunity.ExchangeToBuy.Name}</strong>
                              </br>
                              Exchange Venda: <strong>{opportunity.ExchangeToSell.Name}</strong>
                          </div>
                        </div>
                    </body>
                    </html>
                    ";

        EnviarEmail("", assunto, corpo);
    }

    private void EnviarEmail(string sender, string subject, string body)
    {
        using (SmtpClient client = new SmtpClient("smtp.gmail.com", 465))
        {
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("", "");

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("", "ArbitraX");
            mensagem.To.Add(new MailAddress(sender));
            mensagem.Subject = subject;
            mensagem.Body = body;
            mensagem.IsBodyHtml = true;

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            mensagem.AlternateViews.Add(htmlView);

            client.Send(mensagem);
        }
    }
}