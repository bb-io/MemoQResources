using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Models.Items
{
    public class TBEntryModel
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public string Domain { get; set; }
        public string Project { get; set; }
        public string Subject { get; set; }
        public string Note { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
        public DateTime Modified { get; set; }
        public string Modifier { get; set; }
        public string Image { get; set; }
        public string ImageCaption { get; set; }
        public List<LanguageItem> Languages { get; set; }
        public List<TBCustomMetaItem> CustomMetas { get; set; }
    }

    public class LanguageItem
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Definition { get; set; }
        public bool NeedsModeration { get; set; }
        public List<TermItem> TermItems { get; set; }
        public List<TBCustomMetaItem> CustomMetas { get; set; }
    }

    public class TermItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string WildText { get; set; }
        public string Example { get; set; }
        public string GrammarGender { get; set; }
        public string GrammarNumber { get; set; }
        public string GrammarPartOfSpeech { get; set; }
        public bool IsForbidden { get; set; }
        public int CaseSense { get; set; }
        public int PartialMatch { get; set; }
        public List<int> PrefixBoundaries { get; set; }
        public List<TBCustomMetaItem> CustomMetas { get; set; }
    }

    public class TBCustomMetaItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
