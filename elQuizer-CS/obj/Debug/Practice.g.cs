﻿#pragma checksum "..\..\Practice.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F6EFE8C3A84AF6F91643A7A5B43ECA4A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace elQuizer_CS {
    
    
    /// <summary>
    /// Practice
    /// </summary>
    public partial class Practice : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid progress_grid;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock question_txt;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock question_message_txt;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid answer_grid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label question_order_txt;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mutli_txt;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Practice.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button action_btn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/elQuizer-CS;component/practice.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Practice.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.progress_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.question_txt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.question_message_txt = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.answer_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.question_order_txt = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.mutli_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.action_btn = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\Practice.xaml"
            this.action_btn.Click += new System.Windows.RoutedEventHandler(this.action_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
