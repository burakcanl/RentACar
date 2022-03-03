using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Model.Concretes
{
    public class Customers : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "You must enter a name.")]
        [StringLength(50, MinimumLength = 2)]
        public string CustomerSurname { get; set; }

        [Required(ErrorMessage = "You must enter a surname.")]
        [StringLength(50, MinimumLength = 2)]
        public string CustomerPasskey { get; set; }

        [Required(ErrorMessage = "You must enter a key.")]
        [StringLength(50, MinimumLength = 3)]
        public int IDNo { get; set; }
        public int DriverIDNo { get; set; }
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "You must enter an address.")]
        [StringLength(100, MinimumLength = 20)]
        public int Cellphpne { get; set; }
        
    }
}
