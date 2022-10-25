using Application.Database;
using Application.Database.Tables;

namespace Application;

public class NoteService
{
    private ApplicationContext Db = new ApplicationContext();
    
    public NoteService()
    {
        Db.Database.EnsureCreated();
    }

    public Note[] GetAllNotes()
    {
        return Db.Notes.ToArray();
    }

    public bool TryGetNoteByKey(int key, out Note? note)
    {
        note = (from n in Db.Notes
                where key == n.Key
                    select n).SingleOrDefault();

        return note != null;
    }

    public bool TryPutNote(Note note)
    {
        try
        {
            Db.Notes.Add(note);
            Db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}