using System.Collections.Generic;

namespace fundooNotesCosmos.Models
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Label { get; set; }
        public List<string> Collaboration { get; set; }
        public int UserId { get; set; }
    }
}
