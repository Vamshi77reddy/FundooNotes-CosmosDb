using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fundooNotesCosmos.Entities
{
    public class NoteEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Label { get; set; }=new List<string>();
        public List<string> Collaboration { get; set; }= new List<string>();
        public string UserId { get; set; }
    }
}
