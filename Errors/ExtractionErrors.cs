namespace Career_Path.Errors
{
    public record ExtractionErrors
    {
        public static readonly Error InvalidFileFormat =
                 new("Extraction.InvalidFormat", "Invalid file format. Only PDF and DOCX are supported", StatusCodes.Status400BadRequest);

        public static readonly Error FileTooLarge =
            new("Extraction.FileTooLarge", "File size exceeds the maximum limit of 5MB", StatusCodes.Status400BadRequest);

        public static readonly Error EmptyFile =
            new("Extraction.EmptyFile", "The uploaded file is empty", StatusCodes.Status400BadRequest);

        public static readonly Error ExtractionFailed =
            new("Extraction.Failed", "Failed to extract data from CV", StatusCodes.Status500InternalServerError);

        public static readonly Error ApiConnectionFailed =
            new("Extraction.ApiConnectionFailed", "Failed to connect to extraction API", StatusCodes.Status503ServiceUnavailable);

        public static readonly Error InvalidResponse =
            new("Extraction.InvalidResponse", "Received invalid response from extraction service", StatusCodes.Status500InternalServerError);

        public static readonly Error Timeout =
            new("Extraction.Timeout", "Extraction request timed out", StatusCodes.Status408RequestTimeout);

        public static readonly Error NoFileUploaded =
            new("Extraction.NoFileUploaded", "No file was uploaded", StatusCodes.Status400BadRequest);
    }
}