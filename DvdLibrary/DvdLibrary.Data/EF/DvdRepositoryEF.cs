using DvdLibrary.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibrary.Models.Queries;
using DvdLibrary.Models.Tables;
using DvdLibrary.Data.EF;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace DvdLibrary.Data.EF
{
    public class DvdRepositoryEF : IDvdRepository
    {
        public void Delete(int dvdId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<DvdShortItem> DvdsSearchByDirector(string director)
        {
            List<DvdShortItem> dvds = new List<DvdShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSearchByDirector", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Director", "%" + director + "%");

                cn.Open(); //open connection

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdShortItem currentRow = new DvdShortItem();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.RealeaseYear = dr["RealeaseYear"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();

                        dvds.Add(currentRow);
                    }

                }
            }

            return dvds;
        }

        public List<DvdShortItem> DvdsSearchByRating(string rating)
        {
            List<DvdShortItem> dvds = new List<DvdShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSearchByRating", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Rating", rating);

                cn.Open(); //open connection

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdShortItem currentRow = new DvdShortItem();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.RealeaseYear = dr["RealeaseYear"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();

                        dvds.Add(currentRow);
                    }

                }
            }

            return dvds;
        }

        public List<DvdShortItem> DvdsSearchByRealeaseYear(string realeaseYear)
        {
            List<DvdShortItem> dvds = new List<DvdShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSearchByRealeaseYear", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RealeaseYear", realeaseYear);

                cn.Open(); //open connection

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdShortItem currentRow = new DvdShortItem();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.RealeaseYear = dr["RealeaseYear"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();

                        dvds.Add(currentRow);
                    }

                }
            }

            return dvds;
        }

        public List<DvdShortItem> DvdsSearchByTitle(string title)
        {
            List<DvdShortItem> dvds = new List<DvdShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdsSearchByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", "%" + title + "%");

                cn.Open(); //open connection

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DvdShortItem currentRow = new DvdShortItem();
                        currentRow.DvdId = (int)dr["DvdId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.RealeaseYear = dr["RealeaseYear"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();

                        dvds.Add(currentRow);
                    }

                }
            }

            return dvds;
        }

        public List<DvdShortItem> GetAll()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<DvdShortItem>("DvdsSelectAll",
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public DvdItem GetById(int dvdId)
        {

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@DvdId", dvdId);
                return cn.Query<DvdItem>("DvdSelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void Insert(DvdDetails dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@RealeaseYear", dvd.RealeaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();

                dvd.DvdId = (int)param.Value;
            }
        }

        public void Update(DvdDetails dvd)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvd.DvdId);
                cmd.Parameters.AddWithValue("@Title", dvd.Title);
                cmd.Parameters.AddWithValue("@RealeaseYear", dvd.RealeaseYear);
                cmd.Parameters.AddWithValue("@Director", dvd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvd.Notes);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
