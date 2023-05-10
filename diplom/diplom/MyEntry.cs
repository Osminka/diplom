using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace diplom
{
    public class MyEntry:Entry
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius", typeof(int), typeof(MyEntry), 0);

        public int EntryCornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly BindableProperty BorderColorProperty =
           BindableProperty.Create("CornerRadius", typeof(int), typeof(MyEntry), 0);

        public int EntryBorder
        {
            get { return (int)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

    }
}
