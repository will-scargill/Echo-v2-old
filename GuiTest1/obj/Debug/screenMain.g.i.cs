﻿#pragma checksum "..\..\screenMain.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E50C8F0A8C25F1E839A71E7374CE0062EB434288"
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


namespace GuiTest1 {
    
    
    /// <summary>
    /// screenMain
    /// </summary>
    public partial class screenMain : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbMainMessages;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbMainMessageEntry;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbMainChannels;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbMainChannelMembers;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblMainChannels;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblMainChannelMembers;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\screenMain.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMainSendMsg;
        
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
            System.Uri resourceLocater = new System.Uri("/GuiTest1;component/screenmain.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\screenMain.xaml"
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
            this.lbMainMessages = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.tbMainMessageEntry = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.lbMainChannels = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.lbMainChannelMembers = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.lblMainChannels = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblMainChannelMembers = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.btnMainSendMsg = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\screenMain.xaml"
            this.btnMainSendMsg.Click += new System.Windows.RoutedEventHandler(this.btnMainSendMsg_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

