using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DocumentsDesktopUI.EventModels;

namespace DocumentsDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVm;
        private SimpleContainer _container;

        public ShellViewModel(IEventAggregator events, SalesViewModel salesVm,
            SimpleContainer conatiner)
        {
            _events = events;
            _salesVm = salesVm;
            _container = conatiner;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVm);
        }
    }
}
