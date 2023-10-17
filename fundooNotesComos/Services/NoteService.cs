using fundooNotesCosmos.Context;
using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Interface;
using fundooNotesCosmos.Models;
using Microsoft.Azure.Cosmos;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notesModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                this.fundooContext.Add(noteEntity);
                this.fundooContext.SaveChanges();
                return noteEntity;

            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notesModel"></param>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public NoteEntity UpdateNote(NoteModel notesModel, string userId, string noteId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity = this.fundooContext.Note.Where(x => x.UserId == userId && x.id == noteId).FirstOrDefault();

                noteEntity.Title = notesModel.Title;
                noteEntity.Description = notesModel.Description;
                noteEntity.Label = notesModel.Label;
                noteEntity.Collaboration = notesModel.Collaboration;
                this.fundooContext.SaveChanges();


                return noteEntity;
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="collaboratorEmail"></param>
        /// <returns></returns>
        public bool AddCollaboratorToNote(string noteId, string collaboratorEmail)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);

                if (note != null)
                {
                    note.Collaboration.Add(collaboratorEmail);

                    fundooContext.SaveChanges();

                    return true;
                }else { return false; }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="collaboratorEmail"></param>
        /// <returns></returns>

        public bool RemoveCollaboratorFromNote(string noteId, string collaboratorEmail)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);

                if (note.Collaboration != null)
                {
                    note.Collaboration.Remove(collaboratorEmail);
                    fundooContext.SaveChanges();

                    return true;
                }
                else {  return false; }

              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public bool AddLabelToNote(string noteId, string label)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);

                if (note.Label != null)
                {
                    note.Label.Add(label);

                    fundooContext.SaveChanges();

                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public bool RemoveLabelFromNote(string noteId, string label)
        {
            try
            {
                var note = fundooContext.Note.FirstOrDefault(n => n.id == noteId);
               
                if (note.Label != null)
                {
                    note.Label.Remove(label);
                    fundooContext.SaveChanges();

                    return true;
                }
                else { return false; }
           
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="NoteName"></param>
        /// <returns></returns>
    public NoteEntity GetNoteByLabelName(string userId, string NoteName)
    {
        try
        {
            List<NoteEntity> labelEntities = this.fundooContext.Note
                .Where(x => x.UserId == userId)
                .ToList();

                NoteEntity label = labelEntities.FirstOrDefault(x => x.Title == NoteName);

            return label;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
}