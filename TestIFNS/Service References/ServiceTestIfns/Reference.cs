﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestIFNSTools.ServiceTestIfns {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BakcupJurnal", Namespace="http://schemas.datacontract.org/2004/07/TestIFNSLibary.Xml.XmlDS")]
    [System.SerializableAttribute()]
    public partial class BakcupJurnal : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.DateTime dateFieldField;
        
        private bool dateFieldSpecifiedField;
        
        private bool statusFieldField;
        
        private bool statusFieldSpecifiedField;
        
        private string textFieldField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime dateField {
            get {
                return this.dateFieldField;
            }
            set {
                if ((this.dateFieldField.Equals(value) != true)) {
                    this.dateFieldField = value;
                    this.RaisePropertyChanged("dateField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool dateFieldSpecified {
            get {
                return this.dateFieldSpecifiedField;
            }
            set {
                if ((this.dateFieldSpecifiedField.Equals(value) != true)) {
                    this.dateFieldSpecifiedField = value;
                    this.RaisePropertyChanged("dateFieldSpecified");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool statusField {
            get {
                return this.statusFieldField;
            }
            set {
                if ((this.statusFieldField.Equals(value) != true)) {
                    this.statusFieldField = value;
                    this.RaisePropertyChanged("statusField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool statusFieldSpecified {
            get {
                return this.statusFieldSpecifiedField;
            }
            set {
                if ((this.statusFieldSpecifiedField.Equals(value) != true)) {
                    this.statusFieldSpecifiedField = value;
                    this.RaisePropertyChanged("statusFieldSpecified");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string textField {
            get {
                return this.textFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.textFieldField, value) != true)) {
                    this.textFieldField = value;
                    this.RaisePropertyChanged("textField");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Parametr", Namespace="http://schemas.datacontract.org/2004/07/TestIFNSLibary.PathJurnalAndUse")]
    [System.SerializableAttribute()]
    public partial class Parametr : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HoursField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MinutesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TestDBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkDBField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Hours {
            get {
                return this.HoursField;
            }
            set {
                if ((this.HoursField.Equals(value) != true)) {
                    this.HoursField = value;
                    this.RaisePropertyChanged("Hours");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Minutes {
            get {
                return this.MinutesField;
            }
            set {
                if ((this.MinutesField.Equals(value) != true)) {
                    this.MinutesField = value;
                    this.RaisePropertyChanged("Minutes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TestDB {
            get {
                return this.TestDBField;
            }
            set {
                if ((object.ReferenceEquals(this.TestDBField, value) != true)) {
                    this.TestDBField = value;
                    this.RaisePropertyChanged("TestDB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkDB {
            get {
                return this.WorkDBField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkDBField, value) != true)) {
                    this.WorkDBField = value;
                    this.RaisePropertyChanged("WorkDB");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceTestIfns.IReaderCommandDbf")]
    public interface IReaderCommandDbf {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/SqlFl", ReplyAction="http://tempuri.org/IReaderCommandDbf/SqlFlResponse")]
        System.Data.DataSet SqlFl(string command, string conectionstring, System.Data.DataSet datasetreport, int i);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/SqlFl", ReplyAction="http://tempuri.org/IReaderCommandDbf/SqlFlResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> SqlFlAsync(string command, string conectionstring, System.Data.DataSet datasetreport, int i);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/SqlUl", ReplyAction="http://tempuri.org/IReaderCommandDbf/SqlUlResponse")]
        System.Data.DataSet SqlUl(string inn, string god, string command, string conectionstring, System.Data.DataSet datasetreport, int i);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/SqlUl", ReplyAction="http://tempuri.org/IReaderCommandDbf/SqlUlResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> SqlUlAsync(string inn, string god, string command, string conectionstring, System.Data.DataSet datasetreport, int i);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/IsActive", ReplyAction="http://tempuri.org/IReaderCommandDbf/IsActiveResponse")]
        bool IsActive();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/IsActive", ReplyAction="http://tempuri.org/IReaderCommandDbf/IsActiveResponse")]
        System.Threading.Tasks.Task<bool> IsActiveAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/Jurnal", ReplyAction="http://tempuri.org/IReaderCommandDbf/JurnalResponse")]
        TestIFNSTools.ServiceTestIfns.BakcupJurnal[] Jurnal();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/Jurnal", ReplyAction="http://tempuri.org/IReaderCommandDbf/JurnalResponse")]
        System.Threading.Tasks.Task<TestIFNSTools.ServiceTestIfns.BakcupJurnal[]> JurnalAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/DateBakcup", ReplyAction="http://tempuri.org/IReaderCommandDbf/DateBakcupResponse")]
        System.DateTime DateBakcup();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/DateBakcup", ReplyAction="http://tempuri.org/IReaderCommandDbf/DateBakcupResponse")]
        System.Threading.Tasks.Task<System.DateTime> DateBakcupAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IReaderCommandDbf/FileBakcup")]
        void FileBakcup();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IReaderCommandDbf/FileBakcup")]
        System.Threading.Tasks.Task FileBakcupAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/Config", ReplyAction="http://tempuri.org/IReaderCommandDbf/ConfigResponse")]
        TestIFNSTools.ServiceTestIfns.Parametr Config();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReaderCommandDbf/Config", ReplyAction="http://tempuri.org/IReaderCommandDbf/ConfigResponse")]
        System.Threading.Tasks.Task<TestIFNSTools.ServiceTestIfns.Parametr> ConfigAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IReaderCommandDbf/SaveSeting")]
        void SaveSeting(string testDb, string workDb, int hours, int minutes);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IReaderCommandDbf/SaveSeting")]
        System.Threading.Tasks.Task SaveSetingAsync(string testDb, string workDb, int hours, int minutes);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IReaderCommandDbfChannel : TestIFNSTools.ServiceTestIfns.IReaderCommandDbf, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReaderCommandDbfClient : System.ServiceModel.ClientBase<TestIFNSTools.ServiceTestIfns.IReaderCommandDbf>, TestIFNSTools.ServiceTestIfns.IReaderCommandDbf {
        
        public ReaderCommandDbfClient() {
        }
        
        public ReaderCommandDbfClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReaderCommandDbfClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReaderCommandDbfClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReaderCommandDbfClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet SqlFl(string command, string conectionstring, System.Data.DataSet datasetreport, int i) {
            return base.Channel.SqlFl(command, conectionstring, datasetreport, i);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> SqlFlAsync(string command, string conectionstring, System.Data.DataSet datasetreport, int i) {
            return base.Channel.SqlFlAsync(command, conectionstring, datasetreport, i);
        }
        
        public System.Data.DataSet SqlUl(string inn, string god, string command, string conectionstring, System.Data.DataSet datasetreport, int i) {
            return base.Channel.SqlUl(inn, god, command, conectionstring, datasetreport, i);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> SqlUlAsync(string inn, string god, string command, string conectionstring, System.Data.DataSet datasetreport, int i) {
            return base.Channel.SqlUlAsync(inn, god, command, conectionstring, datasetreport, i);
        }
        
        public bool IsActive() {
            return base.Channel.IsActive();
        }
        
        public System.Threading.Tasks.Task<bool> IsActiveAsync() {
            return base.Channel.IsActiveAsync();
        }
        
        public TestIFNSTools.ServiceTestIfns.BakcupJurnal[] Jurnal() {
            return base.Channel.Jurnal();
        }
        
        public System.Threading.Tasks.Task<TestIFNSTools.ServiceTestIfns.BakcupJurnal[]> JurnalAsync() {
            return base.Channel.JurnalAsync();
        }
        
        public System.DateTime DateBakcup() {
            return base.Channel.DateBakcup();
        }
        
        public System.Threading.Tasks.Task<System.DateTime> DateBakcupAsync() {
            return base.Channel.DateBakcupAsync();
        }
        
        public void FileBakcup() {
            base.Channel.FileBakcup();
        }
        
        public System.Threading.Tasks.Task FileBakcupAsync() {
            return base.Channel.FileBakcupAsync();
        }
        
        public TestIFNSTools.ServiceTestIfns.Parametr Config() {
            return base.Channel.Config();
        }
        
        public System.Threading.Tasks.Task<TestIFNSTools.ServiceTestIfns.Parametr> ConfigAsync() {
            return base.Channel.ConfigAsync();
        }
        
        public void SaveSeting(string testDb, string workDb, int hours, int minutes) {
            base.Channel.SaveSeting(testDb, workDb, hours, minutes);
        }
        
        public System.Threading.Tasks.Task SaveSetingAsync(string testDb, string workDb, int hours, int minutes) {
            return base.Channel.SaveSetingAsync(testDb, workDb, hours, minutes);
        }
    }
}
