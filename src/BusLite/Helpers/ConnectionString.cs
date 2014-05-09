namespace BusLite.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;

    public static class ConnectionString
    {
        public static Uri GetUri(string connectionString)
        {
            string[] strArray = Regex.Split(connectionString, ";(Endpoint|SharedSecretIssuer|SharedSecretValue|)=",
                RegexOptions.IgnoreCase);
            var nameValueCollection = new NameValueCollection();

            for (int index = 1; index < strArray.Length; index = index + 1 + 1)
            {
                string input1 = strArray[index];
                string input2 = strArray[index + 1];
                nameValueCollection[input1] = input2;
            }
            var uri = new Uri(strArray[0]);
            return uri;
        }
    }
}