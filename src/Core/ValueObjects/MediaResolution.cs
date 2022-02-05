using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class MediaResolution : ValueObject
    {
        private MediaResolution() {}

        public MediaResolution(string url, int height, int width)
        {
            Url = url;
            Height = height;
            Width = width;
        }

        #region Fields - Properties
        private string _url;
        public string Url 
        {
            get => _url;
            private set => _url = Guard.Against.NullOrWhiteSpace(value, nameof(Url));
        }

        private int _height;
        public int Height 
        {
            get => _height;
            private set => _height = Guard.Against.NegativeOrZero(value, nameof(Height));
        }

        private int _width;
        public int Width 
        {
            get => _width;
            private set => _width = Guard.Against.NegativeOrZero(value, nameof(Width));
        }
        #endregion
    
        public MediaResolution NewWithChangedResolution(int height, int width)
        {
            return new MediaResolution(this.Url, height, width);
        }

        public MediaResolution NewWithChangedUrl(string url)
        {
            return new MediaResolution(url, this.Height, this.Width);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Height;
            yield return Width;
        }

        public override string ToString()
        {
            return $"MediaRes: [\n" +
                    $"\t Url: {Url}\n" +
                    $"\t H: {Height} - W: {Width}\n]";
        }
    }
}