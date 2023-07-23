using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

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
        public JsonResult GetWorks() {
            string sqlDatasource = _configuration.GetConnectionString("myworkconnectionstring");
            var dt = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(sqlDatasource))
            {
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
                myConnection.Close();

            }
            return new JsonResult(dt);
        }
        [HttpPost]
        [Route("AddWork")]
        public JsonResult AddWork()
        {
            string sqlDatasource = _configuration.GetConnectionString("myworkconnectionstring");
            var dt = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(sqlDatasource))
            {
                myConnection.Open();
                if (myConnection.State == ConnectionState.Closed)
                    myConnection.Open();
                var cmd = new SqlCommand("addwork", myConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.ExecuteNonQuery();
                var da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                myConnection.Close();

            }
            return new JsonResult(dt);
        }

    }
}
