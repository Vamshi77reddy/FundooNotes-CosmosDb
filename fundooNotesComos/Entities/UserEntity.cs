using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fundooNotesCosmos.Entities
{
    public class UserEntity
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailId { get; set; }
            public string Password { get; set; }
        
    }
}
