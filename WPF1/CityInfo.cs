using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF1
{
public class CityInfo
    {
        public CityInfo()
        {
        }
        public CityInfo(String state, String city, bool isCapital, bool isLargest)
           {
            State = state;
            City = city;
            IsCapital = isCapital;
            IsLargest = isLargest;
           }
        public String State
        { set; get; }
        public String City
        { set; get; }
        public bool IsCapital
        { set; get; }
        public bool IsLargest
        { set; get; }
    }
}
