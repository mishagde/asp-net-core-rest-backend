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
    public Note[] Get() => _noteService.GetAllNotes();

    [Route("get/{key}")]
    [HttpGet]
    public string Get(int key) =>
        _noteService.TryGetNoteByKey(key, out var note) ? note!.Value : "Запись не найдена";

    [Route("put")]
    [HttpPost]
    public string Put(Note note) =>
        _noteService.TryPutNote(note)
            ? "Всё ок"
            : "Рыцарь, тебя постигла неудача, но ты попробуй еще раз.";
}