using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using InterviewTask.Models;

namespace InterviewTask.Services
{
    public class HelperServiceRepository : IHelperServiceRepository
    {
        /// <summary>
        /// Returns all HelperService data, form the back-office CRM system.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HelperServiceModel> Get()
        {
            return HelperServiceFactory.Create();
        }

        /// <summary>
        /// Returns a single HelperService from the back-office CRM system.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public HelperServiceModel Get(Guid id)
        {
            return HelperServiceFactory.Create().FirstOrDefault(g => g.Id == id);
        }

        /// <summary>
        /// Create Log File
        /// </summary>
        private void LogError(Exception ex)
        {


            string message = string.Format("Time: {0}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "--------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "--*****************************************************--";
            message += Environment.NewLine;
            string path = HttpContext.Current.Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}