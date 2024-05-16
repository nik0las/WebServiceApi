using System;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;


namespace WebServiceApi
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://myapisrv.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet getToXML()
        {
           
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = ConfigurationManager.AppSettings["server"].ToString();
                builder.UserID = ConfigurationManager.AppSettings["username"].ToString();
                builder.Password = ConfigurationManager.AppSettings["password"].ToString();
                builder.InitialCatalog = ConfigurationManager.AppSettings["database"].ToString();
                builder.Encrypt = Convert.ToBoolean(ConfigurationManager.AppSettings["Encrypt"].ToString());

                string sql = ConfigurationManager.AppSettings["query"].ToString();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    return ds;
                }
                
        }

        [WebMethod]
        public string getToJson()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //Build connection String from WebConfig
            builder.DataSource = ConfigurationManager.AppSettings["server"].ToString();
            builder.UserID = ConfigurationManager.AppSettings["username"].ToString();
            builder.Password = ConfigurationManager.AppSettings["password"].ToString();
            builder.InitialCatalog = ConfigurationManager.AppSettings["database"].ToString();
            builder.Encrypt = Convert.ToBoolean(ConfigurationManager.AppSettings["Encrypt"].ToString());
            //Query 
            string sql = ConfigurationManager.AppSettings["query"].ToString();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, connection))
                {
                    DataSet ds = new DataSet();
                    try 
                    {                        
                        da.Fill(ds);
                    }
                    catch (Exception ex) 
                    {
                        return "failure"+ex;
                    }
                    

                    string a = JsonConvert.SerializeObject(ds);
                    return a;
                }
            }

        }
    }
}
