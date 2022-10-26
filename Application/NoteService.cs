using Application.Database;
using Application.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class NoteService
{
    private readonly ApplicationContext context = new ApplicationContext();
    
    public NoteService()
    {
        context.Database.EnsureCreated();
    }

    public Note[] GetAllNotes()
    {
        return context.Notes.ToArray();
    }

    public bool TryGetNoteByKey(int key, out Note? note)
    {
        note = (from n in context.Notes
                where key == n.Key
                    select n).SingleOrDefault();

        return note != null;
    }

    public void ClearTable()
    {
        context.Database.ExecuteSqlRaw("DELETE FROM Notes");
    }

    public bool TryPutNote(Note note)
    {
        try
        {
            context.Notes.Add(note);
            context.SaveChanges();
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
                context.Notes.Add(note);
            context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}