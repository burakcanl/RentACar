using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace RentACar.Model.Concretes
{
    public class Car : IDisposable
    {

        public int CarId { get; set; }
        public string CarModel { get; set; }

        [Required(ErrorMessage = "You must enter a car model.")]
        [StringLength(50, MinimumLength = 3)]
        public string CarAirbag { get; set; }

        [Required(ErrorMessage = "You must enter a car airbag.")]
        [StringLength(50, MinimumLength = 3)]
        public string CarDriverLicenseType { get; set; }

        [Required(ErrorMessage = "You must enter a license type.")]
        [StringLength(2, MinimumLength = 1)]
        public int CarLuggage { get; set; }
        public int CarMaxKmDaily { get; set; }
        public int CarKm { get; set; }
        public int CarPrice { get; set; }
        public int CarSeats { get; set; }
        public string CarAddress { get; set; }

        [Required(ErrorMessage = "You must enter a car address.")]
        [StringLength(50, MinimumLength = 3)]
        public int TaxID { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
