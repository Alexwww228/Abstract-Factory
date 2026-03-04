using System;

namespace BankSystem
{
    // ======================================================
    // 1. Интерфейсы продуктов
    // ======================================================

    public interface IAccount
    {
        void Open();
        void Deposit(double amount);
        void Withdraw(double amount);
        void ShowBalance();
    }

    public interface ILoan
    {
        void ApproveLoan(double amount);
        void CalculateInterest();
    }

    public interface IBankCard
    {
        void IssueCard();
        void Pay(double amount);
        void ShowCardLimit();
    }

    // ======================================================
    // 2. Retail Family
    // ======================================================

    public class RetailAccount : IAccount
    {
        private double _balance = 0;

        public void Open() => Console.WriteLine("Retail account opened.");

        public void Deposit(double amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited {amount}$");
        }

        public void Withdraw(double amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrawn {amount}$");
            }
            else
                Console.WriteLine("Insufficient funds.");
        }

        public void ShowBalance() =>
            Console.WriteLine($"Current balance: {_balance}$");
    }

    public class RetailLoan : ILoan
    {
        private double _amount;

        public void ApproveLoan(double amount)
        {
            _amount = amount;
            Console.WriteLine($"Retail loan approved: {amount}$ at 10%");
        }

        public void CalculateInterest()
        {
            Console.WriteLine($"Total to repay: {_amount * 1.10}$");
        }
    }

    public class RetailCard : IBankCard
    {
        private double _limit = 5000;
        private double _spent = 0;

        public void IssueCard() =>
            Console.WriteLine("Retail debit card issued (Limit 5000$)");

        public void Pay(double amount)
        {
            if (_spent + amount <= _limit)
            {
                _spent += amount;
                Console.WriteLine($"Paid {amount}$ by card");
            }
            else
                Console.WriteLine("Card limit exceeded.");
        }

        public void ShowCardLimit() =>
            Console.WriteLine($"Remaining limit: {_limit - _spent}$");
    }

    // ======================================================
    // 3. Corporate Family
    // ======================================================

    public class CorporateAccount : IAccount
    {
        private double _balance = 10000;

        public void Open() =>
            Console.WriteLine("Corporate business account opened.");

        public void Deposit(double amount)
        {
            _balance += amount;
            Console.WriteLine($"Corporate deposit: {amount}$");
        }

        public void Withdraw(double amount)
        {
            _balance -= amount;
            Console.WriteLine($"Corporate withdrawal: {amount}$");
        }

        public void ShowBalance() =>
            Console.WriteLine($"Corporate balance: {_balance}$");
    }

    public class CorporateLoan : ILoan
    {
        private double _amount;

        public void ApproveLoan(double amount)
        {
            _amount = amount;
            Console.WriteLine($"Corporate loan approved: {amount}$ at 6%");
        }

        public void CalculateInterest()
        {
            Console.WriteLine($"Total to repay: {_amount * 1.06}$");
        }
    }

    public class CorporateCard : IBankCard
    {
        private double _limit = 50000;

        public void IssueCard() =>
            Console.WriteLine("Corporate credit card issued (Limit 50000$)");

        public void Pay(double amount) =>
            Console.WriteLine($"Corporate payment: {amount}$");

        public void ShowCardLimit() =>
            Console.WriteLine($"Corporate limit: {_limit}$");
    }

    // ======================================================
    // 4. VIP Family
    // ======================================================

    public class VipAccount : IAccount
    {
        private double _balance = 50000;

        public void Open() =>
            Console.WriteLine("VIP premium account opened.");

        public void Deposit(double amount)
        {
            _balance += amount;
            Console.WriteLine($"VIP deposit: {amount}$");
        }

        public void Withdraw(double amount)
        {
            _balance -= amount;
            Console.WriteLine($"VIP withdrawal: {amount}$");
        }

        public void ShowBalance() =>
            Console.WriteLine($"VIP balance: {_balance}$");
    }

    public class VipLoan : ILoan
    {
        private double _amount;

        public void ApproveLoan(double amount)
        {
            _amount = amount;
            Console.WriteLine($"VIP loan approved: {amount}$ at 3%");
        }

        public void CalculateInterest()
        {
            Console.WriteLine($"Total to repay: {_amount * 1.03}$");
        }
    }

    public class VipCard : IBankCard
    {
        private double _limit = 200000;

        public void IssueCard() =>
            Console.WriteLine("VIP Platinum card issued (Limit 200000$)");

        public void Pay(double amount) =>
            Console.WriteLine($"VIP card payment: {amount}$");

        public void ShowCardLimit() =>
            Console.WriteLine($"VIP limit: {_limit}$");
    }

    // ======================================================
    // 5. Абстрактная фабрика
    // ======================================================

    public interface IBankFactory
    {
        IAccount CreateAccount();
        ILoan CreateLoan();
        IBankCard CreateCard();
    }

    public class RetailBankFactory : IBankFactory
    {
        public IAccount CreateAccount() => new RetailAccount();
        public ILoan CreateLoan() => new RetailLoan();
        public IBankCard CreateCard() => new RetailCard();
    }

    public class CorporateBankFactory : IBankFactory
    {
        public IAccount CreateAccount() => new CorporateAccount();
        public ILoan CreateLoan() => new CorporateLoan();
        public IBankCard CreateCard() => new CorporateCard();
    }

    public class VipBankFactory : IBankFactory
    {
        public IAccount CreateAccount() => new VipAccount();
        public ILoan CreateLoan() => new VipLoan();
        public IBankCard CreateCard() => new VipCard();
    }

    // ======================================================
    // 6. Клиент
    // ======================================================

    public class BankService
    {
        private readonly IAccount _account;
        private readonly ILoan _loan;
        private readonly IBankCard _card;

        public BankService(IBankFactory factory)
        {
            _account = factory.CreateAccount();
            _loan = factory.CreateLoan();
            _card = factory.CreateCard();
        }

        public void ServeClient()
        {
            _account.Open();
            _account.Deposit(1000);
            _account.Withdraw(300);
            _account.ShowBalance();

            _loan.ApproveLoan(20000);
            _loan.CalculateInterest();

            _card.IssueCard();
            _card.Pay(500);
            _card.ShowCardLimit();

            Console.WriteLine("Service completed.\n");
        }
    }

    // ======================================================
    // 7. Main
    // ======================================================

    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter client name (or 'exit' to quit):");
                string name = Console.ReadLine();
                if (name.ToLower() == "exit") break;

                Console.WriteLine("Select client type:");
                Console.WriteLine("1 - Retail");
                Console.WriteLine("2 - Corporate");
                Console.WriteLine("3 - VIP");

                string choice = Console.ReadLine();

                IBankFactory factory = choice switch
                {
                    "1" => new RetailBankFactory(),
                    "2" => new CorporateBankFactory(),
                    "3" => new VipBankFactory(),
                    _ => throw new Exception("Invalid choice")
                };

                BankService service = new BankService(factory);

                Console.WriteLine($"\n--- Serving client: {name} ---");
                service.ServeClient();
            }

            Console.WriteLine("Program finished.");
        }
    }
}