using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Q1.Models
{
    public class PassengerBusinessLogic
    {
        static string str = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        SqlConnection con = new SqlConnection(str);
        public List<Passenger> GetDetails()
        {
            List<Passenger> list = new List<Passenger>();
            SqlCommand cmd = new SqlCommand("sp_fetchDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Passenger ps = new Passenger();

                ps.Id = Convert.ToInt32(rd["p_id"]);
                ps.Name = rd["p_name"].ToString();
                ps.Address = rd["address"].ToString();
                ps.Phone = rd["ph_no"].ToString();
                ps.Email = rd["email"].ToString();
                list.Add(ps);

            }
            con.Close();
            return list;
        }

        public string InsertPassengers(Passenger pass)
        {
            SqlCommand cmd = new SqlCommand("sp_insert", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", pass.Name);
            cmd.Parameters.AddWithValue("@address", pass.Address);
            cmd.Parameters.AddWithValue("@phno", pass.Phone);
            cmd.Parameters.AddWithValue("@email", pass.Email);
            con.Open();

            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@msg",
                Size = int.MaxValue,
                Direction = System.Data.ParameterDirection.Output,
            };
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
            return param.Value.ToString();

        }

        public string DeletePassenger(int id)
        {
            SqlCommand cmd = new SqlCommand("sp_deleted", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@msg",
                Size = int.MaxValue,
                Direction = System.Data.ParameterDirection.Output,
            };
            cmd.Parameters.Add(param);
            con.Open();
            cmd.ExecuteNonQuery();
            return param.Value.ToString();
        }
        public string UpdatePassenger(int id, Passenger pass)
        {
            SqlCommand cmd = new SqlCommand("sp_update", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", pass.Name);
            cmd.Parameters.AddWithValue("@address", pass.Address);
            cmd.Parameters.AddWithValue("@phone", pass.Phone);
            cmd.Parameters.AddWithValue("@email", pass.Email);
            SqlParameter param = new SqlParameter()
            {
                ParameterName = "@msg",
                Size = int.MaxValue,
                Direction = System.Data.ParameterDirection.Output,
            };
            cmd.Parameters.Add(param);
            con.Open();
            cmd.ExecuteNonQuery();
            System.Diagnostics.Debug.WriteLine("texst");
            return param.Value.ToString();
        }

        public Passenger StoreDetail(int id)
        {
            Passenger passenger = null;
            SqlCommand cmd = new SqlCommand("select * from sales.Passenger where p_id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                passenger = new Passenger();

                passenger.Id = id;
                passenger.Name = reader["p_name"].ToString();
                passenger.Address = reader["address"].ToString();
                passenger.Phone = reader["ph_no"].ToString();
                passenger.Email = reader["email"].ToString();

            }
            con.Close();
            return passenger;
        }
    }
}