using fundooNotesCosmos.Context;
using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Interface;
using fundooNotesCosmos.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fundooNotesCosmos.Services
{
    public class NoteService : NoteInterface
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;


        public NoteService(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public NoteEntity TakeANote(NoteModel notesModel, string userId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();

                noteEntity.UserId = userId;

                noteEntity.Title = notesModel.Title;
                noteEntity.Description = notesModel.Description;
                noteEntity.Label = notesModel.Label;
                noteEntity.Collaboration = notesModel.Collaboration;
                return noteEntity;

            }
            catch (Exception ex) { throw ex; }
        }

        public List<NoteEntity> GetAllNotes(string userId)
        {
            try
            {
               var notes = this.fundooContext.Note.Where(x => x.UserId == userId).ToList();
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity UpdateNote(NoteModel notesModel, string userId, string noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();



                return noteEntity;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool DeleteNote(string userId, string noteId)
        {
            try
            {
                NoteEntity noteEntity = this.fundooContext.Note.Where(x => x.id == noteId && x.UserId == userId).FirstOrDefault();
                if (noteEntity!=null)
                {
                    this.fundooContext.Remove(noteEntity);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                 
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddCollaboratorToNote(string noteId, string collaboratorEmail)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);

                if (note == null)
                {
                    return false; 
                }

                if (note.Collaboration == null)
                {
                    note.Collaboration = new List<string>();
                }

                note.Collaboration.Add(collaboratorEmail);

                fundooContext.SaveChanges();

                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        public bool RemoveCollaboratorFromNote(string noteId, string collaboratorEmail)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);

                if (note == null)
                {
                    return false; 
                }

                if (note.Collaboration != null)
                {
                    note.Collaboration.Remove(collaboratorEmail);
                }

                fundooContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool AddLabelToNote(string noteId, string label)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);



                if (note.Label == null)
                {
                    note.Label = new List<string>();
                }

                note.Label.Add(label);

                fundooContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveLabelFromNote(string noteId, string label)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);

                

                if (note.Label != null)
                {
                    note.Label.Remove(label);
                }

                fundooContext.SaveChanges();

                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}