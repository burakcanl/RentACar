using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Model.Concretes
{
    public class RentingFirm : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public int TaxID { get; set; }
        public string FirmName { get; set; }
        [Required(ErrorMessage = "You must enter a Firm name.")]
        [StringLength(50, MinimumLength = 3)]
        public string FirmAddress { get; set; }
        [Required(ErrorMessage = "You must enter an address.")]
        [StringLength(100, MinimumLength = 3)]
        public int FirmTNumber { get; set; }
    }
}
