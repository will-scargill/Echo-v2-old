﻿#pragma checksum "..\..\..\screenSettings.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "52615D763757831A137433BCA1098F8FA9404798396714BFE439F871E63894F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GuiTest1;
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


namespace EMessenger {
    
    
    /// <summary>
    /// screenSettings
    /// </summary>
    public partial class screenSettings : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbSettingsServers;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSettingsName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSettingsIP;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSettingsPort;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSettingsNewServ;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSettingsSaveServ;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSettingsDelServ;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSettingsExit;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSettingsServName;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSettingsServIP;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSettingsServPort;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\screenSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboSettingsClrSch;
        
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
            System.Uri resourceLocater = new System.Uri("/EMessenger;component/screensettings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\screenSettings.xaml"
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
            this.lbSettingsServers = ((System.Windows.Controls.ListBox)(target));
            
            #line 12 "..\..\..\screenSettings.xaml"
            this.lbSettingsServers.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lbSettingsServers_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbSettingsName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbSettingsIP = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbSettingsPort = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.bSettingsNewServ = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\screenSettings.xaml"
            this.bSettingsNewServ.Click += new System.Windows.RoutedEventHandler(this.bSettingsNewServ_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bSettingsSaveServ = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\screenSettings.xaml"
            this.bSettingsSaveServ.Click += new System.Windows.RoutedEventHandler(this.bSettingsSaveServ_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bSettingsDelServ = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\screenSettings.xaml"
            this.bSettingsDelServ.Click += new System.Windows.RoutedEventHandler(this.bSettingsDelServ_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bSettingsExit = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\screenSettings.xaml"
            this.bSettingsExit.Click += new System.Windows.RoutedEventHandler(this.button_Copy2_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lSettingsServName = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.lSettingsServIP = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.lSettingsServPort = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.cboSettingsClrSch = ((System.Windows.Controls.ComboBox)(target));
            
            #line 23 "..\..\..\screenSettings.xaml"
            this.cboSettingsClrSch.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboSettingsClrSch_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
