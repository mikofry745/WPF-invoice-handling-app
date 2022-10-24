using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsługaFaktur.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool IsExecuting { get; set; }

        public override bool CanExecute(object? parameter)
        {
            return IsExecuting == false && base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }

        public abstract Task ExecuteAsync(object? parameter);
    }
}
