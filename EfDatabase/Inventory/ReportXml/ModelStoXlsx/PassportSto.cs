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
namespace EfDatabase.PassportSto {
    using AttributeHelperModelXml;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class PassportSto {
        
        private EquipmentSto[] equipmentStoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EquipmentSto")]
        public EquipmentSto[] EquipmentSto {
            get {
                return this.equipmentStoField;
            }
            set {
                this.equipmentStoField = value;
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
    public partial class EquipmentSto {
        
        private System.Nullable<int> countOfLicensesSipField;
        
        private System.Nullable<int> countProcessorField;
        
        private System.Nullable<int> countCoresField;
        
        private System.Nullable<int> countBoardsField;
        
        private System.Nullable<int> countFreeBoardsField;
        
        private System.Nullable<int> countSubscribersAnalogField;
        
        private System.Nullable<int> countSubscribersDigitalField;
        
        private System.Nullable<int> countAdminField;
        
        private int idField;
        
        private int sounField;
        
        private string mifnsObjectField;
        
        private int codeObjectField;
        
        private string viewObjectField;
        
        private string typeObjectField;
        
        private string manufacturerObjectField;
        
        private string modelObjectField;
        
        private string setObjectField;
        
        private string clusterObjectField;
        
        private string sto2019Field;
        
        private string sto2020Field;
        
        private string stoField;
        
        private string serialNumberField;
        
        private string serviceNumberField;
        
        private string inventoryNumberField;
        
        private string serviceNumber2017Field;
        
        private string isNotBalansObjectField;
        
        private string nameComputerField;
        
        private int yearOfIssueField;
        
        private System.DateTime guaranteeField;
        
        private string serviceStatusField;
        
        private string supplyContractField;
        
        private string contractStoField;
        
        private string numberField;
        
        private string commentField;
        
        private string modelDeliveryNoteField;
        
        private string actualOsVersionField;
        
        private string versionOsField;
        
        private string nameZagsField;
        
        private string adressZagsField;
        
        private string transferAgreementNumberField;
        
        private string isNotNullBalansObjectField;
        
        private string ippointmentField;
        
        private string typeUseField;
        
        private string virtualizationEnvironmentField;
        
        private string departmentField;
        
        private string plotField;
        
        private string locationFloorField;
        
        private string locationRoomField;
        
        private string roomField;
        
        private string versionPsField;
        
        private string networkNameField;
        
        private string ipAdreesField;
        
        private string additionalIpAdreesField;
        
        private string ipAdreesStkField;
        
        private string ipAdreesStkMikroField;
        
        private string macAdreesField;
        
        private string mhzField;
        
        private string typeMemoryField;
        
        private string sizeMemoryField;
        
        private string typeProcessorField;
        
        private string cdDvdField;
        
        private string fddField;
        
        private string raidField;
        
        private string publicMemoryField;
        
        private string countHddField;
        
        private string volumeHddField;
        
        private string typeHddField;
        
        private string networkCardField;
        
        private string speedNetworkCardField;
        
        private string inventoryNumberPcField;
        
        private string inventoryNumberIbpField;
        
        private string inventoryNumberMonitorField;
        
        private string serviceNumberMonitorField;
        
        private string modelMonitorField;
        
        private string typeMonitorField;
        
        private string sizeMonitorField;
        
        private string maxSizeMonitorField;
        
        private string typeAnalogField;
        
        private string typeDigitalField;
        
        private string numberKeyField;
        
        private string markaStrimField;
        
        private string markaKardStrimField;
        
        private string osField;
        
        private string servicePackField;
        
        private string officePackField;

        private string fioField;

        private string mainFioField;
        
        private string infoField;
        
        private System.DateTime dateCreateField;
        
        private bool dateCreateFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Кол-во лицензий внутренних абонентов SIP")]
        public System.Nullable<int> CountOfLicensesSip {
            get {
                return this.countOfLicensesSipField;
            }
            set {
                this.countOfLicensesSipField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@" Количество процессоров")]
        public System.Nullable<int> CountProcessor {
            get {
                return this.countProcessorField;
            }
            set {
                this.countProcessorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Количество ядер")]
        public System.Nullable<int> CountCores {
            get {
                return this.countCoresField;
            }
            set {
                this.countCoresField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Кол-во плат")]
        public System.Nullable<int> CountBoards {
            get {
                return this.countBoardsField;
            }
            set {
                this.countBoardsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Кол-во свободных платомест")]
        public System.Nullable<int> CountFreeBoards {
            get {
                return this.countFreeBoardsField;
            }
            set {
                this.countFreeBoardsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Кол-во внутр абон аналог")]
        public System.Nullable<int> CountSubscribersAnalog {
            get {
                return this.countSubscribersAnalogField;
            }
            set {
                this.countSubscribersAnalogField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Кол-во внутр абон цифровых")]
        public System.Nullable<int> CountSubscribersDigital {
            get {
                return this.countSubscribersDigitalField;
            }
            set {
                this.countSubscribersDigitalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        [DataNames(@"Численность администраторов УАТС")]
        public System.Nullable<int> CountAdmin {
            get {
                return this.countAdminField;
            }
            set {
                this.countAdminField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"ID")]
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"СОУН")]
        public int Soun {
            get {
                return this.sounField;
            }
            set {
                this.sounField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Объект ФНС России")]
        public string MifnsObject {
            get {
                return this.mifnsObjectField;
            }
            set {
                this.mifnsObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Код объекта")]
        public int CodeObject {
            get {
                return this.codeObjectField;
            }
            set {
                this.codeObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Вид оборудования")]
        public string ViewObject {
            get {
                return this.viewObjectField;
            }
            set {
                this.viewObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип оборудования")]
        public string TypeObject {
            get {
                return this.typeObjectField;
            }
            set {
                this.typeObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Производитель")]
        public string ManufacturerObject {
            get {
                return this.manufacturerObjectField;
            }
            set {
                this.manufacturerObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Модель")]
        public string ModelObject {
            get {
                return this.modelObjectField;
            }
            set {
                this.modelObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Комплект")]
        public string SetObject {
            get {
                return this.setObjectField;
            }
            set {
                this.setObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Кластер")]
        public string ClusterObject {
            get {
                return this.clusterObjectField;
            }
            set {
                this.clusterObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"СТО 2019")]
        public string Sto2019 {
            get {
                return this.sto2019Field;
            }
            set {
                this.sto2019Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"СТО 2020")]
        public string Sto2020 {
            get {
                return this.sto2020Field;
            }
            set {
                this.sto2020Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"СТО")]
        public string Sto {
            get {
                return this.stoField;
            }
            set {
                this.stoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Серийный номер")]
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
        [DataNames(@"Сервисный номер")]
        public string ServiceNumber {
            get {
                return this.serviceNumberField;
            }
            set {
                this.serviceNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Инвентарный номер")]
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
        [DataNames(@"Сервисный номер 2017")]
        public string ServiceNumber2017 {
            get {
                return this.serviceNumber2017Field;
            }
            set {
                this.serviceNumber2017Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Не на балансе")]
        public string IsNotBalansObject {
            get {
                return this.isNotBalansObjectField;
            }
            set {
                this.isNotBalansObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Имя компьютера")]
        public string NameComputer {
            get {
                return this.nameComputerField;
            }
            set {
                this.nameComputerField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Год выпуска")]
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
        [DataNames(@"Гарантия")]
        public System.DateTime Guarantee {
            get {
                return this.guaranteeField;
            }
            set {
                this.guaranteeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Статус обслуживания")]
        public string ServiceStatus {
            get {
                return this.serviceStatusField;
            }
            set {
                this.serviceStatusField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Контракт на поставку")]
        public string SupplyContract {
            get {
                return this.supplyContractField;
            }
            set {
                this.supplyContractField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Контракт на СТО")]
        public string ContractSto {
            get {
                return this.contractStoField;
            }
            set {
                this.contractStoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Номер")]
        public string Number {
            get {
                return this.numberField;
            }
            set {
                this.numberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Комментарии")]
        public string Comment {
            get {
                return this.commentField;
            }
            set {
                this.commentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Модель в соответствии с накладной/поставкой")]
        public string ModelDeliveryNote {
            get {
                return this.modelDeliveryNoteField;
            }
            set {
                this.modelDeliveryNoteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Фактическая версия ОС")]
        public string ActualOsVersion {
            get {
                return this.actualOsVersionField;
            }
            set {
                this.actualOsVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Версия фактической ОС")]
        public string VersionOs {
            get {
                return this.versionOsField;
            }
            set {
                this.versionOsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Наименование органа ЗАГС")]
        public string NameZags {
            get {
                return this.nameZagsField;
            }
            set {
                this.nameZagsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Адрес органа ЗАГС")]
        public string AdressZags {
            get {
                return this.adressZagsField;
            }
            set {
                this.adressZagsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Номер договора передачи")]
        public string TransferAgreementNumber {
            get {
                return this.transferAgreementNumberField;
            }
            set {
                this.transferAgreementNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"На балансе")]
        public string IsNotNullBalansObject {
            get {
                return this.isNotNullBalansObjectField;
            }
            set {
                this.isNotNullBalansObjectField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Назначение")]
        public string Ippointment {
            get {
                return this.ippointmentField;
            }
            set {
                this.ippointmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип использования")]
        public string TypeUse {
            get {
                return this.typeUseField;
            }
            set {
                this.typeUseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Среда виртуализации")]
        public string VirtualizationEnvironment {
            get {
                return this.virtualizationEnvironmentField;
            }
            set {
                this.virtualizationEnvironmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Отдел")]
        public string Department {
            get {
                return this.departmentField;
            }
            set {
                this.departmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Участок (здание)")]
        public string Plot {
            get {
                return this.plotField;
            }
            set {
                this.plotField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Расположение. Этаж")]
        public string LocationFloor {
            get {
                return this.locationFloorField;
            }
            set {
                this.locationFloorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Расположение. Комната")]
        public string LocationRoom {
            get {
                return this.locationRoomField;
            }
            set {
                this.locationRoomField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Комната")]
        public string Room {
            get {
                return this.roomField;
            }
            set {
                this.roomField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Версия ПС")]
        public string VersionPs {
            get {
                return this.versionPsField;
            }
            set {
                this.versionPsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Сетевое имя (NetBIOS)")]
        public string NetworkName {
            get {
                return this.networkNameField;
            }
            set {
                this.networkNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"IP адрес")]
        public string IpAdrees {
            get {
                return this.ipAdreesField;
            }
            set {
                this.ipAdreesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Дополнительные IP адреса")]
        public string AdditionalIpAdrees {
            get {
                return this.additionalIpAdreesField;
            }
            set {
                this.additionalIpAdreesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"IP адрес в СТК")]
        public string IpAdreesStk {
            get {
                return this.ipAdreesStkField;
            }
            set {
                this.ipAdreesStkField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"IP адрес в СТК (для объектов без МикроРТУ)")]
        public string IpAdreesStkMikro {
            get {
                return this.ipAdreesStkMikroField;
            }
            set {
                this.ipAdreesStkMikroField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"MAC-адрес")]
        public string MacAdrees {
            get {
                return this.macAdreesField;
            }
            set {
                this.macAdreesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Частота процессора, МГц")]
        public string Mhz {
            get {
                return this.mhzField;
            }
            set {
                this.mhzField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"ОЗУ (тип)")]
        public string TypeMemory {
            get {
                return this.typeMemoryField;
            }
            set {
                this.typeMemoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"ОЗУ (размер)")]
        public string SizeMemory {
            get {
                return this.sizeMemoryField;
            }
            set {
                this.sizeMemoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип процессора")]
        public string TypeProcessor {
            get {
                return this.typeProcessorField;
            }
            set {
                this.typeProcessorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"CD/DVD")]
        public string CdDvd {
            get {
                return this.cdDvdField;
            }
            set {
                this.cdDvdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames("FDD")]
        public string Fdd {
            get {
                return this.fddField;
            }
            set {
                this.fddField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"RAID")]
        public string Raid {
            get {
                return this.raidField;
            }
            set {
                this.raidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Общее дисковое пространство, Gb")]
        public string PublicMemory {
            get {
                return this.publicMemoryField;
            }
            set {
                this.publicMemoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Количество HDD")]
        public string CountHdd {
            get {
                return this.countHddField;
            }
            set {
                this.countHddField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Объем жесткого диска")]
        public string VolumeHdd {
            get {
                return this.volumeHddField;
            }
            set {
                this.volumeHddField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип жесткого диска")]
        public string TypeHdd {
            get {
                return this.typeHddField;
            }
            set {
                this.typeHddField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Сетевая карта")]
        public string NetworkCard {
            get {
                return this.networkCardField;
            }
            set {
                this.networkCardField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Скорость сетевой карты")]
        public string SpeedNetworkCard {
            get {
                return this.speedNetworkCardField;
            }
            set {
                this.speedNetworkCardField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Инвентарный номер ПК, к которому подключен")]
        public string InventoryNumberPc {
            get {
                return this.inventoryNumberPcField;
            }
            set {
                this.inventoryNumberPcField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Инвентарный номер ИБП, к которому подключен")]
        public string InventoryNumberIbp {
            get {
                return this.inventoryNumberIbpField;
            }
            set {
                this.inventoryNumberIbpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Инвентарный номер монитора")]
        public string InventoryNumberMonitor {
            get {
                return this.inventoryNumberMonitorField;
            }
            set {
                this.inventoryNumberMonitorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Сервисный номер монитора")]
        public string ServiceNumberMonitor {
            get {
                return this.serviceNumberMonitorField;
            }
            set {
                this.serviceNumberMonitorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Монитор, марка")]
        public string ModelMonitor {
            get {
                return this.modelMonitorField;
            }
            set {
                this.modelMonitorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип монитора")]
        public string TypeMonitor {
            get {
                return this.typeMonitorField;
            }
            set {
                this.typeMonitorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Размер экрана")]
        public string SizeMonitor {
            get {
                return this.sizeMonitorField;
            }
            set {
                this.sizeMonitorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Максимальное разрешение")]
        public string MaxSizeMonitor {
            get {
                return this.maxSizeMonitorField;
            }
            set {
                this.maxSizeMonitorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип аналогового подключения городской ввод ")]
        public string TypeAnalog {
            get {
                return this.typeAnalogField;
            }
            set {
                this.typeAnalogField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Тип цифрового подключения городской ввод")]
        public string TypeDigital {
            get {
                return this.typeDigitalField;
            }
            set {
                this.typeDigitalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Номер Ключа")]
        public string NumberKey {
            get {
                return this.numberKeyField;
            }
            set {
                this.numberKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Марка стримера")]
        public string MarkaStrim {
            get {
                return this.markaStrimField;
            }
            set {
                this.markaStrimField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Маркировка картриджей стримера")]
        public string MarkaKardStrim {
            get {
                return this.markaKardStrimField;
            }
            set {
                this.markaKardStrimField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Операционная система")]
        public string Os {
            get {
                return this.osField;
            }
            set {
                this.osField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Сервис пак")]
        public string ServicePack {
            get {
                return this.servicePackField;
            }
            set {
                this.servicePackField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Офисный пакет")]
        public string OfficePack {
            get {
                return this.officePackField;
            }
            set {
                this.officePackField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Фамилия И.О.")]
        public string Fio
        {
            get
            {
                return this.fioField;
            }
            set
            {
                this.fioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Ответственный")]
        public string MainFio {
            get {
                return this.mainFioField;
            }
            set {
                this.mainFioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [DataNames(@"Описание")]
        public string Info {
            get {
                return this.infoField;
            }
            set {
                this.infoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime DateCreate {
            get {
                return this.dateCreateField;
            }
            set {
                this.dateCreateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateCreateSpecified {
            get {
                return this.dateCreateFieldSpecified;
            }
            set {
                this.dateCreateFieldSpecified = value;
            }
        }
    }
}
