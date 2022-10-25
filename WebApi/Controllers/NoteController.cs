using Application;
using Application.Database.Tables;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly ILogger<NoteController> _logger;
    private readonly NoteService _noteService;

    public NoteController(ILogger<NoteController> logger, 
        NoteService noteService)
    {
        _logger = logger;
        _noteService = noteService;
    }

    [Route("get")]
    [HttpGet]
    public Note[] Get()
    {
        return _noteService.GetAllNotes();
    }

    [Route("get/{key}")]
    [HttpGet]
    public string Get(int key)
    {
        if (!_noteService.TryGetNoteByKey(key, out var note))
            return "Запись не найдена";

        return note.Value;
    }

    [Route("put")]
    [HttpPost]
    public string Put(Note note)
    {
        if (!_noteService.TryPutNote(note))
            return "Рыцарь, тебя постигла неудача, но ты попробуй еще раз.";

        return "Всё ок";
    }
}