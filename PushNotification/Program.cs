using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PushNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            PushNotification();
        }


        static void PushNotification()
        {
            try
            {
                var applicationID = "AIzaSyBjZNrGs9B4Stdy8dkNJOomm1Ev6QbqIOM";
                var senderId = "691007744829";
                string deviceId = "ds1qWhlLXeo:APA91bEvrfXQGAk7UMLjApVUPHKvkmxpitYbiEfGXmEBLmC4ImBR5Lc9nq78RkzGkir7dj_z81kI7DldM1BmNmzfOHQGsYFCOO8IS85DZEsClJI4JyT44rxoD2Niolrx1IMgtIO0kNbW";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = "TESTE DANILO",
                        title = "TITLE MSG",
                        icon = "myicon"
                    }
                };

                var serializer = new JavaScriptSerializer();

                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;


                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
    }
}
