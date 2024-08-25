using Solution.Core.Entities;

namespace Arbitrage_Calculator.Infrastructure.Email;

public interface IEmailService
{
    void SendNotificationOpportunity(Opportunity opportunity);
}
