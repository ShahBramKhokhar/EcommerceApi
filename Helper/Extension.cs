namespace WebRexErpAPI.Helper
{
    public static class CustomExtension
    {
        public static string StringNullHandel(object data)
        {
            if (data == null || data == "" || data.ToString().Trim() == "[]" ||  (data is string && (string)data == "N/A")) 
                return string.Empty;
            else
         #pragma warning disable CS8603 // Possible null reference return.
                return data.ToString();
         #pragma warning restore CS8603 // Possible null reference return.
        }


        public static string NullValueSetNoValue(object data)
        {
            if (data == null || data == "" || data.ToString().Trim() == "[]" || (data is string && (string)data == "N/A"))
                return "No Value";
            else
        #pragma warning disable CS8603 // Possible null reference return.
                return data.ToString();
       #pragma     warning restore CS8603 // Possible null reference return.
        }

        public static int IsNumberAndNotNull(object data)
        {
            if (data == null)
            {
                return 0;
            }
               
            else
            {
               
                return Convert.ToInt32(data);
            }
               
        }
        public static double IsDoubleAndNotNull(object data)
        {
            if (data == null)
                return 0;
            else
                return Convert.ToDouble(data);
        }
    }
}
