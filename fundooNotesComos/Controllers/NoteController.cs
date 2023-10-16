using fundooNotesCosmos.Entities;
using fundooNotesCosmos.Interface;
using fundooNotesCosmos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using fundooNotesCosmos.Services;
using static MassTransit.Monitoring.Performance.BuiltInCounters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Runtime.ConstrainedExecution;

namespace fundooNotesCosmos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteInterface noteInterface;
        public NoteController(NoteInterface noteInterface)
        {
            this.noteInterface = noteInterface; 
        }

        [HttpPost("TakeANote")]
        public IActionResult TakeANote(NoteModel notesModel)
        {
            try
            {
                string userId = (User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteInterface.TakeANote(notesModel, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Takenote Successful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Takenote UnSuccessful" });
                }
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet]
        [Route("Get-All-Notes")]
        public IActionResult GetAllNote()
        {
            try
            {
                string userId = (User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteInterface.GetAllNotes(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { Status = true, Message = "Displaying the Notes.", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<NoteEntity>> { Status = false, Message = "No Notes available to Display", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost("UpdateNote")]
        //public IActionResult UpdateNote(NoteModel notesModel, string noteId)
        //{
        //    try
        //    {
        //        string userId = (User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
        //        var result = noteInterface.UpdateNote(notesModel, userId, noteId);
        //        if (result != null)
        //        {

        //            return Ok(new ResponseModel<NoteEntity> { Status = true, Message = "Updates the Notes.", Data = result });
        //        }
        //        else
        //        {
        //            return BadRequest(new ResponseModel<NoteEntity> { Status = false, Message = "Cannot Update", Data = result });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //[HttpDelete("Delete")]
        //public IActionResult Delete(string noteId)
        //{
        //    string userId = (User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

        //    var result = noteInterface.DeleteNote(userId, noteId);
        //    if (result)
        //    {
        //        return Ok(new ResponseModel<bool> { Status = true, Message = " Note Deleted", Data = result });

        //    }
        //    else
        //    {
        //        return BadRequest(new ResponseModel<bool> { Status = false, Message = " Note NotDeleted but Trashed", Data = result });
        //    }
        //}

        //[HttpPost("addcollaborator")]
        //public IActionResult AddCollaboratorToNote(string noteId,  string collaboratorEmail)
        //{
        //    bool collaboratorAdded = noteInterface.AddCollaboratorToNote(noteId, collaboratorEmail);

        //    if (collaboratorAdded)
        //    {
        //        return Ok(new ResponseModel<bool> { Status = true, Message = " Collab added " });
        //    }
        //    else
        //    {
        //        return BadRequest(new ResponseModel<bool> { Status = false, Message = " Collab not added"});
        //    }
        //}

        //[HttpDelete("removecollaborator")]
        //public IActionResult RemoveCollaboratorFromNote(string noteId, string collaboratorEmail)
        //{
        //    bool collaboratorRemoved = noteInterface.RemoveCollaboratorFromNote(noteId, collaboratorEmail);

        //    if (collaboratorRemoved)
        //    {
        //        return Ok(new ResponseModel<bool> { Status = true, Message = " collab Deleted" });
        //    }
        //    else
        //    {
        //        return BadRequest(new ResponseModel<bool> { Status = false, Message = " Collab not deleted" });
        //    }
        //}

        //[HttpPost("addlabel")]
        //public IActionResult AddLabelToNote(string noteId,  string label)
        //{
        //    bool labelAdded = noteInterface.AddLabelToNote(noteId, label);

        //    if (labelAdded)
        //    {
        //        return Ok(new ResponseModel<bool> { Status = true, Message = " Label Added" });
        //    }
        //    else
        //    {
        //        return BadRequest(new ResponseModel<bool> { Status = false, Message = " Label not Added" });
        //    }
        //}

        //[HttpDelete("removelabel")]
        //public IActionResult RemoveLabelFromNote(string noteId, string label)
        //{
        //    bool labelRemoved = noteInterface.RemoveLabelFromNote(noteId, label);

        //    if (labelRemoved)
        //    {
        //        return Ok(new ResponseModel<bool> { Status = true, Message = " Label Deleted" });
        //    }
        //    else
        //    {
        //        return BadRequest(new ResponseModel<bool> { Status = false, Message = " Label not Deleted" });
        //    }
        //}

    }
    }
