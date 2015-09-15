using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Test.Core;
using MoneyManager.Core.DataAccess;
using MoneyManager.Core.Manager;
using MoneyManager.Core.Repositories;
using MoneyManager.Core.ViewModels;
using MoneyManager.Foundation;
using MoneyManager.Foundation.OperationContracts;
using Moq;
using Xunit;

namespace MoneyManager.Core.Tests.ViewModels
{
    class MainViewModelTests : MvxIoCSupportingTest
    {
        [Theory]
        [InlineData("Income", TransactionType.Income)]
        [InlineData("Spending", TransactionType.Spending)]
        [InlineData("Transfer", TransactionType.Transfer)]
        public void GoToAddTransaction_Transactiontype_CorrectPreparation(string typestring, TransactionType type)
        {
            Setup();
            // for navigation parsing
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());

            var dbHelper = new Mock<IDbHelper>().Object;
            var accountRepository = new AccountRepository(new AccountDataAccess(dbHelper));
            var transactionRepository = new TransactionRepository(new TransactionDataAccess(dbHelper), new RecurringTransactionDataAccess(dbHelper));
            var settings = new SettingDataAccess();
            var transactionManager = new TransactionManager(transactionRepository, accountRepository, new Mock<IDialogService>().Object);

            var defaultManager = new DefaultManager(accountRepository, new SettingDataAccess());

            var modifyTransactionViewModel =
                new ModifyTransactionViewModel(transactionRepository,
                    accountRepository,
                    new Mock<IDialogService>().Object,
                    transactionManager,
                    defaultManager);

            var modifyAccountViewModel = new ModifyAccountViewModel(accountRepository,
                new BalanceViewModel(accountRepository, new Mock<ITransactionRepository>().Object, settings));

            var mainViewModel = new MainViewModel(modifyAccountViewModel, modifyTransactionViewModel);

            mainViewModel.GoToAddTransactionCommand.Execute(typestring);

            Assert.False(modifyTransactionViewModel.IsEdit);
            Assert.True(modifyTransactionViewModel.IsEndless);
            if (type == TransactionType.Transfer)
            {
                Assert.True(modifyTransactionViewModel.IsTransfer);
            }
            else
            {
                Assert.False(modifyTransactionViewModel.IsTransfer);
            }
            Assert.Equal((int)type, modifyTransactionViewModel.SelectedTransaction.Type);
        }
    }
}