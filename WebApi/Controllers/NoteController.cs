using Application;
using Application.Database.Tables;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : ControllerBase
{
    private readonly NoteService _noteService;

    public NoteController(
        NoteService noteService)
    {
        _noteService = noteService;
    }

    [Route("")]
    [HttpGet]
    public NoteModel[] Get([FromQuery] NoteParameters? parameters) =>
        _noteService.GetAllNotes(parameters).Select(x =>
            new NoteModel
            {
                Id = x.Id,
                Key = x.Key,
                Value = x.Value
            }).ToArray();

    [Route("count")]
    [HttpGet]
    public int Count() =>
        _noteService.Count();

    [Route("{key}")]
    [HttpGet]
    public string Get(int key) =>
        _noteService.TryGetNoteByKey(key, out var note) ? note!.Value : "Запись не найдена";

    [Route("clear")]
    [HttpGet]
    public void Clear() =>
        _noteService.ClearTable();

    [Route("")]
    [HttpPut]
    public string Put([FromBody] Dictionary<int, string>[] json)
    {
        var notes = json.Select(x =>
            new Note
            {
                Key = x.Single().Key,
                Value = x.Single().Value
            });

        return _noteService.TryPutNotes(notes)
            ? "Всё ок"
            : "Рыцарь, тебя постигла неудача, но ты попробуй еще раз.";
    }
}