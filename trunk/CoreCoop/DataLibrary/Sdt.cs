using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace DataLibrary
{
    public class Sdt : DataTable
    {
        private int numRow = 0;
        private int row = 0;
        private CultureInfo en = new CultureInfo("en-US");
        private CultureInfo th = new CultureInfo("th-TH");

        public Sdt()
        {
            numRow = 0;
            row = 0;
        }

        public Sdt(DataTable dt):base("Sdt")
        {
            this.Columns.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                this.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType);
            }
            StringWriter strWrite = new StringWriter();
            dt.WriteXml(strWrite);
            StringReader sReader = new StringReader(strWrite.ToString());
            this.ReadXml(sReader);
            numRow = 0;
            row = 0;
            SecoundConstructor();
        }

        public void SecoundConstructor()
        {
            numRow = this.Rows.Count;
        }

        public String GetString(int i)
        {
            String rs = "";
            try
            {
                rs = Convert.ToString(this.Rows[row - 1][i]);
            }
            catch { }
            return rs;
        }

        public String GetString(String column)
        {
            String rs = "";
            try
            {
                rs = Convert.ToString(this.Rows[row - 1][column]);
            }
            catch { }
            return rs;
        }

        public int GetInt32(int i)
        {
            int ii = 0;
            try
            {
                ii = Convert.ToInt32(this.Rows[row - 1][i]);
            }
            catch { }
            return ii;
        }

        public int GetInt32(String column)
        {
            int ii = 0;
            try
            {
                ii = Convert.ToInt32(this.Rows[row - 1][column]);
            }
            catch { }
            return ii;
        }

        public Decimal GetDecimal(int i)
        {
            Decimal ii = 0m;
            try
            {
                ii = Convert.ToDecimal(this.Rows[row - 1][i]);
            }
            catch { }
            return ii;
        }

        public Decimal GetDecimal(String column)
        {
            Decimal ii = 0m;
            try
            {
                ii = Convert.ToDecimal(this.Rows[row - 1][column]);
            }
            catch { }
            return ii;
        }

        public double GetDouble(int i)
        {
            double ii = 0.00d;
            try
            {
                ii = Convert.ToDouble(this.Rows[row - 1][i]);
            }
            catch { }
            return ii;
        }

        public double GetDouble(String column)
        {
            double ii = 0.00d;
            try
            {
                ii = Convert.ToDouble(this.Rows[row - 1][column]);
            }
            catch { }
            return ii;
        }

        public DateTime GetDate(int i)
        {
            DateTime dt = new DateTime(1370, 1, 1);
            try
            {
                dt = Convert.ToDateTime(this.Rows[row - 1][i]);
            }
            catch { }
            return dt;
        }

        public DateTime GetDate(String column)
        {
            DateTime dt = new DateTime(1370, 1, 1);
            try
            {
                dt = Convert.ToDateTime(this.Rows[row - 1][column]);
            }
            catch { }
            return dt;
        }

        public String GetDateEn(int i)
        {
            return GetDate(i).ToString("yyyy-MM-dd", en);
        }

        public String GetDateEn(String column)
        {
            return GetDate(column).ToString("yyyy-MM-dd", en);
        }

        public String GetDateTh(int i)
        {
            return GetDate(i).ToString("dd/MM/yyyy", th);
        }

        public String GetDateTh(String column)
        {
            return GetDate(column).ToString("dd/MM/yyyy", th);
        }

        public int IndexOf(String column)
        {
            int ii = -1;
            int c = this.GetFieldCount();
            for (int i = 0; i < c; i++)
            {
                if (this.Columns[i].ToString() == column)
                {
                    ii = i;
                    break;
                }
            }
            return ii;
        }

        public bool Next()
        {
            row++;
            if (row <= numRow)
            {
                return true;
            }
            else
            {
                row--;
                return false;
            }
        }

        public int GetFieldCount()
        {
            return this.Columns.Count;
        }

        public int GetRowCount()
        {
            return numRow;
        }

        public int GetRowIndex()
        {
            return row;
        }

        public void ReStart()
        {
            row = 0;
        }

        public void Fill(DataTable dt)
        {
        }
    }
}
