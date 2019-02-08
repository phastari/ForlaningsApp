using Prism.Commands;
using System;

namespace FiefApp
{
    public class ShellViewModel
    {
        public ShellViewModel()
        {
            OnShellLoaded = new DelegateCommand(ExecuteOnShellLoaded);
        }

        public DelegateCommand OnShellLoaded { get; set; }
        private void ExecuteOnShellLoaded()
        {
            Console.WriteLine("Working!");
        }
    }
}
