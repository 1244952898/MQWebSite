using System.Data;
using System.Data.SqlClient;

namespace match.dataaccess.mysql
{
    /// <summary>
    /// 赛点连接对象Connection
    /// </summary>
    public class MPConnection
    {
        private static string defaultConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["POSEntities"].ConnectionString;
        /// <summary>
        /// 默认的数据库连接字符串的Mysql库连接对象
        /// </summary>
        /// <returns></returns>
        public static IDbConnection GetConnection()
        {
            return new SqlConnection(defaultConnectionString);
        }
        /// <summary>
        /// 自定义数据库连接字符串的Mysql库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbConnection GetConnection(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                return new SqlConnection(connectionString);
            }
            else
            {
                return (SqlConnection)null;
            }
            //return new MySql.Data.MySqlClient.MySqlConnection(defaultConnectionConfig);
        }
                /// <summary>
        /// 根据配置链接字符串的Key名称获取Mysql库连接对象
        /// </summary>
        /// <param name="connectionStringKeyName"></param>
        /// <returns></returns>
        public static IDbConnection GetConnectionByKey(string connectionStringKeyName)
        {
            if (!string.IsNullOrEmpty(connectionStringKeyName))
            {
                string connString=System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKeyName].ConnectionString;
                if (!string.IsNullOrEmpty(connString))
                {
                    return new SqlConnection(connectionStringKeyName);
                }
                return (SqlConnection)null;
                
            }
            else
            {
                return (SqlConnection)null;
            }         
        }
        
    }
}
