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
using Xamarin.Forms;
using XamarinCRM.Statics;
using XamarinCRM.Models;

namespace XamarinCRM.Views.Products
{
    public class ProductDetailRibbonView : ContentView
    {
        readonly Product _CatalogProduct;

        public ProductDetailRibbonView(Product catalogProduct, bool isPerformingProductSelection = false)
        {
            _CatalogProduct = catalogProduct;

            RelativeLayout relativeLayout = new RelativeLayout();

            BackgroundColor = Palette._009;

            HeightRequest = 20;

            Padding = new Thickness(20, 15);

            if (isPerformingProductSelection)
            {
                Image addToOrderImage = new Image()
                {
                    Aspect = Aspect.AspectFit
                };
                Device.OnPlatform(
                    iOS: () => addToOrderImage.Source = new FileImageSource(){ File = "add_ios_blue" },     
                    Android: () => addToOrderImage.Source = new FileImageSource() { File = "add_android_blue" }
                );

                Label addToOrderTextLabel = new Label()
                {
                    Text = TextResources.Customers_Orders_EditOrder_AddToOrder.ToUpper(),
                    TextColor = Palette._004,
                    XAlign = TextAlignment.Start,
                    YAlign = TextAlignment.Center,
                };

                TapGestureRecognizer addToOrderTapGestureRecognizer = new TapGestureRecognizer()
                { 
                    NumberOfTapsRequired = 1,
                    Command = new Command(AddToOrderTapped)
                };

                addToOrderImage.GestureRecognizers.Add(addToOrderTapGestureRecognizer);
                addToOrderTextLabel.GestureRecognizers.Add(addToOrderTapGestureRecognizer);

                const double imagePaddingPercent = .10;

                relativeLayout.Children.Add(
                    view: addToOrderImage,
                    yConstraint: Constraint.RelativeToParent(parent => parent.Height * imagePaddingPercent),
                    widthConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)),
                    heightConstraint: Constraint.RelativeToParent(parent => parent.Height - (parent.Height * imagePaddingPercent * 2)));

                relativeLayout.Children.Add(
                    view: addToOrderTextLabel,
                    xConstraint: Constraint.RelativeToView(addToOrderImage, (parent, view) => view.X + view.Width + parent.Height * imagePaddingPercent),
                    widthConstraint: Constraint.RelativeToView(addToOrderImage, (parent, view) => parent.Width - view.Width),
                    heightConstraint: Constraint.RelativeToParent(parent => parent.Height)
                );
            }

            Label priceValueLabel = new Label()
            {
                Text = string.Format("{0:C}", _CatalogProduct.Price),
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                XAlign = TextAlignment.End,
                YAlign = TextAlignment.Center
            };

            relativeLayout.Children.Add(
                view: priceValueLabel, 
                xConstraint: Constraint.RelativeToParent(parent => parent.Width * .75), 
                yConstraint: Constraint.RelativeToParent(parent => 0), 
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width * .25), 
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

            Content = relativeLayout;
        }

        async void AddToOrderTapped()
        {
            MessagingCenter.Send(_CatalogProduct, MessagingServiceConstants.UPDATE_ORDER_PRODUCT);
            await Navigation.PopModalAsync();
            
        }
    }
}


