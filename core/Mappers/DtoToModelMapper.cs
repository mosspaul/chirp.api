using core.DTOs.SimpleFinDTOs;
using core.DTOs.UserDtos;
using core.Mappers.Interfaces;
using data.Models;

namespace core.Mappers;

public class DtoToModelMapper : IDtoToModelMapper
{
    public User ProfileToUser(ProfileDto profile)
    {
        return new User()
        {
            UserName = profile.Username,
            PasswordHash = null,
            Email = profile.Email,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            DateCreated = null,
            Id = profile.UserId
        };
    }

    public User SignUpToUser(SignUpDto signUp)
    {
        return new User()
        {
            UserName = signUp.Username,
            PasswordHash = signUp.Password,
            Email = signUp.Email,
            FirstName = signUp.FirstName,
            LastName = signUp.LastName,
            DateCreated = DateTime.UtcNow,
            SimpleFinAccessUrl = null,
        };
    }
    public List<Connection> AccountSetToModels(AccountSetDto accountSetDto, string userId)
    {
        var final = new List<Connection>();
        foreach (var connectionDto in accountSetDto.Connections)
        {
            final.Add(new Connection()
            {
                Id = 0,
                Name = connectionDto.Name,
                UserId = userId,
                MxId = connectionDto.OrgId,
                Url = connectionDto.OrgUrl,
                SimpleFinConnId = connectionDto.ConnId,
                Accounts = AccountDtoToAccount([.. accountSetDto.Accounts.Where(acc => acc.ConnId == connectionDto.ConnId)])
            });
        }
        return final;
    }
    private List<Account> AccountDtoToAccount(List<AccountDto> accountDtos)
    {
        var final = new List<Account>();
        foreach (var accountDto in accountDtos)
        {
            final.Add(new Account()
            {
                Id = 0,
                Name = accountDto.Name,
                Currency = accountDto.Currency,
                Balance = double.Parse(accountDto.Balance),
                AvailableBalance = double.Parse(accountDto.AvailableBalance),
                BalanceDate = DateUtility.ConvertTimestampToDate(accountDto.BalanceDate),
                SimpleFinConnId = accountDto.ConnId,
                SFinId = accountDto.Id,
                Holdings = HoldingDtoToHolding(accountDto.Holdings),
                Transactions = TransactionDtoToTransaction(accountDto.Transactions),

            });
        }
        return final;
    }
    private List<Holding> HoldingDtoToHolding(List<HoldingDto> holdingDtos)
    {
        var final = new List<Holding> ();
        foreach (var holdingDto in holdingDtos)
        {
            final.Add(new Holding()
            {
                Shares = double.Parse(holdingDto.Shares),
                CostBasis = double.Parse(holdingDto.CostBasis),
                PurchasePrice = double.Parse(holdingDto.PurchasePrice),
                MarketValue = double.Parse(holdingDto.MarketValue),
                Description = holdingDto.Description,
                Currency = holdingDto.Currency,
                Symbol = holdingDto.Symbol,
                SFinId = holdingDto.Id,
                CreatedAt = DateUtility.ConvertTimestampToDate(holdingDto.Created)
            });
        }
        return final;
    }
    private List<Transaction> TransactionDtoToTransaction(List<TransactionDto> transactionDtos)
    {
        var final = new List<Transaction> ();
        foreach (var transDto in transactionDtos)
        {
            final.Add(new Transaction()
            {
                Posted = DateUtility.ConvertTimestampToDate(transDto.Posted),
                TransactedAt = DateUtility.ConvertTimestampToDate(transDto.TransactedAt),
                Description = transDto.Description,
                SFinId = transDto.Id,
                Memo = transDto.Memo,
                Payee = transDto.Payee,
                Amount = double.Parse(transDto.Amount),
            });
        }
        return final;
    }
}