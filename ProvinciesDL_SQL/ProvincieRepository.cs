using Microsoft.Data.SqlClient;
using ProvinciesBL.Interfaces;
using ProvinciesBL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesDL_SQL
{
    public class ProvincieRepository : IProvincieRepository
    {
        private string connectionString;

        public ProvincieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void UploadToDatabase(List<Provincie> data)
        {
            string SQLprovincie = "INSERT INTO provincie(naam) output INSERTED.ID VALUES(@naam)";
            string SQLgemeente = "INSERT INTO gemeente(naam,provincieid) output INSERTED.ID VALUES (@naam,@provincieid)";
            string SQLstraat = "INSERT INTO straat(naam,gemeenteid) VALUES(@naam,@gemeenteid)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand cmdProvincie=conn.CreateCommand()) 
            using(SqlCommand cmdGemeente=conn.CreateCommand())
            using(SqlCommand cmdStraat=conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                cmdProvincie.Transaction= sqlTransaction;
                cmdGemeente.Transaction= sqlTransaction;
                cmdStraat.Transaction= sqlTransaction;
                cmdProvincie.CommandText= SQLprovincie;
                cmdGemeente.CommandText= SQLgemeente;
                cmdStraat.CommandText= SQLstraat;
                cmdProvincie.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                cmdGemeente.Parameters.Add(new SqlParameter("@naam",SqlDbType.NVarChar));
                cmdGemeente.Parameters.Add(new SqlParameter("@provincieid",SqlDbType.Int));
                cmdStraat.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                cmdStraat.Parameters.Add(new SqlParameter("@gemeenteid", SqlDbType.Int));
                int provincieId, gemeenteId;
                try
                {
                    foreach (Provincie provincie in data)
                    {
                        cmdProvincie.Parameters["@naam"].Value = provincie.Naam;
                        provincieId=(int)cmdProvincie.ExecuteScalar();
                        cmdGemeente.Parameters["@provincieid"].Value = provincieId;
                        foreach (Gemeente gemeente in provincie.Gemeentes)
                        {
                            cmdGemeente.Parameters["@naam"].Value = gemeente.Naam;
                            gemeenteId=(int)cmdGemeente.ExecuteScalar();
                            cmdStraat.Parameters["@gemeenteid"].Value= gemeenteId;
                            foreach (Straat straat in gemeente.Straten)
                            {
                                cmdStraat.Parameters["@naam"].Value = straat.Naam;
                                cmdStraat.ExecuteNonQuery();
                            }
                        }
                    }
                    sqlTransaction.Commit();
                }
                catch(Exception ex) { sqlTransaction.Rollback(); }





            //using (SqlCommand cmd = conn.CreateCommand())          
            //{
            //    conn.Open();
            //    //SqlTransaction tran = conn.BeginTransaction();
            //    //cmd.Transaction = tran;             
            //    try
            //    {
                    
            //        cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
            //        foreach (Provincie provincie in data)
            //        {
            //            cmd.Parameters.Clear();
            //            cmd.CommandText = SQLprovincie;
            //            cmd.Parameters.AddWithValue("@naam", provincie.Naam);
            //            //cmd.Parameters["@naam"].Value=provincie.Naam;
            //            int provincieId=(int)cmd.ExecuteScalar();
                        
            //            //cmd.Parameters.Clear();
            //            //cmd.Parameters.AddWithValue("@provincieid", provincieId);
            //            //cmd.Parameters.Add(new SqlParameter("@naam",SqlDbType.NVarChar));
            //            foreach (Gemeente gemeente in provincie.Gemeentes)
            //            {
            //                cmd.Parameters.Clear();
            //                cmd.Parameters.AddWithValue("@provincieid", provincieId);
            //                cmd.Parameters.AddWithValue("@naam", gemeente.Naam);
            //                cmd.CommandText = SQLgemeente;
            //                cmd.Parameters["@naam"].Value=gemeente.Naam;
            //                int gemeenteId=(int)cmd.ExecuteScalar();
            //                cmd.CommandText = SQLstraat;
            //                cmd.Parameters.Clear();
            //                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
            //                cmd.Parameters.AddWithValue("@gemeenteid", gemeenteId);
            //                foreach (Straat straat in gemeente.Straten)
            //                {
            //                    cmd.Parameters["@naam"].Value=straat.Naam;
            //                    cmd.ExecuteNonQuery();
            //                }
            //            }
            //        }

            //        //tran.Commit();
            //    }
            //    catch(Exception ex) { //tran.Rollback();
            //                          }

            }
        }
    }
}
