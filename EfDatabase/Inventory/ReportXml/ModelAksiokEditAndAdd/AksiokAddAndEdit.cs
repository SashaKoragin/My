﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Этот исходный код был создан с помощью xsd, версия=4.7.2046.0.
// 
namespace EfDatabase.ModelAksiok.ModelAksiokEditAndAdd {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class AksiokAddAndEdit {
        
        private ParametersModel parametersModelField;
        
        private KitsEquipment kitsEquipmentField;
        
        private ParametersRequestAksiok parametersRequestAksiokField;
        
        private UploadFileAksiok uploadFileAksiokField;
        
        /// <remarks/>
        public ParametersModel ParametersModel {
            get {
                return this.parametersModelField;
            }
            set {
                this.parametersModelField = value;
            }
        }
        
        /// <remarks/>
        public KitsEquipment KitsEquipment {
            get {
                return this.kitsEquipmentField;
            }
            set {
                this.kitsEquipmentField = value;
            }
        }
        
        /// <remarks/>
        public ParametersRequestAksiok ParametersRequestAksiok {
            get {
                return this.parametersRequestAksiokField;
            }
            set {
                this.parametersRequestAksiokField = value;
            }
        }
        
        /// <remarks/>
        public UploadFileAksiok UploadFileAksiok {
            get {
                return this.uploadFileAksiokField;
            }
            set {
                this.uploadFileAksiokField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class ParametersModel {
        
        private int idFullCategoriaField;
        
        private int idStateField;
        
        private int idStateStoField;
        
        private int idExpertiseField;
        
        private string modelRequestField;
        
        private string serNumberField;
        
        private string inventoryNumField;
        
        private int codeErrorField;
        
        private string errorServerField;
        
        private int yearOfIssueField;
        
        private int exploitationStartYearField;
        
        private string loginUserField;
        
        private string passwordField;
        
        public ParametersModel() {
            this.idFullCategoriaField = 0;
            this.idStateField = 0;
            this.idStateStoField = 0;
            this.idExpertiseField = 0;
            this.codeErrorField = 0;
        }
        
        /// <remarks/>
        public int IdFullCategoria {
            get {
                return this.idFullCategoriaField;
            }
            set {
                this.idFullCategoriaField = value;
            }
        }
        
        /// <remarks/>
        public int IdState {
            get {
                return this.idStateField;
            }
            set {
                this.idStateField = value;
            }
        }
        
        /// <remarks/>
        public int IdStateSto {
            get {
                return this.idStateStoField;
            }
            set {
                this.idStateStoField = value;
            }
        }
        
        /// <remarks/>
        public int IdExpertise {
            get {
                return this.idExpertiseField;
            }
            set {
                this.idExpertiseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ModelRequest {
            get {
                return this.modelRequestField;
            }
            set {
                this.modelRequestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SerNumber {
            get {
                return this.serNumberField;
            }
            set {
                this.serNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InventoryNum {
            get {
                return this.inventoryNumField;
            }
            set {
                this.inventoryNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(0)]
        public int CodeError {
            get {
                return this.codeErrorField;
            }
            set {
                this.codeErrorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ErrorServer {
            get {
                return this.errorServerField;
            }
            set {
                this.errorServerField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int YearOfIssue {
            get {
                return this.yearOfIssueField;
            }
            set {
                this.yearOfIssueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ExploitationStartYear {
            get {
                return this.exploitationStartYearField;
            }
            set {
                this.exploitationStartYearField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LoginUser {
            get {
                return this.loginUserField;
            }
            set {
                this.loginUserField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class KitsEquipment {
        
        private KitsEquipmentServer[] kitsEquipmentServerField;
        
        private string inventoryNumField;
        
        private string errorServerField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("KitsEquipmentServer")]
        public KitsEquipmentServer[] KitsEquipmentServer {
            get {
                return this.kitsEquipmentServerField;
            }
            set {
                this.kitsEquipmentServerField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InventoryNum {
            get {
                return this.inventoryNumField;
            }
            set {
                this.inventoryNumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ErrorServer {
            get {
                return this.errorServerField;
            }
            set {
                this.errorServerField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class KitsEquipmentServer {
        
        private int idField;
        
        private string serialNumberField;
        
        private string inventoryNumberField;
        
        private bool isKitField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SerialNumber {
            get {
                return this.serialNumberField;
            }
            set {
                this.serialNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InventoryNumber {
            get {
                return this.inventoryNumberField;
            }
            set {
                this.inventoryNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsKit {
            get {
                return this.isKitField;
            }
            set {
                this.isKitField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class ParametersRequestAksiok {
        
        private FileAkt fileAktField;
        
        private FileExpertise fileExpertiseField;
        
        private int idTypeField;
        
        private int idProducerField;
        
        private int idModelField;
        
        private int idStateField;
        
        private int idStateStoField;
        
        private int idExpertiseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public FileAkt FileAkt {
            get {
                return this.fileAktField;
            }
            set {
                this.fileAktField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public FileExpertise FileExpertise {
            get {
                return this.fileExpertiseField;
            }
            set {
                this.fileExpertiseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdType {
            get {
                return this.idTypeField;
            }
            set {
                this.idTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdProducer {
            get {
                return this.idProducerField;
            }
            set {
                this.idProducerField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdModel {
            get {
                return this.idModelField;
            }
            set {
                this.idModelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdState {
            get {
                return this.idStateField;
            }
            set {
                this.idStateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdStateSto {
            get {
                return this.idStateStoField;
            }
            set {
                this.idStateStoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdExpertise {
            get {
                return this.idExpertiseField;
            }
            set {
                this.idExpertiseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
    public partial class FileAkt {
        
        private string nameFileField;
        
        private byte[] fileField;
        
        private string typeFileField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameFile {
            get {
                return this.nameFileField;
            }
            set {
                this.nameFileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
        public byte[] File {
            get {
                return this.fileField;
            }
            set {
                this.fileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TypeFile {
            get {
                return this.typeFileField;
            }
            set {
                this.typeFileField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=true)]
    public partial class FileExpertise {
        
        private string nameFileField;
        
        private byte[] fileField;
        
        private string typeFileField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameFile {
            get {
                return this.nameFileField;
            }
            set {
                this.nameFileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
        public byte[] File {
            get {
                return this.fileField;
            }
            set {
                this.fileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TypeFile {
            get {
                return this.typeFileField;
            }
            set {
                this.typeFileField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class UploadFileAksiok {
        
        private string nameFileField;
        
        private byte[] fileField;
        
        private string typeFileField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameFile {
            get {
                return this.nameFileField;
            }
            set {
                this.nameFileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
        public byte[] File {
            get {
                return this.fileField;
            }
            set {
                this.fileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TypeFile {
            get {
                return this.typeFileField;
            }
            set {
                this.typeFileField = value;
            }
        }
    }
}