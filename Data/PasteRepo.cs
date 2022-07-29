using MoeBinAPI.Models;

namespace MoeBinAPI.Data
{
    public class PasteRepo
    {
        private readonly Context _context;

        public PasteRepo(Context context)
        {
            _context = context;
        }
 
        public Paste? GetPaste(string pid)
        {
            return _context.Pastes.Where(p => p.PasteId == pid).FirstOrDefault();
        }

        public void CreatePaste(Paste paste)
        {
            _context.Pastes.Add(paste);
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public void DeletePaste(Paste paste)
        {
            _context.Pastes.Remove(paste);
        }

        public string GetPasteID()
        {
            const string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; //this is messed up
            char[] chars = new char[10];
            Random rand = new Random();
            string pasteid = null!;
            bool valid = false;
            while(!valid)
            {
                // for(int i = 0; i < 10; i++)
                // {
                //     chars.Append(s[rand.Next(s.Length)]);
                // }
                // pasteid = new String(chars);
                pasteid = new string(Enumerable.Repeat(s, 10)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
                if(!_context.Pastes.Where(p => p.PasteId == pasteid).Any())
                    valid = true;
            }
            System.Console.WriteLine(pasteid);
            return pasteid;
        }
    }
}