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
namespace EfDatabase.MemoReport {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class ModelMemoReport {
        
        private SelectParameterDocument selectParameterDocumentField;
        
        private UserDepartment userDepartmentField;
        
        private BossDepartment bossDepartmentField;
        
        private BossAgreed bossAgreedField;
        
        private Executor executorField;
        
        private LeaderOrganization leaderOrganizationField;
        
        /// <remarks/>
        public SelectParameterDocument SelectParameterDocument {
            get {
                return this.selectParameterDocumentField;
            }
            set {
                this.selectParameterDocumentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public UserDepartment UserDepartment {
            get {
                return this.userDepartmentField;
            }
            set {
                this.userDepartmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BossDepartment BossDepartment {
            get {
                return this.bossDepartmentField;
            }
            set {
                this.bossDepartmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BossAgreed BossAgreed {
            get {
                return this.bossAgreedField;
            }
            set {
                this.bossAgreedField = value;
            }
        }
        
        /// <remarks/>
        public Executor Executor {
            get {
                return this.executorField;
            }
            set {
                this.executorField = value;
            }
        }
        
        /// <remarks/>
        public LeaderOrganization LeaderOrganization {
            get {
                return this.leaderOrganizationField;
            }
            set {
                this.leaderOrganizationField = value;
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
    public partial class SelectParameterDocument {
        
        private int numberDocumentField;
        
        private string nameDocumentField;
        
        private int typeDocumentField;
        
        private int idUserField;
        
        private string tabelNumberField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int NumberDocument {
            get {
                return this.numberDocumentField;
            }
            set {
                this.numberDocumentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameDocument {
            get {
                return this.nameDocumentField;
            }
            set {
                this.nameDocumentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int TypeDocument {
            get {
                return this.typeDocumentField;
            }
            set {
                this.typeDocumentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int IdUser {
            get {
                return this.idUserField;
            }
            set {
                this.idUserField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TabelNumber {
            get {
                return this.tabelNumberField;
            }
            set {
                this.tabelNumberField = value;
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
    public partial class UserDepartment {
        
        private Orders ordersField;
        
        private string nameUserField;
        
        private string nameOtdelField;
        
        private string rnameOtdelField;
        
        private string positionField;
        
        private string regulationsField;
        
        private string tabelNumberField;
        
        private string ipAdressField;
        
        private string numberKabinetField;
        
        private string telephoneField;
        
        private string smallTabelNumberField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public Orders Orders {
            get {
                return this.ordersField;
            }
            set {
                this.ordersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameUser {
            get {
                return this.nameUserField;
            }
            set {
                this.nameUserField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameOtdel {
            get {
                return this.nameOtdelField;
            }
            set {
                this.nameOtdelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RnameOtdel {
            get {
                return this.rnameOtdelField;
            }
            set {
                this.rnameOtdelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Position {
            get {
                return this.positionField;
            }
            set {
                this.positionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Regulations {
            get {
                return this.regulationsField;
            }
            set {
                this.regulationsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TabelNumber {
            get {
                return this.tabelNumberField;
            }
            set {
                this.tabelNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IpAdress {
            get {
                return this.ipAdressField;
            }
            set {
                this.ipAdressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NumberKabinet {
            get {
                return this.numberKabinetField;
            }
            set {
                this.numberKabinetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Telephone {
            get {
                return this.telephoneField;
            }
            set {
                this.telephoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SmallTabelNumber {
            get {
                return this.smallTabelNumberField;
            }
            set {
                this.smallTabelNumberField = value;
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
    public partial class Orders {
        
        private Orders_Reestr orders_ReestrField;
        
        /// <remarks/>
        public Orders_Reestr Orders_Reestr {
            get {
                return this.orders_ReestrField;
            }
            set {
                this.orders_ReestrField = value;
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
    public partial class Orders_Reestr {
        
        private string lastOrderField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastOrder {
            get {
                return this.lastOrderField;
            }
            set {
                this.lastOrderField = value;
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
    public partial class BossDepartment {
        
        private string nameDepartmentField;
        
        private string smallNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameDepartment {
            get {
                return this.nameDepartmentField;
            }
            set {
                this.nameDepartmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SmallName {
            get {
                return this.smallNameField;
            }
            set {
                this.smallNameField = value;
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
    public partial class BossAgreed {
        
        private string positionAgreedField;
        
        private string nameAgreedField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PositionAgreed {
            get {
                return this.positionAgreedField;
            }
            set {
                this.positionAgreedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameAgreed {
            get {
                return this.nameAgreedField;
            }
            set {
                this.nameAgreedField = value;
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
    public partial class Executor {
        
        private string nameUserField;
        
        private string phoneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameUser {
            get {
                return this.nameUserField;
            }
            set {
                this.nameUserField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
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
    public partial class LeaderOrganization {
        
        private string nameFaceLeaderField;
        
        private string rnameOrganizationField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NameFaceLeader {
            get {
                return this.nameFaceLeaderField;
            }
            set {
                this.nameFaceLeaderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RnameOrganization {
            get {
                return this.rnameOrganizationField;
            }
            set {
                this.rnameOrganizationField = value;
            }
        }
    }
}
