namespace SystemRestauracji.Models.Correspondences
{
    public class DocumentForDocumentPositionsDetails
    {
        public string DocumentFullNane { get; set; }

        public int DocumentId { get; set; }

        public DocumentForDocumentPositionsDetails(string documentFullName, int documentId)
        {
            this.DocumentFullNane = documentFullName;
            this.DocumentId = documentId;
        }
    }
}
