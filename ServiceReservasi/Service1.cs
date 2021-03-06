﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=DESKTOP-B1DFHQO;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=bms120489";
        SqlConnection connection;
        SqlCommand com; // untuk mengkoneksikan database ke visual studio
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); //proses untuk mendeclare nama list yang telah dibuat dengan nama baru
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, kuota from dbo.Lokasi"; //declare query
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection); //proses execute query
                connection.Open(); //membuka conneksi
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    /*nama class*/
                    DetailLokasi data = new DetailLokasi(); //deklarasi data, mengambil 1 persatu dari database
                    //bentuk array
                    data.IDLokasi = reader.GetString(0); //0 itu index, ada dikolom keberapa di string sql diatas
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.kuota = reader.GetInt32(3);
                    LokasiFull.Add(data); //mengumpulkan data yang awalnya dari array
                }
                connection.Close(); //untuk menutup akses ke database
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCustomer + "', '" + NoTelpon + "', " + JumlahPemesanan + ", '" + IDLokasi + "')";
                connection = new SqlConnection(constring); //fungsi koneksi ke db
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                string sql2 = "update dbo.Lokasi set kuota =  kuota - " + JumlahPemesanan + " where ID_Lokasi = '" + IDLokasi + "' ";
                com = new SqlCommand(sql2, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {
            string a = "gagal";
            try
            {
                string sql = "update dbo.Pemesanan set Nama_Customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "'" + " where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }

            return a;

        }

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete dbo.Pemesanan where ID_reservasi = '" + IDPemesanan + "' ";
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }

            return a;
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>(); // proses mendeclare name list yang telah dibuat dengan
            try
            {
                string sql = " select ID_reservasi, Nama_customer, No_telpon," + "Jumlah_pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi l on p.ID_Lokasi = l.ID_lokasi";
                connection = new SqlConnection(constring); //fungsi koneksi ke db
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    /*nama class*/
                    Pemesanan data = new Pemesanan(); //desklarasi data, mengambil 1 persatu dari database
                                                      //bentuk query
                    data.IDPemesanan = reader.GetString(0); // a itu index, ada di kolom keberapa di string sql database
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data); //mengumpulkan dat yang awalnya dari array
                }
                connection.Close(); // untuk menutup akses ke database
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }
    }
}


