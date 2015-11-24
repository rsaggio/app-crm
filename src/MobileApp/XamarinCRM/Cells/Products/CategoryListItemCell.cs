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

namespace XamarinCRM.Cells.Products
{
    public class CategoryListItemCell : ViewCell
    {
        public CategoryListItemCell()
        {
			StyleId = "disclosure";
            #region caregoryNameLabel
            Label caregoryNameLabel = new Label()
            { 
                TextColor = Palette._006,
                FontSize = Device.OnPlatform(
                    iOS: Device.GetNamedSize(NamedSize.Small, typeof(Label)), 
                    Android: Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    WinPhone: Device.GetNamedSize(NamedSize.Medium, typeof(Label))) * 1.2,
                YAlign = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            // The simple form of the Binding constructor.
            caregoryNameLabel.SetBinding(
                Label.TextProperty, 
                new Binding("Name"));
            #endregion

            #region categoryDescriptionLabel
            Label categoryDescriptionLabel = new Label()
            { 
                TextColor = Palette._007,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                YAlign = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            // The simple form of the Binding constructor.
            categoryDescriptionLabel.SetBinding(
                Label.TextProperty, 
                new Binding("Description"));
            #endregion

            // A ContentView, which will serve as the "top-level" of the cell's view hierarchy. 
            // It also allows a Padding to be set; something that can't be done with a plain View.
            var contentView = new ContentView();

            // set the padding of the contentView
            contentView.Padding = new Thickness(10, 10);

            // A container for the "top-level" of the cell's view hierarchy.
            RelativeLayout relativeLayout = new RelativeLayout()
            {
                //BackgroundColor = Color.Transparent
            };

            // add the companyNameLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: caregoryNameLabel, 
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => 0),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            // add the percentCopleteLabel to the relativeLayout
            relativeLayout.Children.Add(
                view: categoryDescriptionLabel,
                xConstraint: Constraint.RelativeToParent(parent => 0),
                yConstraint: Constraint.RelativeToParent(parent => (parent.Height / 2)),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            // Assign the relativeLayout to Content of contentView
            // This lets us take advantage of ContentView's padding.
            contentView.Content = relativeLayout;

            // assign contentView to the View property
            View = contentView;
        }
    }
}

