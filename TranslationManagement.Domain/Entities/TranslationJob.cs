﻿using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Entities
{
    public class TranslationJob
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public TranslationJobStatus Status { get; set; }
        public string OriginalContent { get; set; }
        public string TranslatedContent { get; set; }
        public decimal Price { get; set; }
    }

}