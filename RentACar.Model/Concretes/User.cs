using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Model.Concretes
{
    public class User : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public int UserID { get; set; }
        public int TaxID { get; set; }
        public string UserPassKey { get; set; }

        [Required(ErrorMessage = "You must enter a key.")]
        [StringLength(50, MinimumLength = 3)]
        public byte isOwner { get; set; }
    }
}
