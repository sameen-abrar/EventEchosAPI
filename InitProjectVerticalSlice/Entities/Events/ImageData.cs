﻿using EventEchosAPI.Entities.Common;
using System.Reflection.Metadata.Ecma335;

namespace EventEchosAPI.Entities.Events
{
    public class ImageData:AuditableEntity
    {
        public string? ImageMetaData { get; set; }
        public string? ImageCode { get; set; }
        public string? ImageBase64 { get; set; }
        public string? ImageUrl { get; set; }
        public string? BitmapImage { get; set; }

        public List<EventGuestImage> EventGuestImages { get; set; }

        public ImageData()
        {
            EventGuestImages = [];
        }
    }
}