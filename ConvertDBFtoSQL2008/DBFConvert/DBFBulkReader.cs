using System;
using System.Collections.Generic;
using System.Data;

namespace ConvertDBFtoSQL2008.DBFConvert
{
    public class DbfBulkReader : IDataReader
    {
        System.IO.FileStream FS;
        byte[] buffer;
        int _FieldCount;
        int FieldsLength;
        System.Globalization.DateTimeFormatInfo dfi = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
        System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
        string[] FieldName;
        string[] FieldType;
        byte[] FieldSize;
        byte[] FieldDigs;
        int RowsCount;
        int ReadedRow = 0;
        public Dictionary<string, object> R = new Dictionary<string, object>();
        public Dictionary<int, string> FieldIndex = new Dictionary<int, string>();

        public DbfBulkReader(string FileName, Dictionary<int, string> FieldIndex)
        {
            FS = new System.IO.FileStream(FileName, System.IO.FileMode.Open);
            buffer = new byte[4];
            FS.Position = 4; FS.Read(buffer, 0, buffer.Length);
            RowsCount = buffer[0] + (buffer[1] * 0x100) + (buffer[2] * 0x10000) + (buffer[3] * 0x1000000);
            buffer = new byte[2];
            FS.Position = 8; FS.Read(buffer, 0, buffer.Length);
            _FieldCount = (((buffer[0] + (buffer[1] * 0x100)) - 1) / 32) - 1;
            FieldName = new string[_FieldCount];
            FieldType = new string[_FieldCount];
            FieldSize = new byte[_FieldCount];
            FieldDigs = new byte[_FieldCount];
            buffer = new byte[32 * _FieldCount];
            FS.Position = 32; FS.Read(buffer, 0, buffer.Length);
            FieldsLength = 0;
            for (int i = 0; i < _FieldCount; i++)
            {
                FieldName[i] = System.Text.Encoding.Default.GetString(buffer, i * 32, 10).TrimEnd(new char[] { (char)0x00 });
                FieldType[i] = "" + (char)buffer[i * 32 + 11];
                FieldSize[i] = buffer[i * 32 + 16];
                FieldDigs[i] = buffer[i * 32 + 17];
                FieldsLength = FieldsLength + FieldSize[i];
            }
            FS.ReadByte();

            this.FieldIndex = FieldIndex;
        }


        public void Dispose() { FS.Close(); }

       

        public int FieldCount { get { return _FieldCount; } }

        public object GetValue(int i) { return R[FieldIndex[i]]; }

        public bool Read()
        {
            if (ReadedRow >= RowsCount) return false;

            R.Clear();
            buffer = new byte[FieldsLength];
            FS.ReadByte();
            FS.Read(buffer, 0, buffer.Length);
            int Index = 0;
            for (int i = 0; i < FieldCount; i++)
            {
                string l = System.Text.Encoding.GetEncoding(866).GetString(buffer, Index, FieldSize[i]).TrimEnd(new char[] { (char)0x00 }).TrimEnd(new char[] { (char)0x20 });
                Index = Index + FieldSize[i];
                object Tr;
                if (l.Trim() != "")
                {
                    switch (FieldType[i])
                    {
                        case "L": Tr = l == "T" ? true : false; break;
                        case "D": Tr = DateTime.ParseExact(l, "yyyyMMdd", dfi); break;
                        case "N":
                            {
                                if (FieldDigs[i] == 0)
                                    Tr = int.Parse(l, nfi);
                                else
                                    Tr = decimal.Parse(l, nfi);
                                break;
                            }
                        case "F": Tr = double.Parse(l, nfi); break;
                        default: Tr = l; break;
                    }

                }
                else
                {
                    Tr = DBNull.Value;
                }
                R.Add(FieldName[i], Tr);
            }
            ReadedRow++;
            return true;
        }
        public int Depth { get { return -1; } }
        public bool IsClosed { get { return false; } }
        public Object this[int i] { get { return new object(); } }
        public Object this[string name] { get { return new object(); } }
        public int RecordsAffected { get { return -1; } }

        public void Close() { }
        public bool NextResult() { return true; }
        public bool IsDBNull(int i) { return false; }
        public string GetString(int i) { return ""; }
        public DataTable GetSchemaTable() { return null; }
        public int GetOrdinal(string name) { return -1; }
        public string GetName(int i) { return ""; }
        public long GetInt64(int i) { return -1; }
        public int GetInt32(int i) { return -1; }
        public short GetInt16(int i) { return -1; }
        public Guid GetGuid(int i) { return new Guid(); }
        public float GetFloat(int i) { return -1; }
        public Type GetFieldType(int i) { return typeof(string); }
        public double GetDouble(int i) { return -1; }
        public decimal GetDecimal(int i) { return -1; }
        public DateTime GetDateTime(int i) { return new DateTime(); }
        public string GetDataTypeName(int i) { return ""; }
        public IDataReader GetData(int i) { return this; }
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) { return -1; }
        public char GetChar(int i) { return ' '; }
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) { return -1; }
        public byte GetByte(int i) { return 0x00; }
        public bool GetBoolean(int i) { return false; }
        public int GetValues(Object[] values) { return -1; }
    }
}
