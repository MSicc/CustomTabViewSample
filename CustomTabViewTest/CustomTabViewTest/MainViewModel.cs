using SliMvvm.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms.Internals;

namespace CustomTabViewTest
{
public class MainViewModel : ObservableObject
{
    private TabViewModel _currentTabVm;

    public MainViewModel()
    {

        this.TabVms = new ObservableCollection<TabViewModel>();
        this.TabVms.Add(new TabViewModel("Short Title"));
        this.TabVms.Add(new TabViewModel("A Little Longer Title"));
        this.TabVms.Add(new TabViewModel("An Even Longer Title Than Before"));
        this.TabVms.Add(new TabViewModel("Again Short Title"));
        this.TabVms.Add(new TabViewModel("Mini Title"));
        this.TabVms.Add(new TabViewModel("Different Title"));

        this.CurrentTabVm = this.TabVms.FirstOrDefault();
    }


    public ObservableCollection<TabViewModel> TabVms { get; set; } 

    public TabViewModel CurrentTabVm 
    { 
        get => _currentTabVm;
        set
        {
            Set(ref _currentTabVm, value);
            SetSelection();
        }
    }

    private void SetSelection()
    {
        this.TabVms.ForEach(vm => vm.IsSelected = false);
        this.CurrentTabVm.IsSelected = true;
    }
}
}
