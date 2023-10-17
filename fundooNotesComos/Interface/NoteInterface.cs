using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Models;
using System.Collections.Generic;

namespace fundooNotesCosmos.Interface
{
    public interface NoteInterface
    {
        public NoteEntity TakeANote(NoteModel notesModel, string userId);
        public List<NoteEntity> GetAllNotes(string userId);
        public NoteEntity UpdateNote(NoteModel notesModel, string userId, string noteId);
        public bool DeleteNote(string userId, string noteId);
        public bool AddCollaboratorToNote(string noteId, string collaboratorEmail);
        public bool RemoveCollaboratorFromNote(string noteId, string collaboratorEmail);
        public bool AddLabelToNote(string noteId, string label);
        public bool RemoveLabelFromNote(string noteId, string label);
        public NoteEntity GetNoteByLabelName(string userId, string NoteName);



    }
}
