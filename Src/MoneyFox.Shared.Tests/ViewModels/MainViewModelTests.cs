using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFox.Shared.DataAccess;
using MoneyFox.Shared.Interfaces;
using MoneyFox.Shared.Manager;
using MoneyFox.Shared.Repositories;
using MoneyFox.Shared.Tests.Mocks;
using MoneyFox.Shared.ViewModels;
using Moq;
using MvvmCross.Core.Platform;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Test.Core;

namespace MoneyFox.Shared.Tests.ViewModels {
    [TestClass]
    public class MainViewModelTests : MvxIoCSupportingTest {

        [TestInitialize]
        public void Init() {
            Setup();
        }

        protected MockDispatcher MockDispatcher { get; private set; }

        /// <summary>
        ///     This is needed for the navigation to work in the test.
        /// </summary>
        protected override void AdditionalSetup() {
            MockDispatcher = new MockDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(MockDispatcher);
            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
            Ioc.RegisterSingleton<IMvxMessenger>(new MvxMessengerHub());
        }

        [TestMethod]
        public void GoToAddPayment_IncomeNoEdit_CorrectParameterPassed() {           

            var mainViewModel = new MainViewModel(new Mock<IAccountRepository>().Object);

            mainViewModel.GoToAddPaymentCommand.Execute(PaymentType.Income.ToString());

            MockDispatcher.Requests.Count.ShouldBe(1);
            MockDispatcher.Requests[0].ViewModelType.ShouldBe(typeof(ModifyPaymentViewModel));
            MockDispatcher.Requests[0].ParameterValues.Count.ShouldBe(1);
            MockDispatcher.Requests[0].ParameterValues["typeString"].ShouldBe("Income");
        }

        [TestMethod]
        public void GoToAddPayment_ExpenseNoEdit_CorrectParameterPassed() {

            var mainViewModel = new MainViewModel(new Mock<IAccountRepository>().Object);

            mainViewModel.GoToAddPaymentCommand.Execute(PaymentType.Expense.ToString());

            MockDispatcher.Requests.Count.ShouldBe(1);
            MockDispatcher.Requests[0].ViewModelType.ShouldBe(typeof(ModifyPaymentViewModel));
            MockDispatcher.Requests[0].ParameterValues.Count.ShouldBe(1);
            MockDispatcher.Requests[0].ParameterValues["typeString"].ShouldBe("Expense");
        }

        [TestMethod]
        public void GoToAddPayment_TransferNoEdit_CorrectParameterPassed() {

            var mainViewModel = new MainViewModel(new Mock<IAccountRepository>().Object);

            mainViewModel.GoToAddPaymentCommand.Execute(PaymentType.Transfer.ToString());

            MockDispatcher.Requests.Count.ShouldBe(1);
            MockDispatcher.Requests[0].ViewModelType.ShouldBe(typeof(ModifyPaymentViewModel));
            MockDispatcher.Requests[0].ParameterValues.Count.ShouldBe(1);
            MockDispatcher.Requests[0].ParameterValues["typeString"].ShouldBe("Transfer");
        }
    }
}