using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace DalmiaEmailSchedular
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting email sending process ...");
            string webUrl = System.Configuration.ConfigurationManager.AppSettings["EmailSendingUrl"];

            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(webUrl);
            }
            catch (UriFormatException)
            {
                request = null;
            }
            if (request == null)
                throw new ApplicationException("Invalid URL: " + webUrl);

            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            request.ContentType = @"application/x-www-form-urlencoded";

            // add post data to request
            Stream postStream = null;
            try
            {
                postStream = request.GetRequestStream();
                postStream.Flush();
                postStream.Close();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(request.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (WebException e)
            {
                string msg = e.Message;
                throw;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                throw;
            }
            finally
            {
                request.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            request = null;

            var resp = responseData;
            Console.WriteLine("stopping email sending process ...");
        }
    }
}
