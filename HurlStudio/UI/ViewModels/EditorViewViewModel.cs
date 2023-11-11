﻿using Dock.Model.Controls;
using HurlStudio.Collections.Model.Collection;
using HurlStudio.Collections.Model.Environment;
using HurlStudio.UI.Dock;
using HurlStudio.UI.Views;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HurlStudio.UI.ViewModels
{
    public class EditorViewViewModel : ViewModelBase
    {
        private ILogger _log = null;

        private ObservableCollection<HurlCollection> _collections;
        private ObservableCollection<HurlEnvironment> _environments;
        private IRootDock? _layout;


        public EditorViewViewModel(LayoutFactory layoutFactory, ILogger<EditorViewViewModel> logger) : base(typeof(EditorView))
        {
            _collections = new ObservableCollection<HurlCollection>();
            _environments = new ObservableCollection<HurlEnvironment>();

            _log = logger;
            
        }


        public ObservableCollection<HurlCollection> Collections
        {
            get => _collections;
            set
            {
                _collections = value;
                Notify();
            }
        }

        public ObservableCollection<HurlEnvironment> Environments
        {
            get => _environments;
            set
            {
                _environments = value;
                Notify();
            }
        }

        //public IRootDock? Layout
        //{
        //    get => _layout;
        //    set
        //    {
        //        _layout = value;
        //        Notify();
        //    }
        //}
    }
}