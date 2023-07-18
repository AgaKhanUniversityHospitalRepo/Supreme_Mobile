using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;

namespace Supreme_Mobile.Models
{
    public class GeneralService
    {
        private string m_exePath = string.Empty;

        public static IDbConnection DapperConnection()
        {
            IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);            
            return _db;
        }

        public static void WriteErrorLog(ref Exception e)
        {
            string m_exePath = string.Empty;
            //m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_exePath = ConfigurationManager.AppSettings["LogPath"].ToString();
            try
            {
                TextWriter txtWriter = File.AppendText(m_exePath + "\\ErrorLog" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log");

                txtWriter.Write("\r\nErrorLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", e.Message);
                txtWriter.WriteLine("  :{0}", e.StackTrace);
                txtWriter.WriteLine("/*******************************************************/");
                txtWriter.WriteLine("\n\n");
                txtWriter.Flush();
                txtWriter.Close();

            }
            catch (Exception ex)
            {
                throw ex; //.Message.ToString();
            }
        }

        public void LogWrite(string logMessage)
        {
            //m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_exePath = ConfigurationManager.AppSettings["LogPath"].ToString();
            //LogPath
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("/*******************************************************/");
                txtWriter.WriteLine("\n\n");
                txtWriter.Flush();
                txtWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AuditTrail(string logMessage)
        {
            //m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_exePath = ConfigurationManager.AppSettings["LogPath"].ToString();
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + String.Format("{0:dd MMM yyyy}", DateTime.Now) + ".Log"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

