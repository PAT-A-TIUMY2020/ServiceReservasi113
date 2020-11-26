﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi);//method//proses input data
        [OperationContract]
        string editPemesanan(string IDPemesanan, string NamaCustomer);
        [OperationContract]
        string DeletePemesanan(string IDPemesanan);
        [OperationContract]
        List<CekLokasi> ReviewLokasi();//nsmpilin data yang ada di database (select all) // menampilkan isi dari yang ada contract
        [OperationContract]
        List<DetailLokasi> DetailLokasi(); //menapilkan detail lokasi
        [OperationContract]
        List<Pemesanan> Pemesanan();

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }
    [DataContract]
    public class CekLokasi //daftar lokasi nongrong
    {
        [DataMember]
        public string IDLokasi { get; set; } //variabel dari publick class
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeskripsiSingkat { get; set; }
    }

    [DataContract]
    public class DetailLokasi //menapilkan detail lokasi
    {
        [DataMember]
        public string IDLokasi { get; set; }//variabel dari public class
        [DataMember]
        public string NamaLokasi { get; set; }
        [DataMember]
        public string DeksripsiFull { get; set; }
        [DataMember]
        public int kuota { get; set; }
    }

    [DataContract]
    public class Pemesanan //create
    {
        [DataMember]
        public string IDPemesanan { get; set; }
        [DataMember]
        public string NamaCustomer { get; set; }
        [DataMember]
        public string NoTelpon { get; set; }
        [DataMember]
        public int JumlahPemesanan { get; set; }
        [DataMember]
        public string IDLokasi { get; set; }
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ServiceReservasi.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}