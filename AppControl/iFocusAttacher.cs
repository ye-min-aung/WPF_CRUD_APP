﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CGMWPF.Front.AppControls
{
    public class iFocusAttacher
    {

        public static readonly DependencyProperty FocusProperty = DependencyProperty.RegisterAttached("Focus", typeof(bool), typeof(iFocusAttacher), new PropertyMetadata(false, FocusChanged));
 
        public static bool GetFocus(DependencyObject d)
        {
            return (bool)d.GetValue(FocusProperty);
        }

        public static void SetFocus(DependencyObject d, bool value)
        {
            d.SetValue(FocusProperty, value);
        }

        private static void FocusChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ((UIElement)sender).Focus();
            }
        }

    }
}
