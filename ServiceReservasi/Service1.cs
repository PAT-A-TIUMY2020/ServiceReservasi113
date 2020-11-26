using System;
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
        SqlCommand com; //untuk connection database ke visual studio
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
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

        public List<DetailLokasi> DetailLokasis()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();//proses untuk mendeclare nama lsit yang telah dibuat dengan nama baru
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi,Deskripsi_full, Kuota from dbo.Lokasi"; //declare query
                connection = new SqlConnection(constring); //fungsi konek ke database
                com = new SqlCommand(sql, connection); // proses execute query
                connection.Open(); //membuka koneksi
                SqlDataReader reader = com.ExecuteReader();// menampilkan data query
                while (reader.Read())
                {
                    /*nama class*/
                    DetailLokasi data = new DetailLokasi();//deklarasi data, mengambil 1 persatu dari database
                    //bentuk array
                    data.IDLokasi = reader.GetString(0);//0 itu index, ada dikolom keberapa di string sql diatas
                    data.NamaLokasi = reader.GetString(1);
                    data.DeksripsiFull = reader.GetString(2);
                    data.kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);//mengumpulkan data yang awalnya dari array
                }
                connection.Close();
            }
            //untuk menutup akses ke database
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
                    string sql = "insert into dbo.Pemesanan values('" + IDPemesanan + "', '" + NamaCustomer + "','" + NoTelpon + "','" + JumlahPemesanan + ",'" + IDLokasi + "')";
                    connection = new SqlConnection(constring); //fungsi konek ke database
                    com = new SqlCommand(sql, connection);
                    connection.Open();
                    com.ExecuteNonQuery();
                    connection.Close();
                    a = "sukses";
                }
                catch(Exception es)
                {
                    Console.WriteLine(es);
                    ;   }
            return a;
            }

        public string editPemesanan(string IDPemesanan, string NamaCustomer)
        {
            throw new NotImplementedException();
        }

        public string DeletePemesanan(string IDPemesanan)
        {
            throw new NotImplementedException();
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public List<DetailLokasi> DetailLokasi()
        {
            throw new NotImplementedException();
        }

        public List<Pemesanan> Pemesanan()
        {
            throw new NotImplementedException();
        }
    

        public string Pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            throw new NotImplementedException();
        }

        public string EditPemesanan(string IDPemesanan, string NamaCustomer)
        {
            throw new NotImplementedException();
        }

        public string deletePemesanan(string IDPemesanan)
        {
            throw new NotImplementedException();
        }

       
    }
}


