using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Model.Concretes
{
    public class CarRenting : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public int CarRentingID { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }
        public int CarKMGiving { get; set; }
        public int CarKMGettinBack { get; set; }
        public int CarRenttingTotalPrice { get; set; }
    }
}
