namespace Apps.MemoQResources.Models.Items
{
    public class TermbaseModel
    {
        public int AccessLevel { get; set; }
        public string Client { get; set; }
        public string Domain { get; set; }
        public string FriendlyName { get; set; }
        public List<string> Languages { get; set; }
        public DateTime LastModified { get; set; }
        public int NumEntries { get; set; }
        public string Project { get; set; }
        public string Subject { get; set; }
        public string TBGuid { get; set; }
        public string TBOwner { get; set; }
        public DateTime? LastUsed { get; set; }
        public bool IsUsedInProject { get; set; }
    }
}
