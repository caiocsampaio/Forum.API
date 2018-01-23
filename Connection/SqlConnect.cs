using System.Data.SqlClient;

namespace Forum.API.Connection
{
    public abstract class SqlConnect
    {
        /// <summary>
            /// Object to establish connection with the SQL DB.
            /// </summary>
        protected SqlConnection con = null;
        /// <summary>
            /// Object for SQL Query.
            /// </summary>
        protected SqlCommand cmd = null;
        /// <summary>
            /// Data Reader for SQL DB.
            /// </summary>
        protected SqlDataReader dr = null;
        /// <summary>
            /// Protected SQL connection string method.
            /// </summary>
            /// <returns>Connection string</returns>
        protected static string DbPath(){
            return @"Data Source=.\SQLEXPRESS; Initial Catalog=Forum; uid=sa; pwd=senai@123"; 
        }
    }
}