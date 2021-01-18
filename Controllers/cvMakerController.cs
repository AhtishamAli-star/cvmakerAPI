using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/cvMaker")]

    public class cvMakerController : ApiController
    {
        SqlConnection sqlcon;
        SqlCommand cmd;
        DataTable dt;
        [HttpPost]
        [Route("GET_CV_Details")]
        public HttpResponseMessage GETCVDETAILS(getDetailsModel gdm)
        {
            dt = new DataTable();
            try
            {
                using (sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString))
                {
                    sqlcon.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlcon;
                    cmd.CommandText = "GetRecord";
                    cmd.Parameters.AddWithValue("@personId",gdm.personId);

                    SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                    sqlda.Fill(dt);
                    
                    sqlcon.Close();
                    return Request.CreateResponse(HttpStatusCode.OK,dt);
                }
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }

        }

        [HttpPost]
        [Route("POST_CV_Details")]
        public HttpResponseMessage POSTCVDETAILS(detailsModel dm)
        {
            try
            {
                using (sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbString"].ConnectionString))
                {
                    sqlcon.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlcon;
                    cmd.CommandText = "insertRecord";
                    cmd.Parameters.AddWithValue("@name", dm.name);
                    cmd.Parameters.AddWithValue("@email", dm.email);
                    cmd.Parameters.AddWithValue("@phoneNumber", dm.phoneNumber);
                    cmd.Parameters.AddWithValue("@address", dm.address);
                    cmd.Parameters.AddWithValue("@about", dm.about);
                    cmd.Parameters.AddWithValue("@picture", dm.picture);
                    cmd.Parameters.AddWithValue("@skills", dm.skills);
                    cmd.Parameters.AddWithValue("@hobbies", dm.hobbies);
                    cmd.Parameters.AddWithValue("@jobTitle", dm.jobTitle);
                    cmd.Parameters.AddWithValue("@employer", dm.employer);
                    cmd.Parameters.AddWithValue("@fromTo", dm.fromTo);
                    cmd.Parameters.AddWithValue("@description", dm.description);
                    cmd.Parameters.AddWithValue("@personId", dm.personId);

                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                sqlcon.Close();
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }


        }

    }
}
