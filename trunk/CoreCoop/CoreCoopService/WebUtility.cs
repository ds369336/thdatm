using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace CoreCoopService
{
    public class WebUtility
    {

        private CultureInfo th;
        public CultureInfo TH
        {
            get
            {
                if (th == null)
                {
                    th = new CultureInfo("th-TH");
                }
                return th;
            }
        }

        private CultureInfo en;

        public CultureInfo EN
        {
            get
            {
                if (en == null)
                {
                    en = new CultureInfo("en-US");
                }
                return en;
            }
        }

        public bool IsDateType(Type type)
        {
            try
            {
                return type.FullName.ToLower() == "system.datetime";
            }
            catch { }
            return false;
        }

        public bool IsNumberType(Type type)
        {
            try
            {
                string tName = type.FullName.ToLower();
                return tName == "system.uint32" || tName == "system.int16" || tName == "system.int32" || tName == "system.int64" || tName == "system.decimal" || tName == "system.float" || tName == "system.double";
            }
            catch { }
            return false;
        }
        public String SQLFormat(string sql, params object[] args)
        {
            string[] nargs = new string[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (this.IsDateType(args[i].GetType())) //date type
                {
                    try
                    {
                        DateTime dtm = (DateTime)args[i];
                        if (dtm.Year < 1700)
                        {
                            nargs[i] = "NULL";
                        }
                        else
                        {
                            nargs[i] = "to_date('" + dtm.ToString("yyyy-MM-dd HH:mm:ss", this.EN) + "', 'yyyy-mm-dd hh24:mi:ss')";
                        }
                    }
                    catch
                    {
                        nargs[i] = "NULL";
                    }
                }
                else if (this.IsNumberType(args[i].GetType())) //number type
                {
                    try
                    {
                        decimal dec = Convert.ToDecimal(args[i]);
                        nargs[i] = dec.ToString();
                    }
                    catch
                    {
                        nargs[i] = "NULL";
                    }
                }
                else
                {
                    try
                    {
                        nargs[i] = "'" + args[i].ToString().Trim() + "'";
                    }
                    catch
                    {
                        nargs[i] = "NULL";
                    }
                }
            }
            return string.Format(sql, nargs);
        }
    }
}