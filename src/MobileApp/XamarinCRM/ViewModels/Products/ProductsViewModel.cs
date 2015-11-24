﻿// The MIT License (MIT)
// 
// Copyright (c) 2015 Xamarin
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System.Collections.ObjectModel;
using XamarinCRM.Clients;
using XamarinCRM.ViewModels.Base;
using Xamarin.Forms;
using XamarinCRM.Models;

namespace XamarinCRM.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        readonly IDataClient _DataClient;

        string _CategoryId;
        public string CategoryId
        {
            get { return _CategoryId; }
            set 
            {
                _CategoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                OnPropertyChanged("Products");
            }
        }

        public bool NeedsRefresh { get; set; }

        public ProductsViewModel(string categoryId = null)
        {
            _CategoryId = categoryId;

            _Products = new ObservableCollection<Product>();

            _DataClient = DependencyService.Get<IDataClient>();

        }

        Command _LoadProductsCommand;

        /// <summary>
        /// Command to load accounts
        /// </summary>
        public Command LoadProductsCommand
        {
            get
            {
                return _LoadProductsCommand ??
                    (_LoadProductsCommand = new Command(ExecuteLoadProductsCommand));
            }
        }

        async void ExecuteLoadProductsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadProductsCommand.ChangeCanExecute();

            Products = new ObservableCollection<Product>((await _DataClient.GetProductsAsync(_CategoryId)));

            IsBusy = false;
            LoadProductsCommand.ChangeCanExecute();
        }
    }
}

