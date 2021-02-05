using System;
using System.Threading.Tasks;
using Demo.Forms.Interfaces;
using SquareSix.Core;

namespace Demo.Forms.Services
{
    public class TestInterface : ITestInterface
    {
        private readonly IAlertService _alertService;

        public TestInterface()
        {
            _alertService = SimpleIOC.Container.Resolve<IAlertService>();
        }

        public async Task ShowTestAlertAsync()
        {
            await _alertService.ShowAlertAsync("IoC sample", "Check the viewmodel code for the example on how to use this");
        }
    }
}
