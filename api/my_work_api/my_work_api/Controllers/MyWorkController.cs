using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace my_work_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyWorkController : ControllerBase
    {
        private IConfiguration _configuration;
        public MyWorkController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetWorks")]
        public JsonResult GetWorks()
        {
            var myConnection = new SqlConnection(_configuration.GetConnectionString("myworkconnectionstring"));
            try
            {
                var dt = new DataTable();
                myConnection.Open();
                if (myConnection.State == ConnectionState.Closed)
                    myConnection.Open();
                var cmd = new SqlCommand("getworks", myConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.ExecuteNonQuery();
                var da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                return new JsonResult(dt);
            }
            catch(Exception tdf)
            {
                return new JsonResult("Error :\n"+tdf.ToString());
            }
            finally
            {
                myConnection.Close(); myConnection.Dispose();
            }
        }
        [HttpPost]
        [Route("AddWork")]
        public JsonResult AddWork([FromForm] string newWork)
        {
            var myConnection = new SqlConnection(_configuration.GetConnectionString("myworkconnectionstring"));
            try
            {
                myConnection.Open();
                if (myConnection.State == ConnectionState.Closed)
                    myConnection.Open();
                var cmd = new SqlCommand("addwork", myConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("description_work", SqlDbType.NVarChar)).Value = newWork;
                cmd.ExecuteNonQuery();
                myConnection.Close();

                return new JsonResult("Work successfully added!");
            }
            catch (Exception tdf)
            {
                return new JsonResult("Error :\n" + tdf.ToString());
            }
            finally
            {
                myConnection.Close(); myConnection.Dispose();
            }
        }
        [HttpDelete]
        [Route("DeleteWork")]
        public JsonResult DeleteWork([FromForm] string idwork)
        {
            var myConnection = new SqlConnection(_configuration.GetConnectionString("myworkconnectionstring"));
            try
            {
                myConnection.Open();
                if (myConnection.State == ConnectionState.Closed)
                    myConnection.Open();
                var cmd = new SqlCommand("deletework", myConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.NVarChar)).Value = idwork;
                cmd.ExecuteNonQuery();
                myConnection.Close();

                return new JsonResult("Work successfully deleted!");
            }
            catch (Exception tdf)
            {
                return new JsonResult("Error :\n" + tdf.ToString());
            }
            finally
            {
                myConnection.Close(); myConnection.Dispose();
            }
        }

    }
}
