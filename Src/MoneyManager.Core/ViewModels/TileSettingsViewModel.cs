﻿using Cirrious.MvvmCross.ViewModels;
using MoneyManager.Foundation.OperationContracts.Shotcuts;

namespace MoneyManager.Core.ViewModels
{
    /// <summary>
    ///     Provides the information for the TileSettingsView
    /// </summary>
    public class TileSettingsViewModel : BaseViewModel
    {
        private readonly IIncomeShortcut incomeShortcut;
        private readonly ISpendingShortcut spendingShortcut;
        private readonly ITransferShortcut transferShortcut;

        /// <summary>
        ///     Creates a TileSettingsViewModel object
        /// </summary>
        public TileSettingsViewModel(ISpendingShortcut spendingShortcut, IIncomeShortcut incomeShortcut,
            ITransferShortcut transferShortcut)
        {
            this.spendingShortcut = spendingShortcut;
            this.incomeShortcut = incomeShortcut;
            this.transferShortcut = transferShortcut;
        }

        public bool ShowInfoOnMainTile { get; set; }

        /// <summary>
        ///     Creates a Spending Shortcut
        /// </summary>
        public MvxCommand CreateSpendingShortcutCommand => new MvxCommand(CreateSpendingShortcut);

        /// <summary>
        ///     Creates an Income Shortcut
        /// </summary>
        public MvxCommand CreateIncomeShortcutCommand => new MvxCommand(CreateIncomeShortcut);

        /// <summary>
        ///     Creates an Transfer Shortcut
        /// </summary>
        public MvxCommand CreateTransferShortcutCommand => new MvxCommand(CreateTransferShortcut);

        /// <summary>
        ///     Indicates if there exists a spending shortcut
        /// </summary>
        public bool IsSpendingShortcutPinned => spendingShortcut.IsShortcutExisting;

        /// <summary>
        ///     Indicates if there is a income shortcut
        /// </summary>
        public bool IsIncomeShortcutPinned => incomeShortcut.IsShortcutExisting;

        /// <summary>
        ///     Indicates if there is a transfer shortcut
        /// </summary>
        public bool IsTransferShortcutPinned => transferShortcut.IsShortcutExisting;

        /// <summary>
        ///     Removes the existing Spending Shortcut
        /// </summary>
        public MvxCommand RemoveSpendingShortcutCommand => new MvxCommand(RemoveSpendingShortcut);

        /// <summary>
        ///     Removes the existing Income Shortcut
        /// </summary>
        public MvxCommand RemoveIncomeShortcutCommand => new MvxCommand(RemoveIncomeShortcut);

        /// <summary>
        ///     Removes the existing Transfer Shortcut
        /// </summary>
        public MvxCommand RemoveTransferShortcutCommand => new MvxCommand(RemoveTransferShortcut);

        private async void CreateSpendingShortcut()
        {
            await spendingShortcut.CreateShortCut();
            RaisePropertyChanged(nameof(IsSpendingShortcutPinned));
        }

        private async void CreateIncomeShortcut()
        {
            await incomeShortcut.CreateShortCut();
            RaisePropertyChanged(nameof(IsIncomeShortcutPinned));
        }

        private async void CreateTransferShortcut()
        {
            await transferShortcut.CreateShortCut();
            RaisePropertyChanged(nameof(IsTransferShortcutPinned));
        }

        private async void RemoveSpendingShortcut()
        {
            await spendingShortcut.RemoveShortcut();
            RaisePropertyChanged(nameof(IsSpendingShortcutPinned));
        }

        private async void RemoveIncomeShortcut()
        {
            await incomeShortcut.RemoveShortcut();
            RaisePropertyChanged(nameof(IsIncomeShortcutPinned));
        }

        private async void RemoveTransferShortcut()
        {
            await transferShortcut.RemoveShortcut();
            RaisePropertyChanged(nameof(IsTransferShortcutPinned));
        }
    }
}