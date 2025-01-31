using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Models.Items
{
    public class TermbaseEntryResponse
    {
        public DateTime Created { get; set; }
        public string Creator { get; set; }
        public DateTime Modified { get; set; }
        public string Modifier { get; set; }
        public int Id { get; set; }
        public string Client { get; set; }
        public string Domain { get; set; }
        public string ImageCaption { get; set; }
        public string Image { get; set; }
        public List<LanguageItemDto> Languages { get; set; }
        public string Note { get; set; }
        public string Project { get; set; }
        public string Subject { get; set; }
    }
    public class LanguageItemDto
    {
        public string Language { get; set; }
        public string Definition { get; set; }
        public int Id { get; set; }
        public bool NeedsModeration { get; set; }
        public List<TermItemDto> TermItems { get; set; }
    }
    public class TermItemDto
    {
        public int CaseSense { get; set; }
        public string Example { get; set; }
        public int Id { get; set; }
        public bool IsForbidden { get; set; }
        public int PartialMatch { get; set; }
        public string Text { get; set; }
    }
}
