using Application.Database;
using Application.Database.Tables;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class NoteService
{
    private readonly ApplicationContext _context = new ApplicationContext();

    public NoteService()
    {
        _context.Database.EnsureCreated();
    }

    public Note[] GetAllNotes(NoteParameters? parameters)
    {
        var notes = _context.Notes.AsQueryable();

        if (parameters == null)
            return notes.ToArray();
        
        if (parameters.MinId.HasValue)
            notes = notes.Where(x => x.Id >= parameters.MinId);
        if (parameters.MaxId.HasValue)
            notes = notes.Where(x => x.Id <= parameters.MaxId);
        
        if (parameters.MinKey.HasValue)
            notes = notes.Where(x => x.Key >= parameters.MinKey);
        if (parameters.MaxKey.HasValue)
            notes = notes.Where(x => x.Key <= parameters.MaxKey);

        if (parameters.FromEnd.HasValue)
            notes = notes.OrderByDescending(x=>x.Id);

        if (parameters.Count.HasValue)
        {
            if (parameters.Page.HasValue)
                notes = notes
                    .Skip((parameters.Page.Value - 1) * parameters.Count.Value)
                    .Take(parameters.Count.Value);
            else
                notes = notes.Take(parameters.Count.Value);
        }

        return notes.ToArray();
    }

    public bool TryGetNoteByKey(int key, out Note? note)
    {
        note = (from n in _context.Notes
            where key == n.Key
            select n).SingleOrDefault();

        return note != null;
    }

    public void ClearTable()
    {
        _context.Database.ExecuteSqlRaw("DELETE FROM Notes");
        _context.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET seq = 0 WHERE name = 'Notes'");
    }

    public bool TryPutNote(Note note)
    {
        try
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }

    public bool TryPutNotes(IEnumerable<Note> notes)
    {
        try
        {
            foreach (var note in notes)
                _context.Notes.Add(note);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }
}